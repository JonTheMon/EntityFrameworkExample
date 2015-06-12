using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkExample
{
    public static class ExtensionMethods
   {
      public async static Task<string> RenderPartialView(this ViewContext context, string viewName)
      {
         ICompositeViewEngine viewEngine = context.HttpContext.RequestServices.GetRequiredService<ICompositeViewEngine>();

         ViewEngineResult viewResult = viewEngine.FindPartialView(context, viewName);

         await viewResult.View.RenderAsync(context);

         return context.Writer.ToString();

      }
   }
}
