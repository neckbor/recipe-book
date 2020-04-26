using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Models.BindingModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    public class SearchController : ControllerBase
    {
        /// <summary>
        /// Поиск рецептов по заданным параметрам
        /// </summary>
        /// <param name="conditions">Параметры поиска</param>
        /// <returns>Список найденный рецептов</returns>
        /// <response code="204">Не найдено ни одного рецепта</response>
        /// <response code="400">Некорректные значения</response>
        /// <response code="500">Внутренняя ошибка (читать сообщение в ответе)</response>
        /// <response code="200">Рецепты найдены</response>
        [HttpPost("api/[controller]")]
        public IActionResult Post(SearchConditionBindingModel conditions)
        {
            try
            {
                if (conditions.recipeName == null && conditions.nationality == null && conditions.ingredient == null && conditions.author == null)
                    return BadRequest();

                IEnumerable<GetRcipesBySearch_Result> result = GetRecipes(conditions);
                if (result.Count() < 1)
                    return NoContent();

                return Ok(result);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }            
        }

        private IEnumerable<GetRcipesBySearch_Result> GetRecipes(SearchConditionBindingModel conditions)
        {
            IEnumerable<GetRcipesBySearch_Result> result;
            using (ModelDbContext model = new ModelDbContext())
            {
                result = model.Recipe.Where(r => EF.Functions.Like(r.Name.ToLower(), '%' + conditions.recipeName.ToLower() + '%')
                        && EF.Functions.Like(r.IdnationalityNavigation.Name.ToLower(), '%' + conditions.nationality.ToLower() + '%')
                        && EF.Functions.Like(r.IdingredientNavigation.Name.ToLower(), '%' + conditions.ingredient.ToLower() + '%')
                        && EF.Functions.Like(r.Author.ToLower(), '%' + conditions.author.ToLower() + '%'))
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