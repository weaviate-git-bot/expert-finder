namespace ExpertFinder.Weaviate;

public class VectorObject
{
    public Guid Id { get; set; } = default!;
    public string Class { get; set; } = default!;
    public long CreationTimeUnix { get; set; }
    public Dictionary<string, object> Properties { get; set; } = new();
    public float[] Vector { get; set; } = default!;
}