namespace Currency;

public interface IExpression
{
    Money Reduce(Bank bank, string to);
}

public class Bank
{
    private Dictionary<Pair, int> _rates = new();

    public Money Reduce(IExpression source, string to) => source.Reduce(this, to);

    public void AddRate(string from, string to, int value)
    {
        _rates.Add(new Pair(from, to), value);
    }

    public int Rate(string from, string to)
    {
        return from == to ? 1 : _rates[new Pair(from, to)];
    }

    private record Pair(string From, string To);
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

    public Money Reduce(Bank bank, string to)
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

    public Money Reduce(Bank bank, string to)
    {
        var rate = bank.Rate(Currency, to);
        return new Money(Amount / rate, to);
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