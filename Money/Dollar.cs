namespace Currency;

public class Money: IEquatable<Money>, IEqualityComparer<Money>
{
    private readonly int _amount;
    public string Currency { get; }

    public Money(int amount, string currency)
    {
        _amount = amount;
        Currency = currency;
    }

    public Money Times(int multiplier) => new Money(Amount * multiplier, Currency);
    public string Currency => _currency;
    public Money Times(int multiplier) => new(_amount * multiplier, Currency);

    public static Money Dollar(int amount) => new(amount, "USD");
    public static Money Franc(int amount) => new(amount, "CHF");

    public override string ToString() => $"{_amount} {Currency}";

    public bool Equals(Money? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _amount == other._amount && Currency == other.Currency;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return Equals((Money)obj) && Currency == ((Money)obj).Currency;
    }

    public override int GetHashCode()
    {
        return _amount.GetHashCode() * Currency.GetHashCode();
    }

    public bool Equals(Money? x, Money? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        return x._amount == y._amount && x.Currency == y.Currency;
    }

    public int GetHashCode(Money obj)
    {
        return obj._amount;
    }
}