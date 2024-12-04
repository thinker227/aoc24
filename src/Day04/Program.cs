// var input = """
// MMMSXXMASM
// MSAMXMSMSA
// AMXSXMAAMM
// MSAMASMSMX
// XMASAMXAMM
// XXAMMXXAMA
// SMSMSASXSS
// SAXAMASAAA
// MAMMMXMMMM
// MXMXAXMASX
// """;
var input = File.ReadAllText("input.txt");

var w = input.IndexOf('\n');
var h = input.AsSpan().Count('\n') + 1;

var clean = input.Replace("\n", "");

var xmases = clean
    .Index()
    .Where(v => v.Item is 'X' or 'S')
    .SelectMany(v => Searches(v.Index)
        .Select(a => a
            .Select(x => clean[v.Index + x])
            .ToArray()))
    .Where(l => l is ['X', 'M', 'A', 'S'] or ['S', 'A', 'M', 'X'])
    .Count();

Console.WriteLine($"xmases: {xmases}");

var xMases = clean
    .Index()
    .Where(v =>
    {
        var x = v.Index % w;
        var y = v.Index / h;
        return v.Item is 'A'
            && x <= w - 2
            && x >= 1
            && y <= h - 2
            && y >= 1;
    })
    .Where(v => (
            (clean[v.Index - w - 1], clean[v.Index + w + 1]),
            (clean[v.Index - w + 1], clean[v.Index + w - 1]))
        is (
            ('M', 'S') or ('S', 'M'),
            ('M', 'S') or ('S', 'M')))
    .Count();

Console.WriteLine($"x-mases: {xMases}");

IEnumerable<int[]> Searches(int index)
{
    var x = index % w;
    var y = index / h;

    if (x <= w - 4) yield return [0, 1, 2, 3];
    if (y <= h - 4) yield return [0, w, w * 2, w * 3];
    if (x <= w - 4 && y <= h - 4) yield return [0, w + 1, w * 2 + 2, w * 3 + 3];
    if (x >= 3 && y <= h - 4) yield return [0, w - 1, w * 2 - 2, w * 3 - 3];
}
