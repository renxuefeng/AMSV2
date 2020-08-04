using System;
using System.Collections.Generic;
using System.Text;

namespace amsv2.Core.Configuration
{
    public class RedisConfiguration
    {
        /// <summary>
        /// Redis服务地址
        /// </summary>
        public string ServerIP { get; set; }
        /// <summary>
        /// 服务端口
        /// </summary>
        public string Port { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 连接超时时间
        /// </summary>
        public int ConnectTimeout { get; set; }
        /// <summary>
        /// 同步操作的超时时间
        /// </summary>
        public int SyncTimeout { get; set; }

    }
}
