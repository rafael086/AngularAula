namespace Domain
{
    public class RedeSocial
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string URL { get; set; }
        public int? FkEvento { get; set; }
        public int? FkPalestrante { get; set; }

        public Evento Evento { get; set; }
        public Palestrante Palestrante { get; set; }
    }
}