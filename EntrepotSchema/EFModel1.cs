﻿  
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace EntrepotSchema
{
   public partial class Test
   {
      partial void Init();

      /// <summary>
      /// Default constructor
      /// </summary>
      public Test()
      {
         Init();
      }

      // Persistent properties

      /// <summary>
      /// Identity, Required, Indexed
      /// </summary>
      public int Id { get; set; }

   }
}

using System.Data.Entity;

namespace EntrepotSchema
{
   public partial class EFModel1DatabaseInitializer : MigrateDatabaseToLatestVersion<EFModel1, EFModel1DbMigrationConfiguration>
   {
   }
}
using System.Data.Entity.Migrations;

namespace EntrepotSchema
{
   public sealed partial class EFModel1DbMigrationConfiguration : DbMigrationsConfiguration<EFModel1>
   {
      partial void Init();

      public EFModel1DbMigrationConfiguration()
      {
         AutomaticMigrationsEnabled = false;
         AutomaticMigrationDataLossAllowed = false;
         Init();
      }
   }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace EntrepotSchema
{
   public partial class EFModel1 : System.Data.Entity.DbContext
   {