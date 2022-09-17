using EmployeeManagementApi.Models;

namespace EmployeeManagementApi.Repositories
{
    public interface ILeaveCountRepository : IRepository<LeaveCount>
    {

    }


    public class LeaveCountRepository : BaseRepository<LeaveCount>, ILeaveCountRepository
    {
        public LeaveCountRepository(IDbContext context) : base(context)
        {

        }
    }
}
