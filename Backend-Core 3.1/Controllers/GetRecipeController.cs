using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_Core_3._1.Models;
using Backend_Core_3._1.Models.BindingModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Core_3._1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetRecipeController : ControllerBase
    {
        [HttpGet]
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
            using (ModelDbContext model = new ModelDbContext())
            {
                result = (RecipeBindingModel)model.Recipe.Where(r => r.Idrecipe == idRecipe)
                    .Select(r => new RecipeBindingModel
                    {
                        IDRecipe = r.Idrecipe,
                        Name = r.Name,
                        Nationality = r.IdnationalityNavigation.Name,
                        FirstStep = model.Step.Where(s => s.Idrecipe == r.Idrecipe && s.OrderIndex == 1).FirstOrDefault().Description
                    }).FirstOrDefault();
            }
            return result;
        }

        [HttpPost]
        public IActionResult NextStep()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult PreviousStep()
        {
            return Ok();
        }
    }
}