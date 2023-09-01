namespace Money;

public abstract class Money
{
    protected int Amount;
}

public class Dollar : Money, IEquatable<Dollar>
{
    public Dollar(int amount)
    {
        Amount = amount;
    }

    public Dollar Times(int multiplier) => new(Amount * multiplier);

    public bool Equals(Dollar? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Amount == other.Amount;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Dollar)obj);
    }

    public override int GetHashCode()
    {
        return Amount;
    }
}

public class Franc : Money, IEquatable<Franc>
{

    public Franc(int amount)
    {
        Amount = amount;
    }
    
    public Franc Times(int multiplier) => new(Amount * multiplier);

    public bool Equals(Franc? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Amount == other.Amount;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Franc)obj);
    }

    public override int GetHashCode()
    {
        return Amount;
    }
}