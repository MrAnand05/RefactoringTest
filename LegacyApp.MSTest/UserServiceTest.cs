using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LegacyApp.MSTest
{
    [TestClass]
    public class UserServiceTest
    {

        private UserService _UserService;
        [TestMethod]

        public void AddUserTest_Fail()
        {
            _UserService = new UserService();
            Assert.IsFalse(_UserService.AddUser("Apple", "Patrimonio", "anand.gupta.com", new DateTime(1966, 11, 17), 4));

        }
    }
}