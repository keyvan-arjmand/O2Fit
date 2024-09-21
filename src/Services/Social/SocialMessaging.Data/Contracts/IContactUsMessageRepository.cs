using Data.Contracts;
using Social.Domain.Enum;
using SocialMessaging.Domain.Entities.ContactUs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialMessaging.Data.Contracts
{
   public interface IContactUsMessageRepository:IRepository<ContactUsMessage>
    {
        Task<ContactUsMessage> AddAsync(ContactUsMessage entity, CancellationToken cancellationToken, bool saveNow = true);
        Task<IEnumerable<ContactUsMessage>> GetByUserId(int userId,string LanguageName, CancellationToken cancellationToken);
        Task<IEnumerable<ContactUsMessage>> GetByClassification(int classification, CancellationToken cancellationToken);
        Task<ContactUsMessage> GetMarketMessage(string language,CancellationToken cancellationToken);
        Task<IEnumerable<ContactUsMessage>> GetAllMarketMessage( CancellationToken cancellationToken);
        Task<IEnumerable<ContactUsMessage>> GetAllGeneralMessage(int? Page, int PageSize, CancellationToken cancellationToken);
        Task<List<ContactUsMessage>> GetParentMessages(int? Page, int PageSize, CancellationToken cancellationToken);
        Task<IEnumerable<ContactUsMessage>> GetAsync(int Id, CancellationToken cancellationToken);
        Task<List<ContactUsMessage>> GetLastGeneralMessages(int lastMessageId, string language, CancellationToken cancellationToken);

    }
}
