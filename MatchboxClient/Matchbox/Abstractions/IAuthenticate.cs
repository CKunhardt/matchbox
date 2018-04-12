using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matchbox
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate();
    }
}
