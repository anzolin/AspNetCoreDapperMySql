using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreDapperMySql.Repository;
using Microsoft.Extensions.Options;
using AspNetCoreDapperMySql.Code;

namespace AspNetCoreDapperMySql.Controllers
{
    public class HomeController : Controller
    {
        public RepositoryBase _repository;

        public HomeController(IOptions<ConnectionStringList> connectionStrings)
        {
            _repository = new RepositoryBase(connectionStrings);
        }

        public IActionResult Cidades()
        {
            var cidades = _repository.SearchCidades(string.Empty);

            return View(cidades);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
