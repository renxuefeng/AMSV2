using amsv2.Common;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMSV2.Authorization
{
    public class WebMethodActionAttribute : AuthorizeAttribute
    {
        public WebMethodActionAttribute()
        { }

        public WebMethodActionAttribute(ModulesType modulesType) => ModulesType = modulesType;
        // Get or set the Age property by manipulating the underlying Policy property
        public ModulesType ModulesType { get; set; }
    }
}
