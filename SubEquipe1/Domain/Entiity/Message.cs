namespace SubEquipe1.Domain.Entiity
{
    public class Message
    {
        public Message(
            string question,
            string team,
            string answer)
        {
            Question = question;
            Team = team;
            Answer = answer;
        }

        public string Question { get; private set; }
        public string Team { get; private set; }
        public string Answer { get; private set; }
    }
}
