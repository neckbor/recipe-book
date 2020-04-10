using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Web;
using Backend.Models;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Backend.Controllers
{
    public class SearchController : ApiController
    {
        private ModelDb db = new ModelDb();

        public SearchController()
        {

        }

        [HttpPost]
        [ActionName("Search")]
        public HttpResponseMessage SearchRecipe([FromBody]SearchRequest sr)
        {
            List<GetRecipeBySearch_Result> resultSearch = GetRecipeBySearch(sr.Name, sr.MainIngredient, sr.Nationality);

            string resultJson = JsonConvert.SerializeObject(resultSearch);

            HttpResponseMessage response = Request.CreateResponse(System.Net.HttpStatusCode.OK);

            response.Content = new StringContent(resultJson);

            return response;
        }

        private List<GetRecipeBySearch_Result> GetRecipeBySearch(string name, string mainIngridient, string nationality)
        {
            List<GetRecipeBySearch_Result> resultlist = null;

            var prmName = new System.Data.SqlClient.SqlParameter("@p_Name", System.Data.SqlDbType.NVarChar);
            prmName.Value = name;

            var prmMainIngridient = new System.Data.SqlClient.SqlParameter("@p_MainIngridient", System.Data.SqlDbType.NVarChar);
            prmMainIngridient.Value = mainIngridient;

            var prmNationality = new System.Data.SqlClient.SqlParameter("@p_Nationality", System.Data.SqlDbType.NVarChar);
            prmNationality.Value = nationality;

            var result = db.Database.SqlQuery<GetRecipeBySearch_Result>
                ("GetRecipeBySearch @p_Name, @p_MainIngridient, @p_Nationality ",
                prmName, prmMainIngridient, prmNationality).ToList();

            resultlist = result;

            return resultlist;
        }
    }
}