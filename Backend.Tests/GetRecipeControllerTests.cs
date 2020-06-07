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
    public class GetRecipeControllerTests : ControllerBase
    {
        [Fact]
        public void GetRecipe_0_Id()
        {
            //Arrange
            GetRecipeController controller = new GetRecipeController();
            int idRecipe = 0;            

            //Act
            var response = controller.Get(idRecipe);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void GetRecipe_Real_Id()
        {
            //Arrange
            GetRecipeController controller = new GetRecipeController();
            int idRecipe = 1;

            //Act
            var response = controller.Get(idRecipe);

            //Assert
            var OkRequestResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(Ok().StatusCode, OkRequestResult.StatusCode);
        }
    }
}
