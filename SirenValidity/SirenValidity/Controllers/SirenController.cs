using Microsoft.AspNetCore.Mvc;
using SirenValidity.Interfaces;

namespace SirenValidity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SirenController : ControllerBase
    {
        private readonly ILogger<SirenController> _logger;
        private readonly ISirenService sirenService;

        public SirenController(ILogger<SirenController> logger, ISirenService sirenService)
        {
            _logger = logger;
            this.sirenService = sirenService;
        }

        [HttpGet("CheckSirenValidity")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CheckSirenValidity(string siren)
        {
            try
            {
                bool checkSiren = this.sirenService.CheckSirenValidity(siren);
                return this.Ok(checkSiren);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erreur interne à l'application");
            }

        }

        [HttpGet("ComputeFullSiren")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ComputeFullSiren(string sirenWithoutControlNumber)
        {
            try
            {
                string fullSiren = this.sirenService.ComputeFullSiren(sirenWithoutControlNumber);
                return this.Ok(fullSiren);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}