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
    public class ReferenceDataManagerControllerTests : ControllerBase
    {
        [Fact]
        public void Get_Ingredients_Null_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            IngredientBindingModel ingredient = new IngredientBindingModel();

            //Act
            var response = controller.Get(ingredient);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Get_Ingredients_Empty_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            IngredientBindingModel ingredient = new IngredientBindingModel();
            ingredient.name = "";

            //Act
            var response = controller.Get(ingredient);

            //Assert
            var OkRequestResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(Ok().StatusCode, OkRequestResult.StatusCode);
        }

        [Fact]
        public void Get_Ingredients_Real_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            IngredientBindingModel ingredient = new IngredientBindingModel();
            ingredient.name = "к";

            //Act
            var response = controller.Get(ingredient);

            //Assert
            var OkRequestResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(Ok().StatusCode, OkRequestResult.StatusCode);
        }

        [Fact]
        public void Get_Nationalities_Null_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            NationalityBindingModel nationality = new NationalityBindingModel();

            //Act
            var response = controller.Get(nationality);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Get_Nationalities_Empty_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            NationalityBindingModel nationality = new NationalityBindingModel();
            nationality.name = "";

            //Act
            var response = controller.Get(nationality);

            //Assert
            var OkRequestResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(Ok().StatusCode, OkRequestResult.StatusCode);
        }

        [Fact]
        public void Get_Nationalities_Real_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            NationalityBindingModel nationality = new NationalityBindingModel();
            nationality.name = "к";

            //Act
            var response = controller.Get(nationality);

            //Assert
            var OkRequestResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(Ok().StatusCode, OkRequestResult.StatusCode);
        }

        [Fact]
        public void Add_Ingredient_Null_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            IngredientBindingModel ingredient = new IngredientBindingModel();

            //Act
            var response = controller.AddIngredient(ingredient);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Add_Ingredient_Empty_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            IngredientBindingModel ingredient = new IngredientBindingModel();
            ingredient.name = "";

            //Act
            var response = controller.AddIngredient(ingredient);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Add_Ingredient_Real_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            IngredientBindingModel ingredient = new IngredientBindingModel();
            ingredient.name = "Огурец";

            //Act
            var response = controller.AddIngredient(ingredient);

            //Assert
            var OkRequestResult = Assert.IsType<OkResult>(response);
            Assert.Equal(Ok().StatusCode, OkRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Ingredient_Null_Id_Null_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            IngredientBindingModel ingredient = new IngredientBindingModel();

            //Act
            var response = controller.UpdateIngredient(ingredient);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Ingredient_Null_Id_Empty_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            IngredientBindingModel ingredient = new IngredientBindingModel();
            ingredient.name = "";

            //Act
            var response = controller.UpdateIngredient(ingredient);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Ingredient_Null_Id_Real_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            IngredientBindingModel ingredient = new IngredientBindingModel();
            ingredient.name = "к";

            //Act
            var response = controller.UpdateIngredient(ingredient);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Ingredient_0_Id_Null_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            IngredientBindingModel ingredient = new IngredientBindingModel();
            ingredient.idIngredient = 0;

            //Act
            var response = controller.UpdateIngredient(ingredient);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Ingredient_0_Id_Empty_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            IngredientBindingModel ingredient = new IngredientBindingModel();
            ingredient.idIngredient = 0;
            ingredient.name = "";

            //Act
            var response = controller.UpdateIngredient(ingredient);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Ingredient_0_Id_Real_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            IngredientBindingModel ingredient = new IngredientBindingModel();
            ingredient.idIngredient = 0;
            ingredient.name = "к";

            //Act
            var response = controller.UpdateIngredient(ingredient);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Ingredient_Real_Id_Null_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            IngredientBindingModel ingredient = new IngredientBindingModel();
            ingredient.idIngredient = 1;

            //Act
            var response = controller.UpdateIngredient(ingredient);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Ingredient_Real_Id_Empty_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            IngredientBindingModel ingredient = new IngredientBindingModel();
            ingredient.idIngredient = 1;
            ingredient.name = "";

            //Act
            var response = controller.UpdateIngredient(ingredient);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Ingredient_Real_Id_Real_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            IngredientBindingModel ingredient = new IngredientBindingModel();
            ingredient.idIngredient = 1;
            ingredient.name = "Мука";

            //Act
            var response = controller.UpdateIngredient(ingredient);

            //Assert
            var OkRequestResult = Assert.IsType<OkResult>(response);
            Assert.Equal(Ok().StatusCode, OkRequestResult.StatusCode);
        }

        [Fact]
        public void Delete_Ingredient_0_Id()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            int idIngredient = 0;

            //Act
            var response = controller.DeleteIngredient(idIngredient);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Delete_Ingredient_Real_Id()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            int lastIngredient;
            using (ModelDbContext _model = new ModelDbContext())
            {
                lastIngredient = _model.Ingredient.ToList().Last().Idingredient;
            }

            //Act
            var response = controller.DeleteIngredient(lastIngredient);

            //Assert
            var OkRequestResult = Assert.IsType<OkResult>(response);
            Assert.Equal(Ok().StatusCode, OkRequestResult.StatusCode);
        }

        [Fact]
        public void Add_Nationality_Null_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            NationalityBindingModel nationality = new NationalityBindingModel();

            //Act
            var response = controller.AddNationality(nationality);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Add_Nationality_Empty_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            NationalityBindingModel nationality = new NationalityBindingModel();
            nationality.name = "";

            //Act
            var response = controller.AddNationality(nationality);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Add_Nationality_Real_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            NationalityBindingModel nationality = new NationalityBindingModel();
            nationality.name = "Китайская";

            //Act
            var response = controller.AddNationality(nationality);

            //Assert
            var OkRequestResult = Assert.IsType<OkResult>(response);
            Assert.Equal(Ok().StatusCode, OkRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Nationality_Null_Id_Null_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            NationalityBindingModel nationality = new NationalityBindingModel();

            //Act
            var response = controller.UpdateNationality(nationality);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Nationality_Null_Id_Empty_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            NationalityBindingModel nationality = new NationalityBindingModel();
            nationality.name = "";

            //Act
            var response = controller.UpdateNationality(nationality);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Nationality_Null_Id_Real_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            NationalityBindingModel nationality = new NationalityBindingModel();
            nationality.name = "к";

            //Act
            var response = controller.UpdateNationality(nationality);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Nationality_0_Id_Null_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            NationalityBindingModel nationality = new NationalityBindingModel();
            nationality.idNationality = 0;

            //Act
            var response = controller.UpdateNationality(nationality);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Nationality_0_Id_Empty_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            NationalityBindingModel nationality = new NationalityBindingModel();
            nationality.idNationality = 0;
            nationality.name = "";

            //Act
            var response = controller.UpdateNationality(nationality);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Nationality_0_Id_Real_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            NationalityBindingModel nationality = new NationalityBindingModel();
            nationality.idNationality = 0;
            nationality.name = "к";

            //Act
            var response = controller.UpdateNationality(nationality);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Nationality_Real_Id_Null_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            NationalityBindingModel nationality = new NationalityBindingModel();
            nationality.idNationality = 1;

            //Act
            var response = controller.UpdateNationality(nationality);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Nationality_Real_Id_Empty_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            NationalityBindingModel nationality = new NationalityBindingModel();
            nationality.idNationality = 1;
            nationality.name = "";

            //Act
            var response = controller.UpdateNationality(nationality);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Update_Nationality_Real_Id_Real_Name()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            NationalityBindingModel nationality = new NationalityBindingModel();
            nationality.idNationality = 1;
            nationality.name = "Русская";

            //Act
            var response = controller.UpdateNationality(nationality);

            //Assert
            var OkRequestResult = Assert.IsType<OkResult>(response);
            Assert.Equal(Ok().StatusCode, OkRequestResult.StatusCode);
        }

        [Fact]
        public void Delete_Nationality_0_Id()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            int idNationality = 0;

            //Act
            var response = controller.DeleteNationality(idNationality);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Delete_Nationality_Real_Id()
        {
            //Arrange
            ReferenceDataManagerController controller = new ReferenceDataManagerController();
            int lastNationality;
            using (ModelDbContext _model = new ModelDbContext())
            {
                lastNationality = _model.Nationality.ToList().Last().Idnationality;
            }

            //Act
            var response = controller.DeleteNationality(lastNationality);

            //Assert
            var OkRequestResult = Assert.IsType<OkResult>(response);
            Assert.Equal(Ok().StatusCode, OkRequestResult.StatusCode);
        }
    }
}
