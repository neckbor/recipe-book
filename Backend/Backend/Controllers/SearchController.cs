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
        private YummYummYEntities db = new YummYummYEntities();

        public SearchController()
        {

        }

        [HttpPost]
        [ActionName("Search")]
        public HttpResponseMessage SearchRecipe([FromBody]SearchRequest sr)
        {
            List<GetRecipesBySearch_Result> resultSearch = GetRecipeBySearch(sr.name, sr.mainIngredient, sr.nationality, sr.author);

            string resultJson = JsonConvert.SerializeObject(resultSearch);

            HttpResponseMessage response = Request.CreateResponse(System.Net.HttpStatusCode.OK);

            response.Content = new StringContent(resultJson);

            return response;
        }

        private List<GetRecipesBySearch_Result> GetRecipeBySearch(string name, string mainIngridient, string nationality, string author)
        {
            List<GetRecipesBySearch_Result> resultlist = null;

            var prmName = new System.Data.SqlClient.SqlParameter("@p_Name", System.Data.SqlDbType.NVarChar);
            prmName.Value = name;

            var prmMainIngridient = new System.Data.SqlClient.SqlParameter("@p_MainIngridient", System.Data.SqlDbType.NVarChar);
            prmMainIngridient.Value = mainIngridient;

            var prmNationality = new System.Data.SqlClient.SqlParameter("@p_Nationality", System.Data.SqlDbType.NVarChar);
            prmNationality.Value = nationality;

            var prmAuthor = new System.Data.SqlClient.SqlParameter("@p_Author", System.Data.SqlDbType.NVarChar);
            prmAuthor.Value = author;

            var result = db.Database.SqlQuery<GetRecipesBySearch_Result>
                ("GetRecipesBySearch @p_Name, @p_MainIngridient, @p_Nationality, @p_Author ",
                prmName, prmMainIngridient, prmNationality, prmAuthor).ToList();

            resultlist = result;

            return resultlist;
        }
    }
}