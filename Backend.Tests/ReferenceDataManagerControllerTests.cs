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
    }
}
