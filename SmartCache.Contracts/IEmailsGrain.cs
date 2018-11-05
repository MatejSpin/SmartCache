using Orleans;
using System;
using System.Threading.Tasks;

namespace SmartCache.Contracts
{
    public interface IEmailsGrain : IGrainWithStringKey
    {
        Task<bool> AddEmailAsync(string email);
        Task<bool> HasEmailAsync(string email);
    }
}
