using EntityMapper.Core;
using EntityMapper.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntityMapper.Default
{
    //TODO: USE BASE CLASS FOR COMMON PROPERTIES & METHODS
    public class ManualMapInstanceAdapter<TSource, UResult> : IManualMapInstance<TSource, UResult>
        where TSource : class
        where UResult : class
    {
        internal object resultInstance;
        internal PropertyInfo[] sourcePropertyInfos;
        internal PropertyInfo[] resultPropertyInfos;

        TSource sourceModel = null;

        public ManualMapInstanceAdapter(TSource model)
        {
            resultInstance = Activator.CreateInstance(typeof(UResult));
            sourceModel = model;
        }

        public ManualMapInstanceAdapter<TSource, UResult> ManualPropertyMap<TType>(Expression<Func<TSource, TType>> sourceProperty, Expression<Func<UResult, TType>> resultProperty, bool typeSafe = true)
        {
            object sourceValue = ReflectionHelper.GetPropertyValue(sourceModel, ReflectionHelper.GetMemberInfo(sourceProperty).Member.Name);

            PropertyInfo resultPropertyInfo = ReflectionHelper.GetPropertyInfo(typeof(UResult), resultProperty);

            if (typeSafe)
            {
                resultPropertyInfo.SetValue(resultInstance, sourceValue, null);
            }
            else
            {
                resultPropertyInfo.SetValue(resultInstance, Convert.ChangeType(sourceValue, resultPropertyInfo.PropertyType), null);
            }

            return this;
        }

        public UResult FirstOrDefault()
        {
            sourceModel = null;

            var result = (UResult)resultInstance;
            resultInstance = null;

            return result;
        }
    }
}
