namespace Money;

public record Money(int Amount, string Currency) : IExpression
{
    public Money Reduce(Bank bank, string to)
    {
        var rate = bank.Rate(Currency, to);
        return new Money(Amount / rate, to);
    }

    public IExpression Times(int multiplier) => this with { Amount = Amount * multiplier };
    public IExpression Plus(IExpression added) => new Sum(this, added);

    public static Money Dollar(int amount) => new(amount, "USD");
    public static Money Franc(int amount) => new(amount, "CHF");

    public override string ToString() => $"{Amount} {Currency}";
}