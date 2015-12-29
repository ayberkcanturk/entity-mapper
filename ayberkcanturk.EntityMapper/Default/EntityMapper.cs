using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EntityMapper.Core;

namespace EntityMapper.Default
{
    public class EntityMapper<TSource, UResult> : IEntityMapper<TSource, UResult>, IDisposable
        where TSource : class
        where UResult : class
    {

        public EntityMapper()
        {
        }

        public IAutoMapInstance<TSource, UResult> AutoMap(TSource model = null, int levenshteinDistance = 0)
        {
            AutoMapInstanceAdapter<TSource, UResult> autoMap = new AutoMapInstanceAdapter<TSource, UResult>(model, levenshteinDistance);

            return autoMap;
        }

        public IAutoMapCollection<TSource, UResult> AutoMap(IEnumerable<TSource> model = null, int levenshteinDistance = 0)
        {
            AutoMapCollectionAdapter<TSource, UResult> autoMap = new AutoMapCollectionAdapter<TSource, UResult>(model, levenshteinDistance);

            return autoMap;
        }

        public IManualMapInstance<TSource, UResult> ManualMap(TSource model)
        {
            IManualMapInstance<TSource, UResult> manuelMap = new ManualMapInstanceAdapter<TSource, UResult>(model);

            return manuelMap;
        }

        public IManualMapCollection<TSource, UResult> ManualMap(IEnumerable<TSource> model)
        {
            IManualMapCollection<TSource, UResult> manuelMap = new ManualMapCollectionAdapter<TSource, UResult>(model);

            return manuelMap;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool p)
        {
        }
    }
}
