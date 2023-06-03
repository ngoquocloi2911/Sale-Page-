using _16t1021087.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16t1021087.DataLayers
{
    public interface IStaticDAL
    {
        IList<Order> list();
    }
}
