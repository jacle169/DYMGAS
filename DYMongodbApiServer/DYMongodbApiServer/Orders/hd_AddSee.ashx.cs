using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace DYMongodbApiServer.Orders
{
    /// <summary>
    /// Summary description for hd_AddSee
    /// </summary>
    public class hd_AddSee : AbstractAsyncHandler
    {
        protected override Task ProcessRequestAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var appKey = context.Request.Headers["U-ApiKey"];

            //if (appKey != "appkey")
            //{
            //    return context.Response.Output.WriteAsync("Error:501");
            //}
            var meth = context.Request.HttpMethod;
            //var routeValues = context.Request.RequestContext.RouteData.Values;
            //string dvid = routeValues["dvid"].ToString();
            //string ssid = routeValues["ssid"].ToString();

            var coll = DataDV.GetDV().getCol().GetCollection<Order>("orders");
            
            //添加设备
            if (meth == "POST")
            {
                if (context.Request.InputStream.Length == 0)
                {
                    return context.Response.Output.WriteAsync("Error:500");
                }
                using (var reader = new StreamReader(context.Request.InputStream))
                {
                    var post = JsonConvert.DeserializeObject<Order>(reader.ReadToEnd());
                    try
                    {
                        coll.Insert(post);
                        return context.Response.Output.WriteAsync("{}");
                    }
                    catch { return context.Response.Output.WriteAsync("Error:501"); }
                }
            }

            //罗列设备
            if (meth == "GET")
            {
                IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();//这里使用自定义日期格式，默认是ISO8601格式         
                timeConverter.DateTimeFormat = "yyyy-MM-dd";//设置时间格式 
                string json = JsonConvert.SerializeObject(coll.FindAll(), Formatting.Indented, timeConverter);//转换序列化的对象
                //context.Response.StatusCode = (int)HttpStatusCode.OK;
                return context.Response.Output.WriteAsync(json);
            }
            return context.Response.Output.WriteAsync("Error:403");
        }

    }
}