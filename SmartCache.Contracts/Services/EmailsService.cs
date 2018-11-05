using System;
using System.Threading.Tasks;

namespace SmartCache.Contracts.Services
{
    public interface IEmailsService
    {
        Task<bool> AddEmailAsync(string email);
        Task<bool> HasEmailAsync(string email);
    }
}
