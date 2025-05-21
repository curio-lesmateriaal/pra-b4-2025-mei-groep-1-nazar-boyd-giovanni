using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRA_B4_FOTOKIOSK.models
{
    public class KioskProduct
    {
        public string? Description { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
    }
}
