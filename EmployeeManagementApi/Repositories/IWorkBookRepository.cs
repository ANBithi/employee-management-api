using EmployeeManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Repositories
{
    public interface IWorkBookRepository : IRepository<WorkBook>
    {

    }


    public class WorkBookRepository : BaseRepository<WorkBook>, IWorkBookRepository
    {
        public WorkBookRepository(IDbContext context) : base(context)
        {

        }
    }
}
