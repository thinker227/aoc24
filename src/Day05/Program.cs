var input = File.ReadAllText("input.txt");

var split = input.Split("\n\n");

var rules = split[0]
    .Split("\n")
    .Select(x => x.Split("|"))
    .Select(x => (x[0], x[1]))
    .ToHashSet();

var updates = split[1]
    .Split("\n")
    .Select(x => x
        .Split(","))
    .ToList();

var comparer = Comparer<string>.Create((a, b) =>
    rules.Contains((a, b)) ? -1 : 1);

var vals = updates
    .Select(x => (
        raw: x,
        ordered: x.Order(comparer).ToList()))
    .Select(x => (
        ordered: x.ordered,
        isOrdered: x.raw.SequenceEqual(x.ordered)))
    .ToList();

var orderedSum = vals
    .Where(x => x.isOrdered)
    .Sum(x => int.Parse(x.ordered[x.ordered.Count / 2]));

var unorderedSum = vals
    .Where(x => !x.isOrdered)
    .Sum(x => int.Parse(x.ordered[x.ordered.Count / 2]));

Console.WriteLine($"Ordered sum: {orderedSum}");
Console.WriteLine($"Unordered sum: {unorderedSum}");    
