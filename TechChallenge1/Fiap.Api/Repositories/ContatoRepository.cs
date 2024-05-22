using Fiap.Api.Entities;
using Fiap.Api.Interfaces;
using Fiap.Api.Models;
using Fiap.Infra.Context;

namespace Fiap.Domain.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly FiapDataContext _context;

        public ContatoRepository(FiapDataContext context)
        {
            _context = context;
        }

        public async Task<bool> InserirContato(Contato contato)
        {
            await _context.Contatos.AddAsync(contato);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Contato> AtualizarContato(AtualizarContatoSchema contato)
        {
            var contatoExistente = _context.Contatos.FirstOrDefault(x => x.Id == contato.Id);

            if (contatoExistente != null)
            {
                if (!string.IsNullOrEmpty(contato.Nome))
                    contatoExistente.Nome = contato.Nome;

                if (!string.IsNullOrEmpty(contato.Ddd))
                    contatoExistente.Ddd = contato.Ddd;

                if (!string.IsNullOrEmpty(contato.Telefone))
                    contatoExistente.Telefone = contato.Telefone;

                if (!string.IsNullOrEmpty(contato.Email))
                    contatoExistente.Email = contato.Email;

                await _context.SaveChangesAsync();

                return contatoExistente;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> ExcluirContato(int id)
        {
            var contatoExistente = _context.Contatos.FirstOrDefault(x => x.Id == id);

            if (contatoExistente != null)
            {
                _context.Remove(contatoExistente);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Contato> ConsultarContatosPorDDD(string ddd)
        {
            if (!string.IsNullOrEmpty(ddd))
            {
                return _context.Contatos.Where(x => x.Ddd == ddd).OrderBy(x => x.Nome);
            }
            else
            {
                return _context.Contatos.OrderBy(x => x.Nome);
            }
        }

        public bool ContatoExiste()
        {
            return false;
        }

        public Contato CriarContato(string nome, string ddd, string telefone, string email)
        {
            if (!ContatoExiste())
            {
                var id = 1;

                if (_context.Contatos.Any())
                {
                    id = _context.Contatos.Max(x => x.Id) + 1;
                }
                
                Contato contato = new Contato();
                contato.Id = id;
                contato.Nome = nome;
                contato.Ddd = ddd;
                contato.Telefone = telefone;
                contato.Email = email;

                return contato;
            }
            else
            {
                return null;
            }
        }
    }
}
