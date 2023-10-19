using AutoMapper;
using Number5Poc.Data.Entities;
using Number5Poc.Services.Models;

namespace Number5Poc.Services.Profiles;

public class PermissionProfile: Profile
{
    public PermissionProfile()
    {
        CreateMap<Permission, PermissionDto>()
            .ForMember(d => d.PermissionTypeDescription,
                s => s.MapFrom(x => x.PermissionType.Description));
        CreateMap<PermissionUpsertRequestDto, Permission>();
        CreateMap<PermissionType, PermissionTypeDto>();
    }
}