using System.Threading.Tasks;
using AnyTimerApi.GraphQL.Extensions;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;

namespace AnyTimerApi
{
    public class GraphQLAppMiddleware<TSchema> where TSchema : ISchema
    {
        private readonly GraphQLHttpMiddleware<TSchema> _middleware;
        private readonly PathString _path;

        public GraphQLAppMiddleware(RequestDelegate next, PathString path)
        {
            _middleware = new GraphQLHttpMiddleware<TSchema>(next, path, settings => { });
            _path = path;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments(_path)) context.BindUser();
            await _middleware.InvokeAsync(context);
            if (context.Request.Path.StartsWithSegments(_path)) context.UnBindUser();
        }
    }
}