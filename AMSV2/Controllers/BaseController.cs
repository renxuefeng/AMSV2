using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AMSV2.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AMSV2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [WebMethodAction]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 当前Token中包含的用户名
        /// </summary>
        protected internal string UserName
        {
            get
            {
                string str = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrWhiteSpace(str))
                {
                    return str;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        /// <summary>
        /// 当前Token中包含用户角色
        /// </summary>
        protected internal int Role
        {
            get
            {
                string str = User.FindFirst(x => x.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrWhiteSpace(str))
                {
                    return int.Parse(str);
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
