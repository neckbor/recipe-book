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
        [HttpPost("api/[controller]/add")]
        public IActionResult Post(FullInfoRecipeBindingModel recipe)
        {
            try
            {
                if (recipe.name == null)
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
                            ingredient.Idrecipe = curidrecipe;
                        }
                        _model.IngredientList.AddRange(recipe.ingredientList);

                        foreach (var step in recipe.steps)
                        {
                            step.Idrecipe = curidrecipe;
                        }
                        _model.Step.AddRange(recipe.steps);

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

        [HttpPost("api/[controller]/update")]
        public IActionResult Update(FullInfoRecipeBindingModel recipe)
        {
            try
            {
                if (recipe.name == null)
                    return BadRequest();

                if (UpdateInDB(recipe))
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

        private bool UpdateInDB(FullInfoRecipeBindingModel recipe)
        {
            using (ModelDbContext _model = new ModelDbContext())
            {
                using (var transaction = _model.Database.BeginTransaction())
                {
                    try
                    {
                        Recipe r = _model.Recipe.ToList().Find(r => r.Idrecipe == recipe.idRecipe);

                        if (recipe.name != null)
                            r.Name = recipe.name;

                        if (recipe.idIngredient != 0)
                            r.Idingredient = recipe.idIngredient;

                        if (recipe.idNationality != 0)
                            r.Idnationality = recipe.idNationality;

                        if (recipe.duration != null)
                            r.Duration = TimeSpan.Parse(recipe.duration);

                        if (recipe.steps.Count != 0)
                        {
                            foreach (var step in recipe.steps)
                            {
                                Step s = _model.Step.ToList().Find(s => s.Idrecipe == recipe.idRecipe && s.OrderIndex == step.OrderIndex);
                                if(step.Description!=null)
                                {
                                    s.Description = step.Description;
                                }
                            }
                        }

                        if (recipe.ingredientList.Count != 0)
                        {
                            foreach (var ingredient in recipe.ingredientList)
                            {
                                IngredientList i = _model.IngredientList.ToList().Find(i => i.Idrecipe == recipe.idRecipe && i.IdingredientList == ingredient.IdingredientList);
                                
                                if (ingredient.Idingredient != 0)
                                {
                                    i.Idingredient = ingredient.Idingredient;
                                }

                                if (ingredient.Amount != null)
                                {
                                    i.Amount = ingredient.Amount;
                                }
                            }
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