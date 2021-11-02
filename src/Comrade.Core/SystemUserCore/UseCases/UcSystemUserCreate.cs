using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Bases;
using Comrade.Domain.Extensions;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserCore.UseCases
{
    public class UcSystemUserCreate : UseCase, IUcSystemUserCreate
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly ISystemUserRepository _repository;
        private readonly SystemUserCreateValidation _systemUserCreateValidation;

        public UcSystemUserCreate(ISystemUserRepository repository,
            IPasswordHasher passwordHasher, SystemUserCreateValidation systemUserCreateValidation,
            IUnitOfWork uow
        )
            : base(uow)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _systemUserCreateValidation = systemUserCreateValidation;
        }

        public async Task<ISingleResult<Entity>> Execute(SystemUser entity)
        {
            var isValid = ValidateEntity(entity);
            if (!isValid.Success)
            {
                return isValid;
            }

            var validate = _systemUserCreateValidation.Execute(entity);
            if (!validate.Success) return validate;

            entity.Password = _passwordHasher.Hash(entity.Password);
            entity.RegisterDate = DateTimeBrasilia.GetDateTimeBrasilia();

            await _repository.Add(entity).ConfigureAwait(false);

            _ = await Commit().ConfigureAwait(false);

            return new CreateResult<Entity>();
        }
    }
}