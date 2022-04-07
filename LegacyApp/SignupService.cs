using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LegacyApp
{
    public class SignupService
    {
        private const string EmailText = "Welcome to e-Boks";
        private const string SmsText = "Welcome to e-Boks";

        public SignupServiceResult AddUser(string firstname, string surname, string email, string phone,
            DateTime dateOfBirth, int clientId, string notifications)
        {
            var errorResponse = new SignupServiceResult
            {
                IsSuccess = false,
                Notifications = null
            };

            var user = new User
            {
                Id = clientId,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = firstname,
                Surname = surname,
                Notifications = notifications,
                Phone = phone
            };

            if (!IsValid(user))
                return errorResponse;

            var listOfNotifications = notifications.Split("|").Where(x => !string.IsNullOrWhiteSpace(x)).Select(Enum.Parse<NotificationType>).ToList();

            if (listOfNotifications.Contains(NotificationType.BottleMail))
            {
                return errorResponse;
            }

            try
            {
                UserDataAccess.AddUser(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return errorResponse;
            }

            var result = listOfNotifications.ToDictionary(notification => ((NotificationType) notification).ToString(), notification => notification != NotificationType.BottleMail);

            if (result.Any(x => x.Key == NotificationType.Sms.ToString()))
            {
                var smsService = new SmsService();
                smsService.Send(SmsText);
            }

            if (result.Any(x => x.Key == NotificationType.Email.ToString()))
            {
                EmailService.Send(EmailText);
            }

            return new SignupServiceResult
            {
                IsSuccess = true,
                Notifications = result
            };
            
        }

        private bool IsValid(User user)
        {
            if (string.IsNullOrEmpty(user.Firstname))
                return false;

            if (string.IsNullOrEmpty(user.Surname))
                return false;

            return !string.IsNullOrEmpty(user.Phone) && IsEmailValid(user.EmailAddress) && IsDateOfBirthValid(user.DateOfBirth);
        }

        bool IsEmailValid(string email)
        {
            try
            {
                return new System.Net.Mail.MailAddress(email).Address == email.Trim();
            }
            catch
            {
                return false;
            }
        }

        bool IsDateOfBirthValid(DateTime dateOfBirth)
        {
            var difference = DateTime.Now.Subtract(dateOfBirth);
            return !(difference.TotalDays / 365.25 < 18);
        }
    }
}