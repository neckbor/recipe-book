using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Models.BindingModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Backend.Controllers
{
    [ApiController]
    public class GetRecipeController : ControllerBase
    {
        [HttpGet("api/[controller]")]
        public IActionResult Get(int idRecipe)
        {
            try
            {
                if (idRecipe < 1)
                    return BadRequest();

                RecipeBindingModel recipe = GetRecipe(idRecipe);

                if (recipe == null)
                    return NoContent();

                return Ok(recipe);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private RecipeBindingModel GetRecipe(int idRecipe)
        {
            RecipeBindingModel result;

            using (ModelDbContext _model = new ModelDbContext())
            {
                result = (RecipeBindingModel)_model.Recipe.Where(r => r.Idrecipe == idRecipe)
                    .Select(r => new RecipeBindingModel
                    {
                        idRecipe = r.Idrecipe,
                        name = r.Name,
                        mainIngredient = r.IdingredientNavigation.Name,
                        nationality = r.IdnationalityNavigation.Name,
                        steps = r.Step.ToList(),
                        ingredients = r.IngredientList.ToList(),
                    }).FirstOrDefault();
            }
            return result;
        }

        [HttpGet("api/[controller]/nextstep")]
        public IActionResult NextStep(int idRecipe, int currentStep)
        {
            try
            {
                if (idRecipe < 1 || currentStep < 1)
                    return BadRequest();

                var step = GetStep(idRecipe, currentStep + 1);

                if (step.Count() == 0)
                    return NoContent();

                return Ok(step);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("api/[controller]/previousstep")]
        public IActionResult PreviousStep(int idRecipe, int currentStep)
        {
            try
            {
                if (idRecipe < 1 || currentStep < 1)
                    return BadRequest();

                var step = GetStep(idRecipe, currentStep - 1);

                if (step.Count() == 0)
                    return NoContent();

                return Ok(step);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private string GetStep(int idRecipe, int stepOrderIndex)
        {
            string result = String.Empty;
            using(ModelDbContext model = new ModelDbContext())
            {
                result = model.Step.Where(s => s.Idrecipe == idRecipe && s.OrderIndex == stepOrderIndex).FirstOrDefault().Description;
            }
            return result;
        }
    }
}