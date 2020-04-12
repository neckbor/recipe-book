using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Backend.Models;

namespace Backend.Controllers
{
    public class GetRecipeController : ApiController
    {
        private YummYummYEntities db = new YummYummYEntities();

        [HttpPost]
        [ActionName("Get")]
        public HttpResponseMessage GetRecipe([FromBody] GetRequest gr)
        {
            OutPutRecipe result = GetRecipe(gr.id);

            string resultJson = JsonConvert.SerializeObject(result);

            HttpResponseMessage response = Request.CreateResponse(System.Net.HttpStatusCode.OK);

            response.Content = new StringContent(resultJson);

            return response;
        }

        public OutPutRecipe GetRecipe(int id)
        {
            Recipe r = db.Recipes.ToList().Find(item => item.IDRecipe == id);

            string mainIngredient = db.Ingredients.ToList().Find(item => item.IDIngredient == r.IDIngredient).Name;

            List<Step> ss = db.Steps.ToList().FindAll(item => item.IDRecipe == r.IDRecipe);

            List<OutPutStep> steps = new List<OutPutStep>();

            foreach(var i in ss)
            {
                OutPutStep ops = new OutPutStep(db.Ingredients.ToList().Find(item => item.IDIngredient == i.IDIngredient).Name, db.Actions.ToList().Find(item => item.IDAction == i.IDAction).Name, Convert.ToString(i.Time));

                steps.Add(ops);
            }

            OutPutRecipe recipe = new OutPutRecipe(
                id,
                r.Name,
                mainIngredient,
                r.Nationality,
                r.Author,
                steps
                );

            return recipe;
        }
    }
}