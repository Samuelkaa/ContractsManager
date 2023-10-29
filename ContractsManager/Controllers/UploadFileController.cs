using ContractsManager.Models;
using ContractsManager.Scripts;
using ContractsManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContractsManager.Controllers
{
    [ApiController]
    [Route("/api/Import/[controller]")]
    public class UploadFileController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName);
            if (file == null)
                return Content("Не выбран файл");
            if (fileExtension != ".xls" && fileExtension != ".xlsx")
                return Content("Неверный формат файла");

            Stream stream = file.OpenReadStream();
            List<Contract> contracts = await Task.Run(() =>
            {
                ExcelReader excelReader = new ExcelReader(stream);
                return excelReader.GetContractsFromExcel();
            });

            DBContext dbContext = new DBContext();
            await dbContext.contracts.AddRangeAsync(contracts);
            await dbContext.SaveChangesAsync();

            string addedContent = "";
            foreach (Contract contract in contracts)
            {
                addedContent += $"Добавлен контракт со значениями:\nID: {contract.Id};\n";
                addedContent += $"Шифр: {contract.Code};\n";
                addedContent += $"Название: {contract.Name};\n";
                addedContent += $"Заказчик: {contract.Customer};\n";

                addedContent += "Этапы проекта:\n";
                string contractStages = "";
                foreach (ContractStage stage in contract.Stages)
                {
                    contractStages += "Добавлен этап контракта со значениями:\n";
                    contractStages += $"ID контракта: {stage.ContractId};\n";
                    contractStages += $"Название этапа: {stage.StageName};\n";
                    contractStages += $"Дата начала: {stage.StartDate};\n";
                    contractStages += $"Дата окончания: {stage.EndDate}.\n\n";
                }
                addedContent += contractStages;
                addedContent += $"Количество этапов: {contract.Stages.Count}.\n\n\n";
            }

            return Content(addedContent);
        }
    }
}
