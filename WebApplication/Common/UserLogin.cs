﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Common
{
    [Serializable]
    public class UserLogin
    {   
        public long UserId { get; set; }
        public string UserName { get; set; }
    }
}