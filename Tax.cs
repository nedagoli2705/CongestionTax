using System;

public class Tax
{
    public decimal Amount { get; private set; }

    public Tax(decimal amount)
    {
        Amount = amount;
    }

    public void AddAmount(decimal amount)
    {
        Amount += amount;
    }
}
