//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 14th of June 2006
//	Author: John Powell  (john.powell@britishmicro.com)
//  Author: Paul Jackson (paul@compilewith.net) 24th May 2007
//----------------------------------------------------------------------

using System;
using System.Security.Principal;
using System.Threading;

namespace BritishMicro.TaskClerk.Providers
{
    /// <summary>
    /// Provides Identity from the logged on NT user.
    /// </summary>
    public class NTIdentityProvider : IdentityProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NTIdentityProvider"/> class.
        /// </summary>
        public NTIdentityProvider()
        {
        }

        /// <summary>
        /// When overridden in a provider, it discovers the users identity.
        /// </summary>
        protected override void ProviderDiscoverIdentity()
        {
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            this.Principal = Thread.CurrentPrincipal;
        }
    }
}