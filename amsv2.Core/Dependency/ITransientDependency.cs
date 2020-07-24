﻿using System;
using System.Collections.Generic;
using System.Text;

namespace amsv2.Core.Dependency
{
    /// <summary>
    /// 实现该接口将自动注册到Ioc容器，生命周期为每次创建一个新实例
    /// </summary>
    public interface ITransientDependency
    {
    }
}
