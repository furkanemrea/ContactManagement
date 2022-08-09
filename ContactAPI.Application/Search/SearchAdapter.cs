using Contact.API.Infrastructure.Data;
using Contact.API.Infrastructure.Repository.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAPI.Application.Search
{
    public static class SearchAdapter
    {
        static Dictionary<string, ISearchFactory> interactionCheckStrategyDictionary = new();
        public static ISearchFactory GetFactory(string queryType)
        {
            ContactContext contactContext = new();
            ContactCommandRepository contactCommandRepository = new(contactContext);

            if ( interactionCheckStrategyDictionary.Count == 0 )
            {
                interactionCheckStrategyDictionary.Add("email", new EmailFactory(contactCommandRepository));
                interactionCheckStrategyDictionary.Add("url", new UrlFactory(contactCommandRepository));
                interactionCheckStrategyDictionary.Add("phone", new PhoneFactory(contactCommandRepository));
                interactionCheckStrategyDictionary.Add("address", new AddressFactory(contactCommandRepository));
                interactionCheckStrategyDictionary.Add("socialprofile", new SocialProfileFactory(contactCommandRepository));
                interactionCheckStrategyDictionary.Add("default", new DefaultFactory(contactCommandRepository));
            }
            ISearchFactory searchFactory = interactionCheckStrategyDictionary.Where(x => x.Key.Contains(queryType.ToLower())).FirstOrDefault().Value as ISearchFactory;
            return searchFactory is null ?  interactionCheckStrategyDictionary.Where(x=>x.Key.Contains("default")).FirstOrDefault().Value : searchFactory;
        }
    }
}
