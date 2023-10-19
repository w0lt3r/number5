using AutoMapper;
using Number5Poc.Data;
using Number5Poc.Data.Entities;
using Number5Poc.Services.Interfaces;
using Number5Poc.Services.Models;

namespace Number5Poc.Services;
public class PermissionService : IPermissionService
{
    private IMapper mapper;
    private IUnitOfWork unitOfWork;
    private IAnalyticsHandler analyticsHandler;
    private IMessagingSystem messagingSystem;
    public PermissionService(IMapper mapper,
        IUnitOfWork unitOfWork,
        IAnalyticsHandler analyticsHandler,
        IMessagingSystem messagingSystem)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.analyticsHandler = analyticsHandler;
        this.messagingSystem = messagingSystem;
    }
    
    public async Task<PermissionDto> UpsertPermission(PermissionUpsertRequestDto request, int? permissionId = null)
    {
        var permission = mapper.Map<Permission>(request);
        if (permissionId is not null)
        {
            permission.Id = permissionId.Value;
            this.unitOfWork.GetRepository<Permission>().Update(permission);
        }
        else
        {
            await this.unitOfWork.GetRepository<Permission>().Insert(permission);
        }

        await this.unitOfWork.SaveChanges();
        // await analyticsHandler.Push(permission);
        await messagingSystem.Publish(permissionId is null? Operations.Request: Operations.Modify);
        return mapper.Map<PermissionDto>(permission);
    }

    public async Task<List<PermissionDto>> GetPermissions()
    {
            var permissions = await this.unitOfWork.
        GetRepository<Permission>().Get(includeProperties: "PermissionType");
            await messagingSystem.Publish(Operations.Get);
        return mapper.Map<List<PermissionDto>>(permissions);
    }
    
    public async Task<List<PermissionTypeDto>> GetPermissionTypes()
    {
        var types = await this.unitOfWork.
            GetRepository<PermissionType>().Get();
        return mapper.Map<List<PermissionTypeDto>>(types);
    }
}