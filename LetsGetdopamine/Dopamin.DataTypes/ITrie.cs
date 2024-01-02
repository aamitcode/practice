using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopamin.DataTypes
{
    public interface ITrie<T> where T : IComparable
    {
        public void Register(T[] components, T data);
        public T Get(T[] components);
    }
}
