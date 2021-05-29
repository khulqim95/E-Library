using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace PortalPMO.Component
{
    public static class DataReaderExtensions
    {
        public static List<T> MapToList<T>(this DbDataReader dr) where T : new()
        {
            if (dr != null && dr.HasRows)
            {
                var entity = typeof(T);
                var entities = new List<T>();
                var propDict = new Dictionary<string, PropertyInfo>();
                var props = entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                propDict = props.ToDictionary(p => p.Name.ToUpper(), p => p);

                while (dr.Read())
                {
                    T newObject = new T();
                    for (int index = 0; index < dr.FieldCount; index++)
                    {
                        if (propDict.ContainsKey(dr.GetName(index).ToUpper()))
                        {
                            var info = propDict[dr.GetName(index).ToUpper()];
                            if ((info != null) && info.CanWrite)
                            {
                                var val = dr.GetValue(index);
                                info.SetValue(newObject, (val == DBNull.Value) ? null : val, null);
                            }
                        }
                    }
                    entities.Add(newObject);
                }
                return entities;
            }
            return null;
        }
        public static T MapToFirstData<T>(this DbDataReader dr) where T : new()
        {
            T entities = new T();
            if (dr != null && dr.HasRows)
            {
                var entity = typeof(T);
                var propDict = new Dictionary<string, PropertyInfo>();
                var props = entity.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                propDict = props.ToDictionary(p => p.Name.ToUpper(), p => p);

                while (dr.Read())
                {
                    T newObject = new T();
                    for (int index = 0; index < dr.FieldCount; index++)
                    {
                        if (propDict.ContainsKey(dr.GetName(index).ToUpper()))
                        {
                            var info = propDict[dr.GetName(index).ToUpper()];
                            if ((info != null) && info.CanWrite)
                            {
                                var val = dr.GetValue(index);
                                info.SetValue(newObject, (val == DBNull.Value) ? null : val, null);
                            }
                        }
                    }
                    entities = newObject;
                }
                return entities;
            }
            return default(T);

        }

        public static async Task<T> ToSingle<T>(this DbDataReader dataReader)
        {
            if (dataReader == null || !dataReader.HasRows)
                return default(T);

            var returnType = typeof(T);
            T returnObject = (T)Activator.CreateInstance(returnType);
            var returnObjectProperties = returnType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(property => property.Name, property => property);

            await dataReader.ReadAsync();

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                string colName = dataReader.GetName(i);

                if (returnObjectProperties.ContainsKey(colName))
                {
                    var propertyInfo = returnObjectProperties[colName];

                    if (propertyInfo != null && propertyInfo.CanWrite)
                    {
                        var colValue = dataReader.GetValue(i);
                        var isNull = string.IsNullOrEmpty(colValue.ToString());
                        propertyInfo.SetValue(returnObject, isNull ? null : colValue);
                    }
                }
            }

            return returnObject;
        }

        public static async Task<List<T>> ToListData<T>(this DbDataReader dataReader)
        {
            if (dataReader == null || !dataReader.HasRows)
                return null;

            var returnObjectList = new List<T>();
            var returnType = typeof(T);
            var returnObjectProperties = returnType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(property => property.Name, property => property);

            while (await dataReader.ReadAsync())
            {
                T returnObject = (T)Activator.CreateInstance(returnType);

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    string colName = dataReader.GetName(i);

                    if (returnObjectProperties.ContainsKey(colName))
                    {
                        var propertyInfo = returnObjectProperties[colName];

                        if (propertyInfo != null && propertyInfo.CanWrite)
                        {
                            var colValue = dataReader.GetValue(i);
                            var isNull = string.IsNullOrEmpty(colValue.ToString());
                            propertyInfo.SetValue(returnObject, isNull ? null : colValue);
                        }
                    }
                }

                returnObjectList.Add(returnObject);
            }

            return returnObjectList;
        }
    }
}
