using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayShell.Model
{
    [Serializable]
    public class SubscribeConfig
    {

        public bool useProxy;
        public string name;
        public string url;
    }
}
