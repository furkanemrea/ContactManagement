using ContactAPI.Application.Response;
using ContactAPI.Core.Models.Base;
using ContactAPI.Core.Repositories.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAPI.Application.Search
{
    internal class SocialProfileFactory : ISearchFactory
    {
        private readonly IContactCommandRepository _contactCommandRepository;

        public SocialProfileFactory(IContactCommandRepository contactCommandRepository)
        {
            _contactCommandRepository=contactCommandRepository;
        }

        public async Task<EntityResponse<GetContactListResponse>> GetContactListByKeyword(string keyword)
        {
            GetContactListResponse getContactListResponse = new();
            getContactListResponse.Contacts = (await _contactCommandRepository.GetContactBySocialProfile(keyword)).Result;
            return EntityResponse<GetContactListResponse>.Builder().SetSuccessStatus().SetResult(getContactListResponse).Build();
        }
    }
}
