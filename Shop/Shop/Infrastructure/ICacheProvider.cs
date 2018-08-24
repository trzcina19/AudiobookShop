using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure
{
  public interface ICacheProvider
    {
        object Get(string key); //Downloading data from cache
        void Set(string key, object data, int cacheTime); // Cached data entry
        bool IsSet(string key); // Checking if something is in cached
        void Invalidate(string key); // Deleting data
    }
}
