using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Parquet.Attributes;

namespace Parquet.Data.Inferring
{
   /// <summary>
   /// Infers a class schema using reflection
   /// </summary>
   public class PocoInferrer
   {
      private readonly TypeInfo _classType;

      public PocoInferrer(Type classType)
      {
         if (classType == null)
         {
            throw new ArgumentNullException(nameof(classType));
         }

         _classType = classType.GetTypeInfo();
      }

      public Schema InferSchema()
      {
         IEnumerable<PropertyInfo> properties = _classType.DeclaredProperties;

         return new Schema(properties.Select(GetField).Where(p => p != null));
      }

      private Field GetField(PropertyInfo property)
      {
         Type pt = property.PropertyType;
         if(pt.IsNullable()) pt = pt.GetNonNullable();

         IDataTypeHandler handler = DataTypeFactory.Match(pt);

         if (handler == null) return null;

         ParquetColumnAttribute columnAttr = property.GetCustomAttribute<ParquetColumnAttribute>();

         string name = columnAttr?.Name ?? property.Name;
         DataType type = handler.DataType;

         return new DataField(name,
            property.PropertyType   //use CLR type here as DF constructor will figure out nullability and other parameters
            );
      }
   }
}
