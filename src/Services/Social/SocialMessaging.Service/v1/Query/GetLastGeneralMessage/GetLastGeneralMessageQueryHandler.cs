using Common;
using Data.Contracts;
using MediatR;
using SocialMessaging.Data.Contracts;
using SocialMessaging.Domain.Entities.ContactUs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialMessaging.Service.v1.Query.GetLastGeneralMessage
{
    public class GetLastGeneralMessageQueryHandler : IRequestHandler<GetLastGeneralMessageQuery, List<ContactUsMessage>>, IScopedDependency
    {
        private readonly IContactUsMessageRepository _repository;
        private readonly IRepositoryRedis<List<ContactUsMessage>> _repositoryLastMessagesRedis;
        public GetLastGeneralMessageQueryHandler(IContactUsMessageRepository repository,
            IRepositoryRedis<List<ContactUsMessage>> repositoryLastMessagesRedis)
        {
            _repository = repository;
            _repositoryLastMessagesRedis = repositoryLastMessagesRedis;
        }
        public async Task<List<ContactUsMessage>> Handle(GetLastGeneralMessageQuery request, CancellationToken cancellationToken)
        {
            var messages = new List<ContactUsMessage>();
            messages = await _repositoryLastMessagesRedis.GetAsync($"LastGeneralMessages_{request.language}_{request.lastMessageId}");
            if (messages == null)
            {
                messages=await _repository.GetLastGeneralMessages(request.lastMessageId, request.language, cancellationToken);
                if (messages!=null)
                {
                var messageList = new List<ContactUsMessage>();
                    foreach (var item in messages)
                    {
                        item.ImageUri=item.ImageUri==null?null: Common.CommonStrings.CommonUrl + "GeneralMessageImages/" + item.ImageUri;
                        messageList.Add(item);
                    }
                    messages = messageList;
                    await _repositoryLastMessagesRedis.UpdateAsync($"LastGeneralMessages_{request.language}_{request.lastMessageId}", messages);
                }
            }
            return messages;
        }
    }
}
