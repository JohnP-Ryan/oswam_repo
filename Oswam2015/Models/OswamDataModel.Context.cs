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
        public virtual DbSet<Preference> Preferences { get; set; }
        public virtual DbSet<LocalInventory> LocalInventories { get; set; }
        public virtual DbSet<Shelf> Shelves { get; set; }
    
        public virtual ObjectResult<Nullable<double>> orderGenerator(Nullable<int> orderSize)
        {
            var orderSizeParameter = orderSize.HasValue ?
                new ObjectParameter("orderSize", orderSize) :
                new ObjectParameter("orderSize", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<double>>("orderGenerator", orderSizeParameter);
        }
    
        public virtual int SetPreferenceValue(Nullable<int> preferenceID, Nullable<int> newPreferenceValue)
        {
            var preferenceIDParameter = preferenceID.HasValue ?
                new ObjectParameter("PreferenceID", preferenceID) :
                new ObjectParameter("PreferenceID", typeof(int));
    
            var newPreferenceValueParameter = newPreferenceValue.HasValue ?
                new ObjectParameter("NewPreferenceValue", newPreferenceValue) :
                new ObjectParameter("NewPreferenceValue", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SetPreferenceValue", preferenceIDParameter, newPreferenceValueParameter);
        }
    
        public virtual ObjectResult<GetAllPreferences_Result> GetAllPreferences()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllPreferences_Result>("GetAllPreferences");
        }
    
        public virtual ObjectResult<Nullable<int>> GetPreferenceValue(string preferenceKey)
        {
            var preferenceKeyParameter = preferenceKey != null ?
                new ObjectParameter("PreferenceKey", preferenceKey) :
                new ObjectParameter("PreferenceKey", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("GetPreferenceValue", preferenceKeyParameter);
        }
    
        public virtual ObjectResult<GetAllShelves_Result> GetAllShelves()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllShelves_Result>("GetAllShelves");
        }
    
        public virtual int DeleteShelf(Nullable<int> iD, Nullable<int> locationX, Nullable<int> locationY)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            var locationXParameter = locationX.HasValue ?
                new ObjectParameter("LocationX", locationX) :
                new ObjectParameter("LocationX", typeof(int));
    
            var locationYParameter = locationY.HasValue ?
                new ObjectParameter("LocationY", locationY) :
                new ObjectParameter("LocationY", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteShelf", iDParameter, locationXParameter, locationYParameter);
        }
    
        public virtual int CreateShelf(Nullable<int> locationX, Nullable<int> locationY, Nullable<decimal> availableVolume, Nullable<decimal> availableWeight, Nullable<bool> cellType)
        {
            var locationXParameter = locationX.HasValue ?
                new ObjectParameter("LocationX", locationX) :
                new ObjectParameter("LocationX", typeof(int));
    
            var locationYParameter = locationY.HasValue ?
                new ObjectParameter("LocationY", locationY) :
                new ObjectParameter("LocationY", typeof(int));
    
            var availableVolumeParameter = availableVolume.HasValue ?
                new ObjectParameter("availableVolume", availableVolume) :
                new ObjectParameter("availableVolume", typeof(decimal));
    
            var availableWeightParameter = availableWeight.HasValue ?
                new ObjectParameter("availableWeight", availableWeight) :
                new ObjectParameter("availableWeight", typeof(decimal));
    
            var cellTypeParameter = cellType.HasValue ?
                new ObjectParameter("CellType", cellType) :
                new ObjectParameter("CellType", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateShelf", locationXParameter, locationYParameter, availableVolumeParameter, availableWeightParameter, cellTypeParameter);
        }
    
        public virtual ObjectResult<GetAllCells_Result> GetAllCells()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllCells_Result>("GetAllCells");
        }
    
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
    
        public virtual ObjectResult<GetOrderProducts_Result> GetOrderProducts(string orderID)
        {
            var orderIDParameter = orderID != null ?
                new ObjectParameter("OrderID", orderID) :
                new ObjectParameter("OrderID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetOrderProducts_Result>("GetOrderProducts", orderIDParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> GetOrderTotalPrice(string orderID)
        {
            var orderIDParameter = orderID != null ?
                new ObjectParameter("OrderID", orderID) :
                new ObjectParameter("OrderID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("GetOrderTotalPrice", orderIDParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> GetOrderTotalVolume(string orderID)
        {
            var orderIDParameter = orderID != null ?
                new ObjectParameter("OrderID", orderID) :
                new ObjectParameter("OrderID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("GetOrderTotalVolume", orderIDParameter);
        }
    
        public virtual ObjectResult<Nullable<decimal>> GetOrderTotalWeight(string orderID)
        {
            var orderIDParameter = orderID != null ?
                new ObjectParameter("OrderID", orderID) :
                new ObjectParameter("OrderID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<decimal>>("GetOrderTotalWeight", orderIDParameter);
        }
    
        public virtual ObjectResult<GetOrderCount_Result> GetOrderCount()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetOrderCount_Result>("GetOrderCount");
        }
    }
}
