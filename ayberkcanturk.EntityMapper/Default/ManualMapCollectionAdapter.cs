using EntityMapper.Core;
using EntityMapper.Helper;
using EntityMapper.Model;
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
    public class ManualMapCollectionAdapter<TSource, UResult> : IManualMapCollection<TSource, UResult>
        where TSource : class
        where UResult : class
    {
        private readonly IDictionary<string, MappedValue> mappingDictionary;
        private IEnumerable<TSource> sourceModel = null;
        private object resultInstance = null;
        private PropertyInfo[] sourcePropertyInfos = null;
        private PropertyInfo[] resultPropertyInfos = null;

        private bool LevenshteinDistanceEnabled = false;
        private int _levenshteinDistance = 0;
        private int levenshteinDistance
        {
            get { return _levenshteinDistance; }
            set
            {
                if (value > 0) LevenshteinDistanceEnabled = true;
                else LevenshteinDistanceEnabled = false;

                _levenshteinDistance = value;
            }
        }


        public ManualMapCollectionAdapter(IEnumerable<TSource> sourceModel, int levenshteinDistance = 0, bool typeSafe = true)
        {
            this.mappingDictionary = new Dictionary<string, MappedValue>();
            this.levenshteinDistance = levenshteinDistance;
            this.sourceModel = sourceModel;

            if (resultInstance == null)
                resultInstance = Activator.CreateInstance(typeof(UResult));

            sourcePropertyInfos = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            resultPropertyInfos = typeof(UResult).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            CreateMappingDictionary(sourcePropertyInfos, resultPropertyInfos, typeSafe);
        }

        public ManualMapCollectionAdapter<TSource, UResult> ManualPropertyMap<TType>(Expression<Func<TSource, TType>> sourceProperty, Expression<Func<UResult, TType>> resultProperty, bool typeSafe = true)
        {
            string sourcePropertyName = ReflectionHelper.GetMemberInfo(sourceProperty).Member.Name;

            if (mappingDictionary.ContainsKey(sourcePropertyName))
                return this;

            mappingDictionary.Add(sourcePropertyName, new MappedValue() { Name = ReflectionHelper.GetMemberInfo(resultProperty).Member.Name, IsTypeSafe = typeSafe });

            return this;
        }

        public IEnumerable<UResult> ToList()
        {
            resultInstance = MapByDictionarySourceModel(mappingDictionary, sourceModel);

            return (IEnumerable<UResult>)resultInstance;
        }

        private void CreateMappingDictionary(PropertyInfo[] sourceProperties, PropertyInfo[] resultProperties, bool isTypeSafe)
        {
            foreach (PropertyInfo sourcePropertyInfo in sourcePropertyInfos)
            {
                foreach (PropertyInfo resultPropertyInfo in resultPropertyInfos)
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

                    mappingDictionary.Add(sourcePropertyInfo.Name, new MappedValue() { Name = resultPropertyInfo.Name, IsTypeSafe = isTypeSafe });
                }
            }
        }

        private IEnumerable<UResult> MapByDictionarySourceModel(IDictionary<string, MappedValue> mappingDictionary, IEnumerable<TSource> sourceModel)
        {
            object value = null;
            UResult instance = (UResult)Activator.CreateInstance(typeof(UResult));
            PropertyInfo resultProperyInfo = null;

            foreach (var source in sourceModel)
            {
                foreach (var mapping in mappingDictionary)
                {
                    value = ReflectionHelper.GetPropertyValue(source, mapping.Key);

                    resultProperyInfo = typeof(UResult).GetProperty(mapping.Value.Name);

                    if (mapping.Value.IsTypeSafe)
                    {
                        resultProperyInfo.SetValue(instance, value, null);
                    }
                    else
                    {
                        resultProperyInfo.SetValue(instance, Convert.ChangeType(instance, resultProperyInfo.PropertyType), null);
                    }
                }

                yield return instance;
            }
        }
       
    }
}
