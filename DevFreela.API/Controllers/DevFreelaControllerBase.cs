using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    public class DevFreelaControllerBase : ControllerBase
    {
        protected BadRequestObjectResult BadRequestValidationResult()
        {
            var messages = ModelState
                .SelectMany(q => q.Value.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(messages);
        }
    }
}
