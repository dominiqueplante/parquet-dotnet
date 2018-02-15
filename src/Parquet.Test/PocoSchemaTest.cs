using System;
using System.Collections.Generic;
using System.Text;
using Parquet.Attributes;
using Parquet.Data;
using Parquet.Data.Inferring;
using Xunit;

namespace Parquet.Test
{
   public class PocoSchemaTest : TestBase
   {
      [Fact]
      public void I_can_infer_different_types()
      {
         var inferrer = new PocoInferrer(typeof(PocoClass));

         Schema schema = inferrer.InferSchema();

         Assert.NotNull(schema);
         Assert.Equal(2, schema.Length);

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
      }

      class PocoClass
      {
         public int Id { get; set; }

         [ParquetColumn("AltId")]
         public int AnnotatedId { get; set; }
      }
   }
}
