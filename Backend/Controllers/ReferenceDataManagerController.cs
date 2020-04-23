using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    public class ReferenceDataManagerController : ControllerBase
    {
        [HttpPost("api/[controller]/addIngredient")]
        public IActionResult AddIngredient(Ingredient ingredient)
        {
            try
            {
                if (ingredient.Name == null)
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
        public IActionResult UpdateIngredient(Ingredient ingredient)
        {
            try
            {
                if (ingredient.Idingredient == 0)
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
        public IActionResult AddNationality(Nationality nationality)
        {
            try
            {
                if (nationality.Name == null)
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
        public IActionResult UpdateNationality(Nationality nationality)
        {
            try
            {
                if (nationality.Idnationality == 0)
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
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private bool AddIntoDB(Ingredient ingredient)
        {
            using (ModelDbContext _model = new ModelDbContext())
            {
                using (var transaction = _model.Database.BeginTransaction())
                {
                    try
                    {
                        Ingredient i = new Ingredient();
                        i.Name = ingredient.Name;

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

        private bool UpdateInDB(Ingredient ingredient)
        {
            using (ModelDbContext _model = new ModelDbContext())
            {
                using (var transaction = _model.Database.BeginTransaction())
                {
                    try
                    {
                        Ingredient i = _model.Ingredient.ToList().Find(i => i.Idingredient == ingredient.Idingredient);

                        if (ingredient.Name != null)
                            i.Name = ingredient.Name;

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

        private bool AddIntoDB(Nationality nationality)
        {
            using (ModelDbContext _model = new ModelDbContext())
            {
                using (var transaction = _model.Database.BeginTransaction())
                {
                    try
                    {
                        Nationality n = new Nationality();
                        n.Name = nationality.Name;

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

        private bool UpdateInDB(Nationality nationality)
        {
            using (ModelDbContext _model = new ModelDbContext())
            {
                using (var transaction = _model.Database.BeginTransaction())
                {
                    try
                    {
                        Nationality n = _model.Nationality.ToList().Find(n => n.Idnationality == nationality.Idnationality);

                        if (nationality.Name != null)
                            n.Name = nationality.Name;

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