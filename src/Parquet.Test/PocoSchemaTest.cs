using Parquet.Attributes;
using Parquet.Data;
using Parquet.Serialization;
using Xunit;

namespace Parquet.Test
{
   public class PocoSchemaTest : TestBase
   {
      [Fact]
      public void I_can_infer_different_types()
      {
         var inferrer = new SchemaReflector(typeof(PocoClass));

         Schema schema = inferrer.ReflectSchema();

         Assert.NotNull(schema);
         Assert.Equal(3, schema.Length);

         DataField id = (DataField)schema[0];
         Assert.Equal("Id", id.Name);
         Assert.Equal(DataType.Int32, id.DataType);
         Assert.False(id.HasNulls);
         Assert.False(id.IsArray);

         DataField altId = (DataField)schema[1];
         Assert.Equal("AltId", altId.Name);
         Assert.Equal(DataType.Int32, id.DataType);
         Assert.False(id.HasNulls);
         Assert.False(id.IsArray);

         DataField nullableFloat = (DataField)schema[2];
         Assert.Equal("NullableFloat", nullableFloat.Name);
         Assert.Equal(DataType.Float, nullableFloat.DataType);
         Assert.True(nullableFloat.HasNulls);
         Assert.False(nullableFloat.IsArray);
      }

      /// <summary>
      /// Essentially all the test cases are this class' fields
      /// </summary>
      class PocoClass
      {
         public int Id { get; set; }

         [ParquetColumn("AltId")]
         public int AnnotatedId { get; set; }

         public float? NullableFloat { get; set; }
      }
   }
}
