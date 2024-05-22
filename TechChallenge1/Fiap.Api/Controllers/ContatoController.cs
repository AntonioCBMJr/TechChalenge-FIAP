using Fiap.Api.Entities;
using Fiap.Api.Interfaces;
using Fiap.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Controllers
{
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoController(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        [HttpPost("CriarContato")]
        public async Task<IActionResult> CriarContato([FromBody] CriarContatoSchema novoContatoSchema)
        {
            try
            {
                Contato contato = _contatoRepository.CriarContato(novoContatoSchema.Nome, novoContatoSchema.Ddd, novoContatoSchema.Telefone, novoContatoSchema.Email);

                if (contato != null)
                {
                    await _contatoRepository.InserirContato(contato);
                    return Ok(contato);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Falha interna no servidor." + ex.Message);
            }
        }

        [HttpPost("AtualizarContato")]
        public async Task<IActionResult> AtualizarContato([FromBody] AtualizarContatoSchema atualizarContatoSchema)
        {
            try
            {
                if (atualizarContatoSchema != null)
                {
                    Contato contato = await _contatoRepository.AtualizarContato(atualizarContatoSchema);

                    if (contato != null)
                    {
                        return Ok(contato);
                    }
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Falha interna no servidor." + ex.Message);
            }
        }

        [HttpPost("ExcluirContato")]
        public async Task<IActionResult> ExcluirContato([FromQuery] int id)
        {
            try
            {
                bool excluido = await _contatoRepository.ExcluirContato(id);

                if (excluido)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Falha interna no servidor." + ex.Message);
            }
        }

        [HttpPost("ConsultarContato")]
        public IActionResult ConsultarContato([FromBody] ConsultarContatoSchema consultarContatoSchema)
        {
            try
            {
                IEnumerable<Contato> list = _contatoRepository.ConsultarContatosPorDDD(consultarContatoSchema.Ddd);

                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Falha interna no servidor." + ex.Message);
            }
        }
    }
}
