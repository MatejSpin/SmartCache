using Orleans;
using SmartCache.Contracts.Grains;
using SmartCache.Contracts.Services;
using SmartCache.Service.Models.Exceptions;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SmartCache.Service
{
    public class EmailsService : IEmailsService
    {
        IClusterClient orleansClient;
        public EmailsService(IClusterClient orleansClient)
        {
            this.orleansClient = orleansClient;
        }
        public async Task<bool> AddEmailAsync(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                var result = this.orleansClient.GetGrain<IEmailsGrain>(mailAddress.Host);
                return await result.AddEmailAsync(email);
            }
            catch (FormatException ex)
            {
                //log error    
                throw new EmailsServiceException("Invalid email format.");
            }
            catch (Exception ex)
            {
                //log error
                throw new EmailsServiceException(ex.Message);
            }
        }

        public async Task<bool> HasEmailAsync(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                var result = this.orleansClient.GetGrain<IEmailsGrain>(mailAddress.Host);

                return await result.HasEmailAsync(email);
            }
            catch (FormatException ex)
            {
                //log error
                throw new EmailsServiceException("Invalid email format.");
            }
            catch (Exception ex)
            {
                //log error
                throw new EmailsServiceException(ex.Message);
            }
        }
    }
}
