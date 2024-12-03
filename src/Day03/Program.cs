using Pidgin;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;

// var input = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
// var input = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
var input = File.ReadAllText("input.txt");

var mul =
    from a in String("mul(").Then(Num).Before(String(","))
    from b in Num.Before(String(")"))
    select a * b;

var p1 = Try(mul)
    .Or(Any.WithResult(0))
    .Many()
    .ParseOrThrow(input)
    .Sum();

Console.WriteLine($"Part 1: {p1}");

var instr = OneOf(
    Try(mul).Map<Instr?>(x => new Instr.Mul(x)),
    Try(String("do()")).WithResult<Instr?>(new Instr.Do()),
    Try(String("don't()")).WithResult<Instr?>(new Instr.Dont()),
    Any.WithResult<Instr?>(null));

var p2 = instr
    .Many()
    .ParseOrThrow(input)
    .Aggregate(
        (sum: 0, enabled: true),
        (x, instr) => (instr, x.enabled) switch
        {
            (Instr.Mul(var val), true) => (x.sum + val, true),
            (Instr.Do, _) => (x.sum, true),
            (Instr.Dont, _) => (x.sum, false),
            _ => x
        })
    .sum;

Console.WriteLine($"Part 2: {p2}");

record Instr
{
    public record Mul(int Val) : Instr;
    public record Do : Instr;
    public record Dont : Instr;
}
