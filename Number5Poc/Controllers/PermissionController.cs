using Microsoft.AspNetCore.Mvc;
using Number5Poc.Services.Interfaces;
using Number5Poc.Services.Models;

namespace number5services.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionController : ControllerBase
{
    private readonly ILogger<PermissionController> _logger;
    private readonly IPermissionService permissionService;

    public PermissionController(
        IPermissionService permissionService,
        ILogger<PermissionController> logger)
    {
        _logger = logger;
        this.permissionService = permissionService;
    }

    [HttpGet]
    public async Task<IEnumerable<PermissionDto>> Get()
    {
        return await this.permissionService.GetPermissions();
    }
    
    [HttpGet("type")]
    public async Task<IEnumerable<PermissionTypeDto>> GetTypes()
    {
        return await this.permissionService.GetPermissionTypes();
    }
    
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] PermissionUpsertRequestDto request)
    {
        var result = await this.permissionService.UpsertPermission(request);
        return new OkObjectResult(result);
    }
    
    [HttpPut("{permissionId}")]
    public async Task<IActionResult> Update(int permissionId,
        [FromBody] PermissionUpsertRequestDto request)
    {
        var result = await this.permissionService.UpsertPermission(request, permissionId);
        return new OkObjectResult(result);
    }
}