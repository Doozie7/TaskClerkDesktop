using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;

namespace BritishMicro.TaskClerk.Providers.Sql
{
    [TestClass]
    public class SqlProviderUnitTests
    {

        /// <summary>
        /// Saves the activities.
        /// </summary>
        [TestMethod]
        public void SaveActivities()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            Collection<TaskActivity> activities = new Collection<TaskActivity>();
            for (int n = 0; n < 10; n++)
            {
                TaskActivity ta = new TaskActivity(TaskDescription.Empty, "test user");
                ta.StartDate = DateTime.Now.AddMinutes(-(n * rnd.Next(20)));
                ta.EndDate = DateTime.Now.AddMinutes(n * rnd.Next(20));
                ta.Remarks = "Windows 98, Windows Server 2000 SP4, Windows CE, Windows Millennium Edition, Windows Mobile for Pocket PC, Windows Mobile for Smartphone, Windows Server 2003, Windows XP Media Center Edition, Windows XP Professional x64 Edition, Windows XP SP2, Windows XP Starter Edition".Substring(0, rnd.Next(50));
                ta.CrosstabTaskDescription = TaskDescription.Empty;
                activities.Add(ta);
            }

            SqlTaskActivitiesProvider s = new SqlTaskActivitiesProvider();
            s.SaveActivities(activities);
            Console.WriteLine("10 records written to the Activities table.");
        }

        /// <summary>
        /// Loads the activities.
        /// </summary>
        [TestMethod]
        public void LoadActivities()
        {
            SqlTaskActivitiesProvider s = new SqlTaskActivitiesProvider();
            Collection<TaskActivity> activities = s.LoadActivities(DateTime.Today);
            Console.WriteLine("{0} records read from the Activities table.", activities.Count);
        }


    }
}
