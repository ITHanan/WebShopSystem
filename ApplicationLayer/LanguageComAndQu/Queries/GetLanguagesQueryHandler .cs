using ApplicationLayer.Interfaces;
using ApplicationLayer.LanguageComAndQu.DTOs;
using DomainLayer.Common;
using MediatR;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.LanguageComAndQu.Queries
{
    public class GetLanguagesQueryHandler : IRequestHandler<GetLanguagesQuery, OperationResult<IEnumerable<LanguageDto>>>
    {
        private readonly IGenericRepository<Language> _languageRepository;

        public GetLanguagesQueryHandler(IGenericRepository<Language> languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public async Task<OperationResult<IEnumerable<LanguageDto>>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
        {
            var languages = await _languageRepository.GetAllAsync();

            var result = languages.Data.Select(l => new LanguageDto
            {
                Name = l.Name,
                FlagUrl = l.FlagUrl
            });

            return OperationResult<IEnumerable<LanguageDto>>.Success(result);
        }
    }

}
