// var input = File.ReadAllText("sample.txt");
var input = File.ReadAllText("input.txt");

var nums = input
    .Split('\n')
    .Select(x => x.Split("   "))
    .Select(x => (
        l: int.Parse(x[0]),
        r: int.Parse(x[1])))
    .ToList();

var min = nums.Select(x => x.l).Order();
var max = nums.Select(x => x.r).Order();

var diffs = min.Zip(max)
    .Sum(x => int.Abs(x.First - x.Second));

Console.WriteLine($"Total distances: {diffs}");

var appears = nums
    .GroupBy(x => x.r, x => x.r)
    .ToDictionary(x => x.Key, x => x.Count());

var similarity = nums
    .Sum(x => x.l * appears.GetValueOrDefault(x.l, 0));

Console.WriteLine($"Similarity score: {similarity}");
