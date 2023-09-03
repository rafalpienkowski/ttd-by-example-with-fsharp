namespace Money;

public interface IExpression
{
    Money Reduce(Bank bank, string to);
    IExpression Plus(IExpression added);
    IExpression Times(int multiplier);
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
    public readonly IExpression Added;
    public readonly IExpression Augend;

    public Sum(IExpression augend, IExpression added)
    {
        Augend = augend;
        Added = added;
    }

    public Money Reduce(Bank bank, string to)
    {
        var amount = Augend.Reduce(bank, to).Amount + Added.Reduce(bank, to).Amount;
        return new Money(amount, to);
    }

    public IExpression Plus(IExpression added) => new Sum(this, added);

    public IExpression Times(int multiplier) => new Sum(Augend.Times(multiplier), Added.Times(multiplier));
}

public record Money(int Amount, string Currency) : IExpression
{
    public Money Reduce(Bank bank, string to)
    {
        var rate = bank.Rate(Currency, to);
        return new Money(Amount / rate, to);
    }

    public IExpression Times(int multiplier) => new Money(Amount * multiplier, Currency);

    public IExpression Plus(IExpression added) => new Sum(this, added);

    public static Money Dollar(int amount) => new(amount, "USD");
    public static Money Franc(int amount) => new(amount, "CHF");

    public override string ToString() => $"{Amount} {Currency}";
}