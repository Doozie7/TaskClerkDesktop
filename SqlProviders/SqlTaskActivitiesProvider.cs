//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 06th of Mar 2007
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using System.Data.SqlClient;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System;
namespace BritishMicro.TaskClerk.Providers.Sql
{
    /// <summary>
    /// This TaskActivity provider uses a SQL database as its store.
    /// </summary>
    public class SqlTaskActivitiesProvider : TaskActivitiesProvider
    {

        private static string discoverDateMetricsStatement = "up_DiscoverActivities";
        private static string saveActivityStatement = "up_WriteActivity";
        private static string loadActivitiesStatement = "up_ReadActivities";
        private static string removeActivityStatement = "up_DeleteActivity";
        private static string archiveActivitiesStatement = "up_ArchiveActivities";

        private string _connectionString;
        private string _machineName;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTaskActivitiesProvider"/> class.
        /// </summary>
        public SqlTaskActivitiesProvider()
        {
            _machineName = Environment.MachineName;
        }

        /// <summary>
        /// The OnInit method provides implimenters with an opertunity to
        /// setup their providers after a valid taskclerk engine has been asigned to
        /// the Engine property.
        /// </summary>
        protected override void OnInit()
        {
            base.OnInit();
            _connectionString = DiscoverConnectionStringSettings(this).ConnectionString;
        }

        /// <summary>
        /// When overridden in a provider, it discovers the date metrics stored in the users data area.
        /// </summary>
        /// <param name="question">The question.</param>
        /// <returns></returns>
        protected override Collection<System.DateTime> ProviderDiscoverDateMetrics(TaskActivitiesProvider.MetricQuestion question)
        {
            Collection<System.DateTime> dates = new Collection<System.DateTime>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(discoverDateMetricsStatement, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", Engine.IdentityProvider.Principal.Identity.Name);
                command.Parameters.AddWithValue("@Question", question.ToString());
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DateTime date = reader.GetDateTime(0);
                    dates.Add(date);
                }
            }
            return dates;
        }

        /// <summary>
        /// When overridden in a provider, it loads and returns the activities based on the supplied parameters.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="allowedTaskDescriptions">The allowed task descriptions.</param>
        /// <returns></returns>
        protected override Collection<TaskActivity> ProviderLoadActivities(System.DateTime start, System.DateTime end, System.Collections.ObjectModel.Collection<TaskDescription> allowedTaskDescriptions)
        {
            Collection<TaskActivity> activities = new Collection<TaskActivity>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(loadActivitiesStatement, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", Engine.IdentityProvider.Principal.Identity.Name);
                command.Parameters.AddWithValue("@StartDate", start);
                command.Parameters.AddWithValue("@EndDate", end);
                
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    activities.Add(ReadFromDatabase(reader));
                }
            }
            return activities;
        }

        /// <summary>
        /// When overridden in a provider, it saves the activities.
        /// </summary>
        /// <param name="activities">The activities.</param>
        protected override void ProviderSaveActivities(System.Collections.ObjectModel.Collection<TaskActivity> activities)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                foreach (TaskActivity ta in activities)
                {
                    WriteToDatabase(connection, ta);
                }
            }
        }

        /// <summary>
        /// When overridden in a provider, it adds an activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        protected override void ProviderBeginActivity(TaskActivity activity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                WriteToDatabase(connection, activity);
            }
        }

        /// <summary>
        /// When overridden in a provider, it completes an activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        protected override void ProviderCompleteActivity(TaskActivity activity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                WriteToDatabase(connection, activity);
            }
        }

        /// <summary>
        /// When overridden in a provider, it removes an activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        protected override void ProviderRemoveActivity(TaskActivity activity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(removeActivityStatement, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", activity.Id);
                command.Parameters.AddWithValue("@UserId", activity.UserId);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// When overridden in a provider, it performs an archive action on the activity data.
        /// </summary>
        protected override void ProviderArchive()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(archiveActivitiesStatement, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", Engine.IdentityProvider.Principal.Identity.Name);
                command.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Saves the activity to the SqlConnection
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="activity">The activity.</param>
        private void WriteToDatabase(SqlConnection connection, TaskActivity activity)
        {
            SqlCommand command = new SqlCommand(saveActivityStatement, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Id", activity.Id);
            command.Parameters.AddWithValue("@DescriptionId", activity.TaskDescription.Id);
            command.Parameters.AddWithValue("@Remarks", activity.Remarks);
            command.Parameters.AddWithValue("@StartDate", activity.StartDate);
            if (activity.EndDate != DateTime.MinValue)
            {
                command.Parameters.AddWithValue("@EndDate", activity.EndDate);
            }
            command.Parameters.AddWithValue("@Duration", activity.Duration);
            if (activity.CrosstabTaskDescription != null)
            {
                command.Parameters.AddWithValue("@CrossTabDescriptionId", activity.CrosstabTaskDescription.Id);
            }
            command.Parameters.AddWithValue("@CustomFlags", activity.CustomFlags);
            command.Parameters.AddWithValue("@UserId", activity.UserId);
            command.Parameters.AddWithValue("@SourceMachine", _machineName);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Froms the db reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private TaskActivity ReadFromDatabase(SqlDataReader reader)
        {
            //Id DescriptionId Remarks StartDate EndDate Duration CrossTabDescriptionId CustomFlags UserId SourceMachine OriginalDate
            TaskActivity ta = new TaskActivity();
            ta.Id = reader.GetGuid(0);
            Debug.Assert(ta.Id != null, "The TaskActivity Guid cannot be null.");

            ta.TaskDescription
                = Engine.TaskDescriptionsProvider.FindDescription(reader.GetGuid(1));

            if (!reader.IsDBNull(2))
            {
                ta.Remarks = reader.GetString(2);
            }
            ta.StartDate = reader.GetDateTime(3);
            if (!reader.IsDBNull(4))
            {
                ta.EndDate = reader.GetDateTime(4);
            }
            if (!reader.IsDBNull(6))
            {
                ta.CrosstabTaskDescription
                    = Engine.TaskDescriptionsProvider.FindDescription(reader.GetGuid(6));
            }
            ta.CustomFlags = reader.GetInt32(7);
            ta.UserId = reader.GetString(8);
            ta.OriginalDate = reader.GetDateTime(9);
            return ta;
        }
    }
}