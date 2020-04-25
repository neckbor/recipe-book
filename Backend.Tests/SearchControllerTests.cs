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
    public class SearchControllerTests : ControllerBase
    {
        [Fact]
        public void Post_Null_Condition()
        {
            //Arrange
            SearchController controller = new SearchController();
            SearchConditionBindingModel condition = new SearchConditionBindingModel();

            //Act
            var response = controller.Post(condition);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Post_Empty_Condition()
        {
            //Arrange
            SearchController controller = new SearchController();
            SearchConditionBindingModel condition = new SearchConditionBindingModel();
            condition.author = "";
            condition.ingredient = "";
            condition.nationality = "";
            condition.recipeName = "";

            //Act
            var response = controller.Post(condition);

            //Assert
            var OkRequestResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(Ok().StatusCode, OkRequestResult.StatusCode);
        }

        [Fact]
        public void Post_Real_Condition()
        {
            //Arrange
            SearchController controller = new SearchController();
            SearchConditionBindingModel condition = new SearchConditionBindingModel();
            condition.author = "";
            condition.ingredient = "";
            condition.nationality = "";
            condition.recipeName = "а";

            //Act
            var response = controller.Post(condition);

            //Assert
            var OkRequestResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(Ok().StatusCode, OkRequestResult.StatusCode);
        }
    }
}
