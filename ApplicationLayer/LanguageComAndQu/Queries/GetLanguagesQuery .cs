using ApplicationLayer.LanguageComAndQu.DTOs;
using DomainLayer.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.LanguageComAndQu.Queries
{
    public class GetLanguagesQuery : IRequest<OperationResult<IEnumerable<LanguageDto>>>
    {
    }
}
