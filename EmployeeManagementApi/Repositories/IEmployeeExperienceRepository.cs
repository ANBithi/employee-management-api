using EmployeeManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Repositories
{
    public interface IEmployeeExperienceRepository : IRepository<Experience>
    {
    }


    public class EmployeeExperienceRepository : BaseRepository<Experience>, IEmployeeExperienceRepository
    {
        public EmployeeExperienceRepository(IDbContext context) : base(context)
        {

        }
    }
}
