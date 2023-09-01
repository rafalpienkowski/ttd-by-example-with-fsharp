namespace Currency;

public abstract class Money: IEquatable<Money>, IEqualityComparer<Money>
{
    protected readonly int Amount;

    protected Money(int amount)
    {
        Amount = amount;
    }

    public static Currency Dollar(int amount) => new(amount);
    public static Franc Franc(int amount) => new(amount);

    public abstract Money Times(int multiplier);

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

public class Currency : Money
{
    public Currency(int amount) : base(amount)
    {
    }

    public override Money Times(int multiplier) => new Currency(Amount * multiplier);
}

public class Franc : Money
{

    public Franc(int amount) : base(amount)
    {
    }
    
    public override Money Times(int multiplier) => new Franc(Amount * multiplier);
}