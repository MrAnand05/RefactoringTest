using System;

namespace LegacyApp
{
	public class UserService
    {
		public bool AddUser(string firname, string surname, string email, DateTime dateOfBirth, int clientId)
		{
			var user = new User
			{
				DateOfBirth = dateOfBirth,
				EmailAddress = email,
				Firstname = firname,
				Surname = surname
			};
			try
            {
				if (!ValidateUser(user))
					return false;

				var clientRepository = new ClientRepository();
				var client = clientRepository.GetById(clientId);
				user.Client = client;

				user.HasCreditLimit = false;
				if (client.Name != "VeryImportantClient")
				{
					user.HasCreditLimit = true;
					int creditLimit;
					using (var userCreditService = new UserCreditServiceClient())
					{
						creditLimit = userCreditService.GetCreditLimit(user.Firstname, user.Surname, user.DateOfBirth);
					}
					if (client.Name == "ImportantClient")
						user.CreditLimit = creditLimit * 2;
					else
						user.CreditLimit = creditLimit;
				}

				if (user.HasCreditLimit && user.CreditLimit < 500)
				{
					return false;
				}

				UserDataAccess.AddUser(user);
				return true;
			}
			catch (Exception ex)
            {
				throw new Exception(ex.Message);
            }
			
		}

		private bool ValidateUser(User _user)
        {
			var now = DateTime.Now;
			int age = now.Year - _user.DateOfBirth.Year;
			if (now.Month < _user.DateOfBirth.Month || (now.Month == _user.DateOfBirth.Month && now.Day < _user.DateOfBirth.Day)) 
				age--;

			if (string.IsNullOrEmpty(_user.Firstname) || string.IsNullOrEmpty(_user.Surname) || !_user.EmailAddress.Contains("@") || !_user.EmailAddress.Contains(".") || age < 21)
			{
				return false;
			}
			return true;        
		}
	}
}
