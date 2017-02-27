﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Oswam2015.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class OSWAM_DataEntities : DbContext
    {
        public OSWAM_DataEntities()
            : base("name=OSWAM_DataEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
    
        public virtual ObjectResult<GetInventoryProducts_Result> GetInventoryProducts(string iD, string searchName, Nullable<int> weightLow, Nullable<int> weightHigh, Nullable<int> priceLow, Nullable<int> priceHigh)
        {
            var iDParameter = iD != null ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(string));
    
            var searchNameParameter = searchName != null ?
                new ObjectParameter("SearchName", searchName) :
                new ObjectParameter("SearchName", typeof(string));
    
            var weightLowParameter = weightLow.HasValue ?
                new ObjectParameter("WeightLow", weightLow) :
                new ObjectParameter("WeightLow", typeof(int));
    
            var weightHighParameter = weightHigh.HasValue ?
                new ObjectParameter("WeightHigh", weightHigh) :
                new ObjectParameter("WeightHigh", typeof(int));
    
            var priceLowParameter = priceLow.HasValue ?
                new ObjectParameter("PriceLow", priceLow) :
                new ObjectParameter("PriceLow", typeof(int));
    
            var priceHighParameter = priceHigh.HasValue ?
                new ObjectParameter("PriceHigh", priceHigh) :
                new ObjectParameter("PriceHigh", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetInventoryProducts_Result>("GetInventoryProducts", iDParameter, searchNameParameter, weightLowParameter, weightHighParameter, priceLowParameter, priceHighParameter);
        }
    
        public virtual ObjectResult<Nullable<double>> orderGenerator(Nullable<int> orderSize)
        {
            var orderSizeParameter = orderSize.HasValue ?
                new ObjectParameter("orderSize", orderSize) :
                new ObjectParameter("orderSize", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<double>>("orderGenerator", orderSizeParameter);
        }
    }
}
