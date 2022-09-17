using EmployeeManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Repositories
{
    public interface IResignRepository : IRepository<Resign>
    {

    }


    public class ResignRepository : BaseRepository<Resign>, IResignRepository
    {
        public ResignRepository(IDbContext context) : base(context)
        {

        }
    }
}
