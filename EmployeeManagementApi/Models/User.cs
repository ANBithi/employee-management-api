using EmployeeManagementApi.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace EmployeeManagementApi.Models
{
    public class User :  AbstractDbEntity
    {
        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("hashedPassword")]
        public string HashedPassword { get; set; }

        [BsonElement("role")]
        public string Role { get; set; }

        [BsonElement("designation")]
        public string Designation { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("reportsTo")]
        public string ReportsTo { get; set; }

        [BsonElement("accountCreated")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime AccountCreated { get; set; }

        [BsonElement("profileStatus")]
        public string ProfileStatus { get; set; }
    }

}
