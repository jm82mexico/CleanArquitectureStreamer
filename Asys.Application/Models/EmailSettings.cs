using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asys.Application.Models
{
    public class EmailSettings
    {
        public required string ApiKey { get; set; }
        public required string FromAddress { get; set; }
        public required string FromName { get; set; }
    }
}