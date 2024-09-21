using Common;
using Data.Contracts;
using Data.Repositories;
using MediatR;
using SocialMessaging.Data.Contracts;
using SocialMessaging.Domain.Entities.ContactUs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialMessaging.Service.v1.Query
{
    public class GetMarketMessageHandler : IRequestHandler<GetMarketMessageQuery, ContactUsMessage>, IScopedDependency
    {
        private readonly IContactUsMessageRepository _repository;
        private readonly IRepositoryRedis<ContactUsMessage> _repositoryRedis;
        public GetMarketMessageHandler(IContactUsMessageRepository repository, IRepositoryRedis<ContactUsMessage> repositoryRedis)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
        }

        public async Task<ContactUsMessage> Handle(GetMarketMessageQuery request, CancellationToken cancellationToken)
        {
            ContactUsMessage marketMessage = new ContactUsMessage();
            marketMessage = await _repositoryRedis.GetAsync("MarketMessage_" + request.LanguageName);
            if (marketMessage == null)
            {
                marketMessage = await _repository.GetMarketMessage(request.LanguageName, cancellationToken);
                if (marketMessage != null)
                {
                    marketMessage.ImageUri = marketMessage.ImageUri == null ? null : Common.CommonStrings.CommonUrl + "MarketMessageImages/" + marketMessage.ImageUri;
                    await _repositoryRedis.DeleteAsync("MarketMessage_" + request.LanguageName);
                    await _repositoryRedis.UpdateAsync("MarketMessage_" + request.LanguageName, marketMessage);
                }
            }
            return marketMessage;
        }
    }
}
