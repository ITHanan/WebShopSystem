

using DomainLayer.Common;
using MediatR;

namespace ApplicationLayer.LanguageComAndQu.Commands
{
    public class SetUserLanguageCommand : IRequest<OperationResult<string>>
    {
        public int UserId { get; set; }
        public int LanguageId { get; set; }
    }
}
