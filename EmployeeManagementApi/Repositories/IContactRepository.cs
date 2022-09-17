using EmployeeManagementApi.Models;

namespace EmployeeManagementApi.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {

    }


    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(IDbContext context) : base(context)
        {

        }
    }
}
