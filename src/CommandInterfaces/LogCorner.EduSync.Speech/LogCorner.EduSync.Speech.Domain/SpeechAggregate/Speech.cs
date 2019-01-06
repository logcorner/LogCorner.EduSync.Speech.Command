namespace LogCorner.EduSync.Speech.Domain.SpeechAggregate
{
    public class Speech
    {
        // Here, we have an anemic domain model, we will enrich it when implementing the domain
        private string title;

        private string urlValue;
        private string description;
        private string type;

        public Speech(string title, string urlValue, string description, string type)
        {
            this.title = title;
            this.urlValue = urlValue;
            this.description = description;
            this.type = type;
        }
    }
}