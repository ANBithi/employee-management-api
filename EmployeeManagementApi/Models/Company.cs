using EmployeeManagementApi.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Models
{
    public class Company : AbstractDbEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("domain")]
        public string Domain { get; set; }
        [BsonElement("holidays")]
        public List<string> Holidays { get; set; }

    }
}
