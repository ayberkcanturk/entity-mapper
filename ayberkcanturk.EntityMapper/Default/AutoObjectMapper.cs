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
    public class AutoObjectMapper<TSource, UResult> : IAutoObjectMapper<TSource, UResult>, IDisposable
        where TSource : class
        where UResult : class
    {
        internal object resultInstance;
        internal TSource sourceModel;

        internal PropertyInfo[] sourcePropertyInfos;
        internal PropertyInfo[] resultPropertyInfos;

        private AutoMap<TSource, UResult> autoMap;
        private ManuelMap<TSource, UResult> manuelMap;

        public AutoObjectMapper()
        {
            sourcePropertyInfos = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            resultPropertyInfos = typeof(UResult).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        public AutoObjectMapper<TSource, UResult> AutoMap(TSource model = null, int levenshteinDistance = 0)
        {
            if (sourceModel == null)
                sourceModel = model;

            autoMap = new AutoMap<TSource, UResult>(this, levenshteinDistance);

            return autoMap.Map(model);
        }

        public ManuelMap<TSource, UResult> ManualMap(TSource model = null)
        {
            if (sourceModel == null)
                sourceModel = model;
            
            manuelMap = new ManuelMap<TSource, UResult>(this);

            return manuelMap;
        }

        public UResult Result()
        {
            var result = (UResult)resultInstance;
            
            resultInstance = null;
            sourceModel = null;

            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool p)
        {
            if (resultInstance != null)
                resultInstance = null;

            if (sourceModel != null)
                sourceModel = null;

            if (sourcePropertyInfos != null)
                sourcePropertyInfos = null;

            if (resultPropertyInfos != null)
                resultPropertyInfos = null;

            if (autoMap != null)
                autoMap = null;

            if (manuelMap != null)
                manuelMap = null;
        }
    }
}
