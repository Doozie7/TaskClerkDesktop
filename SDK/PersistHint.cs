//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 10th of Mar 2007
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------
namespace BritishMicro.TaskClerk
{
    /// <summary>
    /// Provides persistance hints for saving user session during the 
    /// normal execution of tha application.
    /// </summary>
    public enum PersistHint
    {
        /// <summary>
        /// No persist hint
        /// </summary>
        None = 0,
        /// <summary>
        /// Only persist in this session
        /// </summary>
        ThisSession = 1,
        /// <summary>
        /// Persist across sessions
        /// </summary>
        AcrossSessions = 2
    }
}