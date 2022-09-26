using EmployeeManagementApi.Models;
using EmployeeManagementApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Controllers
{
    public class AddModelRequest
    {
        public string Name { get; set; }
        public string Domain { get; set; }
        public List<string> Holidays { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {

       private readonly ICompanyRepository _companyRepository;
        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        [HttpPost("addModel")]
        public async Task<ActionResult<bool>> AddModel(AddModelRequest request)
        {
           try
            {
                var companyModel = new Company
                {
                    Name = request.Name,
                    Domain = request.Domain,
                    Holidays = request.Holidays
                };
                _companyRepository.Add(companyModel);
                await _companyRepository.Commit();
                return true;
            }
            catch
            {
                return false;
            }

        }
        [HttpPost("addHoliday")]
        public async Task<ActionResult<bool>> AddHoilday(DateTime[] dates)
        {
            try
            {
                var holidaysString = new List<string>();
                foreach (DateTime date in dates)
                {
                    var holiday = date.ToString("MM-dd-yyyy");
                    holidaysString.Add(holiday);
                }


                var company = await _companyRepository.GetSingle(x => x.Name == "Growth");
                company.Holidays.AddRange(holidaysString);
                _companyRepository.Update(company);
                await _companyRepository.Commit();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet("getHolidays")]
        public async Task<ActionResult<List<string>>> GetHolidays()
        {
            var company = await _companyRepository.GetSingle(x=> x.Name == "Growth");
            return company.Holidays;
        }
    }
}
