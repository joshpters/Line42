using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace CodingBlog.Utilities
{
    public class PostgreSqlConnection
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
    }
}
