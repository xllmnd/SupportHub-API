using Microsoft.AspNetCore.Mvc;
using SupportHub.Application.Features.Customers.Commands.CreateCustomer;

namespace SupportHub.Api.Controllers
{
    public class CustomerController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            var id = await Mediator.Send(command); 
            return Ok(id);
        }

        
    }
}
