using Orleans;
using Orleans.Providers;
using SmartCache.Contracts;
using SmartCache.Grains.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SmartCache.Grains
{
    /// <summary>
    /// Emails grain that enables adding emails and checking if email exists
    /// </summary>
    [StorageProvider(ProviderName = "BlobStore")]
    public class EmailsGrain : Grain<EmailsGrainState>, IEmailsGrain
    {
        bool stateChanged = false;

        /// <summary>
        /// Activates the grain and starts a timer for writing the state to persistante storage
        /// </summary>
        /// <returns></returns>
        public override Task OnActivateAsync()
        {
            RegisterTimer(WriteState, null, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5));
            return base.OnActivateAsync();
        }

        /// <summary>
        /// Adds email to the grain
        /// </summary>
        /// <param name="email">email address</param>
        /// <returns>true if email was added, false if email exists in the list and was not added</returns>
        public Task<bool> AddEmailAsync(string email)
        {
            stateChanged = true;
            return Task.FromResult(State.BreachedEmails.Add(email));
        }

        /// <summary>
        /// Checks if email exists in the grain
        /// </summary>
        /// <param name="email">email address</param>
        /// <returns>true if email exists, otherwise false</returns>
        public Task<bool> HasEmailAsync(string email)
        {
            string value;
            return Task.FromResult(State.BreachedEmails.TryGetValue(email, out value));
        }

        /// <summary>
        /// Writes grain state to given storage provider
        /// </summary>
        /// <param name="_"></param>
        /// <returns>written state</returns>
        async Task WriteState(object _)
        {
            if (!stateChanged) return;
            stateChanged = false;
            await base.WriteStateAsync();
        }
    }
}
