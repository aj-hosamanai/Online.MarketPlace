using Domain.Online.MarketPlace.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Online.MarketPlace.Model
{
   public  class Order: IAggregateRoot
    {
        [Key]
        public int OrderId { get; set; }
        public string Orderstatus { get; set; }
        public int CatalogueId { get; set; }
        public string CatalogueName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OldUnitPrice { get; set; }
        public int OrderQuantity { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

    }
}
