﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class AFSHub : Hub
    {
        public string Ping()
        {
            return "Pong";
        }
    }
}
