﻿namespace UniqueDraw.Domain.Models.Response;

public class ProductResponseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid ClientId { get; set; }
}
