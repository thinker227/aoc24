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

var areOrdered = updates
    .Select(x => (
        item: x,
        ordered: x
            .WindowLeft(x.Length)
            .All(x => x
                .Skip(1)
                .All(y => !rules.Contains((y, x[0]))))));

var orderedSum = areOrdered
    .Where(x => x.ordered)
    .Sum(x => int.Parse(x.item[x.item.Length / 2]));

Console.WriteLine($"Ordered sum: {orderedSum}");

var comparer = Comparer<string>.Create((a, b) =>
    rules.Contains((a, b)) ? -1 : 1);

var unorderedSum = areOrdered
    .Where(x => !x.ordered)
    .Select(x => x.item
        .Order(comparer)
        .ToList())
    .Sum(x => int.Parse(x[x.Count / 2]));

Console.WriteLine($"Unordered sum: {unorderedSum}");    
