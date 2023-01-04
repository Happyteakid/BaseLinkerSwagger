using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BaselinkerREST.Model;

public class BaseLinkerRequest02
{

    public int? OrderId { get; set; }
    public int? Extra_field_ID { get; set; }

    public BaseLinkerRequest02(int orderId, int customExtraFields)
    {
        this.OrderId = orderId;
        this.Extra_field_ID = customExtraFields;
    }

    public BaseLinkerRequest02()
    {
    }

}