using EhBoi.Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EhBoi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusRepository _statusRepository;

        public StatusController(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        /// <summary>
        /// Obtém o status do banco de dados.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     GET /status
        ///     
        /// </remarks>
        /// <response code="200">Retorna o status do banco de dados.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpGet]
        [SwaggerOperation(Summary = "Obtém o status do banco de dados.")]
        [SwaggerResponse(200, "Retorna o status do banco de dados.")]
        [SwaggerResponse(500, "Erro interno do servidor.")]
        public IActionResult Get()
        {
            try
            {
                var status = _statusRepository.ObterStatusDoBanco();
                return Ok(status);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Internal Server Error", Message = ex.Message });
            }
        }
    }
}
