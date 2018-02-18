using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Parquet.Test
{
   public class SerialiserTest
   {
      public class PrimitiveProperties
      {
         public int Id { get; set; }

         public string Name { get; set; }
      }


      [Fact]
      public void Serialise_simple_properties()
      {

      }
   }

}
