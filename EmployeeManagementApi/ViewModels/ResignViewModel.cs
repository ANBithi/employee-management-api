using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.ViewModels
{
    public class ResignViewModel
    {
        public string Id { get; set; }
        public string EmployeeName { get; set; }
        public string Reason { get; set; }
        public string ResignMonth { get; set; }
        public bool IsResigning { get; set; }
        public string Complain { get; set; }
        public string ExperienceUs { get; set; }
        public string AdditionalInfo { get; set; }
        public string Achievements { get; set; }
        public string RefferedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string RejectedBy { get; set; }

    }
}
