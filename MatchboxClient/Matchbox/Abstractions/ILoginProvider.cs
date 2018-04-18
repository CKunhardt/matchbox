﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.MobileServices;

namespace Matchbox.Abstractions
{
    public interface ILoginProvider
    {
        Task LoginAsync(MobileServiceClient client);
    }
}
