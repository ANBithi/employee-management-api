﻿using EmployeeManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Repositories
{
    public interface ILeaveRepository : IRepository<Leave>
    {

    }


    public class LeaveRepository : BaseRepository<Leave>, ILeaveRepository
    {
        public LeaveRepository(IDbContext context) : base(context)
        {

        }
    }
}
