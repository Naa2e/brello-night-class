using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Brello.Controllers;
using System.Web.Mvc;
using Moq;
using Brello.Models;
using System.Collections.Generic;

namespace Brello.Tests.Controllers
{
    [TestClass]
    public class BoardControllerTest
    {
        private Mock<BoardContext> mock_context;
        private Mock<BoardRepository> mock_repository;
        private ApplicationUser owner, user1, user2;


        [TestInitialize]
        public void Initilaize()
        {
            mock_context = new Mock<BoardContext>();
            mock_repository = new Mock<BoardRepository>();
            owner = new ApplicationUser();
            user1 = new ApplicationUser();
            user2 = new ApplicationUser();
        }
        [TestMethod]
        public void BoardControllerEnsureIndexPageExists()
        {
            // Arrange
            BoardController controller = new BoardController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BoardControllerEnsureIndexViewExists()
        {
            // Arrange
            BoardController controller = new BoardController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void BoardControllerEnsureItHasThings()
        {
            // Arrange
            BoardController controller = new BoardController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            string expected_message = "My Boards";
            Assert.AreEqual(expected_message, result.ViewBag.Message);
        }

        [TestMethod]
        public void BoardControllerEnsureListOfBoards()
        {
            // Arrange
            List<Board> my_boards = new List<Board>
            {
                new Board {Title = "My Aweseome Board", BoardId = 1, Owner = owner },
                new Board {Title = "My Grocery Board", BoardId = 2, Owner = owner }

            };
            BoardController controller = new BoardController(mock_repository.Object);
            mock_repository.Setup(r => r.GetAllBoards()).Returns(my_boards);
           
            // Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            CollectionAssert.AreEqual(my_boards, result.ViewBag.Boards);
        }
    }
}
