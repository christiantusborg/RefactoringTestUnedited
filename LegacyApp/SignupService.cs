using System;
using System.Collections.Generic;
using System.Linq;

namespace LegacyApp
{
    public class SignupService
    {
        public SignupServiceResult AddUser(string firname, string surname, string email, string phone,
            DateTime dateOfBirth, int clientId, string notifications)
        {
            if (string.IsNullOrEmpty(firname) || string.IsNullOrEmpty(surname))
                return new SignupServiceResult
                {
                    IsSuccess = false,
                    Notifications = null
                };

            if (string.IsNullOrEmpty(phone))
                return new SignupServiceResult
                {
                    IsSuccess = false,
                    Notifications = null
                };

            if (!email.Contains("@") && !email.Contains("."))
                return new SignupServiceResult
                {
                    IsSuccess = false,
                    Notifications = null
                };

            var now = DateTime.Now;
            var age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day) age--;

            if (age < 18)
                return new SignupServiceResult
                {
                    IsSuccess = false,
                    Notifications = null
                };

            var listOfnotifications = notifications.Split("|").Where(x => !string.IsNullOrWhiteSpace(x)).ToList(); ;
            ;

            var user = new User
            {
                Id = clientId,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = firname,
                Surname = surname,
                Notifications = notifications
            };

            var result = new Dictionary<string, bool>();
            if (listOfnotifications.Contains("Sms"))
            {
                // allowed to send Sms
                var smsService = new SmsService();
                if (smsService.Send("Welcome to e-Boks"))
                    result.Add("Sms", true);
            }

            if (listOfnotifications.Contains("Email"))
                // allowed to send Email
                if (EmailService.Send("Welcome to e-Boks"))
                    result.Add("Email", true);

            if (result.ContainsKey("Sms")
                && result.ContainsKey("Email") && result.Count() == 2 ||
                result.ContainsKey("Sms") ||
                result.ContainsKey("Email") && result.Count() == 1
            )
            {
                UserDataAccess.AddUser(user);
                return new SignupServiceResult
                {
                    IsSuccess = true,
                    Notifications = result
                };
            }

            throw new NotImplementedException("Service not found!");
            {
            }

            return new SignupServiceResult
            {
                IsSuccess = true,
                Notifications = result
            };
            ;
        }
    }
}