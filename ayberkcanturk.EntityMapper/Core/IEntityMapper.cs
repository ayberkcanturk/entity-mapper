using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityMapper.Default;

namespace EntityMapper.Core
{
    public interface IEntityMapper<TSource, UResult>
        where TSource : class
        where UResult : class
    {
        EntityMapper<TSource, UResult> AutoMap(TSource model, int levenshteinDistance = 0);
        ManuelMap<TSource, UResult> ManualMap(TSource model);
        UResult Result();
        void Dispose();
    }
}
