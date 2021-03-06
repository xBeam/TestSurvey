﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TestSurvey
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{userId}/{surveyId}/{pageNumber}",
                defaults: new {
                    controller = "Home",
                    action = "Index",
                    userId = UrlParameter.Optional,
                    surveyId = UrlParameter.Optional,
                    pageNumber = UrlParameter.Optional }
            );
        }
    }
}
