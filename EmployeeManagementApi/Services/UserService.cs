using System;
using EmployeeManagementApi.Models;
using EmployeeManagementApi.Repositories;
using System.Threading.Tasks;
using EmployeeManagementApi.ViewModels;
using System.Collections.Generic;
using System.Globalization;

namespace EmployeeManagementApi.Services
{ public class PasswordChangeStatus
    {
        public bool ResponseStatus { get; set; }
        public string Message { get; set; }
    }

    public class UserAddStatus
    {
        public bool IsCreated { get; set; }
        public string Message { get; set; }
    }
    public class LoginStatus
    {
        public UserViewModel User { get; set; }
        public bool IsAuthorized { get; set; }
        public string Message { get; set; }
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IResignRepository _resignRepository;

        public UserService(IUserRepository userRepository, IResignRepository resignRepository)
        {
            _userRepository = userRepository;
            _resignRepository = resignRepository;
        }

        public async Task<PasswordChangeStatus> ChangePassword(string oldPassword, string newPassword, string id)
        {
            var passwordStatus = new PasswordChangeStatus
            {
                ResponseStatus = false,
                Message = ""
            };
            var user = await _userRepository.GetById(id);
            // old password incorrect
            if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.HashedPassword))
            {
                passwordStatus.Message = "Old password is incorrect";
                return passwordStatus;
            }

            user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _userRepository.Update(user);
           await _userRepository.Commit();
            passwordStatus.Message = "Password Changed.";
            passwordStatus.ResponseStatus = true;
            return passwordStatus;
        }

        public async Task<bool> CheckAndUpdateProfileStatus(string id)
        {
            var resign = await _resignRepository.GetSingle(x => x.BelongsTo == id && x.Resigned == true);

            if (resign is null)
            {
                return false;
            }

            if (resign.ResignMonth == new DateTimeFormatInfo().GetMonthName(DateTime.Today.Month))
            {
                var user = await _userRepository.GetById(id);
                user.ProfileStatus = "Inactive";
                _userRepository.Update(user);
                await _userRepository.Commit();
                return true;
            }
            else
            {
                return false;
            }



        }

        public async Task<SupervisorViewModel> GetSupervisorById(string superId)
        {
            var userView = new SupervisorViewModel();
            if(superId is null)
            {
                return userView;
            }
            var user = await _userRepository.GetById(superId);
            if (user is null)
            {
                return userView;
            }
            else
            {
                userView.Id = user.Id;
                userView.FullName = $"{user.FirstName} {user.LastName}";
                userView.Designation = user.Designation;
                userView.ReportsTo = user.ReportsTo;
            }

            return userView;
        }

        public async Task<List<SupervisorViewModel>> GetSupervisors(string id)
        {

            var allUsers = new List<SupervisorViewModel>();
            var users = await _userRepository.GetAllAsync(x => x.Id != id);

            foreach (User user in users)
            {
                var userView = new SupervisorViewModel
                {
                    Id = user.Id,
                    FullName = $"{user.FirstName} {user.LastName}",
                    Designation = user.Designation,
                    ReportsTo = user.ReportsTo,
                };
                allUsers.Add(userView);
            }

            return allUsers;
        }

        public async Task<List<SupervisorViewModel>> GetAllUsers()
        {

            var allUsers = new List<SupervisorViewModel>();
            var users = await _userRepository.GetAll();

            foreach (User user in users)
            {
                var userView = new SupervisorViewModel
                {
                    Id = user.Id,
                    FullName = $"{user.FirstName} {user.LastName}",
                    Designation = user.Designation,
                    ReportsTo = user.ReportsTo,
                };
                allUsers.Add(userView);
            }

            return allUsers;
        }


        public async Task<UserAddStatus> CreateUser(CreateUserRequest newUser)
        {

            var userAddStatus = new UserAddStatus
            {
                IsCreated = false,
                Message = ""
            };
            var user = await _userRepository.GetSingle(x => x.Email == newUser.Email);

            if (user is null)
            {
                var addUser = new User
                {
                    Email = newUser.Email,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    HashedPassword = BCrypt.Net.BCrypt.HashPassword(newUser.Password),
                    Role = newUser.Role,
                    Designation = newUser.Designation,
                    ProfileStatus = newUser.ProfileStatus,
                    ReportsTo = newUser.ReportsTo
                };

                addUser.AccountCreated = DateTime.UtcNow;

                _userRepository.Add(addUser);
                await _userRepository.Commit();

                userAddStatus.IsCreated = true;
                userAddStatus.Message = "User Added Succesfully!";
                return userAddStatus;
            }

            userAddStatus.Message = "User Already Exist.";
            return userAddStatus;

        }


        public async Task<LoginStatus> LogInUser(string email, string password)
        {

            var loginStatus = new LoginStatus
            {
                IsAuthorized = false,
                User = null,
                Message = ""
            };

            var user = await _userRepository.GetSingle(x => x.Email == email);

            // verify valid user
            if (user is null)
            {
                loginStatus.Message = "User Doesn't Exit!";
                return loginStatus;
            }

            // verify user pofile is active
            if (user.ProfileStatus != "Active")
            {
                loginStatus.Message = "Profile Is Not Active.";
                loginStatus.User = null;
                return loginStatus;
            }


            //Verfiy pass
            if (!BCrypt.Net.BCrypt.Verify(password, user.HashedPassword))
            {
                loginStatus.Message = "Password Incorrect.";
                loginStatus.User = null;
                return loginStatus;
            }


            var userView = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                Designation = user.Designation,
                ReportsTo = user.ReportsTo,
                ProfileStatus = user.ProfileStatus,
                LastLogin = DateTime.UtcNow
            };

            loginStatus.IsAuthorized = true;
            loginStatus.User = userView;
            loginStatus.Message = "Login Successfull!";

            return loginStatus;
        }

    }

}

