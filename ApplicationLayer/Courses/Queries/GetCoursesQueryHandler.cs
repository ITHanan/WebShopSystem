using ApplicationLayer.Courses.DTOs;
using ApplicationLayer.Interfaces;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Courses.Queries
{
    public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, OperationResult<IEnumerable<CourseDTO>>>
    {
        private readonly IGenericRepository<Course> _courseRepository;

        public GetCoursesQueryHandler(IGenericRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<OperationResult<IEnumerable<CourseDTO>>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            // Include Language, Teacher, Materials
            var courses = await _courseRepository.GetAllIncludingAsync(
                c => c.Language,
                c => c.Teacher,
                c => c.Materials
            );


            var result = courses.Select(c => new CourseDTO
            {
                CourseID = c.CourseID,
                Name = c.Name,
                Description = c.Description,
                Level = c.Level.ToString(),
                Language = c.Language?.Name,
                FlagUrl= c.Language?.FlagUrl,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                MaxParticipants = c.MaxParticipants,
                Location = c.Location,
                TeacherName = $"{c.Teacher?.FullName} ",
                MaterialCount = c.Materials?.Count ?? 0
            });

            return OperationResult<IEnumerable<CourseDTO>>.Success(result);
        }
    }
}