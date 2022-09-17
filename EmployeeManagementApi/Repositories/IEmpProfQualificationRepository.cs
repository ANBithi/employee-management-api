using EmployeeManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Repositories
{
    public interface IEmpProfQualificationRepository : IRepository<ProfQualification>
    {
    }


    public class EmpProfQualificationRepository : BaseRepository<ProfQualification>, IEmpProfQualificationRepository
    {
       public EmpProfQualificationRepository(IDbContext context) : base(context)
        {

        }
    }
}
