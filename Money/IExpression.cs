namespace Money;

public interface IExpression
{
    Money Reduce(Bank bank, string to);
    IExpression Plus(IExpression added);
    IExpression Times(int multiplier);
}