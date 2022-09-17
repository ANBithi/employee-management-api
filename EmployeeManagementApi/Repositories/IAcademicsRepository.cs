using EmployeeManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Repositories
{
    public interface IAcademicsRepository : IRepository<Academic>
    {

    }


    public class EmployeeAcademicsRepository : BaseRepository<Academic>, IAcademicsRepository
    {
        public EmployeeAcademicsRepository(IDbContext context) : base(context)
        {

        }
    }
}
