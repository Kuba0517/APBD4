using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!CheckNameOrLastName(firstName) || !CheckNameOrLastName(lastName))
            {
                return false;
            }
            
            if (!CheckEmail(email))
            {
                return false;
            }

            int age = CalculateAge(dateOfBirth);
            if (age < 21)
            {
                return false;
            }
            
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = CreateNewUser(client, dateOfBirth, email, firstName, lastName);

            AssignCreditLimit(user);

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }
        
        private int CalculateAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            return age;
        }

        private bool CheckEmail(string email)
        {
            if (email.Contains("@") && email.Contains("."))
            {
                return true;
            }

            return false;
        }

        private bool CheckNameOrLastName(string name)
        {
            return !string.IsNullOrEmpty(name);
        }

        private User CreateNewUser(Client client, DateTime dateOfBirth, string emailAddress, string firstName,
            string lastName)
        {
            return new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = emailAddress,
                FirstName = firstName,
                LastName = lastName
            };
        }
        
        private void AssignCreditLimit(User user)
        {
            var client = user.Client;
            switch (((Client)client).Type)
            {
                case "VeryImportantClient":
                    user.HasCreditLimit = false;
                    break;
                case "ImportantClient":
                    user.HasCreditLimit = true;
                    using (var userCreditService = new UserCreditService())
                    {
                        int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                        creditLimit = creditLimit * 2;
                        user.CreditLimit = creditLimit;
                    }
                    break;
                default:
                    user.HasCreditLimit = true;
                    using (var userCreditService = new UserCreditService())
                    {
                        int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                        user.CreditLimit = creditLimit;
                    }
                    break;
            }
        }
    }
}
