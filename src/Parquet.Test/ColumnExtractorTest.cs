using System.Collections;
using System.Collections.Generic;
using Parquet.Data;
using Parquet.Serialization;
using Xunit;

namespace Parquet.Test
{
   public class ColumnExtractorTest
   {
      public class SimpleColumns
      {
         public int Id { get; set; }

         public string Name { get; set; }
      }

      [Fact]
      public void Extract_simple_columns()
      {
         Schema schema = new PocoInferrer(typeof(SimpleColumns)).InferSchema();
         var extractor = new ColumnExtractor();
         SimpleColumns[] classes = new[]
         {
            new SimpleColumns { Id = 1, Name = "First"}, new SimpleColumns { Id = 2, Name = "Second"}
         };

         var result = new List<IList> { new List<int>(), new List<string>() };

         extractor.Extract(classes, schema, result);
         Assert.Equal(new[] { 1, 2 }, result[0]);
         Assert.Equal(new[] { "First", "Second" }, result[1]);
      }
   }
}
