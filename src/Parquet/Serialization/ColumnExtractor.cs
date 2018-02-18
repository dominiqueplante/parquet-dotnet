using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Parquet.Data;
using Parquet.Serialization.Values;

namespace Parquet.Serialization
{
   public class ColumnExtractor
   {
      private IColumnValuesExtractor _valuesExtractor;

      public ColumnExtractor()
      {
         _valuesExtractor = new ColumnValuesReflectionExtractor();
      }

      public void Extract<TClass>(IEnumerable<TClass> classInstances, Schema schema, List<IList> columns)
      {
         _valuesExtractor.ExtractToList(typeof(TClass), classInstances, schema, columns);
      }
   }
}
