namespace WebApplication1
{
    public class TypeIntervention
    {
        public int Id { get; set; }

        public string Label { get; set; } = string.Empty;
        public ICollection<Intervention> Intervention { get; set; } = new List<Intervention>();
    }
}
