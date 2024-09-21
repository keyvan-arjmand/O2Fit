using Common;
using Common.Utilities;
using Data.Database;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using SocialMessaging.Data.Contracts;
using SocialMessaging.Domain.Entities.ContactUs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SocialMessaging.Data.Repositories
{
    public class ContactUsMessageRepository : Repository<ContactUsMessage>, IContactUsMessageRepository, IScopedDependency
    {
        public ContactUsMessageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public virtual async Task<ContactUsMessage> AddAsync(ContactUsMessage entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return entity;
        }

        public async Task<IEnumerable<ContactUsMessage>> GetByClassification(int classification, CancellationToken cancellationToken)
        {
            var messageList = await TableNoTracking.Where(m => (int)m.Classification == classification).ToListAsync(cancellationToken);
            return messageList;
        }

        public async Task<IEnumerable<ContactUsMessage>> GetByUserId(int userId, string Language, CancellationToken cancellationToken)
        {
            // var messageList = await TableNoTracking.Where(m => m.UserId == userId || (m.IsGeneral == true && m.IsForce==false && m.Language == Language)).OrderBy(m=>m.Id).ToListAsync(cancellationToken);
            var messageList = await TableNoTracking.Where(m => m.UserId == userId && m.UserId != 0).OrderBy(m => m.Id).ToListAsync(cancellationToken);
            return messageList;
        }

        public async Task<IEnumerable<ContactUsMessage>> GetAsync(int Id, CancellationToken cancellationToken)
        {
            var mess = await TableNoTracking.Where(m => m.Id == Id || m.ReplyToMessage == Id).ToListAsync(cancellationToken);
            return mess;
        }

        public async Task<ContactUsMessage> GetMarketMessage(string language, CancellationToken cancellationToken)
        {
            if (language == null) { language = "English"; }
            var _marketMessage = await TableNoTracking.Where(m => m.IsForce == true && m.IsGeneral == true && m.Language == language).ToListAsync(cancellationToken);
            ContactUsMessage marketMessage = _marketMessage.LastOrDefault();
            return marketMessage;
        }

        public async Task<IEnumerable<ContactUsMessage>> GetAllMarketMessage(CancellationToken cancellationToken)
        {
            return await Table.Where(m => m.IsForce == true).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<ContactUsMessage>> GetAllGeneralMessage(int? Page, int PageSize, CancellationToken cancellationToken)
        {
            var GeneralMessage = await TableNoTracking.Where(m => m.IsGeneral == true && m.IsForce == false)
                .OrderByDescending(m => m.InsertDate)
                .Skip((Page - 1 ?? 0) * PageSize)
                .Take(PageSize)
                .ToListAsync(cancellationToken);
            return GeneralMessage;
        }

        public async Task<List<ContactUsMessage>> GetLastGeneralMessages(int lastMessageId, string language, CancellationToken cancellationToken)
        {
            return await TableNoTracking.Where(m => m.IsGeneral == true && m.IsForce == false && m.Id > lastMessageId && m.Language == language)
                   .OrderByDescending(m => m.Id).Take(5)
                   .ToListAsync(cancellationToken);
        }

        public async Task<List<ContactUsMessage>> GetParentMessages(int? Page, int PageSize, CancellationToken cancellationToken)
        {
            var marketMessage = await TableNoTracking.Where(m => m.IsReadAdmin == false && m.IsForce == false && m.IsGeneral == false && m.ToAdmin)
                //.OrderByDescending(m => m.InsertDate)
                .OrderByDescending(a => a.Id)
                .ToListAsync(cancellationToken);

            return marketMessage;
        }
    }
}
