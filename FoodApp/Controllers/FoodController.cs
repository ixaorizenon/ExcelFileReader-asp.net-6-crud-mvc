using FoodApp.Models;
using FoodApp.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OfficeOpenXml;

namespace FoodApp.Controllers
{
    public class FoodController : Controller
    {
        IFoodService _foodService = null;
        List<Yemekler> _food = new List<Yemekler>();
        public FoodController(IFoodService foodService)
        {
            _foodService = foodService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult SaveFood(List<Foods> foods)
        {
            _food = _foodService.SaveYemeklers(foods);
            return Json(_food);
        }
        public string GenerateAndDownloadExcel(int foodId , string foodName)
        {
             List<Foods> Foodss = _foodService.GetFoodss();

            var dataTable = CommonMethods.ConvertListDataTable(Foodss);
            dataTable.Columns.Remove("FoodId");

            byte[] fileContents = null;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Yemekler");
                ws.Cells["B1"].Value = "Pazartesi";
                ws.Cells["B1"].Style.Font.Bold = true;
                ws.Cells["B1"].Style.Font.Size = 16;
                ws.Cells["B1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["B1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                ws.Cells["B2"].Value = "Salı";
                ws.Cells["B2"].Style.Font.Bold = true;
                ws.Cells["B2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["B2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                ws.Cells["B3"].LoadFromDataTable(dataTable, true);
                ws.Cells["B3"].Style.Font.Bold = true;
                ws.Cells["B3"].Style.Font.Size = 12;
                ws.Cells["B3"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells["B3"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.SkyBlue);
                ws.Cells["B3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["B3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                pck.Save();
                fileContents = pck.GetAsByteArray();
            }
            return Convert.ToBase64String(fileContents);
        }
    }
}
