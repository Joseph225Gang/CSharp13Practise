using System.Text.Json;

string webJson = JsonSerializer.Serialize(
    new { SomeValue = 42 },
    JsonSerializerOptions.Web // Defaults to camelCase naming policy.
    );
Console.WriteLine(webJson);

string sourceText = """
    Lorem ipsum dolor sit amet, consectetur adipiscing elit.
    Sed non risus. Suspendisse lectus tortor, dignissim sit amet, 
    adipiscing nec, ultricies sed, dolor. Cras elementum ultrices amet diam.
""";

// Find the most frequent word in the text.
KeyValuePair<string, int> mostFrequentWord = sourceText
    .Split(new char[] { ' ', '.', ',' }, StringSplitOptions.RemoveEmptyEntries)
    .Select(word => word.ToLowerInvariant())
    .CountBy(word => word)
    .MaxBy(pair => pair.Value);

Console.WriteLine(mostFrequentWord.Key); // amet

(string id, int score)[] data =
    [
        ("0", 42),
        ("1", 5),
        ("2", 4),
        ("1", 10),
        ("0", 25),
    ];

var aggregatedData =
    data.AggregateBy(
        keySelector: entry => entry.id,
        seed: 0,
        (totalScore, curr) => totalScore + curr.score
        );

foreach (var item in aggregatedData)
{
    Console.WriteLine(item);
}

var movies = new List<Movie>
{
    new Movie("Titanic", 1997, 4.5f),
    new Movie("The Fifth Element", 1997, 4.6f),
    new Movie("Forrest Gump", 1994, 4.3f),
    new Movie("Terminator 2", 1991, 4.7f),
    new Movie("Armageddon", 1998, 3.35f),
    new Movie("Platoon", 1986, 4),
    new Movie("My Neighbor Totoro", 1988, 5),
    new Movie("Pulp Fiction", 1994, 4.3f),
};

var countByReleaseYear = movies.CountBy(m => m.ReleaseYear);
//                              ^^^^^^^
foreach (var (year, count) in countByReleaseYear)
{
    Console.WriteLine($"{year}: [{count}]");
}

// Output
// 1997: [2]
// 1994: [2]
// 1991: [1]
// 1998: [1]
// 1986: [1]
// 1988: [1]

Console.WriteLine("------------");
var groupByReleaseYear = movies.GroupBy(x => x.ReleaseYear,
     (releaseYear, movies) => new
     {
         Year = releaseYear,
         Count = movies.Count()
     });
foreach (var item in groupByReleaseYear)
{
    Console.WriteLine($"{item.Year}: [{item.Count}]");
}

foreach (var (index, movie) in movies.Index())
//                                    ^^^^^
{
    Console.WriteLine($"{index}: [{movie.Name}]");
}

Console.ReadKey();

record Movie(string Name, int ReleaseYear, float Rating);
