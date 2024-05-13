namespace ServerAPI.Server.Interfaces
{
    public interface ICategory
    {
        ulong Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
