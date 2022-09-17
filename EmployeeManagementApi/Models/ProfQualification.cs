using EmployeeManagementApi.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApi.Models
{
    public class ProfQualification : AbstractDbEntity
    {

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("belongsTo")]
        public string BelongsTo { get; set; }

        [BsonElement("courseType")]
        public string CourseType { get; set; }

        [BsonElement("courseTitle")]
        public string CourseTitle { get; set; }

        [BsonElement("institute")]
        public string Institute { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("duration")]
        public string Duration { get; set; }

        [BsonElement("result")]
        public string Result { get; set; }

        [BsonElement("year")]
        public string Year { get; set; }

        [BsonElement("achievement")]
        public string Achievement { get; set; }

    }
}
