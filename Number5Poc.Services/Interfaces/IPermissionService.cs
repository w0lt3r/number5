using Number5Poc.Data.Entities;
using Number5Poc.Services.Models;

namespace Number5Poc.Services.Interfaces;

public interface IPermissionService
{
    Task<PermissionDto> UpsertPermission(PermissionUpsertRequestDto request, int? permissionId = null);
    Task<List<PermissionDto>> GetPermissions();
    Task<List<PermissionTypeDto>> GetPermissionTypes();
}