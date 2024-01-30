using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using test.Models;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Конструктор контроллера, принимающий экземпляр ILogger в качестве зависимости
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Метод для отображения домашней страницы
        public IActionResult Index()
        {
            return View();
        }

        // Метод для отображения страницы конфиденциальности
        public IActionResult Privacy()
        {
            return View();
        }

        // Метод для отображения страницы с ошибкой
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
