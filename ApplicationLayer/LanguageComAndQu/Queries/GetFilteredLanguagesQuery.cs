using ApplicationLayer.LanguageComAndQu.DTOs;
using DomainLayer.Common;
using MediatR;


namespace ApplicationLayer.LanguageComAndQu.Queries
{
    public class GetFilteredLanguagesQuery : IRequest<OperationResult<IEnumerable<LanguageDto>>>
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string SortOrder { get; set; } = "asc";
        public string SortBy { get; set; } = "name";
    }
}
