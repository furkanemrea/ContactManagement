using ContactAPI.Application.Response;
using ContactAPI.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAPI.Application.Search
{
    public interface ISearchFactory
    {
        Task<EntityResponse<GetContactListResponse>> GetContactListByKeyword(string keyword);
    }
}
