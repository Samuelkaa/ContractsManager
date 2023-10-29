using ContractsManager.Models;
using OfficeOpenXml;

namespace ContractsManager.Scripts
{
    public class ExcelReader
    {
        ExcelPackage package;
        public ExcelReader(Stream stream)
        {
            package = new ExcelPackage(stream);
        }

        ~ExcelReader()
        {
            package.Dispose();
        }

        public List<Contract> GetContractsFromExcel()
        {
            List<Contract> contracts = new List<Contract>();
            if (package.Workbook.Worksheets != null)
            {
                foreach (var sheet in package.Workbook.Worksheets)
                {
                    if (sheet.Name == "Договора")
                    {
                        List<string> columnNames = new List<string>();
                        for (int i = 1; i <= sheet.Dimension.End.Column; i++)
                        {
                            columnNames.Add(sheet.Cells[1, i].Value.ToString());
                        }

                        for (int i = 2; i <= sheet.Dimension.End.Row; i++)
                        {
                            Contract contract = new Contract();
                            foreach (var column in columnNames)
                            {
                                int columnIndex = columnNames.IndexOf(column) + 1;
                                switch (column)
                                {
                                    case "Идентификатор":
                                        contract.Id = Convert.ToInt32(sheet.Cells[i, columnIndex].Value);
                                        break;
                                    case "Шифр договора":
                                        contract.Code = sheet.Cells[i, columnIndex].Value.ToString();
                                        break;
                                    case "Наименование договора":
                                        contract.Name = sheet.Cells[i, columnIndex].Value.ToString();
                                        break;
                                    case "Заказчик":
                                        contract.Customer = sheet.Cells[i, columnIndex].Value.ToString();
                                        break;
                                }
                            }
                            contracts.Add(contract);
                        }
                    }
                    else if (sheet.Name == "Этапы договоров")
                    {
                        List<string> columnNames = new List<string>();
                        for (int i = 1; i <= sheet.Dimension.End.Column; i++)
                        {
                            columnNames.Add(sheet.Cells[1, i].Value.ToString());
                        }

                        for (int i = 2; i <= sheet.Dimension.End.Row; i++)
                        {
                            ContractStage contractStage = new ContractStage();
                            foreach (var column in columnNames)
                            {
                                int columnIndex = columnNames.IndexOf(column) + 1;
                                switch (column)
                                {
                                    case "Идентификатор договора":
                                        contractStage.ContractId = Convert.ToInt32(sheet.Cells[i, columnIndex].Value);
                                        Console.WriteLine(contractStage.ContractId);
                                        break;
                                    case "Наименование этапа":
                                        contractStage.StageName = sheet.Cells[i, columnIndex].Value.ToString();
                                        Console.WriteLine(contractStage.StageName);
                                        break;
                                    case "Дата начала":
                                        contractStage.StartDate = DateOnly.FromDateTime(Convert.ToDateTime(sheet.Cells[i, columnIndex].Value.ToString()));
                                        Console.WriteLine(contractStage.StartDate);
                                        break;
                                    case "Дата окончания":
                                        contractStage.EndDate = DateOnly.FromDateTime(Convert.ToDateTime(sheet.Cells[i, columnIndex].Value.ToString()));
                                        Console.Write(contractStage.EndDate + "\n");
                                        break;
                                }
                            }
                            try
                            {
                                contracts.Where(c => c.Id == contractStage.ContractId).First().Stages.Add(contractStage);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Ошибка добавления этапа: " + ex.Message);
                            }
                        }
                    }
                }
            }

            return contracts;
        }
    }
}
