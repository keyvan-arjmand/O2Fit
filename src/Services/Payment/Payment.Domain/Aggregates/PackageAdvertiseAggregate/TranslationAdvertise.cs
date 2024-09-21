﻿using Payment.Domain.Common;

namespace Payment.Domain.Aggregates.PackageAdvertiseAggregate;

public class TranslationAdvertise:BaseEntity
{
    public TranslationAdvertise()
    {
        Id = ObjectId.GenerateNewId().ToString()!;
    }

    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
    
}