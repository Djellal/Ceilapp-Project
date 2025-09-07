using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using Ceilapp.Data;

namespace Ceilapp.Controllers
{
    public partial class ExportceilappController : ExportController
    {
        private readonly ceilappContext context;
        private readonly ceilappService service;

        public ExportceilappController(ceilappContext context, ceilappService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/ceilapp/appsettings/csv")]
        [HttpGet("/export/ceilapp/appsettings/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAppSettingsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAppSettings(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/appsettings/excel")]
        [HttpGet("/export/ceilapp/appsettings/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAppSettingsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAppSettings(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/coursecomponents/csv")]
        [HttpGet("/export/ceilapp/coursecomponents/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCourseComponentsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCourseComponents(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/coursecomponents/excel")]
        [HttpGet("/export/ceilapp/coursecomponents/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCourseComponentsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCourseComponents(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/courseregistrations/csv")]
        [HttpGet("/export/ceilapp/courseregistrations/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCourseRegistrationsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCourseRegistrations(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/courseregistrations/excel")]
        [HttpGet("/export/ceilapp/courseregistrations/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCourseRegistrationsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCourseRegistrations(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/courses/csv")]
        [HttpGet("/export/ceilapp/courses/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCoursesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCourses(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/courses/excel")]
        [HttpGet("/export/ceilapp/courses/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCoursesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCourses(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/coursetypes/csv")]
        [HttpGet("/export/ceilapp/coursetypes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCourseTypesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCourseTypes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/coursetypes/excel")]
        [HttpGet("/export/ceilapp/coursetypes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCourseTypesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCourseTypes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/evaluations/csv")]
        [HttpGet("/export/ceilapp/evaluations/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEvaluationsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEvaluations(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/evaluations/excel")]
        [HttpGet("/export/ceilapp/evaluations/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEvaluationsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEvaluations(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/groupes/csv")]
        [HttpGet("/export/ceilapp/groupes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportGroupesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetGroupes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/groupes/excel")]
        [HttpGet("/export/ceilapp/groupes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportGroupesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetGroupes(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/municipalities/csv")]
        [HttpGet("/export/ceilapp/municipalities/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMunicipalitiesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetMunicipalities(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/municipalities/excel")]
        [HttpGet("/export/ceilapp/municipalities/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportMunicipalitiesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetMunicipalities(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/professions/csv")]
        [HttpGet("/export/ceilapp/professions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportProfessionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetProfessions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/professions/excel")]
        [HttpGet("/export/ceilapp/professions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportProfessionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetProfessions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/sessions/csv")]
        [HttpGet("/export/ceilapp/sessions/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSessionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSessions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/sessions/excel")]
        [HttpGet("/export/ceilapp/sessions/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSessionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSessions(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/states/csv")]
        [HttpGet("/export/ceilapp/states/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportStatesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetStates(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/states/excel")]
        [HttpGet("/export/ceilapp/states/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportStatesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetStates(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/courselevels/csv")]
        [HttpGet("/export/ceilapp/courselevels/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCourseLevelsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCourseLevels(), Request.Query, false), fileName);
        }

        [HttpGet("/export/ceilapp/courselevels/excel")]
        [HttpGet("/export/ceilapp/courselevels/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCourseLevelsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCourseLevels(), Request.Query, false), fileName);
        }
    }
}
