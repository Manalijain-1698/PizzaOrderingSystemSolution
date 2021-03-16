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

    public partial class IntegradientDetail
    {
        [Display(Name = "Ingredient Id")]
        public int IngradientId { get; set; }

        [Display(Name = "Ingredient Name")]
        public string IngradientName { get; set; }

        [Display(Name = "Pizza Id")]
        public Nullable<int> PizzaId { get; set; }

        [Display(Name = "Quantity")]
        public Nullable<int> Quantity { get; set; }

        [Display(Name = "Availability Status")]
        public Nullable<int> AvaliabiltyStatus { get; set; }

        
        public virtual PizzaDetail PizzaDetail { get; set; }
    }
}
