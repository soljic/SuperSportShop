using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Web.Models;
using Web.Models.Dtos;
using Web.Utilities.ImageServices;
using ActionResult = System.Web.Mvc.ActionResult;

namespace Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IUserService _userService;
        private readonly IImageService _imageService;

        public ProductsController(IUserService userService, IImageService imageService)
        {
            _userService = userService;
            _imageService = imageService;
        }

        public ActionResult Index()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"]
                .ConnectionString;
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("GetAllProducts", sqlConnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.Fill(dtbl);
            }

            var viewModel = new List<ProductViewModel>();
            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                viewModel.Add(new ProductViewModel
                {
                    Id = Convert.ToInt32(dtbl.Rows[i]["Id"]),
                    Name = dtbl.Rows[i]["Name"].ToString(),
                    Price = Convert.ToDecimal(dtbl.Rows[i]["Price"]),
                    Category = dtbl.Rows[i]["Category"].ToString(),
                    UserId = dtbl.Rows[i]["UserId"].ToString(),
                });
            }

            return View(viewModel);
        }

        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> Add(AddProductDto addProductDto)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"]
                .ConnectionString;
            var pictureUrl = await SaveImage(addProductDto.Picture);
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                // Otvorite vezu s bazom podataka
                sqlConnection.Open();

                // Stvorite SQL naredbu za pohranjeni postupak
                using (SqlCommand sqlCommand = new SqlCommand("NewProduct", sqlConnection))
                {
                    // Postavite tip naredbe na Stored Procedure
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    // Dodajte parametre pohranjenom postupku
                    sqlCommand.Parameters.AddWithValue("@Name", addProductDto.Name);
                    sqlCommand.Parameters.AddWithValue("@Category", addProductDto.Category);
                    sqlCommand.Parameters.AddWithValue("@Price", addProductDto.Price);
                    sqlCommand.Parameters.AddWithValue("@PictureUrl", pictureUrl);
                    sqlCommand.Parameters.AddWithValue("@UserId", 1);

                    // Izvršite pohranjeni postupak
                   var count = sqlCommand.ExecuteNonQuery();
                }

                // Vratiti JSON odgovor
                return Json(new { Id = 1, Name = addProductDto.Name, Category = addProductDto.Category, Price = addProductDto.Price, UserId = 1 },
                    JsonRequestBehavior.AllowGet);
            }

        }
        
        
        [System.Web.Mvc.HttpGet]
        public ActionResult ExportToExcel()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("GetAllProducts", sqlConnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.Fill(dtbl);
            }

            // Stvaranje novog Excel paketa
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Products");

            // Dodavanje zaglavlja
            for (int i = 0; i < dtbl.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1].Value = dtbl.Columns[i].ColumnName;
            }

            // Popunjavanje podacima
            for (int row = 0; row < dtbl.Rows.Count; row++)
            {
                for (int column = 0; column < dtbl.Columns.Count; column++)
                {
                    worksheet.Cells[row + 2, column + 1].Value = dtbl.Rows[row][column].ToString();
                }
            }

            // Konvertovanje Excel paketa u byte[] koji će se vratiti kao odgovor
            byte[] fileContents = excelPackage.GetAsByteArray();

            // Vraćanje Excel datoteke kao rezultat
            return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "products.xlsx");
        }
        
        private async Task<string> SaveImage(HttpPostedFileWrapper picture)
        {
            return await _imageService.UploadImage(picture);
        }

    }
}
