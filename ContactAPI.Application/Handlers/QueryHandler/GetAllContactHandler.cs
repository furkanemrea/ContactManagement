using ContactAPI.Application.Mapper;
using ContactAPI.Application.Queries;
using ContactAPI.Application.Response;
using ContactAPI.Core.Entities;
using ContactAPI.Core.Models.Base;
using ContactAPI.Core.Repositories.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContactAPI.Application.Handlers.QueryHandler
{
    public class GetAllContactHandler : IRequestHandler<GetAllContactQuery, EntityResponse<IReadOnlyList<ContactResponse>>>
    {
        private readonly IContactQueryRepository _contactQueryRepository;

        public GetAllContactHandler(IContactQueryRepository contactQueryRepository)
        {
            _contactQueryRepository = contactQueryRepository;
        }
        public async Task<EntityResponse<IReadOnlyList<ContactResponse>>> Handle(GetAllContactQuery request, CancellationToken cancellationToken)
        {
            EntityResponse<IReadOnlyList<ContactResponse>> response = new();
            IReadOnlyList<ContactAPI.Core.Entities.Contact> contactList = await _contactQueryRepository.GetAllAsync();
            IReadOnlyList<ContactResponse> contactResponse  = ContactMapper.Mapper.Map<IReadOnlyList<ContactResponse>>(contactList);
            response.Result = contactResponse;
            return response;
        }
    }
}
