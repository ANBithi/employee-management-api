using EmployeeManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Repositories
{
    public interface IFinanceRepository : IRepository<Finance>
    {
    }

    public class FinanceRepository : BaseRepository<Finance>, IFinanceRepository
    {
        public FinanceRepository(IDbContext context) : base(context)
        {

        }
    }
}
