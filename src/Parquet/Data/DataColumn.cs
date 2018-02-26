using System.Collections;
using System.Collections.Generic;

namespace Parquet.Data
{
   public class DataColumn
   {
      private List<byte> _definitionLevels;
      private IList _values;

      public DataColumn(DataField field)
      {
      }

      public void NewLevel()
      {

      }
   }
}
