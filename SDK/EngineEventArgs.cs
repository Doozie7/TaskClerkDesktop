//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 01st of Jan 2006
//	Author: John Powell (john.powell@britishmicro.com)
//----------------------------------------------------------------------

using System;

namespace BritishMicro.TaskClerk
{
    /// <summary>
    /// Provides EngineContext based event data.
    /// </summary>
    public class EngineEventArgs : EventArgs
    {
        private readonly TaskClerkEngine _context;

        private EngineEventArgs()
        {
        }

        /// <summary>
        /// Initalises a new instance of the eventargs with the 
        /// application engine context as a parameter.
        /// </summary>
        /// <param name="context"></param>
        public EngineEventArgs(TaskClerkEngine context)
        {
            _context = context;
        }

        /// <summary>
        /// The application engin context, this provides access to all the 
        /// providers in the application.
        /// </summary>
        public TaskClerkEngine Context
        {
            get { return _context; }
        }
    }
}