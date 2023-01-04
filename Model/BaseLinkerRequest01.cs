using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaselinkerREST.Model;

public class BaseLinkerRequest01
{
    public int? orderId { get; set; }
    //Seller Comments
    [MaxLength(200)]
    public string? admin_comments { get; set; }
    //Value of the "extra field 1". - the seller can store any information there
    [MaxLength(50)]
    public string? extra_field_1 { get; set; }
    // Value of the "extra field 2". - the seller can store any information there
    [MaxLength(50)]
    public string? extra_field_2 { get; set; }

    public BaseLinkerRequest01(int orderId, string adminComments, string extraField1, string extraField2)
    {
        this.orderId = orderId;
        admin_comments = adminComments;
        extra_field_1 = extraField1;
        extra_field_2 = extraField2;
    }

    public BaseLinkerRequest01()
    {
    }

}