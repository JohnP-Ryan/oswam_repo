using System;
using System.Collections.Generic;

namespace OSWAM
{
    public partial class Product
    {
        public string Id { get; set; }
        public string ItemName { get; set; }
        public decimal? DimLength { get; set; }
        public decimal? DimWidth { get; set; }
        public decimal? DimHeight { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Price { get; set; }
        public short? Quantity { get; set; }
    }
}
