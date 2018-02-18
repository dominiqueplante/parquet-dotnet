using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Parquet.Data;
using System.Reflection;

namespace Parquet.Serialization.Values
{
   /// <summary>
   /// Reflection based extractor, only created to compare the performance and shouldn't be used in production
   /// </summary>
   class ColumnValuesReflectionExtractor : IColumnValuesExtractor
   {
      public void ExtractToList(Type classType, IEnumerable classInstances, Schema schema, List<IList> columns)
      {
         //i'm not sure enumerating is a good idea, worth collecting all fields in one iteration
         foreach (object instance in classInstances)
         {
            int listIdx = 0;

            foreach (Field field in schema.Fields)
            {
               IList destination = columns[listIdx++];

               PropertyInfo pi = classType.GetTypeInfo().GetDeclaredProperty(field.Path);

               object value = pi.GetValue(instance);

               destination.Add(value);
            }
         }
      }
   }
}