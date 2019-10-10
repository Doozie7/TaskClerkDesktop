using System;

namespace BritishMicro.TaskClerk.Providers.Sql
{
    internal class SqlAssistance
    {
        internal static string[] GetInfoFromRaisError(string error)
        {
            return error.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
