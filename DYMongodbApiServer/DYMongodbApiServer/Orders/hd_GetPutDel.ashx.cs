using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DYMongodbApiServer.Orders
{
    /// <summary>
    /// Summary description for hd_GetPutDel
    /// </summary>
    public class hd_GetPutDel : AbstractAsyncHandler
    {
        protected override Task ProcessRequestAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            //var req = context.Request.QueryString["id"];

            var appKey = context.Request.Headers["U-ApiKey"];

            //if (appKey != "appkey")
            //{
            //    return context.Response.Output.WriteAsync("Error:501");
            //}

            var meth = context.Request.HttpMethod;
            var routeValues = context.Request.RequestContext.RouteData.Values;
            string dvid = routeValues["id"].ToString();

            var coll = DataDV.GetDV().getCol().GetCollection<Order>("orders");
            //查看设备信息
            if (meth == "GET")
            {
                if (!string.IsNullOrEmpty(dvid))
                {
                    var query = Query<Order>.EQ(e => e.Id, dvid);
                    var obj = coll.FindOne(query);
                    if (obj != null)
                    {
                        return context.Response.Output.WriteAsync(JsonConvert.SerializeObject(obj));
                    }
                    else
                    {
                        return context.Response.Output.WriteAsync("{error:dvid not ext}");
                    }
                }
                else
                {
                    return context.Response.Output.WriteAsync("{error:dvid not ext}");
                }
            }

            //修改设备信息
            if (meth == "PUT")
            {
                if (context.Request.InputStream.Length == 0)
                {
                    return context.Response.Output.WriteAsync("Error:500");
                }
                using (var reader = new StreamReader(context.Request.InputStream))
                {
                    if (!string.IsNullOrEmpty(dvid))
                    {
                        try
                        {
                            var post = JsonConvert.DeserializeObject<Order>(reader.ReadToEnd());
                            var query = Query<Order>.EQ(e => e.Id, dvid);
                            var obj = coll.FindOne(query);
                            if (obj != null)
                            {
                                obj.oBuyer = post.oBuyer;
                                obj.oCount = post.oCount;
                                obj.oSum = post.oSum;
                                obj.sellTime = post.sellTime;
                                coll.Save(obj);
                                return context.Response.Output.WriteAsync("{}");
                            }
                        }
                        catch { return context.Response.Output.WriteAsync("Error:501"); }
                    }
                    else
                    {
                        return context.Response.Output.WriteAsync("{error:dvid not ext}");
                    }
                }
            }

            //删除设备
            if (meth == "DELETE")
            {
                using (var reader = new StreamReader(context.Request.InputStream))
                {
                    //string postedData = reader.ReadToEnd();
                    if (!string.IsNullOrEmpty(dvid))
                    {
                        var query = Query<Order>.EQ(e => e.Id, dvid);
                        coll.Remove(query);

                        return context.Response.Output.WriteAsync("{}");
                    }
                    else
                    {
                        return context.Response.Output.WriteAsync("{error:dvid not ext}");
                    }
                }
            }

            return context.Response.Output.WriteAsync("{null}");
        }
    }
}