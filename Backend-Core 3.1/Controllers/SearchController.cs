using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Backend_Core_3._1.Models;
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
        public IActionResult Get()
        {
            IEnumerable<GetRcipesBySearch_Result> list = GetRecipes();
            return Ok(list);
        }
        private IEnumerable<GetRcipesBySearch_Result> GetRecipes()//(string recipeName, string ingredent, string nationality, string author)
        {
            //var parName = new SqlParameter("@p_Name", System.Data.SqlDbType.NVarChar);
            //parName.Value = recipeName;

            //var parIngredient = new SqlParameter("@p_Ingredient", System.Data.SqlDbType.NVarChar);
            //parIngredient.Value = ingredent;

            //var parNationality = new SqlParameter("@p_Nationality", System.Data.SqlDbType.NVarChar);
            //parNationality.Value = nationality;

            //var parAuthor = new SqlParameter("@p_Author", System.Data.SqlDbType.NVarChar);
            //parAuthor.Value = author;
            IEnumerable<GetRcipesBySearch_Result> result;
            using (ModelDbContext model = new ModelDbContext())
            {
                result = model.Recipe.Select(r => new GetRcipesBySearch_Result
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