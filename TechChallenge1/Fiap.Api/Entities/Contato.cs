using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.Api.Entities
{
    [Table("Contato")]
    public class Contato
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Ddd { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}