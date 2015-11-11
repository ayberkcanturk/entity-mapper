using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EntityMapper.Default;
using EntityMapper.Helper;

namespace EntityMapper.Default
{
    public class AutoMap<TSource, UResult>
        where TSource : class
        where UResult : class
    {
        private readonly EntityMapper<TSource, UResult> mapper;

        private bool LevenshteinDistanceEnabled = false;

        private int _levenshteinDistance = 0;
        private int levenshteinDistance 
        {
            get { return _levenshteinDistance; }
            set {
                    if (value > 0)  LevenshteinDistanceEnabled = true;
                    else LevenshteinDistanceEnabled = false;

                    _levenshteinDistance = value; 
            }
        }

        public AutoMap(EntityMapper<TSource, UResult> mapper, int levenshteinDistance = 0)
        {
            this.mapper = mapper;
            this.levenshteinDistance = levenshteinDistance;
        }

        public EntityMapper<TSource, UResult> Map(TSource model)
        {
            if (mapper.resultInstance == null)
                mapper.resultInstance = Activator.CreateInstance(typeof(UResult));

            foreach (PropertyInfo sourcePropertyInfo in mapper.sourcePropertyInfos)
            {
                object value = ReflectionHelper.GetPropertyValue(mapper.sourceModel, sourcePropertyInfo.Name);
                Debug.WriteLine(sourcePropertyInfo.Name + ":" + value);

                foreach (PropertyInfo resultPropertyInfo in mapper.resultPropertyInfos)
                {
                    if (!LevenshteinDistanceEnabled)
                    {
                        if (sourcePropertyInfo.Name != resultPropertyInfo.Name && sourcePropertyInfo.PropertyType != resultPropertyInfo.PropertyType)
                            continue;
                    }
                    else
                    {
                        if (Helper.LevenshteinDistance.Compute(sourcePropertyInfo.Name, resultPropertyInfo.Name) > levenshteinDistance)
                            continue;
                    }

                    resultPropertyInfo.SetValue(mapper.resultInstance, value, null);
                }
            }

            return mapper;
        }
    }
}
