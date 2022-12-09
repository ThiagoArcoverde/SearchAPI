using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APISearch.Model;
using Microsoft.AspNetCore.Mvc;

namespace APISearch.Repositories
{
    public interface IResultRepository
    {
        Task<IEnumerable<Result>> GetResults(string str);
    }
}
