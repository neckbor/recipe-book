using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;
using Backend.Models;

namespace Backend.Controllers
{
    public class SearchController : Controller
    {
        private ModelDb db = new ModelDb();

        public SearchController()
        {

        }

        public string SearchRecipe(string name, string mainIngridient, string nationality)
        {
            List<GetRecipeBySearch_Result> resultSearch = GetRecipeBySearch(name, mainIngridient, nationality);

            string resultJson = JsonConvert.SerializeObject(resultSearch);

            return resultJson;
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