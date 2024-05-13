using System.IO;

namespace ServerAPI.Server.Interfaces.Product
{
    public interface IProduct
    {
        long Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        //float Price { get; set; }
        //long Category { get; set; }
        //IPeriodicity Periodicity { get; set; }
        //long PhotoId { get; set; }
    }
}
