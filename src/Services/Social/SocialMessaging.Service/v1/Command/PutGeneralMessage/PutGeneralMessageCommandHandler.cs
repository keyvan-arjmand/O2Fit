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

namespace SocialMessaging.Service.v1.Command.PutGeneralMessage
{
    public class PutGeneralMessageCommandHandler : IRequestHandler<PutGeneralMessageCommand, ContactUsMessage>, IScopedDependency
    {
        private readonly IContactUsMessageRepository _repository;
        private readonly IRepositoryRedis<List<ContactUsMessage>> _repositoryLastMessagesRedis;
        public PutGeneralMessageCommandHandler(IContactUsMessageRepository repository,
            IRepositoryRedis<List<ContactUsMessage>> repositoryLastMessagesRedis)
        {
            _repository = repository;
            _repositoryLastMessagesRedis = repositoryLastMessagesRedis;
        }
        public async Task<ContactUsMessage> Handle(PutGeneralMessageCommand request, CancellationToken cancellationToken)
        {
           request.generalMessage.IsForce = false;
           request.generalMessage.IsGeneral = true;
           request.generalMessage.ToAdmin = false;
           await _repository.UpdateAsync(request.generalMessage, cancellationToken);
           var allGeneralMessage = await _repository.GetAllGeneralMessage(1, 10000, cancellationToken);
            if (allGeneralMessage != null)
            {
                List<string> keys = new List<string>();
                foreach (var item in allGeneralMessage)
                {
                    keys.Add($"LastGeneralMessages_{item.Language}_{item.Id}");
                }
                await _repositoryLastMessagesRedis.DeleteAllAsync(keys);
            }
            return request.generalMessage;
        }
    }
}
