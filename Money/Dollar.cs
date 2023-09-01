namespace Money;

public abstract class Money: IEquatable<Money>, IEqualityComparer<Money>
{
    protected readonly int Amount;

    protected Money(int amount)
    {
        Amount = amount;
    }

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
    public Dollar(int amount) : base(amount)
    {
    }

    public Dollar Times(int multiplier) => new(Amount * multiplier);
}

public class Franc : Money
{

    public Franc(int amount) : base(amount)
    {
    }
    
    public Franc Times(int multiplier) => new(Amount * multiplier);
}