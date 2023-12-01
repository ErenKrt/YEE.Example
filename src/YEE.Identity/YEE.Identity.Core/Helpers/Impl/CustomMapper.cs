using Mapster;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEE.Identity.Core.Helpers.Interfaces;

namespace YEE.Identity.Core.Helpers.Impl
{
    public class CustomMapper : Mapper, ICustomMapper
    {
        public CustomMapper(TypeAdapterConfig config) : base(config) { }
    }
}
