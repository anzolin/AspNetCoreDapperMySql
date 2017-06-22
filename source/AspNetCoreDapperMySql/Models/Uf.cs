using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace AspNetCoreDapperMySql.Models
{
    [Table("GLB_UF")]
    public class Uf
    {
        [Key]
        public int Id { get; set; }

        public int Id_GLB_Pais { get; set; }

        public string Nome { get; set; }

        public string Sigla { get; set; }

        public Pais Pais { get; set; }

        public ICollection<Cidade> Cidades { get; set; }
    }
}
