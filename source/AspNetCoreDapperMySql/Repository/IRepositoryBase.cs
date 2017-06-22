using AspNetCoreDapperMySql.Models;
using System.Collections.Generic;

namespace AspNetCoreDapperMySql.Repository
{
    internal interface IRepositoryBase
    {
        List<Cidade> SearchCidades(string nome);

        List<Uf> SearchUfs(string nome);

        List<Pais> SearchPaises(string nome);
    }
}
