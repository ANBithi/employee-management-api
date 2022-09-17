using EmployeeManagementApi.Models;
using EmployeeManagementApi.Repositories;
using EmployeeManagementApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Controllers
{
    public class AddEntryRequest
    {
        public string BelongsTo { get; set; }
        public DateTime Date { get; set; }
        public string WorkType { get; set; }
        public int Hours { get; set; }
        public int Days { get; set; } 

    }

    [ApiController]
    [Route("api/[controller]")]
    public class WorkbookController : ControllerBase
    {
        private readonly IWorkBookRepository _workBookRepository;
        public WorkbookController(IWorkBookRepository workBookRepository)
        {
            _workBookRepository = workBookRepository;
        }


        [HttpPost("Entry")]
        public async Task<ActionResult<bool>> Entry(AddEntryRequest request)
        {
                
            try
            {
                var entry = new WorkBook
                {
                    BelongsTo = request.BelongsTo,
                    Date = request.Date,
                    WorkType = request.WorkType,
                };

                if (request.WorkType == "Holiday" || request.WorkType == "On Leave")
                {
                    entry.Days = request.Days;
                }
                else
                {
                     entry.TotalHours = request.Hours;
                }
 
            _workBookRepository.Add(entry);
                await _workBookRepository.Commit();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        [HttpGet("GetTotalHours")]
        public async Task<ActionResult<double>> GetTotalHours([FromQuery]string belongsTo)
        {
            var startDate = DateTime.Today.AddDays(-DateTime.Today.Day + 1);
            var endDate = DateTime.Today.AddMonths(1).AddDays(-DateTime.Today.Day);
            var totalHours = 0.0;
            var allWorks = await _workBookRepository.GetAllAsync(x => x.BelongsTo == belongsTo && x.Date <= endDate && x.Date >= startDate);
            foreach(WorkBook work in allWorks)
            {
                totalHours += work.TotalHours;
            }
            return totalHours;
        }

        [HttpGet("getTotalWorkDays")]
        public async Task<ActionResult<double>> GetTotalWorkDays([FromQuery] string belongsTo)
        {
            var startDate = DateTime.Today.AddDays(-DateTime.Today.Day + 1);
            var endDate = DateTime.Today.AddMonths(1).AddDays(-DateTime.Today.Day);
            var totalDays = 0.0;
            var allWorks = await _workBookRepository.GetAllAsync(x => x.BelongsTo == belongsTo && x.Date <= endDate && x.Date >= startDate);
            foreach (WorkBook work in allWorks)
            {
                if (work.WorkType == "Holiday" || work.WorkType == "On Leave")
                    continue;
                    totalDays++;    
            }
            return totalDays;
        }



        [HttpGet("EntriedData")]
        public async Task<ActionResult<List<WorkBookViewModel>>> EntriedData([FromQuery] string belongsTo)
        {
            var workBookData = new List<WorkBookViewModel>();
           
            var datas = await _workBookRepository.GetAllAsync(x => x.BelongsTo == belongsTo);

           foreach(WorkBook data in datas)
            {
                var workBookModel = new WorkBookViewModel
                {
                    Hours = data.TotalHours,
                    WorkType = data.WorkType,
                    Date = data.Date.ToString("MM/dd/yyyy")
                 };  
                 workBookData.Add(workBookModel);

                if (data.WorkType == "Holiday" || data.WorkType == "On Leave")
                {
                    for (int i = 1; i < data.Days; i++)
                    {
                        var newDate = data.Date.AddDays(i);
                        if (newDate.DayOfWeek == DayOfWeek.Sunday || newDate.DayOfWeek == DayOfWeek.Saturday)
                        {
                            data.Days += 1;
                            continue;
                        }

                        var bookModel = new WorkBookViewModel
                        {
                            Hours = data.TotalHours,
                            WorkType = data.WorkType,
                            Date = newDate.ToString("MM/dd/yyyy")
                    };
                        workBookData.Add(bookModel);
                    }
                }

            }


            return workBookData;
        }

    }
}
