using AutoMapper;
using EntityFrameworkExample.Models;
using EntityFrameworkExample.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkExample
{
    public class MapperConfig
    {
      public static void InitializeAutoMapper()
      {
         Mapper.CreateMap<Contact, ContactViewModel>();
         Mapper.CreateMap<ContactViewModel, Contact>()
            .ForMember(c => c.Addresses, m => m.ResolveUsing(new MapperResolver<ContactViewModel, AddressViewModel, Address>(s => s.Addresses)));
         Mapper.CreateMap<Address, AddressViewModel>();
         Mapper.CreateMap<AddressViewModel, Address>();
      }
   }
}
