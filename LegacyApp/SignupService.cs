
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LegacyApp
{

    public class SignupService
    {
        public SignupServiceResult AddUser(string firstname, string surname, string email, string phone,
            DateTime dateOfBirth, int clientId, string notifications)
        {
            var result = new SignupServiceResult { IsSuccess = false, Notifications = new Dictionary<string, bool>() };
            var notificationTypes = notifications.Split('|');
            if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(surname))
                return result;
            if (!IsUserAdult(dateOfBirth))
                return result;
            if (!IsValidEmail(email))
                return result;

            var user = new User
            {
                Firstname = firstname,
                Surname = surname,
                EmailAddress = email,
                PhoneNumber = phone,
                DateOfBirth = dateOfBirth,
            };

            try
            {
                UserDataAccess.AddUser(user);
                result.IsSuccess = true;
                foreach (var notification in notificationTypes)
                {
                    switch (notification.ToLower())
                    {
                        case "email":
                            EmailService.Send(email);
                            result.Notifications.Add("email", true);
                            break;
                        case "sms":
                            if (string.IsNullOrEmpty(phone)) // Validation: Phone number must be provided for SMS
                            {
                                result.Notifications.Add("sms", false);
                                continue;
                            }

                            SmsService.Send(phone);
                            result.Notifications.Add("sms", true);
                            break;
                        case "push":
                            PushNotificationService
                                .Send(user.DeviceId); // Assuming a PushNotificationService is implemented
                            result.Notifications.Add("push", true);
                            break;
                        default:
                            throw new NotImplementedException($"Notification type '{notification}' not supported.");
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                // Handle exception (logging, etc.)
                return new SignupServiceResult { IsSuccess = false };
            }
        }
        
        public static bool IsUserAdult(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age >= 18;
        }

        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}
