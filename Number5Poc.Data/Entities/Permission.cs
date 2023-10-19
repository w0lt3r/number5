namespace Number5Poc.Data.Entities;

public class Permission
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public int PermissionTypeId { get; set; }
    public PermissionType PermissionType { get; set; }
    public DateTime EffectiveFrom { get; set; }
}