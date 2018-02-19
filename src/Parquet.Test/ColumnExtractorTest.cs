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

      public class ArrayColumns
      {
         public int Id { get; set; }

         public string[] Addresses { get; set; }
      }

      [Fact]
      public void Extract_simple_columns()
      {
         Schema schema = new SchemaReflector(typeof(SimpleColumns)).Reflect();
         var extractor = new ColumnExtractor();
         SimpleColumns[] classes = new[]
         {
            new SimpleColumns { Id = 1, Name = "First"}, new SimpleColumns { Id = 2, Name = "Second"}, new SimpleColumns { Id = 3, Name = "Third" }
         };

         var result = new List<IList> { new List<int>(), new List<string>() };

         extractor.Extract(classes, schema, result);
         Assert.Equal(new[] { 1, 2, 3 }, result[0]);
         Assert.Equal(new[] { "First", "Second", "Third" }, result[1]);
      }

      [Fact]
      public void Extract_array_columns()
      {
         Schema schema = SchemaReflector.Reflect<ArrayColumns>();
         var extractor = new ColumnExtractor();
         ArrayColumns[] ac = new[]
         {
            new ArrayColumns
            {
               Id = 1,
               Addresses = new[] { "Fiddler", "On" }
            },
            new ArrayColumns
            {
               Id = 2,
               Addresses = new[] { "The", "Roof" }
            }
         };

         var result = new List<IList> { new List<int>(), new List<string[]>() };
         extractor.Extract(ac, schema, result);

         Assert.Equal(new[] { 1, 2 }, result[0]);
         Assert.Equal(new[] { "Fiddler", "On", "The", "Roof" }, result[1]);
      }
   }
}
