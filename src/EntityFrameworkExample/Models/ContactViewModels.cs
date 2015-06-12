using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkExample.Models
{
    public class ContactViewModel
    {
      public int Id { get; set; }

      [Required]
      public string FirstName { get; set; }

      public DateTime Birthdate { get; set; }

      public List<AddressViewModel> Addresses { get; set; }
   }
}
