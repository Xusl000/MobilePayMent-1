﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore
{
    interface ISign
    {
        /// <summary>
        /// 创建签名
        /// </summary>
        /// <returns></returns>
        string BuildSign();

    }
}
