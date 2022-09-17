using EmployeeManagementApi.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Models
{
    public class Experience : AbstractDbEntity
    {

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("belongsTo")]
        public string BelongsTo { get; set; }

        [BsonElement("company")]
        public string Company { get; set; }

        [BsonElement("business")]
        public string Business { get; set; }

        [BsonElement("location")]
        public string Location { get; set; }

        [BsonElement("positionHeld")]
        public string PositionHeld { get; set; }

        [BsonElement("jobNature")]
        public string JobNature { get; set; }

        [BsonElement("responsibilities")]
        public string Responsibilities { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("fromDate")]
        public DateTime FromDate { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonElement("toDate")]
        public DateTime ToDate { get; set; }

        [BsonElement("department")]
        public string Department { get; set; }

        [BsonElement("lastSalary")]
        public string LastSalary { get; set; }

        [BsonElement("remarks")]
        public string Remarks { get; set; }

    }
}
