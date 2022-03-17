using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRS_System.Models.Data
{
    public class ProductDataModel
    {
        public string NameProduct { get; set; }
        public int AmtProduct { get; set; }
        public string Unit { get; set; }
        public double Price_Per_Piece { get; set;}
        public string status { get; set; }
    }
}
