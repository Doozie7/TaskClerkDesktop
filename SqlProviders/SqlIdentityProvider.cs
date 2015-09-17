using System;
using System.Collections.Generic;
using System.Text;
using BritishMicro.TaskClerk.Providers;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Diagnostics;
using System.Configuration;

namespace BritishMicro.TaskClerk.Providers.Sql
{
    /// <summary>
    /// Represents an indentity provider that works against an SQL Database.
    /// </summary>
    public class SqlIdentityProvider : IdentityProvider
    {
        private static string discoverIdentity = @"up_DiscoverIdentity";
        private static string changePassword = @"up_ChangePassword";
        private string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlIdentityProvider"/> class.
        /// </summary>
        public SqlIdentityProvider()
        {
        }

        /// <summary>
        /// When overridden in a provider, it discovers the users identity.
        /// </summary>
        protected override void ProviderDiscoverIdentity()
        {
            this._connectionString = DiscoverConnectionStringSettings(this).ConnectionString;
            using (SqlLogin loginForm = new SqlLogin(this))
            {
                loginForm.ShowDialog();
            }
        }

        /// <summary>
        /// Attempts to login to the configured sql database.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        internal string AttemptLogin(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(
                    discoverIdentity, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserName", username);
                command.Parameters.AddWithValue("@Password", password);

                this.Principal = new GenericPrincipal
                    (new GenericIdentity(string.Empty, this.ProviderName), null);

                Debug.Assert(this.Principal.Identity.IsAuthenticated == false, 
                    "User should not be authenticated at this point _name.Length==0");

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.Principal = new GenericPrincipal
                                (new GenericIdentity(reader[0].ToString(), this.ProviderName), null);

                            Debug.Assert(this.Principal.Identity.IsAuthenticated == true,
                                "User should be authenticated at this point _name.Length!=0");
                        }
                    }
                }
                catch (SqlException se)
                {
                    // return the error to the caller
                    return se.Message;
                }
            }
            return string.Empty;
        }

        internal string ChangePassword(string username, string oldpassword, string newpassword)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(
                        changePassword, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", username);
                    command.Parameters.AddWithValue("@OldPassword", oldpassword);
                    command.Parameters.AddWithValue("@NewPassword", newpassword);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException se)
            {
                // return the error to the caller
                return se.Message;
            }

            return AttemptLogin(username, newpassword);
        }
    }
}