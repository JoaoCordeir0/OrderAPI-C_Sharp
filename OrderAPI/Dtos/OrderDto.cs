﻿namespace OrderAPI.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public bool Paid { get; set; }
        public double TotalValue { get; set; }
        public List<ItemOrderDto> Items { get; set; }
    }
}
