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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PaymentDetail
    {
        [Display(Name = "Payment Id")]
        public int PaymentId { get; set; }

        public Nullable<int> OrderId { get; set; }

        [Display(Name = "Payment Mode")]
        public string Paymentmode { get; set; }

        [Display(Name ="Payment Status")]
        public Nullable<int> PaymentStatus { get; set; }
    
        public virtual OrderDetail OrderDetail { get; set; }
    }
}
