namespace Currency;

public abstract class Money: IEquatable<Money>, IEqualityComparer<Money>
{
    protected readonly int Amount;

    protected Money(int amount)
    {
        Amount = amount;
    }
    
    public abstract Money Times(int multiplier);
    public abstract string Currency { get; }

    public static Dollar Dollar(int amount) => new(amount);
    public static Franc Franc(int amount) => new(amount);

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
    private readonly string _currency;
    
    public Dollar(int amount) : base(amount)
    {
        _currency = "USD";
    }

    public override Money Times(int multiplier) => new Dollar(Amount * multiplier);
    public override string Currency => _currency;
}

public class Franc : Money
{
    private readonly string _currency;
    
    public Franc(int amount) : base(amount)
    {
        _currency = "CHF";
    }
    
    public override Money Times(int multiplier) => new Franc(Amount * multiplier);
    public override string Currency => _currency;
}