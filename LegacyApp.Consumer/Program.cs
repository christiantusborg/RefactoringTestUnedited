using System;

namespace LegacyApp.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            ProveAddUser(args);
        }

        public static void ProveAddUser(string[] args)
        {
            /*
			 *	DO NOT CHANGE THIS FILE AT ALL
        	*/

            var userService = new SignupService();
            var addJohn = userService.AddUser("John", "Doe", "john.doe@nowhere.na", "12345678", new DateTime(1993, 1, 1), 4, "Email|Sms");
            Console.WriteLine("Adding John Doe was " + (addJohn.IsSuccess ? $"successful" : "unsuccessful"));
            if (addJohn.IsSuccess)
            {
                foreach (var next in addJohn.Notifications)
                {
                    Console.WriteLine(" -> " + next.Key);
                }
            }

            var addJane = userService.AddUser("Jane", "Doe", "jane.doe@nowhere.na", "23456781", new DateTime(1992, 4, 3), 5, "Sms|Email");
            Console.WriteLine("Adding Jane Doe was " + (addJane.IsSuccess ? "successful" : "unsuccessful"));
            if (addJane.IsSuccess)
            {
                foreach (var next in addJane.Notifications)
                {
                    Console.WriteLine(" -> " + next.Key);
                }
            }

            var addOle = userService.AddUser("Ole", "Doe", "ole.doe@nowhere.na", "34567812", new DateTime(1991, 7, 12), 6, "Email");
            Console.WriteLine("Adding Ole Doe was " + (addOle.IsSuccess ? "successful" : "unsuccessful"));
            if (addOle.IsSuccess)
            {
                foreach (var next in addOle.Notifications)
                {
                    Console.WriteLine(" -> " + next.Key);
                }
            }

            var addPeter = userService.AddUser("Peter", "Doe", "peter.doe@nowhere.na", "45678123", new DateTime(1990, 3, 9), 7, "Sms");
            Console.WriteLine("Adding Peter Doe was " + (addPeter.IsSuccess ? "successful" : "unsuccessful"));
            if (addPeter.IsSuccess)
            {
                foreach (var next in addPeter.Notifications)
                {
                    Console.WriteLine(" -> " + next.Key);
                }
            }

            //This throw an error!!!! due Push is not implemented!
            /*
			 *	DO NOT CHANGE THIS FILE AT ALL
        	*/
            var addMaria = userService.AddUser("Maria", "Doe", "maria.doe@nowhere.na", "45678123", new DateTime(1990, 3, 9), 7, "Push");
            Console.WriteLine("Adding Maria Doe was " + (addMaria.IsSuccess ? "successful" : "unsuccessful"));
            if (addMaria.IsSuccess)
            {
                foreach (var next in addMaria.Notifications)
                {
                    Console.WriteLine(" -> " + next.Key);
                }
            }

            var addMike = userService.AddUser("Mike", "Doe", "Mike.doe@nowhere.na", "45678123", new DateTime(1990, 3, 9), 8, "BottleMail");
            Console.WriteLine("Adding Mike Doe was " + (addMike.IsSuccess ? "successful" : "unsuccessful"));
            if (addMike.IsSuccess)
            {
                foreach (var next in addMike.Notifications)
                {
                    Console.WriteLine(" -> " + next.Key);
                }
            }
        }
    }
}
