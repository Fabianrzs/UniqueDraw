﻿namespace UniqueDraw.Domain.Exceptions;

public class BusinessRuleViolationException(string message) : DomainException(message)
{
}
