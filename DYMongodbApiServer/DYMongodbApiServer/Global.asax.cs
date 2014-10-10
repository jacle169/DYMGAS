using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace DYMongodbApiServer
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
            BsonClassMap.RegisterClassMap<Order>();
        }

        void RegisterRoutes(RouteCollection routes)
        {
            routes.MapHttpHandler<DYMongodbApiServer.Orders.hd_AddSee>("v1.0/orders");
            routes.MapHttpHandler<DYMongodbApiServer.Orders.hd_GetPutDel>("v1.0/orders/{id}");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}