using EmployeeManagementApi.Models;
using EmployeeManagementApi.Repositories;
using EmployeeManagementApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Controllers
{
    public class ApprovalRequest
    {
        public string Supervisor { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }

    }

    public class GetAllResponse
    {
        public bool Response { get; set; }
        public List<ResignViewModel> Data { get; set; }
    }

    public class UserResignResponse
    {
        public bool Response { get; set; }
        public ResignViewModel data { get; set; }
    }

    public class ApplyResignRequest
    {
        public string BelongsTo { get; set; }
        public string Supervisor { get; set; }

        public bool IsResigning { get; set; }

        public string Reason { get; set; }

        public string ResignMonth { get; set; }

        public string ExperienceUs { get; set; }

        public string AdditionalInfo { get; set; }

        public string Achievements { get; set; }
        public string Complain { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ResignController : ControllerBase
    {
        private readonly IResignRepository _resignRepository;
        private readonly IUserRepository _userRepository;

        public ResignController(IResignRepository resignRepository, IUserRepository userRepository)
        {
            _resignRepository = resignRepository;
            _userRepository = userRepository;
        }

        [HttpPost("Apply")]
        public async Task<ActionResult<bool>> Apply(ApplyResignRequest request)
        {
            try
            {
                var newResign = new Resign
                {
                    BelongsTo = request.BelongsTo,
                    Supervisor = request.Supervisor,
                    Reason = request.Reason,
                    ResignMonth = request.ResignMonth,
                    IsResigning = request.IsResigning,
                    ExperienceUs = request.ExperienceUs,
                    AdditionalInfo = request.AdditionalInfo,
                    Achievements = request.Achievements,
                    Complain = request.Complain
                };

                _resignRepository.Add(newResign);
                await _resignRepository.Commit();
                return true;
            }
            catch
            {
                return false;
            }
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<GetAllResponse>> GetAll([FromQuery]string supervisedTo)
        {
            var response = new GetAllResponse
            {
                Data = new List<ResignViewModel>(),
                Response = false
            };

            var resigns = await _resignRepository.GetAllAsync(x => x.Supervisor == supervisedTo && x.IsResigning == true);

            if (resigns.Count == 0)
            {
                return response;
            }
            foreach (Resign res in resigns)
            {
                var user = await _userRepository.GetById(res.BelongsTo);
                var resignView = new ResignViewModel
                {
                    Id = res.Id,
                    EmployeeName = $"{user.FirstName} {user.LastName}",
                    Reason = res.Reason,
                    ResignMonth = res.ResignMonth,
                    Complain = res.Complain,
                    ExperienceUs = res.ExperienceUs,
                    AdditionalInfo = res.AdditionalInfo,
                    Achievements = res.Achievements,

                };
                if (res.RefferedBy != null)
                {
                    var refer = await _userRepository.GetById(res.RefferedBy);
                    resignView.RefferedBy = $"{refer.FirstName} {refer.LastName}";
                }
                response.Data.Add(resignView);
            }
            response.Response = true;


            return response;

        }

        [HttpPost("Approval")]
        public async Task<ActionResult<bool>> Approval(ApprovalRequest request)
        {
            if(request.Type == "secondary")
            {
                try
                {
                    var resign = await _resignRepository.GetById(request.Id);
                    resign.RefferedBy = resign.Supervisor;
                    resign.Supervisor = request.Supervisor;
                    _resignRepository.Update(resign);
                    await _resignRepository.Commit();

                }
                catch
                {
                    return false;
                }
                return true;
            }
            else
            {
                try
                {
                   
                    var resign = await _resignRepository.GetById(request.Id);
                    resign.ApprovedBy = resign.Supervisor;
                    resign.Resigned = true;
                    resign.IsResigning = false;
                    _resignRepository.Update(resign);
                    await _resignRepository.Commit();

                }
                catch
                {
                    return false;
                }
                return true;
            }
           
        }
        
        [HttpPost("Denial")]
        public async Task<ActionResult<bool>> Denial(ApprovalRequest request)
        {
          
                try
                {
                    var resign = await _resignRepository.GetById(request.Id);
                    resign.RejectedBy = resign.Supervisor;
                    _resignRepository.Update(resign);
                    await _resignRepository.Commit();

                }
                catch
                {
                    return false;
                }
                return true;
            }
        [HttpGet("Remove")]
        public async Task<ActionResult<bool>> Remove([FromQuery] string id)
        {

            try
            {
                _resignRepository.Remove(id);
                await _resignRepository.Commit();

            }
            catch
            {
                return false;
            }
            return true;
        }

        [HttpGet("GetUserResign")]
        public async Task<ActionResult<UserResignResponse>> GetUserResign(string belongsTo)
        {
            var response = new UserResignResponse
            {
                Response = false,
                data = null
            };

            var res = await _resignRepository.GetSingle(x => x.BelongsTo == belongsTo);
            if (res is null)
            {
                return response;
            }
            var resignView = new ResignViewModel
            {
                Id = res.Id,
                Reason = res.Reason,
                ResignMonth = res.ResignMonth,
                Complain = res.Complain,
                ExperienceUs = res.ExperienceUs,
                AdditionalInfo = res.AdditionalInfo,
                Achievements = res.Achievements,
                IsResigning = res.IsResigning,

            };
            if (res.RefferedBy != null)
            {
                var refer = await _userRepository.GetById(res.RefferedBy);
                resignView.RefferedBy = $"{refer.FirstName} {refer.LastName}";
            }
            if (res.ApprovedBy != null)
            {
                var user = await _userRepository.GetById(res.ApprovedBy);
                resignView.ApprovedBy = $"{user.FirstName} {user.LastName}";
            }

            if (res.RejectedBy != null)
            {
                var user = await _userRepository.GetById(res.RejectedBy);
                resignView.RejectedBy = $"{user.FirstName} {user.LastName}";
            }

            response.Response = true;
            response.data = resignView;
            return response;
        }
    }
}
