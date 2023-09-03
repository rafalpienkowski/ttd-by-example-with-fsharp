namespace Money;

public class Bank
{
    private readonly Dictionary<Pair, int> _rates = new();

    public Money Reduce(IExpression source, string to) => source.Reduce(this, to);

    public void AddRate(string from, string to, int value)
    {
        _rates.Add(new Pair(from, to), value);
    }

    public int Rate(string from, string to)
    {
        return from == to ? 1 : _rates[new Pair(from, to)];
    }

    private sealed record Pair(string From, string To);
}