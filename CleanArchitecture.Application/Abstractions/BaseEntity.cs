﻿namespace CleanArchitecture.Application.Abstract;

public abstract class BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}
