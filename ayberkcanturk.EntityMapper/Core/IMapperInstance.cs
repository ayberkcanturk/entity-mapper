using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityMapper.Core
{
    public interface IMapperInstance<TSource, UResult>
        where TSource : class
        where UResult : class
    {
        UResult FirstOrDefault();
    }
}
