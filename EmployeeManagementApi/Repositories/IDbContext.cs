using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Repositories
{
    public interface IDbContext : IDisposable
    {
        void AddCommand(Func<Task> func);
        Task<int> SaveChanges();
        void Reset();
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
