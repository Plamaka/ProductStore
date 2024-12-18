﻿namespace ProductStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderPlaced { get; set; }

        public DateTime? OrderFulfulled { get; set; }

        public string? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public ICollection<OrderDetail> Orderdetails { get; set; } = new List<OrderDetail>();



    }
}