using EmployeeManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {

    }


    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(IDbContext context) : base(context)
        {

        }
    }
}
