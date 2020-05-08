using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Models.BindingModels;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    public class RecipeManagerController : ControllerBase
    {
        /// <summary>
        /// Добавить новый рецепт
        /// </summary>
        /// <param name="recipe">Данные рецепта</param>
        /// <returns>Результат, получилось добавить или нет</returns>
        /// <response code="200">Рецепт добавлен</response>
        /// <response code="400">Некорректные значения</response>
        /// <response code="500">Внутренняя ошибка (читать сообщение в ответе)</response>
        [HttpPost("api/[controller]/add")]
        public IActionResult Post(FullInfoRecipeBindingModel recipe)
        {
            try
            {
                if (recipe == null)
                    return BadRequest();

                if(AddIntoDB(recipe))
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "Во время добавления что-то пошло не так");
                }
            }
            catch(Exception e)
            {
                return StatusCode(500, e.InnerException.Message);
            }
        }

        private bool AddIntoDB(FullInfoRecipeBindingModel recipe)
        {
            using (ModelDbContext _model = new ModelDbContext())
            {
                using (var transaction = _model.Database.BeginTransaction()) 
                {
                    try
                    {
                        Recipe r = new Recipe();
                        r.Name = recipe.name;
                        r.Idingredient = recipe.idIngredient;
                        r.Idnationality = recipe.idNationality;
                        r.Author = recipe.author;
                        r.DtimeCreate = DateTime.Now;
                        if (recipe.duration != null)
                            r.Duration = TimeSpan.Parse(recipe.duration);
                        _model.Recipe.Add(r);

                        _model.SaveChanges();

                        int curidrecipe = _model.Recipe.ToList().Last().Idrecipe;

                        foreach (var ingredient in recipe.ingredientList)
                        {
                            IngredientList i = new IngredientList();
                            i.Idrecipe = curidrecipe;
                            i.Idingredient = ingredient.idIngredient;
                            //if(_model.Ingredient.ToList().FindAll(i => i.Name.ToLower() == ingredient.ingredient.ToLower()).Count() == 0)
                            //{
                            //    _model.Ingredient.Add(new Ingredient { Name = ingredient.ingredient });
                            //    _model.SaveChanges();
                            //    i.Idingredient = _model.Ingredient.ToList().Last().Idingredient;
                            //}
                            //else
                            //{
                            //    i.Idingredient = _model.Ingredient.ToList().Find(i => i.Name.ToLower() == ingredient.ingredient.ToLower()).Idingredient;
                            //}
                            i.Amount = ingredient.amount;
                            _model.IngredientList.Add(i);
                        }
                        
                        foreach (var step in recipe.steps)
                        {
                            Step s = new Step();
                            s.Idrecipe = curidrecipe;
                            s.OrderIndex = step.orderIndex;
                            s.Description = step.description;
                            _model.Step.Add(s);
                        }

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

        /// <summary>
        /// Изменить данные рецепта
        /// </summary>
        /// <param name="recipe">Данные рецепта</param>
        /// <returns>Результат, получилось изменить или нет</returns>
        /// <response code="400">Некорректные значения</response>
        /// <response code="200">Данные рецепта изменены</response>
        /// <response code="500">Внутренняя ошибка (читать сообщение в ответе)</response>
        [HttpPost("api/[controller]/update")]
        public IActionResult Update(FullInfoRecipeBindingModel recipe)
        {
            try
            {
                if (recipe.idRecipe < 1)
                    return BadRequest();

                var response = UpdateInDB(recipe);

                if (response == "OK")
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "Во время изменения что-то пошло не так!" + response);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private string UpdateInDB(FullInfoRecipeBindingModel recipe)
        {
            using (ModelDbContext _model = new ModelDbContext())
            {
                using (var transaction = _model.Database.BeginTransaction())
                {
                    try
                    {
                        Recipe r = _model.Recipe.ToList().Find(r => r.Idrecipe == recipe.idRecipe);

                        if (recipe.name != null && recipe.name.Length > 0)
                            r.Name = recipe.name;
                        else
                            throw new Exception("Имя нулевое");

                        if (recipe.idIngredient != 0)
                            r.Idingredient = recipe.idIngredient;
                        else
                            throw new Exception("Id главного ингредиента присвоен нулю");


                        if (recipe.idNationality != 0)
                            r.Idnationality = recipe.idNationality;
                        else
                            throw new Exception("Id национальности присвоено нулю");

                        if (recipe.duration != null || recipe.duration.Length > 0)
                            r.Duration = TimeSpan.Parse(recipe.duration);

                        if (recipe.steps.Count != 0)
                        {
                            foreach (var step in recipe.steps)
                            {
                                Step s = _model.Step.ToList().Find(s => s.Idrecipe == recipe.idRecipe && s.OrderIndex == step.orderIndex);
                                if(step.description!=null)
                                {
                                    s.Description = step.description;
                                }
                            }
                        }
                        else
                            throw new Exception("Отсутствуют шаги");

                        if (recipe.ingredientList.Count != 0)
                        {
                            foreach (var ingredient in recipe.ingredientList)
                            {
                                IngredientList i = _model.IngredientList.ToList().Find(i => i.Idrecipe == recipe.idRecipe && i.IdingredientList == ingredient.idIngredientList);

                                if (ingredient.idIngredientList != 0)
                                {
                                    i.Idingredient = ingredient.idIngredientList;
                                }

                                if (ingredient.amount != null)
                                {
                                    i.Amount = ingredient.amount;
                                }
                            }
                        }
                        else
                            throw new Exception("Отсутствует список ингредиентов");

                        _model.SaveChanges();
                        transaction.Commit();
                        return "OK";
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        return e.Message;
                    }
                }
            }
        }

        /// <summary>
        /// Удалить рецепт
        /// </summary>
        /// <param name="idRecipe">id Рецепта</param>
        /// <returns>Результат, удалён рецепт или нет</returns>
        /// <response code="400">Некорректные значения</response>
        /// <response code="200">Рецепт удалён</response>
        /// <response code="500">Внутренняя ошибка (читать сообщение в ответе)</response>
        [HttpDelete("api/[controller]/delete")]
        public IActionResult Delete(int idRecipe)
        {
            try
            {
                if (idRecipe < 1)
                    return BadRequest();

                if (DeleteFromDB(idRecipe))
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "Во время удаления что-то пошло не так");
                }
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private bool DeleteFromDB(int idRecipe)
        {
            using (ModelDbContext _model = new ModelDbContext())
            {
                using (var transaction = _model.Database.BeginTransaction())
                {
                    try
                    {
                        Recipe recipe = _model.Recipe.ToList().Find(r => r.Idrecipe == idRecipe);
                        _model.Recipe.Remove(recipe);

                        List<Step> steps = _model.Step.ToList().FindAll(s => s.Idrecipe == idRecipe);
                        _model.Step.RemoveRange(steps);

                        List<IngredientList> ingredients = _model.IngredientList.ToList().FindAll(i => i.Idrecipe == idRecipe);
                        _model.IngredientList.RemoveRange(ingredients);

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
    }
}