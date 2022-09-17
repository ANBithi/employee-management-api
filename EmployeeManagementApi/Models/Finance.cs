using EmployeeManagementApi.Enums;
using EmployeeManagementApi.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Models
{
    public class Finance : AbstractDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("belongsTo")]
        public string BelongsTo { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("salaryIssueDate")]
        public DateTime SalaryIssueDate { get; set; }

        [BsonElement("month")]
        public string Month { get; set; }

        [BsonElement("basicSalary")]
        [BsonIgnoreIfDefault(false)]
        public double BasicSalary { get; set; }

        [BsonElement("housingSalary")]
        [BsonIgnoreIfDefault(false)]
        public double HousingSalary { get; set; }

        [BsonElement("medicalAllowance")]
        [BsonIgnoreIfDefault(false)]
        public double MedicalAllowance { get; set; }

        [BsonElement("taxDeduction")]
        [BsonIgnoreIfDefault(false)]
        public double TaxDeduction { get; set; }

        [BsonElement("totalWorkDays")]
        [BsonIgnoreIfDefault(false)]
        public double TotalWorkDays { get; set; }

        [BsonElement("salaryType")]
        [BsonIgnoreIfDefault(false)]
        public FinanceAddEnum SalaryType { get; set; }

        [BsonElement("joinedDate")]
        public string JoinedMonth { get; set; }

        [BsonElement("year")]
        public int Year { get; set; }
    }
}
