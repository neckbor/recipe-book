using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Backend_Core_3._1.Models;
using Backend_Core_3._1.Models.BindingModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Backend_Core_3._1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        ModelDbContext _model = new ModelDbContext();

        [HttpGet]
        public IActionResult Get(SearchConditionBindigModel conditions)
        {
            try
            {
                if (conditions == null)
                    return BadRequest();

                IEnumerable<GetRcipesBySearch_Result> result = GetRecipes(conditions);
                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            catch(Exception e)
            {
                Response.StatusCode = 500;
                return StatusCode(500);
            }
            
        }

        private IEnumerable<GetRcipesBySearch_Result> GetRecipes(SearchConditionBindigModel conditions)
        {
            IEnumerable<GetRcipesBySearch_Result> result;
            using (ModelDbContext model = new ModelDbContext())
            {
                result = model.Recipe.Where(r => EF.Functions.Like(r.Name.ToLower(), conditions.RecipeName.ToLower()))
                    .Where(r => r.Idnationality == conditions.IDNationality
                        && r.Idingredient == conditions.IDIngredient
                        && EF.Functions.Like(r.Author.ToLower(), conditions.Author.ToLower()))
                    .Select(r => new GetRcipesBySearch_Result
                    {
                        IDRecipe = r.Idrecipe,
                        Name = r.Name,
                        Author = r.Author,
                        Duration = r.Duration,
                        Ingredient = r.IdingredientNavigation.Name,
                        Nationality = r.IdnationalityNavigation.Name
                    }).ToList(); 
            }
            return result;
        }
    }
}