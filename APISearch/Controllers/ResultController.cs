using APISearch.Model;
using APISearch.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace APISearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultRepository _resultRepository;
        public ResultController(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Result>> GetResults(string str)
        {
            return await _resultRepository.GetResults(str);
        }

    }
}
