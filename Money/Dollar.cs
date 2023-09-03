namespace Money;

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