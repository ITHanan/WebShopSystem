

using ApplicationLayer.Interfaces;
using ApplicationLayer.LanguageComAndQu.DTOs;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

namespace ApplicationLayer.LanguageComAndQu.Queries
{
    public class GetFilteredLanguagesQueryHandler : IRequestHandler<GetFilteredLanguagesQuery, OperationResult<IEnumerable<LanguageDto>>>
    {
        private readonly IGenericRepository<Language> _languageRepository;

        public GetFilteredLanguagesQueryHandler(IGenericRepository<Language> languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public async Task<OperationResult<IEnumerable<LanguageDto>>> Handle(GetFilteredLanguagesQuery request, CancellationToken cancellationToken)
        {
            var data = await _languageRepository.GetAllAsync();
            var query = data.Data.AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(request.Name))
                query = query.Where(l => l.Name.Contains(request.Name));

            if (!string.IsNullOrWhiteSpace(request.Code))
                query = query.Where(l => l.Code != null && l.Code.Contains(request.Code));

            // Sorting
            query = request.SortBy.ToLower() switch
            {
                "code" => request.SortOrder.ToLower() == "desc"
                            ? query.OrderByDescending(l => l.Code)
                            : query.OrderBy(l => l.Code),
                _ => request.SortOrder.ToLower() == "desc"
                            ? query.OrderByDescending(l => l.Name)
                            : query.OrderBy(l => l.Name)
            };

            var result = query.Select(l => new LanguageDto
            {
                Name = l.Name,
                FlagUrl = l.FlagUrl
            });

            return OperationResult<IEnumerable<LanguageDto>>.Success(result);
        }


    }
}
