using ContractsManager.Models;
using ContractsManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContractsManager.Controllers
{
    [ApiController]
    [Route("/api/Contracts/[controller]")]
    public class GetContractsController : ControllerBase
    {
        [HttpGet]
        public async Task<IAsyncEnumerable<Contract>> GetContracts()
        {
            DBContext context = new DBContext();
            return context.contracts.Include(s => s.Stages).AsAsyncEnumerable();
        }
    }
}
