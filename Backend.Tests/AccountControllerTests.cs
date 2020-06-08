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
    public class AccountControllerTests : ControllerBase
    {
        [Fact]
        public void Register_Empty_Model()
        {
            //Arrange
            AccountController controller = new AccountController();
            RegisterBindingModel model = new RegisterBindingModel();
            model.email = "";
            model.login = "";
            model.password = "";

            //Act
            var response = controller.Register(model);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Register_Real_Model()
        {
            //Arrange
            AccountController controller = new AccountController();
            RegisterBindingModel model = new RegisterBindingModel();
            model.email = "uniqemail@mail.ru";
            model.login = "uniqlog";
            model.password = "Password_123";

            //Act
            var response = controller.Register(model);

            //Assert
            var ConflictRequestResult = Assert.IsType<ConflictResult>(response);
            Assert.Equal(Conflict().StatusCode, ConflictRequestResult.StatusCode);
        }

        [Fact]
        public void Login_Empty_Model()
        {
            //Arrange
            AccountController controller = new AccountController();
            LoginBindingModel model = new LoginBindingModel();
            model.login = "";
            model.password = "";

            //Act
            var response = controller.Login(model);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(response);
            Assert.Equal(BadRequest().StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Login_Real_Model()
        {
            //Arrange
            AccountController controller = new AccountController();
            LoginBindingModel model = new LoginBindingModel();
            model.login = "uniqlog";
            model.password = "Password_123";

            //Act
            var response = controller.Login(model);

            //Assert
            var OkRequestResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(Ok().StatusCode, OkRequestResult.StatusCode);
        }

        [Fact]
        public void Block_Not_Exist_User()
        {
            //Arrange
            AccountController controller = new AccountController();
            UserLoginString user = new UserLoginString();
            user.login = "Not_Exist_User";

            //Act
            var response = controller.Block(user);

            //Assert
            var badRequestResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(StatusCode(500).StatusCode, badRequestResult.StatusCode);
        }

        [Fact]
        public void Block_Exist_User()
        {
            //Arrange
            AccountController controller = new AccountController();
            UserLoginString user = new UserLoginString();
            user.login = "uniqlog";

            //Act
            var response = controller.Block(user);

            //Assert
            var OkRequestResult = Assert.IsType<OkResult>(response);
            Assert.Equal(Ok().StatusCode, OkRequestResult.StatusCode);
        }
    }
}
