using ContractsManager.Models;
using ContractsManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContractsManager.Controllers
{
    [ApiController]
    [Route("/api/Contracts/[controller]")]
    public class GetContractStagesController : ControllerBase
    {
        [HttpGet("{contractID:int}")]
        public async Task<IAsyncEnumerable<ContractStage>> GetContractStages(int contractID)
        {
            DBContext context = new DBContext();
            return context.contractstages.Where(s => s.ContractId == contractID).AsAsyncEnumerable();
        }
    }
}