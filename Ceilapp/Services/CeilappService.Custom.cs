using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Radzen;

using Ceilapp.Data;

namespace Ceilapp
{
    public partial class ceilappService
    {
        public ceilappContext dbContext
        {
           get
           {
             return this.context;
           }
        }
        
        // Add this new method
        public async Task<IEnumerable<Ceilapp.Models.ceilapp.CourseLevel>> GetCourseLevelsByCourseId(int courseId)
        {
            return await context.CourseLevels
                .Where(x => x.CourseId == courseId)
                .ToListAsync();
        }
     }
}