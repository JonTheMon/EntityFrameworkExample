using EntityFrameworkExample.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkExample.Models
{
    public class AddressViewModel : IIdentifiable
   {
      public int Id { get; set; }
      public string Street { get; set; }
      public string City { get; set; }
      public string State { get; set; }
      public string Zip { get; set; }

   }
}
