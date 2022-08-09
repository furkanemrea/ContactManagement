using Contact.API.Infrastructure.Caching;
using ContactAPI.Application.Constants;
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
        private readonly ICacheHelper _cacheHelper;
        public GetAllContactHandler(IContactQueryRepository contactQueryRepository, ICacheHelper cacheHelper)
        {
            _contactQueryRepository = contactQueryRepository;
            _cacheHelper=cacheHelper;
        }
        public async Task<EntityResponse<IReadOnlyList<ContactResponse>>> Handle(GetAllContactQuery request, CancellationToken cancellationToken)
        {
            
            EntityResponse<IReadOnlyList<ContactResponse>> response = new();
            IReadOnlyList<ContactAPI.Core.Entities.Contact> contactList = await _cacheHelper.GetEntities<Core.Entities.Contact>(CacheConstants.Keys.ContactList);
            IReadOnlyList<ContactResponse> contactResponse  = ContactMapper.Mapper.Map<IReadOnlyList<ContactResponse>>(contactList);
            response.Result = contactResponse;
            return response;
        }
    }
}
