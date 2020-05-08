using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore;
using Backend.Controllers;
using Xunit;
using Backend.Models.BindingModels;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Linq;

namespace Backend.Tests
{
    public class RecipeManagerControllerTests : ControllerBase
    {
        [Fact]
        public void Add_Recipe_Null_Data()
        {
            //Arrange
            RecipeManagerController controller = new RecipeManagerController();
            FullInfoRecipeBindingModel recipe = new FullInfoRecipeBindingModel();

            //Act
            var response = controller.Post(recipe);

            //Assert
            var badRequestResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(StatusCode(500).StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Add_Recipe_Incorrect_Data()
        {
            //Arrange
            RecipeManagerController controller = new RecipeManagerController();
            FullInfoRecipeBindingModel recipe = new FullInfoRecipeBindingModel();
            recipe.name = "";
            recipe.idIngredient = 0;
            recipe.idNationality = 0;
            recipe.author = "";
            recipe.duration = "";

            recipe.steps.Add(new StepBindingModel { description = "", orderIndex = 0 });
            recipe.ingredientList.Add(new IngredientListBindingModel { amount = "", idIngredient = 0 });

            //Act
            var response = controller.Post(recipe);

            //Assert
            var badRequestResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(StatusCode(500).StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Add_Recipe_Correct_Data()
        {
            //Arrange
            RecipeManagerController controller = new RecipeManagerController();
            FullInfoRecipeBindingModel recipe = new FullInfoRecipeBindingModel();
            recipe.name = "Тестовый рецепт";
            recipe.idIngredient = 1;
            recipe.idNationality = 1;
            recipe.author = "Тестовый автор";
            recipe.duration = "1:30";

            recipe.steps.Add(new StepBindingModel { description = "Тестовое описание", orderIndex = 1 });
            recipe.ingredientList.Add(new IngredientListBindingModel { amount = "100г", idIngredient = 1 });

            //Act
            var response = controller.Post(recipe);

            //Assert
            var okRequestResult = Assert.IsType<OkResult>(response);
            Assert.Equal(Ok().StatusCode, okRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Recipe_Null_Id_Null_Data()
        {
            //Arrange
            RecipeManagerController controller = new RecipeManagerController();
            FullInfoRecipeBindingModel recipe = new FullInfoRecipeBindingModel();

            //Act
            var response = controller.Update(recipe);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Recipe_Null_Id_Incorrect_Data()
        {
            //Arrange
            RecipeManagerController controller = new RecipeManagerController();
            FullInfoRecipeBindingModel recipe = new FullInfoRecipeBindingModel();
            recipe.name = "";
            recipe.idIngredient = 0;
            recipe.idNationality = 0;
            recipe.author = "";
            recipe.duration = "";

            recipe.steps.Add(new StepBindingModel { description = "", orderIndex = 0 });
            recipe.ingredientList.Add(new IngredientListBindingModel { amount = "", idIngredient = 0 });

            //Act
            var response = controller.Update(recipe);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Recipe_Null_Id_Correct_Data()
        {
            //Arrange
            RecipeManagerController controller = new RecipeManagerController();
            FullInfoRecipeBindingModel recipe = new FullInfoRecipeBindingModel();
            recipe.name = "Тестовый рецепт";
            recipe.idIngredient = 1;
            recipe.idNationality = 1;
            recipe.author = "Тестовый автор";
            recipe.duration = "1:30";

            recipe.steps.Add(new StepBindingModel { description = "Тестовое описание", orderIndex = 1 });
            recipe.ingredientList.Add(new IngredientListBindingModel { amount = "100г", idIngredient = 1 });

            //Act
            var response = controller.Update(recipe);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Recipe_Real_Id_Null_Data()
        {
            //Arrange
            RecipeManagerController controller = new RecipeManagerController();
            FullInfoRecipeBindingModel recipe = new FullInfoRecipeBindingModel();
            recipe.idRecipe = 8;

            //Act
            var response = controller.Update(recipe);

            //Assert
            var badRequestResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(StatusCode(500).StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Recipe_Real_Id_Incorrect_Data()
        {
            //Arrange
            RecipeManagerController controller = new RecipeManagerController();
            FullInfoRecipeBindingModel recipe = new FullInfoRecipeBindingModel();
            recipe.idRecipe = 8;
            recipe.name = "";
            recipe.idIngredient = 0;
            recipe.idNationality = 0;
            recipe.author = "";
            recipe.duration = "";

            recipe.steps.Add(new StepBindingModel { description = "", orderIndex = 0 });
            recipe.ingredientList.Add(new IngredientListBindingModel { amount = "", idIngredient = 0, idIngredientList = 0 });

            //Act
            var response = controller.Update(recipe);

            //Assert
            var badRequestResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(StatusCode(500).StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Recipe_Real_Id_Correct_Data()
        {
            //Arrange
            RecipeManagerController controller = new RecipeManagerController();
            FullInfoRecipeBindingModel recipe = new FullInfoRecipeBindingModel();
            recipe.idRecipe = 1;
            recipe.name = "Тестовый рецепт";
            recipe.idIngredient = 1;
            recipe.idNationality = 1;
            recipe.author = "Тестовый автор";
            recipe.duration = "1:30";

            recipe.steps.Add(new StepBindingModel { description = "Тестовое описание", orderIndex = 1 });
            recipe.ingredientList.Add(new IngredientListBindingModel { amount = "100г", idIngredient = 1, idIngredientList = 1});

            //Act
            var response = controller.Update(recipe);

            //Assert
            var okRequestResult = Assert.IsType<OkResult>(response);
            Assert.Equal(Ok().StatusCode, okRequestResult.StatusCode);
        }

        [Fact]
        public void Delete_Recipe_0_Id()
        {
            //Arrange
            RecipeManagerController controller = new RecipeManagerController();

            //Act
            var response = controller.Delete(0);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Delete_Recipe_Real_Id()
        {
            //Arrange
            RecipeManagerController controller = new RecipeManagerController();
            int lastRecipe;
            using (ModelDbContext _model = new ModelDbContext())
            {
                lastRecipe = _model.Recipe.ToList().Last().Idrecipe;
            }

            //Act
            var response = controller.Delete(lastRecipe);

            //Assert
            var okRequestResult = Assert.IsType<OkResult>(response);
            Assert.Equal(Ok().StatusCode, okRequestResult.StatusCode);
        }
    }
}
