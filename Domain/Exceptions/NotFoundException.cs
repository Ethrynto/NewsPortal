﻿namespace Domain.Exceptions;

public class NotFoundException : Exception
{
    private const string DefaultMessage = "Resource not found.";

    public NotFoundException()
        : base(DefaultMessage)
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

