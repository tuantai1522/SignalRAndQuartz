using Application.Sender.Queries.GetListSender;
using Domain.Interface;
using MediatR;

namespace Application.Sender.Queries.GetSenderByUserName
{
    public class GetSenderByUserNameQueryHandler
        : IRequestHandler<GetSenderByUserNameQuery, Domain.Entity.Sender>

    {
        private readonly ISenderRepository _senderRepository;

        public GetSenderByUserNameQueryHandler(
            ISenderRepository senderRepository)
        {
            _senderRepository = senderRepository
                ?? throw new ArgumentNullException(nameof(senderRepository));
        }

        public async Task<Domain.Entity.Sender> Handle(GetSenderByUserNameQuery request, CancellationToken cancellationToken)
        {
            return await _senderRepository.GetSenderByUserName(request.UserName);
        }
    }
}
