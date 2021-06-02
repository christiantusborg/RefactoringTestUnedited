using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    public class SignupServiceResult
    {
        public bool IsSuccess { get; set; }
        public Dictionary<string,bool> Notifications { get; set; } = new Dictionary<string, bool>();
    }
}
