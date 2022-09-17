using EmployeeManagementApi.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Services
{
    public interface IUserService
    {
        Task<LoginStatus> LogInUser(string email, string password);
        Task<UserAddStatus> CreateUser(CreateUserRequest newUser);
        Task<List<SupervisorViewModel>> GetSupervisors(string id);
        Task<SupervisorViewModel> GetSupervisorById(string supervisor);
        Task<PasswordChangeStatus> ChangePassword(string oldPassword, string newPassword, string id);
        Task<List<SupervisorViewModel>> GetAllUsers();
        Task<bool> CheckAndUpdateProfileStatus(string id);
    }
}
