using LegacyApp;
using NUnit.Framework;
using System;
 

namespace LegacyAppUnitTest
{
    [TestFixture]
    public class UserServiceTest
    {
        private UserService? _UserService;

        [SetUp]
        public void Setup()
        {
            _UserService = new UserService();
        }

        [Test]
        [TestCase("Apple")]
        public void AddUser_Pass(string name)
        {
            Assert.IsTrue(_UserService.AddUser(name, "Patrimonio", "alvin.patrimonio@example.com", new DateTime(1966, 11, 17), 4));
        }

        [Test]
        //[TestCase("anandgupta.abc.com")]
        //[TestCase("anandgupta@abc")]
        public void AddUser_EmailValidation_Fail(string EmailId)
        {

            _UserService = new UserService();
            EmailId = "anandgupta@abc";
            Assert.IsFalse(_UserService.AddUser("Apple", "Patrimonio", EmailId, new DateTime(1966, 11, 17), 4));
        }
    }
}