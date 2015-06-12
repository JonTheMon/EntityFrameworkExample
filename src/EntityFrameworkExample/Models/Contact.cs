using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkExample.Models
{
    public class Contact
   {
      virtual public int Id { get; set; }

      virtual public bool IsIndividual { get; set; }

      // Fields if IsIndividual is True
      virtual public string Salutation { get; set; }
      [Required]
      virtual public string FirstName { get; set; }
      virtual public string LastName { get; set; }
      virtual public int? CompanyId { get; set; }
      virtual public Contact Company { get; set; }
      virtual public DateTime? Birthdate { get; set; }

      // Fields if IsIndividual is True
      virtual public string CompanyName { get; set; }

      virtual public string Website { get; set; }
      virtual public List<Address> Addresses { get; set; }


      public Contact()
      {
         IsIndividual = true;
      }

      virtual public string FullName()
      {
         if (IsIndividual)
         {
            return FirstName + " " + LastName;
         }
         return CompanyName;
      }
   }
}
