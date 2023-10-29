using ContractsManager.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;

namespace ContractsManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();

            try
            {
                DBContext.ConnectionString = app.Configuration.GetConnectionString("DefaultConnection");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            DBContext dBContext = new DBContext();
            Console.WriteLine(DBContext.ConnectionString);
            if (dBContext.Database.CanConnect())
            {
                Console.WriteLine("Подключение к базе данных успешно");
            }
            else
            {
                Console.WriteLine("Ошибка подключения к базе данных");
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            app.Run();
        }
    }
}