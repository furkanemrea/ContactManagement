using ContactAPI.Application.Commands;
using ContactAPI.Application.Queries;
using ContactAPI.Application.Response;
using ContactAPI.Application.Search;
using ContactAPI.Core.Models.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using ValidationLibrary.Models;
using ValidationLibrary.Validators;

namespace Contact.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator=mediator;
        }
        [HttpGet]
        [Route("get-all-contacts")]
        public async Task<IActionResult> GetAllContact()
        {
            return Ok(await _mediator.Send(new GetAllContactQuery()));
        }

        [HttpDelete]
        [Route("delete-contact")]
        public async Task<IActionResult> DeleteCommand(int contactId)
        {
            return Ok(await _mediator.Send(new DeleteContactCommand(contactId)));
        }

        [HttpPost]
        [Route("/create-contact")]
        public async Task<IActionResult> CreateContact(CreateContactCommand createContactCommand)
        {
            // you can use validator it like this.
            //ValidateResponseModel validateResult = ValidateClassProperties.GetValidateResult(createContactCommand);
            return Ok(await _mediator.Send(createContactCommand));
        }
        [Route("/get-contact-by-query")]
        [HttpGet]
        public async Task<IActionResult> GetContacyByQuery(string queryType, string keyword)
        {
            ISearchFactory searchFactory = SearchAdapter.GetFactory(queryType);
            EntityResponse<GetContactListResponse> contactListResponse = await searchFactory.GetContactListByKeyword(keyword);
            return Ok(contactListResponse);
        }
    }
}
