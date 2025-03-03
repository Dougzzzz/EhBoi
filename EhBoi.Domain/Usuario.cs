using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EhBoi.Domain
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        
        [Column("Nome")]
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }
    }
}
