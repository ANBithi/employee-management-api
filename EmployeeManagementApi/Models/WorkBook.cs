using EmployeeManagementApi.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace EmployeeManagementApi.Models
{
    public class WorkBook : AbstractDbEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("belongsTo")]
        public string BelongsTo { get; set; }

        [BsonElement("date")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Date { get; set; }

        [BsonElement("workType")]
        public string WorkType { get; set; }


        [BsonElement("totalHours")]
        [BsonIgnoreIfDefault(false)]
        public int TotalHours { get; set; }

        [BsonElement("Days")]
        public int Days { get; set; } 
    }
}
