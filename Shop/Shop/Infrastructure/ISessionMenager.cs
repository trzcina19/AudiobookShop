using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure
{
    public interface ISessionMenager
    {
        T Get<T>(string key); ///Generic types
        void Set<T>(string name, T value);
        void Abandon(); //Delete item
        T TryGet<T>(string key);
    }
}
