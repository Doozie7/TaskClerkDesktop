//----------------------------------------------------------------------
//  Copyright (c) BritishMicro
//
//  Date  : 14th of June 2006
//	Author: John Powell (john.powell@britishmicro.com)
//  Author: Paul Jackson (paul@compilewith.net) 24th May 2007
//----------------------------------------------------------------------

using System;
using System.Security.Principal;

namespace BritishMicro.TaskClerk.Providers
{
    /// <summary>
    /// The Identity provider provides extensibility points for customizers
    /// to provide their own identity mechanisms
    /// </summary>
    public abstract class IdentityProvider : TaskClerkProvider
    {

        /// <summary>
        /// When overridden in a provider, this method discovers the users identity.
        /// </summary>
        protected abstract void ProviderDiscoverIdentity();

        private IPrincipal _principal;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityProvider"/> class.
        /// </summary>
        protected IdentityProvider()
        {
            this.ProviderName = this.GetType().Name;
        }
                
        /// <summary>
        /// This method is called as soon as a valid Engine is added to the service. 
        /// The purpose of the IdentityProvider is too establish identity
        /// and set the Principal.
        /// </summary>
        protected override void OnInit()
        {
            base.OnInit();
        }

        /// <summary>
        /// Discovers the users identity. Only called by the TaskClerk engine.
        /// </summary>
        internal void DiscoverIdentity()
        {
            ProviderDiscoverIdentity();
            OnDiscoveredIdentity(EventArgs.Empty);
        }

        /// <summary>
        /// Gets or sets the principal.
        /// </summary>
        /// <value>The principal.</value>
        public IPrincipal Principal
        {
            get
            {
                return this._principal;
            }
            protected set
            {
                this._principal = value;
            }
        }

        /// <summary>
        /// Gets the name from the configured Identity.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                if (this._principal != null 
                    && this._principal.Identity != null 
                    && !string.IsNullOrEmpty(this._principal.Identity.Name))
                {
                    return this._principal.Identity.Name;
                }
                return null;
            }
        }

        /// <summary>
        /// Determines whether the configured principal belongs to the specified role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>
        /// 	<c>true</c> if [is in role] [the specified role]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInRole(string role)
        {
            if (this._principal != null)
            {
                return this._principal.IsInRole(role);
            }
            return false;
        }

        #region Discovered Identity Event

        /// <summary>
        /// This event is fired when the provider descovers the users identity
        /// </summary>
        public event EventHandler<EventArgs> DiscoveredIdentity;

        /// <summary>
        /// Raises the <see cref="E:DiscoveredIdentity"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnDiscoveredIdentity(EventArgs e)
        {
            EventHandler<EventArgs> handler = DiscoveredIdentity;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion
   }
}