//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 06th of Mar 2007
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using BritishMicro.TaskClerk.Providers.Sql.Properties;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;

namespace BritishMicro.TaskClerk.Providers.Sql
{
    /// <summary>
    /// Provides an Sql TaskDescription provider.
    /// </summary>
    public class SqlTaskDescriptionsProvider : TaskDescriptionsProvider
    {

        private string _connectionString;
        private const string loadDescriptionsStatement = "up_ReadDescriptions";
        private const string saveDescriptionStatement = "up_WriteDescription";
        private const string removeDescriptionStatement = "up_RemoveDescription";
        private const string archiveDescriptionsStatement = "up_ArchiveDescriptions";

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTaskDescriptionsProvider"/> class.
        /// </summary>
        public SqlTaskDescriptionsProvider()
        {
        }

        /// <summary>
        /// This method provides the implimenter of a TaskDescriptionsProvider the first access
        /// to the TaskClerk Engine, and should be where most of the providers setup should
        /// happen.
        /// </summary>
        protected override void OnInit()
        {
            base.OnInit();
            _connectionString = DiscoverConnectionStringSettings(this).ConnectionString;
        }

        /// <summary>
        /// When overridden in a provider, it loads and returns the descriptions.
        /// </summary>
        /// <returns></returns>
        protected override Collection<TaskDescription> ProviderLoadDescriptions()
        {
            Collection<TaskDescription> descriptions = new Collection<TaskDescription>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(loadDescriptionsStatement, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    command.Parameters.AddWithValue("@UserId", Engine.IdentityProvider.Principal.Identity.Name);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        FromDbReader(reader, descriptions);
                    }
                }
            }

            if (descriptions.Count == 0)
            {
                // use the default assembly resource
                using (StringReader sr = new StringReader(Resources.DefaultTaskDescriptions))
                {
                    descriptions =
                        (Collection<TaskDescription>)new XmlSerializer(typeof(Collection<TaskDescription>)).Deserialize(sr);
                }
                // save the descriptions to the sql strore
                ProviderSaveDescriptions(descriptions);
            }



            return descriptions;
        }

        /// <summary>
        /// When overridden in a provider, it saves the descriptions.
        /// </summary>
        /// <param name="descriptions">The descriptions.</param>
        protected override void ProviderSaveDescriptions(Collection<TaskDescription> descriptions)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                foreach (TaskDescription td in descriptions)
                {
                    SaveDescriptionHierarchy(connection, td, null);
                }
            }
        }

        /// <summary>
        /// Saves the description hierarchy.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="child">The child.</param>
        /// <param name="parent">The parent.</param>
        private void SaveDescriptionHierarchy(SqlConnection connection, TaskDescription child, TaskDescription parent)
        {
            SaveToDb(connection, child, parent);
            if (child.Children.Count > 0)
            {
                foreach (TaskDescription td in child.Children)
                {
                    SaveDescriptionHierarchy(connection, td, child);
                }
            }
        }

        /// <summary>
        /// When overridden in a provider, adds a description.
        /// </summary>
        /// <param name="taskDescription">The task description.</param>
        /// <param name="parent">The parent.</param>
        protected override void ProviderAddDescription(TaskDescription taskDescription, TaskDescription parent)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SaveToDb(connection, taskDescription, parent);
            }
        }

        /// <summary>
        /// When overridden in a provider, removes a description.
        /// </summary>
        /// <param name="taskDescription">The task description.</param>
        protected override void ProviderRemoveDescription(TaskDescription taskDescription)
        {
            if (taskDescription == null)
            {
                throw new ArgumentNullException("taskDescription");
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(removeDescriptionStatement, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    command.Parameters.AddWithValue("@UserId", Engine.IdentityProvider.Principal.Identity.Name);
                    command.Parameters.AddWithValue("@Id", taskDescription.Id);
                    command.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// When overridden in a provider, archives the descriptions.
        /// </summary>
        protected override void ProviderArchive()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(archiveDescriptionsStatement, connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    command.Parameters.AddWithValue("@UserId", Engine.IdentityProvider.Principal.Identity.Name);
                    command.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// Froms the db reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private void FromDbReader(SqlDataReader reader, Collection<TaskDescription> descriptions)
        {
            TaskDescription td = new TaskDescription
            {
                Id = reader.GetGuid(0),
                Name = reader.GetString(2)
            };
            if (!reader.IsDBNull(3))
            {
                td.Description = reader.GetString(3);
            }
            td.NoNagMinutes = reader.GetInt32(4);
            td.Colour = reader.GetString(5);
            td.Sequence = reader.GetInt32(6);
            td.CustomFlags = reader.GetInt32(7);
            td.IsPrivate = reader.GetBoolean(8);
            td.IsCategory = reader.GetBoolean(9);
            td.IsEvent = reader.GetBoolean(10);
            if (!reader.IsDBNull(11))
            {
                td.GroupName = reader.GetString(11);
            }
            td.MenuColumn = reader.GetInt32(12);
            if (!reader.IsDBNull(13))
            {
                td.Url = reader.GetString(13);
            }
            if (!reader.IsDBNull(14))
            {
                td.Server = reader.GetString(14);
            }
            if (!reader.IsDBNull(15))
            {
                td.ValidFromDate = reader.GetDateTime(15);
            }
            if (!reader.IsDBNull(16))
            {
                td.ValidToDate = reader.GetDateTime(16);
            }
            td.Enabled = reader.GetBoolean(17);
            if (!reader.IsDBNull(1))
            {
                Guid parentId = reader.GetGuid(1);
                TaskDescription parent = TaskDescriptionsProvider.FindChildInHierarchy(descriptions, parentId);
                if (parent != null)
                {
                    parent.Children.Add(td);
                }
            }
            else
            {
                descriptions.Add(td);
            }
        }

        /// <summary>
        /// Saves to db.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="description">The description.</param>
        /// <param name="parent">The parent.</param>
        private void SaveToDb(SqlConnection connection, TaskDescription description, TaskDescription parent)
        {
            using (SqlCommand command = new SqlCommand(saveDescriptionStatement, connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                command.Parameters.AddWithValue("@UserId", Engine.IdentityProvider.Principal.Identity.Name);
                command.Parameters.AddWithValue("@Id", description.Id);
                if (parent != null)
                {
                    command.Parameters.AddWithValue("@ParentId", parent.Id);
                }
                command.Parameters.AddWithValue("@Name", description.Name);
                if (description.Description != null)
                {
                    command.Parameters.AddWithValue("@Description", description.Description);
                }
                command.Parameters.AddWithValue("@NoNagMinutes", description.NoNagMinutes);
                command.Parameters.AddWithValue("@Colour", description.Colour);
                command.Parameters.AddWithValue("@Sequence", description.Sequence);
                command.Parameters.AddWithValue("@CustomFlags", description.CustomFlags);
                command.Parameters.AddWithValue("@IsPrivate", description.IsPrivate);
                command.Parameters.AddWithValue("@IsCategory", description.IsCategory);
                command.Parameters.AddWithValue("@IsEvent", description.IsEvent);
                if (description.GroupName != null)
                {
                    command.Parameters.AddWithValue("@GroupName", description.GroupName);
                }
                command.Parameters.AddWithValue("@MenuColumn", description.MenuColumn);
                command.Parameters.AddWithValue("@Url", description.Url);
                if (description.Server != null)
                {
                    command.Parameters.AddWithValue("@Server", description.Server);
                }
                if (description.ValidFromDate != DateTime.MinValue)
                {
                    command.Parameters.AddWithValue("@ValidFromDate", description.ValidFromDate);
                }
                if (description.ValidToDate != DateTime.MinValue)
                {
                    command.Parameters.AddWithValue("@ValidToDate", description.ValidToDate);
                }
                command.Parameters.AddWithValue("@Enabled", description.Enabled);
                command.ExecuteScalar();
            }
        }
    }
}
