using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EntityFrameworkExample.Shared
{
   public interface IIdentifiable
   {
      int Id { get; set; }
   }

   public class MapperResolver<TSourceParent, TSource, TDest> : IValueResolver
      where TSource : IIdentifiable    // allows us to use Id as a parameter. 
      where TDest : IIdentifiable      //  If needed to use in entity, add "class"
   {
      private Expression<Func<TSourceParent, ICollection>> sourceMember;

      public MapperResolver(Expression<Func<TSourceParent, ICollection>> sourceMember)
      {
         this.sourceMember = sourceMember;
      }

      public ResolutionResult Resolve(ResolutionResult source)
      {
         bool newDestCollection = false;

         //get source collection
         var sourceProperty = source.Value.GetType().GetProperty(source.Context.MemberName);

         ICollection<TSource> sourceCollection = (ICollection<TSource>)sourceProperty.GetValue(source.Value);

         if (sourceCollection == null)
         {
            sourceCollection = new List<TSource>();
         }

         if (source.Context.DestinationValue != null)
         {
            var destinationProperty = source.Context.DestinationValue.GetType().GetProperty(source.Context.MemberName);
            ICollection<TDest> destinationCollection = (ICollection<TDest>)destinationProperty.GetValue(source.Context.DestinationValue);

            if (destinationCollection != null)
            {
               //delete entities that are not in source collection
               var sourceIds = sourceCollection.Select(i => i.Id).ToList();
               foreach (var item in destinationCollection.ToList())
               {
                  if (!sourceIds.Contains(item.Id))
                  {
                     destinationCollection.Remove(item);
                  }
               }
               //map entities that are in source collection
               foreach (var sourceItem in sourceCollection)
               {
                  var originalItem = destinationCollection.Where(o => o.Id == sourceItem.Id).SingleOrDefault();
                  if ((originalItem != null) && (originalItem.Id > 0))
                  {
                     //Map on top of existing item
                     Mapper.Map(sourceItem, originalItem);
                  }
                  else
                  {
                     // Create new item in collection
                     destinationCollection.Add(Mapper.Map<TDest>(sourceItem));
                  }
               }
               return source.New(destinationCollection, source.Context.DestinationType);

            }
            else
            {
               newDestCollection = true;
            }

         }
         //we are mapping to new collection of destination items
         else
         {
            newDestCollection = true;
         }

         if (newDestCollection)
         {
            // If we don't have a destination collection, create it
            var value = new HashSet<TDest>();
            // Map items from source
            foreach (var item in sourceCollection)
            {
               value.Add(Mapper.Map<TDest>(item));
            }
            //create new result mapping context
            source = source.New(value, source.Context.DestinationType);
         }
         return source;

      }
   }
}
