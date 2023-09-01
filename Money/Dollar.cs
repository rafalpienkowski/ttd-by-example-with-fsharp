namespace Money;

public class Money
{
    
}

public class Dollar : Money, IEquatable<Dollar>
{
    private readonly int _amount;

    public Dollar(int amount)
    {
        _amount = amount;
    }

    public Dollar Times(int multiplier) => new(_amount * multiplier);

    public bool Equals(Dollar? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _amount == other._amount;
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
        return _amount;
    }
}

public class Franc : Money, IEquatable<Franc>
{
    private readonly int _amount;

    public Franc(int amount)
    {
        _amount = amount;
    }
    
    public Franc Times(int multiplier) => new(_amount * multiplier);

    public bool Equals(Franc? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _amount == other._amount;
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
        return _amount;
    }
}