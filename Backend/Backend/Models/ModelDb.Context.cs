﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Backend.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class YummYummYEntities : DbContext
    {
        public YummYummYEntities()
            : base("name=YummYummYEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Action> Actions { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<Step> Steps { get; set; }
    
        public virtual ObjectResult<GetRecipeBySearch_Result> GetRecipeBySearch(string p_Name, string p_MainIngridient, string p_Nationality)
        {
            var p_NameParameter = p_Name != null ?
                new ObjectParameter("p_Name", p_Name) :
                new ObjectParameter("p_Name", typeof(string));
    
            var p_MainIngridientParameter = p_MainIngridient != null ?
                new ObjectParameter("p_MainIngridient", p_MainIngridient) :
                new ObjectParameter("p_MainIngridient", typeof(string));
    
            var p_NationalityParameter = p_Nationality != null ?
                new ObjectParameter("p_Nationality", p_Nationality) :
                new ObjectParameter("p_Nationality", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRecipeBySearch_Result>("GetRecipeBySearch", p_NameParameter, p_MainIngridientParameter, p_NationalityParameter);
        }
    
        public virtual ObjectResult<GetRecipesBySearch_Result> GetRecipesBySearch(string p_Name, string p_MainIngredient, string p_Nationality, string p_Author)
        {
            var p_NameParameter = p_Name != null ?
                new ObjectParameter("p_Name", p_Name) :
                new ObjectParameter("p_Name", typeof(string));
    
            var p_MainIngredientParameter = p_MainIngredient != null ?
                new ObjectParameter("p_MainIngredient", p_MainIngredient) :
                new ObjectParameter("p_MainIngredient", typeof(string));
    
            var p_NationalityParameter = p_Nationality != null ?
                new ObjectParameter("p_Nationality", p_Nationality) :
                new ObjectParameter("p_Nationality", typeof(string));
    
            var p_AuthorParameter = p_Author != null ?
                new ObjectParameter("p_Author", p_Author) :
                new ObjectParameter("p_Author", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRecipesBySearch_Result>("GetRecipesBySearch", p_NameParameter, p_MainIngredientParameter, p_NationalityParameter, p_AuthorParameter);
        }
    }
}