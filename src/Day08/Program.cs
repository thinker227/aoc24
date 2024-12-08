// var input = """
// ......#....#
// ...#....0...
// ....#0....#.
// ..#....0....
// ....0....#..
// .#....A.....
// ...#........
// #......#....
// ........A...
// .........A..
// ..........#.
// ..........#.
// """.Replace('#', '.');
var input = File.ReadAllText("input.txt");

var w = input.IndexOf('\n');
var h = input.AsSpan().Count("\n") + 1;

var antennas = input.Split("\n")
    .SelectMany((line, y) => line
        .Select((c, x) => (
            pos: new Vector2(x, y),
            frequency: c)))
    .Where(v => v.frequency is not '.')
    .GroupBy(v => v.frequency);

var antinodes = antennas
    .Where(v => v.Count() >= 2)
    .SelectMany(v => v
        .Select(x => x.pos)
        .Subsets(2)
        .SelectMany(x => new[] { (a: x[0], b: x[1]), (a: x[1], b: x[0]) })
        .Select(x => x.a + x.b.VectorTo(x.a))
        .Where(x => x.X >= 0 && x.X < w && x.Y >= 0 && x.Y < h))
    .ToHashSet();

var impact = antinodes.Count;

Console.WriteLine($"Total impact: {impact}");
