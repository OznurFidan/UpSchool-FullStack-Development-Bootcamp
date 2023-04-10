using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSChool.Console.Enums;

namespace UpSChool.Console.Domain
{
    public class AccsessControlLog
    {
        public int UserId { get; set; }
        public string DeviceSerialNo { get; set; }
        public AccessType AccessType { get; set; }
        public DateTimeOffset Date { get; set; }

        internal static AccessType ConvertToAccessType(string v)
        {
            throw new NotImplementedException();
        }
    }
}
