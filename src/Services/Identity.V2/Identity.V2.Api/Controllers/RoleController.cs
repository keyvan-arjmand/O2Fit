using Identity.V2.Application.Roles.V1.Commands.AddSinglePermissionToRole;

namespace Identity.V2.Api.Controllers
{
    [ApiVersion("1")]
    public class RoleController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IRoleService _roleService;

        public RoleController(IMapper mapper, IMediator mediator, IRoleService roleService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _roleService = roleService;
        }

        [HasPermission(PermissionsConstants.GetAllRoles)]
        [HttpGet("get-all")]
        public ActionResult GetAll()
        {
            var roles = _roleService.GetList();
            var result = _mapper.Map<List<Role>, List<RoleDto>>(roles);
            return Ok(new ApiResult<List<RoleDto>>(result, string.Empty, ApiResultStatusCode.Success));
        }

        [HasPermission(PermissionsConstants.GetRoleById)]
        [HttpGet("get-by-id")]
        public async Task<ActionResult> GetById(string id)
        {
            var role = await _roleService.GetByIdAsync(id).ConfigureAwait(false);
            if (role is null)
            {
                return NotFound(new ApiResult<RoleDto?>(null, string.Empty, ApiResultStatusCode.NotFound, false));
            }

            var result = _mapper.Map<Role, RoleDto>(role);
            return Ok(new ApiResult<RoleDto?>(result, string.Empty, ApiResultStatusCode.Success));
        }

        [HasPermission(PermissionsConstants.CreateRole)]
        [HttpPost("create-role")]
        public async Task<ActionResult> CreateRole([FromBody] CreateRoleDto dto)
        {
            if (_roleService.FindByDisplayName(dto.DisplayName))
            {
                return BadRequest(new ApiResult("Duplicate role display name", ApiResultStatusCode.BadRequest,false));
            }

            var role = new Role(dto.RoleName, dto.DisplayName);

            var result = await _roleService.CreateAsync(role).ConfigureAwait(false);
            if (result.Succeeded)
            {
                return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
            }

            return BadRequest(new ApiResult(string.Join(", ", result.Errors.Select(s => s.Description)),
                ApiResultStatusCode.BadRequest, false));
        }

        [HasPermission(PermissionsConstants.AddPermissionToRole)]
        [HttpPost("add-permission-to-role")]
        public async Task<ActionResult> AddPermissionToRole([FromBody] AddPermissionsToRoleDto dto,
            CancellationToken cancellationToken)
        {
            var role = await _roleService.GetByIdAsync(dto.RoleId).ConfigureAwait(false);
            if (role != null)
            {
                await _mediator.Send(new AddPermissionToRoleCommand
                {
                    SelectedPermissionNames = dto.Permissions,
                    Role = role
                }, cancellationToken).ConfigureAwait(false);
                return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
            }

            return NotFound(new ApiResult(string.Empty, ApiResultStatusCode.NotFound, false));
        }

        [HasPermission(PermissionsConstants.AddSinglePermissionToRole)]
        [HttpPost("add-single-permission-to-role")]
        public async Task<ActionResult> AddSinglePermissionToRole([FromBody] AddSinglePermissionToRoleCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
        }
        
        
        [HasPermission(PermissionsConstants.UpdateRole)]
        [HttpPut("update-role")]
        public async Task<ActionResult> UpdateRole([FromBody] UpdateRoleDto dto)
        {
            if (_roleService.FindByDisplayName(dto.DisplayName))
            {
                return BadRequest(new ApiResult("Duplicate role display name", ApiResultStatusCode.BadRequest, false));
            }

            var role = await _roleService.GetByNameAsync(dto.OldRoleName).ConfigureAwait(false);
            if (role != null)
            {
                role.DisplayName = dto.DisplayName;
                role.Name = dto.RoleName;
                var result = await _roleService.UpdateAsync(role).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
                }

                return BadRequest(new ApiResult(string.Join(", ", result.Errors.Select(s => s.Description)),
                    ApiResultStatusCode.BadRequest, false));
            }

            return NotFound(new ApiResult("role name not found", ApiResultStatusCode.NotFound, false));
        }

        [HasPermission(PermissionsConstants.DeleteRole)]
        [HttpDelete("delete-role")]
        public async Task<ActionResult> DeleteRole(string id)
        {
            var role = await _roleService.GetByIdAsync(id).ConfigureAwait(false);
            if (role != null)
            {
                var result = await _roleService.DeleteAsync(role).ConfigureAwait(false);
                if (result.Succeeded)
                {
                    return Ok(new ApiResult(string.Empty, ApiResultStatusCode.Success));
                }

                return BadRequest(new ApiResult(string.Join(", ", result.Errors.Select(s => s.Description)),
                    ApiResultStatusCode.BadRequest, false));
            }

            return NotFound(new ApiResult("Role not found", ApiResultStatusCode.NotFound, false));
        }
    }
}