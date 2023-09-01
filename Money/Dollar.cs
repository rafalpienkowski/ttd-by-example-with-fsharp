namespace Currency;

public class Money: IEquatable<Money>, IEqualityComparer<Money>
{
    protected readonly int Amount;
    protected readonly string _currency;

    public Money(int amount, string currency)
    {
        Amount = amount;
        _currency = currency;
    }

    public Money Times(int multiplier) => new Money(Amount * multiplier, Currency);
    public string Currency => _currency;

    public static Dollar Dollar(int amount) => new(amount, "USD");
    public static Franc Franc(int amount) => new(amount, "CHF");

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

public class Dollar : Money
{
    public Dollar(int amount, string currency) : base(amount, currency)
    {
    }
}

public class Franc : Money
{
    public Franc(int amount, string currency) : base(amount, currency)
    {
    }
}