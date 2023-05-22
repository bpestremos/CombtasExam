using CombtasExam.DTOs;
using CombtasExam.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CombtasExam.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterfaceApiController : ControllerBase
    {
        private readonly IInterfaceApiService _interfaceApiService;
        private readonly ILogger<InterfaceApiController> _logger;
        public InterfaceApiController(IInterfaceApiService interfaceApiService, ILogger<InterfaceApiController> logger) 
        {
            _interfaceApiService = interfaceApiService;
            _logger = logger;
        }



        [HttpPost]
        [Route("ValidateDTO")]
        public async Task<IActionResult> ValidateDTO(InterfaceModel model)
        {
            try
            {
                return Ok(await _interfaceApiService.ValidateDTO(model));
            }
            catch (Exception error)
            {
                _logger.LogError(error.Message);
                throw;
            }
        }

    }
}
