//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PizzaOrderingSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PizzaOrderingSystemEntities : DbContext
    {
        public PizzaOrderingSystemEntities()
            : base("name=PizzaOrderingSystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CustomerDetail> CustomerDetails { get; set; }
        public virtual DbSet<LoginDetail> LoginDetails { get; set; }
        public virtual DbSet<RegisterDetail> RegisterDetails { get; set; }
        public virtual DbSet<AdminDetail> AdminDetails { get; set; }
        public virtual DbSet<IntegradientDetail> IntegradientDetails { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }
        public virtual DbSet<PizzaDetail> PizzaDetails { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
    }
}
