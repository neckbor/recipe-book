using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Models.BindingModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    public class SearchController : ControllerBase
    {
        [HttpPost("api/[controller]")]
        public IActionResult Post(SearchConditionBindigModel conditions)
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
                return StatusCode(500, e.Message);
            }
            
        }

        private IEnumerable<GetRcipesBySearch_Result> GetRecipes(SearchConditionBindigModel conditions)
        {
            IEnumerable<GetRcipesBySearch_Result> result;
            using (ModelDbContext model = new ModelDbContext())
            {
                result = model.Recipe.Where(r => EF.Functions.Like(r.Name.ToLower(), conditions.recipeName.ToLower()))
                    .Where(r => r.Idnationality == conditions.idNationality
                        && r.Idingredient == conditions.idIngredient
                        && EF.Functions.Like(r.Author.ToLower(), conditions.author.ToLower()))
                    .Select(r => new GetRcipesBySearch_Result
                    {
                        idRecipe = r.Idrecipe,
                        name = r.Name,
                        author = r.Author,
                        duration = r.Duration,
                        ingredient = r.IdingredientNavigation.Name,
                        nationality = r.IdnationalityNavigation.Name
                    }).ToList(); 
            }
            return result;
        }
    }
}