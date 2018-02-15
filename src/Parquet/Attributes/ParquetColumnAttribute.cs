using System;

namespace Parquet.Attributes
{
   /// <summary>
   /// Annotates a class property to provide some extra metadata for it.
   /// </summary>
   [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
   public class ParquetColumnAttribute : Attribute
   {
      public ParquetColumnAttribute()
      {

      }

      public ParquetColumnAttribute(string name)
      {
         Name = name;
      }

      /// <summary>
      /// Column name. When undefined a default propety name is used which is simply the declared property name on the class.
      /// </summary>
      public string Name { get; set; }
   }
}
