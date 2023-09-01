namespace Currency;

public interface IExpression
{
}

public class Bank
{
    public Money Reduce(IExpression source, string to)
    {
        if (source is Money money)
        {
            return money;
        }
        var sum = (Sum)source;
        return sum.Reduce(to);
    }
}

public class Sum : IExpression
{
    public Money Added;
    public Money Augend;

    public Sum(Money augend, Money added)
    {
        Augend = augend;
        Added = added;
    }

    public Money Reduce(string to)
    {
        var amount = Augend.Amount + Added.Amount;
        return new Money(amount, to);
    }
}

public class Money: IEquatable<Money>, IEqualityComparer<Money>, IExpression
{
    public int Amount { get; }
    public string Currency { get; }

    public Money(int amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public Money Times(int multiplier) => new(Amount * multiplier, Currency);

    public IExpression Plus(Money added) => new Sum(this, added);

    public static Money Dollar(int amount) => new(amount, "USD");
    public static Money Franc(int amount) => new(amount, "CHF");

    public override string ToString() => $"{Amount} {Currency}";

    public bool Equals(Money? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Amount == other.Amount && Currency == other.Currency;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return Equals((Money)obj) && Currency == ((Money)obj).Currency;
    }

    public override int GetHashCode()
    {
        return Amount.GetHashCode() * Currency.GetHashCode();
    }

    public bool Equals(Money? x, Money? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        return x.Amount == y.Amount && x.Currency == y.Currency;
    }

    public int GetHashCode(Money obj)
    {
        return obj.Amount;
    }
}