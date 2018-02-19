using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Parquet.Data;

namespace Parquet.Serialization
{
   public class DatasetGenerator<T>
   {
      private static readonly Dictionary<Type, Schema> typeToSchema = new Dictionary<Type, Schema>();
      private readonly Schema _schema;
      private readonly ColumnExtractor _columnExtractor;

      public DatasetGenerator()
      {
         _schema = GetSchema(typeof(T));
         _columnExtractor = new ColumnExtractor();
      }

      public DataSet Generate(IEnumerable<T> classInstances)
      {
         var ds = new DataSet(_schema);

         List<IList> columns = CreateListContainer(_schema);

         _columnExtractor.Extract<T>(classInstances, _schema, columns);

         throw new NotImplementedException();
      }

      private static List<IList> CreateListContainer(Schema schema)
      {
         var r = new List<IList>(schema.Length);

         foreach(DataField field in schema.Fields)
         {
            IDataTypeHandler handler = DataTypeFactory.Match(field);
            IList list = handler.CreateEmptyList(field.HasNulls, field.IsArray, 0);
            r.Add(list);
         }

         return r;
      }

      private static Schema GetSchema(Type classType)
      {
         if(!typeToSchema.TryGetValue(classType, out Schema r))
         {
            var reflector = new SchemaReflector(classType);
            r = reflector.ReflectSchema();
            typeToSchema[classType] = r;
         }

         return r;
      }
   }
}