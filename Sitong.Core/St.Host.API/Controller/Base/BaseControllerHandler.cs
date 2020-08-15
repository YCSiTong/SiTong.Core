using Microsoft.AspNetCore.Mvc;
using St.Common.Helper;
using St.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace St.Host.API.Controller.Base
{
    [ApiController]
    public class BaseControllerHandler : ControllerBase
    {
        public JwtModel Identity { get; private set; }
        public BaseControllerHandler()
        {
            var token = Request.HttpContext.Request.Headers["Authorization"].ToString(); // 获取Token
            if (token.IsNotEmptyOrNull())
                Identity = JwtHelper.SerializeJwt(token);
            else
                Identity = new JwtModel();
        }


    }
}
