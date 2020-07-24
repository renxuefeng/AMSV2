using amsv2.Core.Configuration;
using amsv2.Core.JWT;
using amsv2.Model.Entitys;
using amsv2.Repository.IRepositories;
using amsv2.Service.UserService;
using AMSV2.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AMSV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IUserInfoService _userInfoService;
        private readonly ResponseData _responseData;
        private readonly AudienceConfiguration _audienceConfiguration;
        private readonly IStringLocalizer<LoginController> _stringLocalizer;
        public LoginController(IUserInfoService userInfoService, ResponseData responseData, AudienceConfiguration audienceConfiguration, IStringLocalizer<LoginController> stringLocalizer)
        {
            _userInfoService = userInfoService;
            _responseData = responseData;
            _audienceConfiguration = audienceConfiguration;
            _stringLocalizer = stringLocalizer;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ResponseData>> Login(LoginInfo loginInfo)
        {
            string jwtStr = string.Empty;
            var user = await _userInfoService.CheckUserPassword(loginInfo.userName, loginInfo.password);
            if (user != null)
            {
                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, await _userInfoService.GetUserPermission(user)),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_audienceConfiguration.Expiration).ToString())
                };
                //用户标识
                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaims(claims);
                var token = JWTHelper.BuildJwtToken(claims.ToArray(), _audienceConfiguration);
                _responseData.Success = true;
                _responseData.Data = token;
            }
            else
            {
                _responseData.Success = false;
                _responseData.Message = _stringLocalizer["ErrorMsg"];
            }
            return _responseData;
        }
    }
}
