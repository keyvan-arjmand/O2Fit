﻿namespace Payment.Web.Models;

public class BaseApi
{
    public int StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
}