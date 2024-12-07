var input = File.ReadAllText("input.txt");

var equations = input.Split("\n")
    .Select(x => x.Split(": "))
    .Select(x => new Equation(
        ulong.Parse(x[0]),
        x[1].Split(" ")
            .Select(ulong.Parse)
            .ToArray()));

var sum = equations
    .Where(IsSolvable)
    .Aggregate(0ul, (a, b) => a + b.TestValue);

Console.WriteLine($"Sum: {sum}");

var concatSum = equations
    .Where(IsSolvableConcat)
    .Aggregate(0ul, (a, b) => a + b.TestValue);

Console.WriteLine($"Concat sum: {concatSum}");

static bool IsSolvable(Equation equation)
{
    return Test(equation.TestValue, equation.Nums[0], equation.Nums.AsSpan(1));

    static bool Test(ulong testValue, ulong value, ReadOnlySpan<ulong> nums)
    {
        if (nums.IsEmpty) return value == testValue;
        if (value > testValue) return false;

        var other = nums[0];
        var next = nums[1..];
        return
            Test(testValue, value * other, next) ||
            Test(testValue, value + other, next);
    }
}

static bool IsSolvableConcat(Equation equation)
{
    return Test(equation.TestValue, equation.Nums[0], equation.Nums.AsSpan(1));

    static bool Test(ulong testValue, ulong value, ReadOnlySpan<ulong> nums)
    {
        if (nums.IsEmpty) return value == testValue;
        if (value > testValue) return false;

        var other = nums[0];
        var next = nums[1..];
        return
            Test(testValue, value * other, next) ||
            Test(testValue, ulong.Parse($"{value}{other}"), next) ||
            Test(testValue, value + other, next);
    }
}

readonly record struct Equation(ulong TestValue, ulong[] Nums);
