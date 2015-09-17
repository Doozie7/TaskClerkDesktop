//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 06th of Mar 2007
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using System;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Configuration;

namespace BritishMicro.TaskClerk.Providers.Sql
{
    public class SqlUserMemoryProvider : UserMemoryProvider
    {
        private ConnectionStringSettings _connectionSettings;
        private static string loadAllStatement
            = @"select SettingKey, SettingValue from tc_UserMemory where UserId = @userId";

        private static string writeStatement = @"up_WriteUserMemory";

        /// <summary>
        /// The OnInit method provides implimenters with an opertunity to
        /// setup their providers after a valid taskclerk engine has been asigned to
        /// the Engine property.
        /// </summary>
        protected override void OnInit()
        {
            base.OnInit();
            _connectionSettings = DiscoverConnectionStringSettings(this);
            LoadFromDatabase();
        }

        /// <summary>
        /// Loads from database.
        /// </summary>
        private void LoadFromDatabase()
        {
            using (SqlConnection connection = new SqlConnection(_connectionSettings.ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(
                    loadAllStatement, connection);
                command.Parameters.AddWithValue("@userId", Engine.IdentityProvider.Principal.Identity.Name);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    try
                    {
                        //  ...we need to help the Assembly resolution process, we have assemlies
                        //  in a "Plugins" folder, this folder is not a "known path" and therefore
                        //  fails to load the assembly
                        AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

                        while (reader.Read())
                        {
                            UsersSessionMemory.Add(
                                reader[0],
                                DeserializeObject((byte[])reader[1]));
                        }
                    }
                    finally
                    {
                        //  ...ensure that we unwire from the event once the deserialize process is complete
                        AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the AssemblyResolve event of the CurrentDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="System.ResolveEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return AssemblyResolve(args);
        }

        /// <summary>
        /// Allows a set on settings
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="hint">The persistance hint.</param>
        public override void Set(object key, object value, PersistHint hint)
        {
            base.Set(key, value, hint);
            if (hint == PersistHint.AcrossSessions)
            {
                byte[] buffer = SerializeObject(value);
                using (SqlConnection connection = new SqlConnection(_connectionSettings.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(writeStatement, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SettingKey", key);
                    command.Parameters.AddWithValue("@UserId", Engine.IdentityProvider.Principal.Identity.Name);
                    command.Parameters.AddWithValue("@SettingValue", buffer);                    
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Method to convert a custom Object to XML string
        /// </summary>
        /// <param name="pObject">Object that is to be serialized to XML</param>
        /// <returns>XML string</returns>
        private static byte[] SerializeObject(Object source)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(memoryStream, source);
            return memoryStream.ToArray();
        }

        /// <summary>
        /// Method to reconstruct an Object from XML string
        /// </summary>
        /// <param name="pXmlizedString"></param>
        /// <returns></returns>
        private static Object DeserializeObject(byte[] source)
        {
            MemoryStream memoryStream = new MemoryStream(source);
            memoryStream.Position = 0;
            BinaryFormatter bf = new BinaryFormatter();
            return bf.Deserialize(memoryStream);
        }
    }
}
