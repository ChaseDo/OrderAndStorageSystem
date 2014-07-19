using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace KN_Order_Storage.Helpers
{
    public static class ModelParserHelper
    {
        public static List<T> ConvertToModel<T>(this DataTable dataTable) where T : new()
        {
            List<T> ts = new List<T>();

            List<PropertyInfo> properties = GetPropertiesInDataTable<T>(dataTable);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                ts.Add(PopulateObject<T>(properties, dataRow));
            }

            return ts;
        }

        private static List<PropertyInfo> GetPropertiesInDataTable<T>(DataTable dataTable) where T : new()
        {
            List<PropertyInfo> propertys = new List<PropertyInfo>(typeof(T).GetProperties());
            propertys.RemoveAll(p => !(p.CanWrite && dataTable.Columns.Contains(p.Name)));
            return propertys;
        }

        private static T PopulateObject<T>(List<PropertyInfo> properties, DataRow dataRow) where T : new()
        {
            T newObject = new T();

            string stringValue;

            foreach (PropertyInfo property in properties)
            {
                object value = dataRow[property.Name];

                if (value != DBNull.Value)
                {
                    stringValue = value.ToString();
                    switch (property.PropertyType.Name)
                    {
                        case "Double":
                            property.SetValue(newObject, Decimal.ToDouble(Convert.ToDecimal(stringValue)), null);
                            break;
                        case "Float":
                            property.SetValue(newObject, (float)Decimal.ToDouble(Convert.ToDecimal(stringValue)), null);
                            break;
                        case "Int32":
                            property.SetValue(newObject, Convert.ToInt32(stringValue), null);
                            break;
                        case "Single":
                            property.SetValue(newObject, Convert.ToSingle(stringValue), null);
                            break;
                        case "Boolean":
                            property.SetValue(newObject, Convert.ToBoolean(stringValue), null);
                            break;
                        case "DateTime":
                            property.SetValue(newObject, Convert.ToDateTime(stringValue), null);
                            break;
                        default:
                            property.SetValue(newObject, stringValue, null);
                            break;
                    }
                }
            }
            return newObject;
        }
    }
        

    public static class WebModelSerializeHelper
    {
        public static string ModelToXMLSerialize<T>(this T webModel) where T : new()
        {
            XmlSerializer serializer = GetSerializer<T>();

            using (MemoryStream stream = new MemoryStream(100))
            {
                serializer.Serialize(stream, webModel);

                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static XmlSerializer GetSerializer<T>()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            return serializer;
        }

        public static T XMLToModelDeserialize<T>(this string xml) where T : new()
        {
            XmlSerializer serializer = GetSerializer<T>();

            using (StringReader stringReader = new StringReader(xml))
            {
                return (T)serializer.Deserialize(stringReader);
            }
        }
    }


    public abstract class PagingList<T>
    {
        private List<T> items = null;
        public List<T> Items
        {
            get
            {
                if (items == null)
                { 
                    items = new List<T>(); 
                }
                return items;
            }
            set { items = value; }
        }

        public virtual int TotalCount { get; set; }
    }

}
