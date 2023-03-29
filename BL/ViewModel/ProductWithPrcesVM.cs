using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class ProductWithPrcesVM
    {
        public ProductWithPrcesVM(string prodname, double prodprice)
        {
            this.prodname = prodname;
            this.prodprice = prodprice;
        }

        public string prodname { get; set; }
        public double prodprice { get; set; }

    }
}
