using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundbae.Services.Options
{
    public class AppSettings
    {
        public JWT JWT { get; set; }

    }

    public class JWT
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string Secret { get; set; }
    }
}
