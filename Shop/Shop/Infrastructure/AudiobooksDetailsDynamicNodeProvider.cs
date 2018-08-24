using MvcSiteMapProvider;
using Shop.DAL;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Infrastructure
{
    public class AudiobooksDetailsDynamicNodeProvider : DynamicNodeProviderBase
    {
        private AudiobooksContext db = new AudiobooksContext();


        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode nodee)
        {
            var returnValue = new List<DynamicNode>();

            foreach (Audiobook audiobook in db.Audiobooks)
            {
                DynamicNode node = new DynamicNode();
                node.Title = audiobook.NameAudiobook;
                node.Key = "Audiobook_" + audiobook.AudiobookId;
                node.ParentKey = "Category_" + audiobook.CategoryId;
                node.RouteValues.Add("id", audiobook.AudiobookId);
                returnValue.Add(node);
            }
            return returnValue;
        }
    }
}