using EmployeeManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Repositories
{
    public interface IEmployeeAddressRepository : IRepository<EmployeeAddress>
    {
    }

    public class EmployeeAddressRepository : BaseRepository<EmployeeAddress>, IEmployeeAddressRepository
    {
        public EmployeeAddressRepository(IDbContext context) : base(context)
        {

        }
    }
}
