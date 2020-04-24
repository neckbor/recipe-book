using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Models.BindingModels;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    public class ReferenceDataManagerController : ControllerBase
    {
        [HttpPost("api/[controller]/searchIngredients")]
        public IActionResult Get(IngredientBindingModel ingredient)
        {
            try
            {
                IEnumerable<IngredientBindingModel> result = GetIngredients(ingredient);
                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("api/[controller]/searchNationalities")]
        public IActionResult Get(NationalityBindingModel nationality)
        {
            try
            {
                IEnumerable<NationalityBindingModel> result = GetNationalities(nationality);
                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("api/[controller]/addIngredient")]
        public IActionResult AddIngredient(IngredientBindingModel ingredient)
        {
            try
            {
                if (ingredient.name == null)
                    return BadRequest();

                if (AddIntoDB(ingredient))
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "Во время добавления что-то пошло не так");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }
        }

        [HttpPost("api/[controller]/updateIngredient")]
        public IActionResult UpdateIngredient(IngredientBindingModel ingredient)
        {
            try
            {
                if (ingredient.idIngredient == 0)
                    return BadRequest();

                if (UpdateInDB(ingredient))
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "Во время изменения что-то пошло не так");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("api/[controller]/deleteIngredient")]
        public IActionResult DeleteIngredient(int idIngredient)
        {
            try
            {
                if (idIngredient < 1)
                    return BadRequest();

                string response = DeleteIngredientFromDB(idIngredient);

                if (response == "OK")
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "Ошибка! " + response);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("api/[controller]/addNationality")]
        public IActionResult AddNationality(NationalityBindingModel nationality)
        {
            try
            {
                if (nationality.name == null)
                    return BadRequest();

                if (AddIntoDB(nationality))
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "Во время добавления что-то пошло не так");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }
        }

        [HttpPost("api/[controller]/updateNationality")]
        public IActionResult UpdateNationality(NationalityBindingModel nationality)
        {
            try
            {
                if (nationality.idNationality == 0)
                    return BadRequest();

                if (UpdateInDB(nationality))
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "Во время изменения что-то пошло не так");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("api/[controller]/deleteNationality")]
        public IActionResult DeleteNationality(int idNationality)
        {
            try
            {
                if (idNationality < 1)
                    return BadRequest();

                string response = DeleteNationalityFromDB(idNationality);

                if (response == "OK")
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "Ошибка! " + response);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private IEnumerable<IngredientBindingModel> GetIngredients(IngredientBindingModel ingredient)
        {
            IEnumerable<IngredientBindingModel> result;
            using (ModelDbContext _model = new ModelDbContext())
            {
                result = _model.Ingredient.Where(i => EF.Functions.Like(i.Name.ToLower(), '%' + ingredient.name.ToLower() + '%'))
                        .Select(i => new IngredientBindingModel
                        {
                            idIngredient = i.Idingredient,
                            name = i.Name
                        }).ToList();
            }
            return result;
        }

        private IEnumerable<NationalityBindingModel> GetNationalities(NationalityBindingModel nationality)
        {
            IEnumerable<NationalityBindingModel> result;
            using (ModelDbContext _model = new ModelDbContext())
            {
                result = _model.Nationality.Where(n => EF.Functions.Like(n.Name.ToLower(), '%' + nationality.name.ToLower() + '%'))
                        .Select(n => new NationalityBindingModel
                        {
                            idNationality = n.Idnationality,
                            name = n.Name
                        }).ToList();
            }
            return result;
        }

        private bool AddIntoDB(IngredientBindingModel ingredient)
        {
            using (ModelDbContext _model = new ModelDbContext())
            {
                using (var transaction = _model.Database.BeginTransaction())
                {
                    try
                    {
                        Ingredient i = new Ingredient();
                        i.Name = ingredient.name;

                        _model.Ingredient.Add(i);

                        _model.SaveChanges();

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        private bool UpdateInDB(IngredientBindingModel ingredient)
        {
            using (ModelDbContext _model = new ModelDbContext())
            {
                using (var transaction = _model.Database.BeginTransaction())
                {
                    try
                    {
                        Ingredient i = _model.Ingredient.ToList().Find(i => i.Idingredient == ingredient.idIngredient);

                        if (ingredient.name != null && i != null)
                        {
                            i.Name = ingredient.name;
                            _model.SaveChanges();
                            transaction.Commit();

                            return true;
                        }

                        return false;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        private string DeleteIngredientFromDB(int idInredient)
        {
            using (ModelDbContext _model = new ModelDbContext())
            {
                using (var transaction = _model.Database.BeginTransaction())
                {
                    try
                    {
                        Ingredient ingredient = _model.Ingredient.ToList().Find(i => i.Idingredient == idInredient);
                        _model.Ingredient.Remove(ingredient);

                        _model.SaveChanges();
                        transaction.Commit();
                        return "OK";
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        if(e.Message.Contains("Value cannot be null. (Parameter "))
                        {
                            return "Неверно передан id ингредиента!";
                        }
                        return "Не удалось удалить выбранный ингредиент! Скорее всего он используется в одном из существующих рецептов!";
                    }
                }
            }
        }

        private bool AddIntoDB(NationalityBindingModel nationality)
        {
            using (ModelDbContext _model = new ModelDbContext())
            {
                using (var transaction = _model.Database.BeginTransaction())
                {
                    try
                    {
                        Nationality n = new Nationality();
                        n.Name = nationality.name;

                        _model.Nationality.Add(n);

                        _model.SaveChanges();

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        private bool UpdateInDB(NationalityBindingModel nationality)
        {
            using (ModelDbContext _model = new ModelDbContext())
            {
                using (var transaction = _model.Database.BeginTransaction())
                {
                    try
                    {
                        Nationality n = _model.Nationality.ToList().Find(n => n.Idnationality == nationality.idNationality);

                        if (nationality.name != null && n != null)
                        {
                            n.Name = nationality.name;
                            _model.SaveChanges();
                            transaction.Commit();
                            return true;
                        }

                        return false;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        private string DeleteNationalityFromDB(int idNationality)
        {
            using (ModelDbContext _model = new ModelDbContext())
            {
                using (var transaction = _model.Database.BeginTransaction())
                {
                    try
                    {
                        Nationality nationality = _model.Nationality.ToList().Find(n => n.Idnationality == idNationality);
                        _model.Nationality.Remove(nationality);

                        _model.SaveChanges();
                        transaction.Commit();
                        return "OK";
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        if (e.Message.Contains("Value cannot be null. (Parameter "))
                        {
                            return "Неверно передан id национальности!";
                        }
                        return "Не удалось удалить выбранную национальность! Скорее всего она используется в одном из существующих рецептов!";
                    }
                }
            }
        }
    }
}