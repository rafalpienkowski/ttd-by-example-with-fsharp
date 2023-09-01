namespace Currency;

public abstract class Money: IEquatable<Money>, IEqualityComparer<Money>
{
    protected readonly int Amount;
    protected readonly string _currency;

    protected Money(int amount, string currency)
    {
        Amount = amount;
        _currency = currency;
    }
    
    public abstract Money Times(int multiplier);
    public string Currency => _currency;

    public static Dollar Dollar(int amount) => new(amount, "USD");
    public static Franc Franc(int amount) => new(amount, "CHF");

    public bool Equals(Money? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Amount == other.Amount;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Money)obj);
    }

    public override int GetHashCode()
    {
        return Amount;
    }

    public bool Equals(Money? x, Money? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Amount == y.Amount;
    }

    public int GetHashCode(Money obj)
    {
        return obj.Amount;
    }
}

public class Dollar : Money
{
    internal Dollar(int amount, string currency) : base(amount, currency)
    {
    }

    public override Money Times(int multiplier) => Dollar(Amount * multiplier);
}

public class Franc : Money
{
    internal Franc(int amount, string currency) : base(amount, currency)
    {
    }
    
    public override Money Times(int multiplier) => Franc(Amount * multiplier);
}