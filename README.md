<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

---


# ASP.NET Core, Dapper ORM and MySql
A project example using ASP.NET Core, Dapper ORM and MySQL.


What you need to do
-------------------

First, install the following applications:
- [.NET Core SDK](https://www.microsoft.com/net/download/core)
- [Visual Studio Code](https://code.visualstudio.com/)
- [C# for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp)
- [MySQL Community Edition](https://dev.mysql.com/downloads/mysql/)

Considering that you know a little about .Net Core, open the 'Visual Studio Code' and create a new .NET Core web project.

Apply the nuget packages listed below:
- [Dapper](https://www.nuget.org/packages/Dapper)
- [Dapper.Contrib](https://www.nuget.org/packages/Dapper.Contrib/)
- [MySqlConnector](https://www.nuget.org/packages/MySqlConnector/)

Open the file 'appsettings.json' and put yours MySQL's configurations like the example below:

```json
  "ConnectionStrings": {
    "ConnectionString1": "host=localhost;port=3306;user id=USER;password=PASSWORD;database=DATABASENAME;"
  }
```

Create a directory called 'Code' in the root folder of your project and in this folder create a class file called 'ConnectionStringList.cs' with the following content:

```csharp
namespace AspNetCoreDapperMySql.Code
{
    public class ConnectionStringList
    {
        public string ConnectionString1 { get; set; }
    }
}
```

Now, create other directory called 'Models' and three classes files with the following contents:

`Pais.cs`
```csharp
using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace AspNetCoreDapperMySql.Models
{
    [Table("GLB_Pais")]
    public class Pais
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Uf> Ufs { get; set; }
    }
}
```

`Uf.cs`
```csharp
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
```

`Cidade.cs`
```csharp
using Dapper.Contrib.Extensions;

namespace AspNetCoreDapperMySql.Models
{
    [Table("GLB_Cidade")]
    public class Cidade
    {
        [Key]
        public int Id { get; set; }
        public int Id_GLB_UF { get; set; }
        public string Nome { get; set; }
        public Uf Uf { get; set; }
    }
}
```

And, create another directory called 'Repository' and two classes files with the following contents:

`IRepositoryBase.cs`
```csharp
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
```

`RepositoryBase.cs`
```csharp
using AspNetCoreDapperMySql.Code;
using AspNetCoreDapperMySql.Models;
using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AspNetCoreDapperMySql.Repository
{
    public class RepositoryBase : IRepositoryBase
    {
        private readonly IDbConnection _db;

        public RepositoryBase(IOptions<ConnectionStringList> connectionStrings)
        {
            _db = new MySqlConnection(connectionStrings.Value.ConnectionString1);
        }

        public void Dispose()
        {
            _db.Close();
        }

        public List<Cidade> SearchCidades(string nome)
        {
            nome = "Juiz de Fora";

            if (string.IsNullOrEmpty(nome))
                return _db.Query<Cidade>("SELECT * FROM GLB_Cidade ORDER BY Nome ASC LIMIT 10").ToList();

            nome = nome.Trim();

            return _db.Query<Cidade>("SELECT * FROM GLB_Cidade WHERE Nome LIKE @Nome ORDER BY Nome ASC LIMIT 10", new { Nome = string.Format("%{0}%", nome) }).ToList();
        }

        public List<Uf> SearchUfs(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                return _db.Query<Uf>("SELECT * FROM GLB_UF ORDER BY Nome ASC LIMIT 10").ToList();

            nome = nome.Trim();

            return _db.Query<Uf>("SELECT * FROM GLB_UF WHERE Nome LIKE @Nome ORDER BY Nome ASC LIMIT 10", new { Nome = string.Format("%{0}%", nome) }).ToList();
        }

        public List<Pais> SearchPaises(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                return _db.Query<Pais>("SELECT * FROM GLB_Pais ORDER BY Nome ASC LIMIT 10").ToList();

            nome = nome.Trim();

            return _db.Query<Pais>("SELECT * FROM GLB_Pais WHERE Nome LIKE @Nome ORDER BY Nome ASC LIMIT 10", new { Nome = string.Format("%{0}%", nome) }).ToList();
        }
    }
}
```


Nuget packages applied
----------------------

- [Dapper](https://www.nuget.org/packages/Dapper)
- [Dapper.Contrib](https://www.nuget.org/packages/Dapper.Contrib/)
- [MySqlConnector](https://www.nuget.org/packages/MySqlConnector/)


License
-------

This example application is [MIT Licensed](https://github.com/anzolin/AspNetCoreDapperMySql/blob/master/LICENSE).


About the author
----------------

Hello everyone, my name is Diego Anzolin Ferreira. I'm a .NET developer from Brazil. I hope you will enjoy this simple example application as much as I enjoy developing it. If you have any problems, you can post a [GitHub issue](https://github.com/anzolin/AspNetCoreDapperMySql/issues). You can reach me out at diego@anzolin.com.br.


## Donate
  
Want to help me keep creating open source projects, make a donation:

[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg?style=for-the-badge)](https://www.paypal.com/donate?business=DN2VPNW42RTXY&no_recurring=0&currency_code=BRL) [![Donate](https://img.shields.io/badge/-buy_me_a%C2%A0coffee-gray?logo=buy-me-a-coffee&style=for-the-badge)](https://www.buymeacoffee.com/anzolin)

Thank you!



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/anzolin/AspNetCoreDapperMySql.svg?style=for-the-badge
[contributors-url]: https://github.com/anzolin/AspNetCoreDapperMySql/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/anzolin/AspNetCoreDapperMySql.svg?style=for-the-badge
[forks-url]: https://github.com/anzolin/AspNetCoreDapperMySql/network/members
[stars-shield]: https://img.shields.io/github/stars/anzolin/AspNetCoreDapperMySql.svg?style=for-the-badge
[stars-url]: https://github.com/anzolin/AspNetCoreDapperMySql/stargazers
[issues-shield]: https://img.shields.io/github/issues/anzolin/AspNetCoreDapperMySql.svg?style=for-the-badge
[issues-url]: https://github.com/anzolin/AspNetCoreDapperMySql/issues
[license-shield]: https://img.shields.io/github/license/anzolin/AspNetCoreDapperMySql.svg?style=for-the-badge
[license-url]: https://github.com/anzolin/AspNetCoreDapperMySql/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/diego-anzolin/
