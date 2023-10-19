namespace Number5Poc.Services.Models;

public class PermissionDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public int PermissionTypeId { get; set; }
    public string PermissionTypeDescription { get; set; }
    public DateTime EffectiveFrom { get; set; }
}