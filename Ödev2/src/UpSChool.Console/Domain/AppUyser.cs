using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSChool.Console.Comman;

namespace UpSChool.Console.Domain
{
    public class AppUyser : IdentityUser<string>, IEntityBase, ICreatedByEntity
    {
        public string CreatedByUserId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTimeOffset CreatedOn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
