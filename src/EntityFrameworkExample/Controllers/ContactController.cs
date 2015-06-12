using AutoMapper;
using EntityFrameworkExample.Models;
using Microsoft.AspNet.Mvc;
//using Microsoft.AspNet.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkExample.Controllers
{
    public class ContactController : Controller
   {
      protected AppDbContext db;

      public ContactController(AppDbContext dbContext)
      {
         db = dbContext;
      }

      public IActionResult Index()
      {
         var Contacts = db.Contacts.Include(c => c.Addresses).ToList();

         return View(Contacts);
      }

      [HttpGet]
      public IActionResult Edit(int id = 0)
      {
         Contact contact = new Contact() { Birthdate = DateTime.Now };

         // edit mode, retreive the existing contact
         if (id != 0)
         {
            contact = db.Contacts.Include(c => c.Addresses).SingleOrDefault(c => c.Id == id);
            if (contact == null)
            {
               return HttpNotFound();
            }
         }

         ContactViewModel viewModel = Mapper.Map<ContactViewModel>(contact);

         return View(viewModel);
      }

      [HttpPost]
      public async Task<IActionResult> Edit(ContactViewModel viewModel)
      {
         bool isValid = ModelState.IsValid;
         if (!isValid)
         {
            bool hasErrors = ModelState.Values.Any(v => v.Errors.Count > 0);
            isValid = !hasErrors;
         }
         Contact contact = null;
         bool addMode = (viewModel.Id == 0);

         if (isValid)
         {
            // Map the view model to the contact model
            if (addMode)
            {
               contact = Mapper.Map<Contact>(viewModel);
               db.Contacts.Add(contact);
               db.Addresses.AddRange(contact.Addresses);
            }
            else
            { // edit mode
               contact = db.Contacts.SingleOrDefault(c => c.Id == viewModel.Id);
               db.Contacts.Include(c => c.Addresses).Where(c => c.Id == viewModel.Id).Load();

               if (contact == null)
               {
                  return HttpNotFound();
               }
               if (viewModel.Addresses == null)
               {
                  viewModel.Addresses = new List<AddressViewModel>();
               }

               //{  // Just delete the unused. Automapper will handle add and modify
               //   //contact.Addresses.Where(addr => !viewModel.Addresses.Any(vmAddr => vmAddr.Id == addr.Id))
               //   //      .Each(del => db.Addresses.Remove(del));
               //   Mapper.Map<ContactViewModel, Contact>(viewModel, contact);
               //}

               // Straight up delete an address
               if (contact.Addresses.Count > 1)
               {
                  var addr = contact.Addresses.Last();
                  contact.Addresses.Remove(addr);
               }
            }

         }

         if (isValid)
         {
            db.SaveChanges();
         }
         else
         {
         }

         return RedirectToAction("Index");
      }


      public IActionResult Create()
      {
         return View(new ContactViewModel() { Addresses = new List<AddressViewModel>() });
      }
      
      [HttpPost]
      public IActionResult Create(ContactViewModel viewModel)
      {
         if (ModelState.IsValid)
         {
            Contact
               contact = Mapper.Map<Contact>(viewModel);
            db.Contacts.Add(contact);
            db.Addresses.AddRange(contact.Addresses);
            db.SaveChanges();
            return RedirectToAction("Index");
         }

         return View(viewModel);
      }
   }
}
