namespace Money;

public record Sum(IExpression Augend, IExpression Added) : IExpression
{
    public Money Reduce(Bank bank, string to)
    {
        var amount = Augend.Reduce(bank, to).Amount + Added.Reduce(bank, to).Amount;
        return new Money(amount, to);
    }

    public IExpression Plus(IExpression added) => new Sum(this, added);
    public IExpression Minus(IExpression subtracted)
    {
        return null!;
    }

    public IExpression Times(int multiplier) => new Sum(Augend.Times(multiplier), Added.Times(multiplier));
}