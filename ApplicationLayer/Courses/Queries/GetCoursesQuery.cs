using ApplicationLayer.Courses.DTOs;
using DomainLayer.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Courses.Queries
{
    public class GetCoursesQuery : IRequest<OperationResult<IEnumerable<CourseDTO>>>
    {
    }
}
