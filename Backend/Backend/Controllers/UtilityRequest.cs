using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Backend.Models;

namespace Backend.Controllers
{
    public class SearchRequest
    {
        public string name { get; set; }
        public string mainIngredient { get; set; }
        public string nationality { get; set; }
        public string author { get; set; }
    }

    public class GetRequest
    {
        public int id { get; set; }
    }

    public class OutPutRecipe
    {
        public int id { get; set; }
        public string name { get; set; }
        public string mainIngredient { get; set; }
        public string nationality { get; set; }
        public string author { get; set; }
        public List<OutPutStep> steps { get; set; }

        public OutPutRecipe(int id, string name, string mainIngredient, string nationality, string author, List<OutPutStep> steps)
        {
            this.id = id;
            this.name = name;
            this.mainIngredient = mainIngredient;
            this.nationality = nationality;
            this.author = author;
            this.steps = steps;
        }
    }

    public class OutPutStep
    {
        public string ingredient { get; set; }
        public string action { get; set; }
        public string time { get; set; }

        public OutPutStep(string ingredient, string action, string time)
        {
            this.ingredient = ingredient;
            this.action = action;
            this.time = time;
        }
    }
}