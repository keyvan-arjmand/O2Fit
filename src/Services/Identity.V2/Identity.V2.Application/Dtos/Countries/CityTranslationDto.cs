﻿namespace Identity.V2.Application.Dtos.Countries;

public class CityTranslationDto : IDto
{
    public string Id { get; set; } = string.Empty;
    public string Persian { get; set; } = string.Empty;
    public string English { get; set; } = string.Empty;
    public string Arabic { get; set; } = string.Empty;
}