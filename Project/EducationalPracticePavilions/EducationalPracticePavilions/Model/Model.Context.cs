﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EducationalPracticePavilions.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PavilionsBase : DbContext
    {
        private static PavilionsBase context_;
        public PavilionsBase()
            : base("name=PavilionsBase")
        {
        }
        public static PavilionsBase GetContext()
        {
            if (context_ == null)
                context_ = new PavilionsBase();
            return context_;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Mall> Malls { get; set; }
        public virtual DbSet<Pavilion> Pavilions { get; set; }
        public virtual DbSet<Rent> Rents { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<StatePavilion> StatePavilions { get; set; }
        public virtual DbSet<StatusEmployee> StatusEmployees { get; set; }
        public virtual DbSet<StatusMall> StatusMalls { get; set; }
        public virtual DbSet<StatusPavilion> StatusPavilions { get; set; }
        public virtual DbSet<StatusRent> StatusRents { get; set; }
        public virtual DbSet<Tenant> Tenants { get; set; }
    
        public virtual int AddPavilionToMall(Nullable<int> mallId, string pavilionName, Nullable<int> floor, Nullable<int> statusId, Nullable<double> square, Nullable<double> cost, Nullable<int> pavilionCount, Nullable<double> valueAddedFactor)
        {
            var mallIdParameter = mallId.HasValue ?
                new ObjectParameter("MallId", mallId) :
                new ObjectParameter("MallId", typeof(int));
    
            var pavilionNameParameter = pavilionName != null ?
                new ObjectParameter("PavilionName", pavilionName) :
                new ObjectParameter("PavilionName", typeof(string));
    
            var floorParameter = floor.HasValue ?
                new ObjectParameter("Floor", floor) :
                new ObjectParameter("Floor", typeof(int));
    
            var statusIdParameter = statusId.HasValue ?
                new ObjectParameter("StatusId", statusId) :
                new ObjectParameter("StatusId", typeof(int));
    
            var squareParameter = square.HasValue ?
                new ObjectParameter("Square", square) :
                new ObjectParameter("Square", typeof(double));
    
            var costParameter = cost.HasValue ?
                new ObjectParameter("Cost", cost) :
                new ObjectParameter("Cost", typeof(double));
    
            var pavilionCountParameter = pavilionCount.HasValue ?
                new ObjectParameter("PavilionCount", pavilionCount) :
                new ObjectParameter("PavilionCount", typeof(int));
    
            var valueAddedFactorParameter = valueAddedFactor.HasValue ?
                new ObjectParameter("ValueAddedFactor", valueAddedFactor) :
                new ObjectParameter("ValueAddedFactor", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddPavilionToMall", mallIdParameter, pavilionNameParameter, floorParameter, statusIdParameter, squareParameter, costParameter, pavilionCountParameter, valueAddedFactorParameter);
        }
    
        public virtual int DeletePavilion(Nullable<int> pavilionId)
        {
            var pavilionIdParameter = pavilionId.HasValue ?
                new ObjectParameter("PavilionId", pavilionId) :
                new ObjectParameter("PavilionId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeletePavilion", pavilionIdParameter);
        }
    }
}
