using CatFacts.Models;
using CsvHelper;
using Newtonsoft.Json;
using System.Globalization;

namespace CatFacts.Services
{
    public class CatFactService : ICatFactService
    {
        public async Task<Dictionary<string, int>?> ReadAllFacts(string inputFile)
        {
            if (!File.Exists(inputFile))
            {
                return null;
            }

            string allFactsText = await File.ReadAllTextAsync(inputFile);

            var factsList = JsonConvert.DeserializeObject<List<Fact>>(allFactsText);
            var userUpvoteCollection = new Dictionary<string, int>();

            if (factsList != null)
            {
                var userUpVotes = factsList.Select(x => new { x.user, x.UpVotes }).ToList();

                foreach (var x in userUpVotes)
                {
                    var keyName = $"{x.user.FullName.First} {x.user.FullName.Last}";
                    if (userUpvoteCollection.ContainsKey(keyName))
                    {
                        userUpvoteCollection[keyName] += x.UpVotes ;
                    }
                    else
                    {
                        userUpvoteCollection.Add(keyName, x.UpVotes);
                    }
                }
            }

            var sortedCollection = userUpvoteCollection.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            WriteDataToCSVFile(sortedCollection);

            return sortedCollection;
        }

        private async void WriteDataToCSVFile(Dictionary<string, int> usersCollection)
        {
            using (var writer = new StreamWriter("TotalVotes.csv"))
            {
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteField("user");
                    csvWriter.WriteField("totalVotes");
                    csvWriter.NextRecord();

                    csvWriter.WriteRecords(usersCollection);
                }
            }
        }
    }
}
