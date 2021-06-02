using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LegacyApp
{

    /*
     *	DO NOT CHANGE THIS FILE AT ALL
    */

    public static class UserDataAccess
    {

        /*
         *	DO NOT CHANGE THIS FILE AT ALL
         *	CONCIDER THIS FILE THE DATABASE FX ENTITY FRAMWORK
        */
        public static void AddUser(User user)
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

            //using (var connection = new SqlConnection(connectionString))
            //{
            //    var command = new SqlCommand
            //    {
            //        Connection = connection,
            //        CommandType = CommandType.StoredProcedure,
            //        CommandText = "uspAddUser"
            //    };

            //    var firstNameParameter = new SqlParameter("@Firstname", SqlDbType.VarChar, 50) { Value = user.Firstname };
            //    command.Parameters.Add(firstNameParameter);
            //    var surnameParameter = new SqlParameter("@Surname", SqlDbType.VarChar, 50) { Value = user.Surname };
            //    command.Parameters.Add(surnameParameter);
            //    var dateOfBirthParameter = new SqlParameter("@DateOfBirth", SqlDbType.DateTime) { Value = user.DateOfBirth };
            //    command.Parameters.Add(dateOfBirthParameter);
            //    var emailAddressParameter = new SqlParameter("@EmailAddress", SqlDbType.VarChar, 50) { Value = user.EmailAddress };
            //    command.Parameters.Add(emailAddressParameter);
            //    command.Parameters.Add(creditLimitParameter);
            //    var clientNotificationsParameter = new SqlParameter("@Notifications", SqlDbType.VarChar, 250) { Value = user.Notifications };
            //    command.Parameters.Add(clientNotificationsParameter);
            //    connection.Open();
            //    command.ExecuteNonQuery();
            //}

            if (user.Firstname.Length > 50)
                throw new DataException("Firstname to long");

            if (user.Surname.Length > 50)
                throw new DataException("Surname to long");

            if (user.EmailAddress.Length > 50)
                throw new DataException("EmailAddress to long");

            if (user.Notifications.Length > 250)
                throw new DataException("EmailAddress to long");


        }
    }
}
