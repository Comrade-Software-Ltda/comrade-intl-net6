using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.Application.Services.SystemUserServices.Validations;
using Comrade.Core.SystemUserCore;
using Comrade.Domain.Models;

namespace Comrade.Application.Services.SystemUserServices.Commands;

public class SystemUserCommand : Service, ISystemUserCommand
{
    private readonly IUcSystemUserCreate _createSystemUser;
    private readonly IUcSystemUserDelete _deleteSystemUser;
    private readonly IUcSystemUserEdit _editSystemUser;

    public SystemUserCommand(
        IUcSystemUserEdit editSystemUser,
        IUcSystemUserCreate createSystemUser,
        IUcSystemUserDelete deleteSystemUser,
        IMapper mapper)
        : base(mapper)
    {
        _editSystemUser = editSystemUser;
        _createSystemUser = createSystemUser;
        _deleteSystemUser = deleteSystemUser;
    }

    public async Task<ISingleResultDto<EntityDto>> Create(SystemUserCreateDto dto)
    {
        var validator = await new SystemUserCreateValidation().ValidateAsync(dto)
            .ConfigureAwait(false);

        if (!validator.IsValid)
        {
            return new SingleResultDto<EntityDto>(validator);
        }

        var mappedObject = Mapper.Map<SystemUser>(dto);

        var result = await _createSystemUser.Execute(mappedObject).ConfigureAwait(false);

        var resultDto = new SingleResultDto<EntityDto>(result);

        return resultDto;
    }

    public async Task<ISingleResultDto<EntityDto>> Edit(SystemUserEditDto dto)
    {
        var validator = await new SystemUserEditValidation().ValidateAsync(dto)
            .ConfigureAwait(false);

        if (!validator.IsValid)
        {
            return new SingleResultDto<EntityDto>(validator);
        }

        var mappedObject = Mapper.Map<SystemUser>(dto);

        var result = await _editSystemUser.Execute(mappedObject).ConfigureAwait(false);

        var resultDto = new SingleResultDto<EntityDto>(result);

        return resultDto;
    }

    public async Task<ISingleResultDto<EntityDto>> Delete(int id)
    {
        var result = await _deleteSystemUser.Execute(id).ConfigureAwait(false);

        var resultDto = new SingleResultDto<EntityDto>(result);

        return resultDto;
    }
}