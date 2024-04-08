namespace eMart.Payloads.Requests;

public class UpdateProductRequest
{
    public string Name { get; set; } = null!;
    public double Weigh { get; set; } 
    public double UnitPrice { get; set; }   
    public int UnitsInStock { get; set; }   
    public int CategoryId { get; set; } 
}