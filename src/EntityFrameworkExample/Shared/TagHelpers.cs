using EntityFrameworkExample.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkExample.Shared
{
   [TargetElement("address-edit")]
   public class AddressTagHelper : TagHelper
   {
      [Activate]
      public ViewContext ViewContext { get; set; }
      [Activate]
      public IHtmlGenerator Generator { get; set; }

      [HtmlAttributeName("asp-for")]
      public ModelExpression For { get; set; }

      public async override void Process(TagHelperContext context, TagHelperOutput output)
      {
         var sw = new StringWriter();
         // Get viewmodel from razor
         AddressViewModel address = For.Model as AddressViewModel;
         if (address == null)
         {
            address = new AddressViewModel();
         }

         // Create a new viewData (viewbag). This will be used in a new ViewContext to define the model we want
         ViewDataDictionary viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
         {
            Model = address
         };

         // Parse the name of the element. If it has an index, put that into the viewbag. 
         var nameSplit = For.Name.Split('[', ']');
         var val = "";
         if (nameSplit.GetLength(0) > 1)
         {
            //var val = nameSplit[1];
            val = nameSplit[1];
            viewData.Add("listVal", val);
            viewData.Add("listIndex", "Addresses.Index");
         }

         // Prefix all created elements with the name.
         viewData.TemplateInfo.HtmlFieldPrefix = For.Name;

         TagBuilder addressTags = new TagBuilder("span");
         var viewName = "~/Views/Shared/_Address.cshtml";

         // Generate a viewContext with our viewData
         var viewContext = new ViewContext(ViewContext, ViewContext.View, viewData, ViewContext.TempData, sw);

         // Get the partial view
         addressTags.InnerHtml = await viewContext.RenderPartialView(viewName);

         output.Content.Append(addressTags.ToString());
         // Kill the original tag
         output.TagName = "";
      }
   }
}
