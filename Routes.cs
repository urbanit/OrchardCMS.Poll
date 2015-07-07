using System.Collections.Generic;
using Orchard.Mvc.Routes;
using Orchard.WebApi.Routes;

namespace Urbanit.Polls
{
    public class Routes : IHttpRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes()) routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[] 
            {
                new HttpRouteDescriptor
                {
                    Name = "Voting",
                    Priority = 0,
                    RouteTemplate = "api/voting/post/{voteId}",
                    Defaults = new
                    { 
                        area = "Urbanit.Polls",
                        controller = "Voting",
                        action = "Post"
                    }
                }
            };
        }
    }
}