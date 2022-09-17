using EmployeeManagementApi.Enums;
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
    public class FinanceRespose
    {
        public bool ResponseStatus { get; set; }
        public FinanceViewModel Response { get; set; }
        public string Message { get; set; }
    }
    public class PaySlipResponse
    {
        public bool ResponseStatus { get; set; }
        public PayslipViewModel Response { get; set; }
        public string Message { get; set; }
    }

    public class AddFinanceRequest
    {
        public string BelongsTo { get; set; }
        public double SalaryPerHour { get; set; }
        public DateTime SalaryIssueDate { get; set; }
        public double TotalHours { get; set; }
        public double TotalWorkDays { get; set; }
        public FinanceAddEnum Type { get; set; }
    }


    [ApiController]
    [Route("api/[controller]")]
    public class FinanceController : ControllerBase
    {
        private readonly IFinanceRepository _financeRepository;
        private readonly IEmployeeInfoRepository _infoRepository;
        public FinanceController(IFinanceRepository financeRepository, IEmployeeInfoRepository infoRepository)
        {
            _financeRepository = financeRepository;
            _infoRepository = infoRepository;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<bool>> Add(AddFinanceRequest request)
        {
         
           try
            {
                var finance = new Finance
                {
                    BelongsTo = request.BelongsTo,
                    SalaryIssueDate = request.SalaryIssueDate,
                    //SalaryLastMonth = (DateTime.DaysInMonth(DateTime.Today.Year,DateTime.Today.AddMonths(-1).Month) * request.SalaryPerHour),
                    Month = new DateTimeFormatInfo().GetMonthName(request.SalaryIssueDate.Month),
                    Year = request.SalaryIssueDate.Year,
                    //currentMonth = new DateTimeFormatInfo().GetMonthName(DateTime.Today.Month),
                    BasicSalary = request.SalaryPerHour * request.TotalHours,
                    SalaryType = request.Type,
                    TotalWorkDays = request.TotalWorkDays,
                };
                finance.HousingSalary = finance.BasicSalary * .1;
                finance.MedicalAllowance = finance.BasicSalary * .02;
                finance.TaxDeduction = finance.BasicSalary * .02;
                if (request.Type == FinanceAddEnum.JoinedNew)
                {
                    finance.JoinedMonth = new DateTimeFormatInfo().GetMonthName(request.SalaryIssueDate.Month);
                }

                _financeRepository.Add(finance);
                await _financeRepository.Commit();
                return true;

            }
            catch
            {
                return false;
            }
           
        }

        [HttpGet("GetCurrentMonth")]
        public async Task<ActionResult<FinanceRespose>> GetCurrentMonth([FromQuery] string belongsTo) 
        {
            var financeRespose = new FinanceRespose
            {
                ResponseStatus = false,
                Message = "",
                Response  = null
            };
            var currMonth = new DateTimeFormatInfo().GetMonthName(DateTime.Today.Month);
            var currYear = DateTime.Today.Year;
            var currMonthFinance = await _financeRepository.GetSingle(x => x.BelongsTo == belongsTo && currMonth == x.Month && currYear == x.Year);

            if (currMonthFinance is null)
            {
                financeRespose.Message = "This months salary hasn't issued." ;
            }
            else
            {
                var financeView = new FinanceViewModel
                {
                   SalaryIssueDate =  currMonthFinance.SalaryIssueDate.ToString("MM/dd/yyyy"),
                   SalaryType = currMonthFinance.SalaryType,
                   Salary =  currMonthFinance.BasicSalary,
                   Month = currMonthFinance.Month,
                   Year =  currMonthFinance.Year
                };

                financeRespose.Response = financeView;
                financeRespose.ResponseStatus = true;
                financeRespose.Message = "Salary has issued.";
            }
            return financeRespose;
        }

        [HttpGet("GeneratePayslip")]
        public async Task<ActionResult<PaySlipResponse>> GeneratePayslip([FromQuery] string month, string id , int year)
        {
            var payslipResponse = new PaySlipResponse
            {
                Response = null,
                ResponseStatus = false,
                Message = "No payslip found."
            };
           var finance = await _financeRepository.GetSingle(x => x.Month == month && x.Year == year && x.BelongsTo == id);
            if (finance is null)
            {
                payslipResponse.Message = "Salary details not found.";
                return payslipResponse;
            }
           var userInfo = await _infoRepository.GetSingle(x => x.BelongsTo == id);
            if (userInfo is null)
            {
                payslipResponse.Message = "User details not found.";
                return payslipResponse;
            }
            if (userInfo is null && finance is null)
            {
                payslipResponse.Message = "User and Finance details not found.";
                return payslipResponse;
            }
            var paySlip = new PayslipViewModel
            {
                Pin = userInfo.Pin,
                CardNo = userInfo.CardNo,
                SalaryIssueDate = finance.SalaryIssueDate.ToString("MM/dd/yyyy"),
                BasicSalary = finance.BasicSalary,
                Tin = userInfo.TinNo,
                HousingSalary = finance.HousingSalary ,
                MedicalAllowance = finance.MedicalAllowance,
                TaxDeduction = finance.TaxDeduction,
                Total = finance.BasicSalary + finance.HousingSalary - finance.MedicalAllowance - finance.TaxDeduction,
                TotalWorkDays = finance.TotalWorkDays
            };

            payslipResponse.Response = paySlip;
            payslipResponse.ResponseStatus = true;
            payslipResponse.Message = "Succesfull.";

            return payslipResponse;
        }
    }
}
