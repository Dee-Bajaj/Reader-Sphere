using Microsoft.AspNetCore.Mvc.Routing;
using System;

namespace ReaderSphere.Controllers
{
    public class ControllerRouteTemplateAttribute : Attribute, IRouteTemplateProvider
    {
        public string Template => "api/[controller]";
        public int? Order => 1;
        public string Name { get; set; }
    }

}
