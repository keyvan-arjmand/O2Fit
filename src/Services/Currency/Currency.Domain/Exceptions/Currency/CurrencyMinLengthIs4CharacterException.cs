﻿namespace Currency.Domain.Exceptions.Currency;

public class CurrencyCodeLenghtException : Exception
{
    public CurrencyCodeLenghtException(string message) : base(message)
    {

    }
}