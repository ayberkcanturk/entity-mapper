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
        IAutoMapInstance<TSource, UResult> AutoMap(TSource model, int levenshteinDistance = 0);
        IAutoMapCollection<TSource, UResult> AutoMap(IEnumerable<TSource> model, int levenshteinDistance = 0);
        IManualMapInstance<TSource, UResult> ManualMap(TSource model);
        IManualMapCollection<TSource, UResult> ManualMap(IEnumerable<TSource> model);
        void Dispose();
    }
}
