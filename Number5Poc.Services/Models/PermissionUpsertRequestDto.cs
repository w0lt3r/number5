namespace Number5Poc.Services.Models;

public class PermissionUpsertRequestDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public int PermissionTypeId { get; set; }
    public DateTime EffectiveFrom { get; set; }
}