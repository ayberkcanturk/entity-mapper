using EntityMapper.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EntityMapper.Core
{
    public interface IManualMapInstance<TSource, UResult> : IMapperInstance<TSource, UResult>
        where TSource : class
        where UResult : class
    {
        ManualMapInstanceAdapter<TSource, UResult> ManualPropertyMap<TType>(Expression<Func<TSource, TType>> sourceProperty, Expression<Func<UResult, TType>> resultProperty, bool typeSafe = true);
    }
}
