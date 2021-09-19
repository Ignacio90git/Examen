﻿using System.Web.Mvc;

namespace Examen
{

    public static class HTMLHelperExtensions
    {
        public static string isActive(this HtmlHelper html, string controller = null, string action = null)
        {
            var activeClass = "active"; // change here if you another name to activate sidebar items
            // detect current app state
            var actualAction = (string)html.ViewContext.RouteData.Values["action"];
            var actualController = (string)html.ViewContext.RouteData.Values["controller"];

            if (string.IsNullOrEmpty(controller))
                controller = actualController;

            if (string.IsNullOrEmpty(action))
                action = actualAction;

            return controller == actualController && action == actualAction ? activeClass : string.Empty;
        }
    }
}