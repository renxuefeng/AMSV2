﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amsv2.Core.Configuration
{
    public class AudienceConfiguration
    {
        /// <summary>
        /// 秘钥
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// 发行者
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 订阅者
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public double Expiration { get; set; }
    }
}
