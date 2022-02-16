using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstReact.Core.Models.Dtos
{
    public class CarSale
    {
        public string DealNumber { get; set; }
        public string CustomerName { get; set; }
        public string DealershipName { get; set; }
        public string Vehicle { get; set; }
        public string Price { get; set; }
        public string Date { get; set; }
    }
}
