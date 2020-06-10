using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Models.BindingModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Backend.Controllers
{
    [ApiController]
    public class GetRecipeController : ControllerBase
    {
        private readonly ILogger<GetRecipeController> _logger;

        public GetRecipeController(ILogger<GetRecipeController> logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// Получить данные рецепта по id
        /// </summary>
        /// <param name="idRecipe">id Рецепта</param>
        /// <returns>Данные рецепта</returns>
        /// <response code="200">ОК, рецепт найден</response>
        /// <response code="400">Некорректные значения</response>
        /// <response code="204">Рецепт не найден</response>
        /// <response code="500">Внутренняя ошибка (читать сообщение в ответе)</response>
        [HttpGet("api/[controller]")]
        public IActionResult Get(int idRecipe)
        {
            _logger.LogError("Get: запуск с параметрами\n" + JsonConvert.SerializeObject(idRecipe));
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

        private int GetRandomId()
        {
            using ModelDbContext model = new ModelDbContext();

            Random rnd = new Random();

            int result = -1;

            int min = model.Recipe.Min(r => r.Idrecipe);
            int max = model.Recipe.Max(r => r.Idrecipe);

            while (result == -1)
            {
                result = rnd.Next(min, max + 1);

                if (model.Recipe.Find(result) == null)
                    result = -1;
            }

            return result;
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
                        author = r.Author,
                        mainIngredient = r.IdingredientNavigation.Name,
                        nationality = r.IdnationalityNavigation.Name,
                        duration = r.Duration,
                        steps = r.Step.Select(s => new StepBindingModel
                        {
                            idStep = s.Idstep,
                            description = s.Description,
                            orderIndex = s.OrderIndex
                        }).ToList(),
                        ingredients = r.IngredientList.Select(il => new IngredientListBindingModel
                        {
                            idIngredientList = il.IdingredientList,
                            idIngredient = il.IdingredientNavigation.Idingredient,
                            name = il.IdingredientNavigation.Name,
                            amount = il.Amount
                        }).ToList(),
                    }).FirstOrDefault();
            }
            return result;
        }

        /// <summary>
        /// Получить случайный рецепт
        /// </summary>
        /// <returns>Данные рецепта</returns>
        /// <response code="200">ОК, рецепт</response>
        /// <response code="500">Внутренняя ошибка (читать сообщение в ответе)</response>
        [HttpGet("api/[controller]/random")]
        public IActionResult GetRandom()
        {
            _logger.LogError("GetRandom: запуск");
            try
            {
                int idRecipe = GetRandomId();

                RecipeBindingModel recipe = GetRecipe(idRecipe);

                return Ok(recipe);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Получить описание следующего шага
        /// </summary>
        /// <param name="idRecipe">id Рецепта</param>
        /// <param name="currentStep">Номер нынешнего шага</param>
        /// <returns>Описание следующего шага</returns>
        /// <response code="200">ОК, шаг найден</response>
        /// <response code="400">Некорректные значения</response>
        /// <response code="204">Шаг не найден</response>
        /// <response code="500">Внутренняя ошибка (читать сообщение в ответе)</response>
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

        /// <summary>
        /// Получить описание предыдущего шага
        /// </summary>
        /// <param name="idRecipe">id Рецепта</param>
        /// <param name="currentStep">Номер нынешнего шага</param>
        /// <returns>Описание предыдущего шага</returns>
        /// <response code="200">ОК, шаг найден</response>
        /// <response code="400">Некорректные значения</response>
        /// <response code="204">Шаг не найден</response>
        /// <response code="500">Внутренняя ошибка (читать сообщение в ответе)</response>
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