﻿namespace Minha_primeira_API.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public decimal Price { get; set; }
    }
}
