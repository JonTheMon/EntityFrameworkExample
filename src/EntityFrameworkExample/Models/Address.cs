using EntityFrameworkExample.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkExample.Models
{
   public class Address : IIdentifiable
   {      
      [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 0)]
      virtual public int Id { get; set; }
      [Key, Column(Order = 1)]
      virtual public int ContactId { get; set; }
      [StringLength(150)]
      virtual public string Street { get; set; }
      [StringLength(50)]
      virtual public string City { get; set; }
      [StringLength(30)]
      virtual public string State { get; set; }
      [StringLength(20)]
      virtual public string Zip { get; set; }
   }
}