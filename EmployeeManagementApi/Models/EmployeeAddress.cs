using EmployeeManagementApi.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Models
{
    public class EmployeeAddress : AbstractDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("belongsTo")]
        public string BelongsTo { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("upazilla")]
        public string Upazilla { get; set; }

        [BsonElement("district")]
        public string District { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("zip")]
        public string Zip { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }

        [BsonElement("mobile")]
        public string Mobile { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("alternateEmail")]
        public string AlternateEmail { get; set; }
    }
}
