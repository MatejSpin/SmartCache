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
    [StorageProvider(ProviderName = "BlobStore")]
    public class EmailsGrain : Grain<EmailsGrainState>, IEmailsGrain
    {
        bool stateChanged = false;
        public override Task OnActivateAsync()
        {
            RegisterTimer(WriteState, null, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5));
            return base.OnActivateAsync();
        }

        public Task<bool> AddEmailAsync(string email)
        {
            stateChanged = true;
            return Task.FromResult(State.BreachedEmails.Add(email));
        }

        public Task<bool> HasEmailAsync(string email)
        {
            string value;
            return Task.FromResult(State.BreachedEmails.TryGetValue(email, out value));
        }

        async Task WriteState(object _)
        {
            if (!stateChanged) return;
            stateChanged = false;
            await base.WriteStateAsync();
        }
    }
}
