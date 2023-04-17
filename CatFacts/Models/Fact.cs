namespace CatFacts.Models
{
    public class Fact
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public User user { get; set; }
        public int UpVotes { get; set; }
    }
}
