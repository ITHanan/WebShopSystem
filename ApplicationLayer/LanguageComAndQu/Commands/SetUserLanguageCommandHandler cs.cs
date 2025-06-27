using ApplicationLayer.LanguageComAndQu.Commands;
using ApplicationLayer.Interfaces;
using DomainLayer.Models;
using DomainLayer.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationLayer.LanguageComAndQu.Handlers
{
    public class SetUserLanguageCommandHandler : IRequestHandler<SetUserLanguageCommand, OperationResult<string>>
    {
        private readonly IGenericRepository<User> _userRepository;

        public SetUserLanguageCommandHandler(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<string>> Handle(SetUserLanguageCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _userRepository.GetByIdAsync(request.UserId);

            if (!userResult.IsSuccess || userResult.Data == null)
                return OperationResult<string>.Failure("User not found");

            var user = userResult.Data;
            user.LanguageId = request.LanguageId;

            var updateResult = await _userRepository.UpdateAsync(user, cancellationToken);

            return updateResult.IsSuccess
                ? OperationResult<string>.Success("Language updated successfully")
                : OperationResult<string>.Failure(updateResult.ErrorMessage);
        }
    }
}
