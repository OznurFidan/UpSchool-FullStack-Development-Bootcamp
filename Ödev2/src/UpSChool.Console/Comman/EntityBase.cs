using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSChool.Console.Comman
{
    internal class EntityBase : IEntityBase
    {
        public string Id {get ; set; }

        public bool IsDeleted { get; set; } 

        

        public string ModifiedByUserId { get; set; }
        public DateTimeOffset ModifiedOn { get;set; }

        public string DelataedByUserId { get; set; }
        public DateTimeOffset DelataedOn { get;set; }

    }
}
