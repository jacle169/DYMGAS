﻿using System;
using System.Threading.Tasks;
using System.Web;

namespace DYMongodbApiServer
{
    public abstract class AbstractAsyncHandler : IHttpAsyncHandler
    {
        protected abstract Task ProcessRequestAsync(HttpContext context);

        private Task ProcessRequestAsync(HttpContext context, AsyncCallback cb)
        {
            return ProcessRequestAsync(context).ContinueWith(task => cb(task));
        }

        public void ProcessRequest(HttpContext context)
        {
            ProcessRequestAsync(context).Wait();
        }

        public bool IsReusable
        {
            get { return true; }
        }

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            return ProcessRequestAsync(context, cb);
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            if (result == null)
                return;
            ((Task)result).Dispose();
        }
    }
}