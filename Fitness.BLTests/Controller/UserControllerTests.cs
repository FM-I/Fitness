using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fitness.BL.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.BL.Controller.Tests
{
    [TestClass()]
    public class UserControllerTests
    {
        [TestMethod()]
        public void SaveTest()
        {
            // Arrage
            var userName = Guid.NewGuid().ToString();

            // Act
            var controller = new UserController(userName);

            //Assert

            Assert.AreEqual(userName, controller.CurrentUser.Name);
            
        }

        [TestMethod()]
        public void SetNewUserDataTest()
        {
            // Arrage
            var userName = Guid.NewGuid().ToString();
            var birthdate = DateTime.Now.AddYears(-18);
            var weight = 10;
            var height = 231;
            var gendet = "male";
            var controller = new UserController(userName);

            // Act
            controller.SetNewUserData(gendet, birthdate, weight, height);
            var contoller2 = new UserController(userName);

            // Assert

            Assert.AreEqual(userName, contoller2.CurrentUser.Name);
            Assert.AreEqual(birthdate, contoller2.CurrentUser.BirthDate);

        }
    }
}