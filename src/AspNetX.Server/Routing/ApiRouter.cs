﻿using System;
using System.Threading.Tasks;
using AspNetX.Server.Abstractions;
using AspNetX.Server.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetX.Server.Routing
{
    public class ApiRouter : ITemplateRouter
    {
        private IApiDescriptionWrapperProvider _descriptionProvider;

        public string RouteTemplate => "api/{id}";

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }

        public async Task RouteAsync(RouteContext context)
        {
            string id = context.RouteData.Values["id"] as string;
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            EnsureServices(context.HttpContext);

            var api = (IApiDescriptionWrapper)null;
            _descriptionProvider.TryGetValue(id, out api);
            context.HttpContext.Response.ContentType = "application/json";
            await context.HttpContext.Response.WriteJsonAsync(new ApiModel(api.ApiDescription));
            context.IsHandled = true;
        }

        private void EnsureServices(HttpContext context)
        {
            if (_descriptionProvider == null)
            {
                _descriptionProvider = context.ApplicationServices.GetService<IApiDescriptionWrapperProvider>();
            }
        }
    }
}
