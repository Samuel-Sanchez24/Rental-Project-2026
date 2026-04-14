using System;
namespace Rental_Project_2026.Domain.Exceptions;

public class BusinessRulesException : Exception
{
	public BusinessRulesException(string message) : base(message)
	{
    }
}
