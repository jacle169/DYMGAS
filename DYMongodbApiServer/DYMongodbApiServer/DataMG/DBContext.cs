using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DYMongodbApiServer
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string oBuyer { get; set; }
        public double oSum { get; set; }
        public int oCount { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)]
        public DateTime sellTime { get; set; }
        //public virtual IList<OrderDetail> ods { get; set; }
    }

    public class OrderDetail
    {
        public string pName { get; set; }
        public decimal pPrice { get; set; }
        public decimal pDisPrice { get; set; }
    }
}