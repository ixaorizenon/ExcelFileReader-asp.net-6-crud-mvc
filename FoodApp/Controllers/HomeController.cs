using FoodApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore;

namespace FoodApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TestDBContext _context;

        public HomeController(ILogger<HomeController> logger, TestDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ExcelFileReader()
        { return View(); }

        [HttpPost]
        public async Task<IActionResult> ExcelFileReader(IFormFile file)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            if (file != null && file.Length > 0)
            {
                var uploadDirectory = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Uploads";

                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                var filePath = Path.Combine(uploadDirectory, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var kategoriAraliklari = new List<Tuple<int, int, int>>
                    {
                        new Tuple<int, int, int>(12, 16, 1),
                        new Tuple<int, int, int>(17, 24, 2),
                        new Tuple<int, int, int>(25, 28, 3),
                        new Tuple<int, int, int>(29, 43, 4),
                        new Tuple<int, int, int>(44, 58, 5),

                        new Tuple<int, int, int>(66, 70, 1),
                        new Tuple<int, int, int>(71, 78, 2),
                        new Tuple<int, int, int>(79, 82, 3),
                        new Tuple<int, int, int>(83, 97, 4),
                        new Tuple<int, int, int>(98, 112, 5),

                        new Tuple<int, int, int>(120, 124, 1),
                        new Tuple<int, int, int>(125, 132, 2),
                        new Tuple<int, int, int>(133, 136, 3),
                        new Tuple<int, int, int>(137, 151, 4),
                        new Tuple<int, int, int>(152, 166, 5),

                        new Tuple<int, int, int>(174, 178, 1),
                        new Tuple<int, int, int>(179, 186, 2),
                        new Tuple<int, int, int>(187, 190, 3),
                        new Tuple<int, int, int>(191, 205, 4),
                        new Tuple<int, int, int>(206, 220, 5),

                        new Tuple<int, int, int>(228, 232, 1),
                        new Tuple<int, int, int>(233, 240, 2),
                        new Tuple<int, int, int>(241, 244, 3),
                        new Tuple<int, int, int>(245, 259, 4),
                        new Tuple<int, int, int>(260, 274, 5),
                    };

                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var foodNameList = new List<string>();

                        int currentRow = 0;
                        while (reader.Read())
                        {
                            if (currentRow++ < 12 - 1)
                                continue;

                            if (currentRow > 274)
                                break;

                            bool isRowEmpty = true;

                            List<string> nameFoodnameList = new List<string>();

                            for (int i = 1; i <= 5; i++)
                            {
                                string foodName = reader.GetValue(i)?.ToString();
                                nameFoodnameList.Add(foodName);

                                if (!string.IsNullOrWhiteSpace(foodName))
                                {
                                    int weekId = GetWeek(currentRow);
                                    int categorieId = GetCategorie(currentRow, CATEGORYRANGES);

                                    if (weekId != -1)
                                    {
                                        _context.foodss.Add(new foods
                                        {
                                            FoodName = foodName,
                                            DayId = i,
                                            CategorieId = categorieId,
                                            WeekId = WeekId
                                        });
                                    }
                                }
                            }
                        }
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return View();
        }
        private int GetCategorie(int currentRow, List<Tuple<int, int, int>> CATEGORYRANGES)
        {
            foreach (var RANGES in CATEGORYRANGES)
            {
                if (currentRow >= RANGES.Item1 && currentRow <= RANGES.Item2)
                {
                    return RANGES.Item3;
                }
            }
            return -1;
        }
        private int GetWeek(int currentRow)
        {
            if (currentRow >= 12 && currentRow <= 58)
            {
                return 1;
            }
            else if (currentRow >= 66 && currentRow <= 112)
            {
                return 2;
            }
            else if (currentRow >= 120 && currentRow <= 167)
            {
                return 3;
            }
            else if (currentRow >= 174 && currentRow <= 220)
            {
                return 4;
            }
            else if (currentRow >= 228 && currentRow <= 274)
            {
                return 5;
            }
            else
            {
                return -1;
            }
        }


    }
}