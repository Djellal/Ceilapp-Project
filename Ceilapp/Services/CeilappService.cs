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
        ceilappContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly ceilappContext context;
        private readonly NavigationManager navigationManager;

        public ceilappService(ceilappContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public void ApplyQuery<T>(ref IQueryable<T> items, Query query = null)
        {
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }
        }


        public async Task ExportAppSettingsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/appsettings/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/appsettings/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportAppSettingsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/appsettings/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/appsettings/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnAppSettingsRead(ref IQueryable<Ceilapp.Models.ceilapp.AppSetting> items);

        public async Task<IQueryable<Ceilapp.Models.ceilapp.AppSetting>> GetAppSettings(Query query = null)
        {
            var items = Context.AppSettings.AsQueryable();

            items = items.Include(i => i.Session);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnAppSettingsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnAppSettingGet(Ceilapp.Models.ceilapp.AppSetting item);
        partial void OnGetAppSettingById(ref IQueryable<Ceilapp.Models.ceilapp.AppSetting> items);


        public async Task<Ceilapp.Models.ceilapp.AppSetting> GetAppSettingById(int id)
        {
            var items = Context.AppSettings
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Session);
 
            OnGetAppSettingById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnAppSettingGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnAppSettingCreated(Ceilapp.Models.ceilapp.AppSetting item);
        partial void OnAfterAppSettingCreated(Ceilapp.Models.ceilapp.AppSetting item);

        public async Task<Ceilapp.Models.ceilapp.AppSetting> CreateAppSetting(Ceilapp.Models.ceilapp.AppSetting appsetting)
        {
            OnAppSettingCreated(appsetting);

            var existingItem = Context.AppSettings
                              .Where(i => i.Id == appsetting.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.AppSettings.Add(appsetting);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(appsetting).State = EntityState.Detached;
                throw;
            }

            OnAfterAppSettingCreated(appsetting);

            return appsetting;
        }

        public async Task<Ceilapp.Models.ceilapp.AppSetting> CancelAppSettingChanges(Ceilapp.Models.ceilapp.AppSetting item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnAppSettingUpdated(Ceilapp.Models.ceilapp.AppSetting item);
        partial void OnAfterAppSettingUpdated(Ceilapp.Models.ceilapp.AppSetting item);

        public async Task<Ceilapp.Models.ceilapp.AppSetting> UpdateAppSetting(int id, Ceilapp.Models.ceilapp.AppSetting appsetting)
        {
            OnAppSettingUpdated(appsetting);

            var itemToUpdate = Context.AppSettings
                              .Where(i => i.Id == appsetting.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(appsetting);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterAppSettingUpdated(appsetting);

            return appsetting;
        }

        partial void OnAppSettingDeleted(Ceilapp.Models.ceilapp.AppSetting item);
        partial void OnAfterAppSettingDeleted(Ceilapp.Models.ceilapp.AppSetting item);

        public async Task<Ceilapp.Models.ceilapp.AppSetting> DeleteAppSetting(int id)
        {
            var itemToDelete = Context.AppSettings
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnAppSettingDeleted(itemToDelete);


            Context.AppSettings.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterAppSettingDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCourseComponentsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/coursecomponents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/coursecomponents/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCourseComponentsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/coursecomponents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/coursecomponents/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCourseComponentsRead(ref IQueryable<Ceilapp.Models.ceilapp.CourseComponent> items);

        public async Task<IQueryable<Ceilapp.Models.ceilapp.CourseComponent>> GetCourseComponents(Query query = null)
        {
            var items = Context.CourseComponents.AsQueryable();

            items = items.Include(i => i.Course);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCourseComponentsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCourseComponentGet(Ceilapp.Models.ceilapp.CourseComponent item);
        partial void OnGetCourseComponentById(ref IQueryable<Ceilapp.Models.ceilapp.CourseComponent> items);


        public async Task<Ceilapp.Models.ceilapp.CourseComponent> GetCourseComponentById(int id)
        {
            var items = Context.CourseComponents
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Course);
 
            OnGetCourseComponentById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCourseComponentGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCourseComponentCreated(Ceilapp.Models.ceilapp.CourseComponent item);
        partial void OnAfterCourseComponentCreated(Ceilapp.Models.ceilapp.CourseComponent item);

        public async Task<Ceilapp.Models.ceilapp.CourseComponent> CreateCourseComponent(Ceilapp.Models.ceilapp.CourseComponent coursecomponent)
        {
            OnCourseComponentCreated(coursecomponent);

            var existingItem = Context.CourseComponents
                              .Where(i => i.Id == coursecomponent.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.CourseComponents.Add(coursecomponent);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(coursecomponent).State = EntityState.Detached;
                throw;
            }

            OnAfterCourseComponentCreated(coursecomponent);

            return coursecomponent;
        }

        public async Task<Ceilapp.Models.ceilapp.CourseComponent> CancelCourseComponentChanges(Ceilapp.Models.ceilapp.CourseComponent item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCourseComponentUpdated(Ceilapp.Models.ceilapp.CourseComponent item);
        partial void OnAfterCourseComponentUpdated(Ceilapp.Models.ceilapp.CourseComponent item);

        public async Task<Ceilapp.Models.ceilapp.CourseComponent> UpdateCourseComponent(int id, Ceilapp.Models.ceilapp.CourseComponent coursecomponent)
        {
            OnCourseComponentUpdated(coursecomponent);

            var itemToUpdate = Context.CourseComponents
                              .Where(i => i.Id == coursecomponent.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(coursecomponent);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCourseComponentUpdated(coursecomponent);

            return coursecomponent;
        }

        partial void OnCourseComponentDeleted(Ceilapp.Models.ceilapp.CourseComponent item);
        partial void OnAfterCourseComponentDeleted(Ceilapp.Models.ceilapp.CourseComponent item);

        public async Task<Ceilapp.Models.ceilapp.CourseComponent> DeleteCourseComponent(int id)
        {
            var itemToDelete = Context.CourseComponents
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCourseComponentDeleted(itemToDelete);


            Context.CourseComponents.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCourseComponentDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCourseLevelsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/courselevels/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/courselevels/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCourseLevelsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/courselevels/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/courselevels/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCourseLevelsRead(ref IQueryable<Ceilapp.Models.ceilapp.CourseLevel> items);

        public async Task<IQueryable<Ceilapp.Models.ceilapp.CourseLevel>> GetCourseLevels(Query query = null)
        {
            var items = Context.CourseLevels.AsQueryable();

            items = items.Include(i => i.Course);
            items = items.Include(i => i.CourseLevel1);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCourseLevelsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCourseLevelGet(Ceilapp.Models.ceilapp.CourseLevel item);
        partial void OnGetCourseLevelById(ref IQueryable<Ceilapp.Models.ceilapp.CourseLevel> items);


        public async Task<Ceilapp.Models.ceilapp.CourseLevel> GetCourseLevelById(int id)
        {
            var items = Context.CourseLevels
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Course);
            items = items.Include(i => i.CourseLevel1);
 
            OnGetCourseLevelById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCourseLevelGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCourseLevelCreated(Ceilapp.Models.ceilapp.CourseLevel item);
        partial void OnAfterCourseLevelCreated(Ceilapp.Models.ceilapp.CourseLevel item);

        public async Task<Ceilapp.Models.ceilapp.CourseLevel> CreateCourseLevel(Ceilapp.Models.ceilapp.CourseLevel courselevel)
        {
            OnCourseLevelCreated(courselevel);

            var existingItem = Context.CourseLevels
                              .Where(i => i.Id == courselevel.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.CourseLevels.Add(courselevel);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(courselevel).State = EntityState.Detached;
                throw;
            }

            OnAfterCourseLevelCreated(courselevel);

            return courselevel;
        }

        public async Task<Ceilapp.Models.ceilapp.CourseLevel> CancelCourseLevelChanges(Ceilapp.Models.ceilapp.CourseLevel item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCourseLevelUpdated(Ceilapp.Models.ceilapp.CourseLevel item);
        partial void OnAfterCourseLevelUpdated(Ceilapp.Models.ceilapp.CourseLevel item);

        public async Task<Ceilapp.Models.ceilapp.CourseLevel> UpdateCourseLevel(int id, Ceilapp.Models.ceilapp.CourseLevel courselevel)
        {
            OnCourseLevelUpdated(courselevel);

            var itemToUpdate = Context.CourseLevels
                              .Where(i => i.Id == courselevel.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(courselevel);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCourseLevelUpdated(courselevel);

            return courselevel;
        }

        partial void OnCourseLevelDeleted(Ceilapp.Models.ceilapp.CourseLevel item);
        partial void OnAfterCourseLevelDeleted(Ceilapp.Models.ceilapp.CourseLevel item);

        public async Task<Ceilapp.Models.ceilapp.CourseLevel> DeleteCourseLevel(int id)
        {
            var itemToDelete = Context.CourseLevels
                              .Where(i => i.Id == id)
                              .Include(i => i.CourseLevels1)
                              .Include(i => i.Groupes)
                              .Include(i => i.CourseRegistrations)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCourseLevelDeleted(itemToDelete);


            Context.CourseLevels.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCourseLevelDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCourseRegistrationsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/courseregistrations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/courseregistrations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCourseRegistrationsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/courseregistrations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/courseregistrations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCourseRegistrationsRead(ref IQueryable<Ceilapp.Models.ceilapp.CourseRegistration> items);

        public async Task<IQueryable<Ceilapp.Models.ceilapp.CourseRegistration>> GetCourseRegistrations(Query query = null)
        {
            var items = Context.CourseRegistrations.AsQueryable();

            items = items.Include(i => i.Municipality);
            items = items.Include(i => i.State);
            items = items.Include(i => i.Course);
            items = items.Include(i => i.CourseLevel);
            items = items.Include(i => i.Groupe);
            items = items.Include(i => i.Profession);
            items = items.Include(i => i.Session);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCourseRegistrationsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCourseRegistrationGet(Ceilapp.Models.ceilapp.CourseRegistration item);
        partial void OnGetCourseRegistrationById(ref IQueryable<Ceilapp.Models.ceilapp.CourseRegistration> items);


        public async Task<Ceilapp.Models.ceilapp.CourseRegistration> GetCourseRegistrationById(int id)
        {
            var items = Context.CourseRegistrations
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Municipality);
            items = items.Include(i => i.State);
            items = items.Include(i => i.Course);
            items = items.Include(i => i.CourseLevel);
            items = items.Include(i => i.Groupe);
            items = items.Include(i => i.Profession);
            items = items.Include(i => i.Session);
 
            OnGetCourseRegistrationById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCourseRegistrationGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCourseRegistrationCreated(Ceilapp.Models.ceilapp.CourseRegistration item);
        partial void OnAfterCourseRegistrationCreated(Ceilapp.Models.ceilapp.CourseRegistration item);

        public async Task<Ceilapp.Models.ceilapp.CourseRegistration> CreateCourseRegistration(Ceilapp.Models.ceilapp.CourseRegistration courseregistration)
        {
            OnCourseRegistrationCreated(courseregistration);

            var existingItem = Context.CourseRegistrations
                              .Where(i => i.Id == courseregistration.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.CourseRegistrations.Add(courseregistration);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(courseregistration).State = EntityState.Detached;
                throw;
            }

            OnAfterCourseRegistrationCreated(courseregistration);

            return courseregistration;
        }

        public async Task<Ceilapp.Models.ceilapp.CourseRegistration> CancelCourseRegistrationChanges(Ceilapp.Models.ceilapp.CourseRegistration item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCourseRegistrationUpdated(Ceilapp.Models.ceilapp.CourseRegistration item);
        partial void OnAfterCourseRegistrationUpdated(Ceilapp.Models.ceilapp.CourseRegistration item);

        public async Task<Ceilapp.Models.ceilapp.CourseRegistration> UpdateCourseRegistration(int id, Ceilapp.Models.ceilapp.CourseRegistration courseregistration)
        {
            OnCourseRegistrationUpdated(courseregistration);

            var itemToUpdate = Context.CourseRegistrations
                              .Where(i => i.Id == courseregistration.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(courseregistration);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCourseRegistrationUpdated(courseregistration);

            return courseregistration;
        }

        partial void OnCourseRegistrationDeleted(Ceilapp.Models.ceilapp.CourseRegistration item);
        partial void OnAfterCourseRegistrationDeleted(Ceilapp.Models.ceilapp.CourseRegistration item);

        public async Task<Ceilapp.Models.ceilapp.CourseRegistration> DeleteCourseRegistration(int id)
        {
            var itemToDelete = Context.CourseRegistrations
                              .Where(i => i.Id == id)
                              .Include(i => i.Evaluations)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCourseRegistrationDeleted(itemToDelete);


            Context.CourseRegistrations.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCourseRegistrationDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCoursesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/courses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/courses/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCoursesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/courses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/courses/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCoursesRead(ref IQueryable<Ceilapp.Models.ceilapp.Course> items);

        public async Task<IQueryable<Ceilapp.Models.ceilapp.Course>> GetCourses(Query query = null)
        {
            var items = Context.Courses.AsQueryable();

            items = items.Include(i => i.CourseType);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCoursesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCourseGet(Ceilapp.Models.ceilapp.Course item);
        partial void OnGetCourseById(ref IQueryable<Ceilapp.Models.ceilapp.Course> items);


        public async Task<Ceilapp.Models.ceilapp.Course> GetCourseById(int id)
        {
            var items = Context.Courses
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.CourseType);
 
            OnGetCourseById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCourseGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCourseCreated(Ceilapp.Models.ceilapp.Course item);
        partial void OnAfterCourseCreated(Ceilapp.Models.ceilapp.Course item);

        public async Task<Ceilapp.Models.ceilapp.Course> CreateCourse(Ceilapp.Models.ceilapp.Course course)
        {
            OnCourseCreated(course);

            var existingItem = Context.Courses
                              .Where(i => i.Id == course.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Courses.Add(course);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(course).State = EntityState.Detached;
                throw;
            }

            OnAfterCourseCreated(course);

            return course;
        }

        public async Task<Ceilapp.Models.ceilapp.Course> CancelCourseChanges(Ceilapp.Models.ceilapp.Course item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCourseUpdated(Ceilapp.Models.ceilapp.Course item);
        partial void OnAfterCourseUpdated(Ceilapp.Models.ceilapp.Course item);

        public async Task<Ceilapp.Models.ceilapp.Course> UpdateCourse(int id, Ceilapp.Models.ceilapp.Course course)
        {
            OnCourseUpdated(course);

            var itemToUpdate = Context.Courses
                              .Where(i => i.Id == course.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(course);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCourseUpdated(course);

            return course;
        }

        partial void OnCourseDeleted(Ceilapp.Models.ceilapp.Course item);
        partial void OnAfterCourseDeleted(Ceilapp.Models.ceilapp.Course item);

        public async Task<Ceilapp.Models.ceilapp.Course> DeleteCourse(int id)
        {
            var itemToDelete = Context.Courses
                              .Where(i => i.Id == id)
                              .Include(i => i.CourseLevels)
                              .Include(i => i.Groupes)
                              .Include(i => i.CourseComponents)
                              .Include(i => i.Evaluations)
                              .Include(i => i.CourseRegistrations)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCourseDeleted(itemToDelete);


            Context.Courses.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCourseDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCourseTypesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/coursetypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/coursetypes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCourseTypesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/coursetypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/coursetypes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCourseTypesRead(ref IQueryable<Ceilapp.Models.ceilapp.CourseType> items);

        public async Task<IQueryable<Ceilapp.Models.ceilapp.CourseType>> GetCourseTypes(Query query = null)
        {
            var items = Context.CourseTypes.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCourseTypesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCourseTypeGet(Ceilapp.Models.ceilapp.CourseType item);
        partial void OnGetCourseTypeById(ref IQueryable<Ceilapp.Models.ceilapp.CourseType> items);


        public async Task<Ceilapp.Models.ceilapp.CourseType> GetCourseTypeById(int id)
        {
            var items = Context.CourseTypes
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetCourseTypeById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCourseTypeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCourseTypeCreated(Ceilapp.Models.ceilapp.CourseType item);
        partial void OnAfterCourseTypeCreated(Ceilapp.Models.ceilapp.CourseType item);

        public async Task<Ceilapp.Models.ceilapp.CourseType> CreateCourseType(Ceilapp.Models.ceilapp.CourseType coursetype)
        {
            OnCourseTypeCreated(coursetype);

            var existingItem = Context.CourseTypes
                              .Where(i => i.Id == coursetype.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.CourseTypes.Add(coursetype);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(coursetype).State = EntityState.Detached;
                throw;
            }

            OnAfterCourseTypeCreated(coursetype);

            return coursetype;
        }

        public async Task<Ceilapp.Models.ceilapp.CourseType> CancelCourseTypeChanges(Ceilapp.Models.ceilapp.CourseType item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCourseTypeUpdated(Ceilapp.Models.ceilapp.CourseType item);
        partial void OnAfterCourseTypeUpdated(Ceilapp.Models.ceilapp.CourseType item);

        public async Task<Ceilapp.Models.ceilapp.CourseType> UpdateCourseType(int id, Ceilapp.Models.ceilapp.CourseType coursetype)
        {
            OnCourseTypeUpdated(coursetype);

            var itemToUpdate = Context.CourseTypes
                              .Where(i => i.Id == coursetype.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(coursetype);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCourseTypeUpdated(coursetype);

            return coursetype;
        }

        partial void OnCourseTypeDeleted(Ceilapp.Models.ceilapp.CourseType item);
        partial void OnAfterCourseTypeDeleted(Ceilapp.Models.ceilapp.CourseType item);

        public async Task<Ceilapp.Models.ceilapp.CourseType> DeleteCourseType(int id)
        {
            var itemToDelete = Context.CourseTypes
                              .Where(i => i.Id == id)
                              .Include(i => i.Courses)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCourseTypeDeleted(itemToDelete);


            Context.CourseTypes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCourseTypeDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportEvaluationsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/evaluations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/evaluations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEvaluationsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/evaluations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/evaluations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEvaluationsRead(ref IQueryable<Ceilapp.Models.ceilapp.Evaluation> items);

        public async Task<IQueryable<Ceilapp.Models.ceilapp.Evaluation>> GetEvaluations(Query query = null)
        {
            var items = Context.Evaluations.AsQueryable();

            items = items.Include(i => i.Course);
            items = items.Include(i => i.CourseRegistration);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnEvaluationsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEvaluationGet(Ceilapp.Models.ceilapp.Evaluation item);
        partial void OnGetEvaluationById(ref IQueryable<Ceilapp.Models.ceilapp.Evaluation> items);


        public async Task<Ceilapp.Models.ceilapp.Evaluation> GetEvaluationById(int id)
        {
            var items = Context.Evaluations
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Course);
            items = items.Include(i => i.CourseRegistration);
 
            OnGetEvaluationById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnEvaluationGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEvaluationCreated(Ceilapp.Models.ceilapp.Evaluation item);
        partial void OnAfterEvaluationCreated(Ceilapp.Models.ceilapp.Evaluation item);

        public async Task<Ceilapp.Models.ceilapp.Evaluation> CreateEvaluation(Ceilapp.Models.ceilapp.Evaluation evaluation)
        {
            OnEvaluationCreated(evaluation);

            var existingItem = Context.Evaluations
                              .Where(i => i.Id == evaluation.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Evaluations.Add(evaluation);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(evaluation).State = EntityState.Detached;
                throw;
            }

            OnAfterEvaluationCreated(evaluation);

            return evaluation;
        }

        public async Task<Ceilapp.Models.ceilapp.Evaluation> CancelEvaluationChanges(Ceilapp.Models.ceilapp.Evaluation item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEvaluationUpdated(Ceilapp.Models.ceilapp.Evaluation item);
        partial void OnAfterEvaluationUpdated(Ceilapp.Models.ceilapp.Evaluation item);

        public async Task<Ceilapp.Models.ceilapp.Evaluation> UpdateEvaluation(int id, Ceilapp.Models.ceilapp.Evaluation evaluation)
        {
            OnEvaluationUpdated(evaluation);

            var itemToUpdate = Context.Evaluations
                              .Where(i => i.Id == evaluation.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(evaluation);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterEvaluationUpdated(evaluation);

            return evaluation;
        }

        partial void OnEvaluationDeleted(Ceilapp.Models.ceilapp.Evaluation item);
        partial void OnAfterEvaluationDeleted(Ceilapp.Models.ceilapp.Evaluation item);

        public async Task<Ceilapp.Models.ceilapp.Evaluation> DeleteEvaluation(int id)
        {
            var itemToDelete = Context.Evaluations
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEvaluationDeleted(itemToDelete);


            Context.Evaluations.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEvaluationDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportGroupesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/groupes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/groupes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportGroupesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/groupes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/groupes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGroupesRead(ref IQueryable<Ceilapp.Models.ceilapp.Groupe> items);

        public async Task<IQueryable<Ceilapp.Models.ceilapp.Groupe>> GetGroupes(Query query = null)
        {
            var items = Context.Groupes.AsQueryable();

            items = items.Include(i => i.Course);
            items = items.Include(i => i.CourseLevel);
            items = items.Include(i => i.Session);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnGroupesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnGroupeGet(Ceilapp.Models.ceilapp.Groupe item);
        partial void OnGetGroupeById(ref IQueryable<Ceilapp.Models.ceilapp.Groupe> items);


        public async Task<Ceilapp.Models.ceilapp.Groupe> GetGroupeById(int id)
        {
            var items = Context.Groupes
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Course);
            items = items.Include(i => i.CourseLevel);
            items = items.Include(i => i.Session);
 
            OnGetGroupeById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnGroupeGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnGroupeCreated(Ceilapp.Models.ceilapp.Groupe item);
        partial void OnAfterGroupeCreated(Ceilapp.Models.ceilapp.Groupe item);

        public async Task<Ceilapp.Models.ceilapp.Groupe> CreateGroupe(Ceilapp.Models.ceilapp.Groupe groupe)
        {
            OnGroupeCreated(groupe);

            var existingItem = Context.Groupes
                              .Where(i => i.Id == groupe.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Groupes.Add(groupe);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(groupe).State = EntityState.Detached;
                throw;
            }

            OnAfterGroupeCreated(groupe);

            return groupe;
        }

        public async Task<Ceilapp.Models.ceilapp.Groupe> CancelGroupeChanges(Ceilapp.Models.ceilapp.Groupe item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnGroupeUpdated(Ceilapp.Models.ceilapp.Groupe item);
        partial void OnAfterGroupeUpdated(Ceilapp.Models.ceilapp.Groupe item);

        public async Task<Ceilapp.Models.ceilapp.Groupe> UpdateGroupe(int id, Ceilapp.Models.ceilapp.Groupe groupe)
        {
            OnGroupeUpdated(groupe);

            var itemToUpdate = Context.Groupes
                              .Where(i => i.Id == groupe.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(groupe);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterGroupeUpdated(groupe);

            return groupe;
        }

        partial void OnGroupeDeleted(Ceilapp.Models.ceilapp.Groupe item);
        partial void OnAfterGroupeDeleted(Ceilapp.Models.ceilapp.Groupe item);

        public async Task<Ceilapp.Models.ceilapp.Groupe> DeleteGroupe(int id)
        {
            var itemToDelete = Context.Groupes
                              .Where(i => i.Id == id)
                              .Include(i => i.CourseRegistrations)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnGroupeDeleted(itemToDelete);


            Context.Groupes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterGroupeDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportMunicipalitiesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/municipalities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/municipalities/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportMunicipalitiesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/municipalities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/municipalities/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnMunicipalitiesRead(ref IQueryable<Ceilapp.Models.ceilapp.Municipality> items);

        public async Task<IQueryable<Ceilapp.Models.ceilapp.Municipality>> GetMunicipalities(Query query = null)
        {
            var items = Context.Municipalities.AsQueryable();

            items = items.Include(i => i.State);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnMunicipalitiesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnMunicipalityGet(Ceilapp.Models.ceilapp.Municipality item);
        partial void OnGetMunicipalityById(ref IQueryable<Ceilapp.Models.ceilapp.Municipality> items);


        public async Task<Ceilapp.Models.ceilapp.Municipality> GetMunicipalityById(int id)
        {
            var items = Context.Municipalities
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.State);
 
            OnGetMunicipalityById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnMunicipalityGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnMunicipalityCreated(Ceilapp.Models.ceilapp.Municipality item);
        partial void OnAfterMunicipalityCreated(Ceilapp.Models.ceilapp.Municipality item);

        public async Task<Ceilapp.Models.ceilapp.Municipality> CreateMunicipality(Ceilapp.Models.ceilapp.Municipality municipality)
        {
            OnMunicipalityCreated(municipality);

            var existingItem = Context.Municipalities
                              .Where(i => i.Id == municipality.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Municipalities.Add(municipality);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(municipality).State = EntityState.Detached;
                throw;
            }

            OnAfterMunicipalityCreated(municipality);

            return municipality;
        }

        public async Task<Ceilapp.Models.ceilapp.Municipality> CancelMunicipalityChanges(Ceilapp.Models.ceilapp.Municipality item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnMunicipalityUpdated(Ceilapp.Models.ceilapp.Municipality item);
        partial void OnAfterMunicipalityUpdated(Ceilapp.Models.ceilapp.Municipality item);

        public async Task<Ceilapp.Models.ceilapp.Municipality> UpdateMunicipality(int id, Ceilapp.Models.ceilapp.Municipality municipality)
        {
            OnMunicipalityUpdated(municipality);

            var itemToUpdate = Context.Municipalities
                              .Where(i => i.Id == municipality.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(municipality);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterMunicipalityUpdated(municipality);

            return municipality;
        }

        partial void OnMunicipalityDeleted(Ceilapp.Models.ceilapp.Municipality item);
        partial void OnAfterMunicipalityDeleted(Ceilapp.Models.ceilapp.Municipality item);

        public async Task<Ceilapp.Models.ceilapp.Municipality> DeleteMunicipality(int id)
        {
            var itemToDelete = Context.Municipalities
                              .Where(i => i.Id == id)
                              .Include(i => i.CourseRegistrations)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnMunicipalityDeleted(itemToDelete);


            Context.Municipalities.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterMunicipalityDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportProfessionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/professions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/professions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportProfessionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/professions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/professions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnProfessionsRead(ref IQueryable<Ceilapp.Models.ceilapp.Profession> items);

        public async Task<IQueryable<Ceilapp.Models.ceilapp.Profession>> GetProfessions(Query query = null)
        {
            var items = Context.Professions.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnProfessionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnProfessionGet(Ceilapp.Models.ceilapp.Profession item);
        partial void OnGetProfessionById(ref IQueryable<Ceilapp.Models.ceilapp.Profession> items);


        public async Task<Ceilapp.Models.ceilapp.Profession> GetProfessionById(int id)
        {
            var items = Context.Professions
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetProfessionById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnProfessionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnProfessionCreated(Ceilapp.Models.ceilapp.Profession item);
        partial void OnAfterProfessionCreated(Ceilapp.Models.ceilapp.Profession item);

        public async Task<Ceilapp.Models.ceilapp.Profession> CreateProfession(Ceilapp.Models.ceilapp.Profession profession)
        {
            OnProfessionCreated(profession);

            var existingItem = Context.Professions
                              .Where(i => i.Id == profession.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Professions.Add(profession);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(profession).State = EntityState.Detached;
                throw;
            }

            OnAfterProfessionCreated(profession);

            return profession;
        }

        public async Task<Ceilapp.Models.ceilapp.Profession> CancelProfessionChanges(Ceilapp.Models.ceilapp.Profession item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnProfessionUpdated(Ceilapp.Models.ceilapp.Profession item);
        partial void OnAfterProfessionUpdated(Ceilapp.Models.ceilapp.Profession item);

        public async Task<Ceilapp.Models.ceilapp.Profession> UpdateProfession(int id, Ceilapp.Models.ceilapp.Profession profession)
        {
            OnProfessionUpdated(profession);

            var itemToUpdate = Context.Professions
                              .Where(i => i.Id == profession.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(profession);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterProfessionUpdated(profession);

            return profession;
        }

        partial void OnProfessionDeleted(Ceilapp.Models.ceilapp.Profession item);
        partial void OnAfterProfessionDeleted(Ceilapp.Models.ceilapp.Profession item);

        public async Task<Ceilapp.Models.ceilapp.Profession> DeleteProfession(int id)
        {
            var itemToDelete = Context.Professions
                              .Where(i => i.Id == id)
                              .Include(i => i.CourseRegistrations)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnProfessionDeleted(itemToDelete);


            Context.Professions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterProfessionDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportSessionsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/sessions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/sessions/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportSessionsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/sessions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/sessions/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnSessionsRead(ref IQueryable<Ceilapp.Models.ceilapp.Session> items);

        public async Task<IQueryable<Ceilapp.Models.ceilapp.Session>> GetSessions(Query query = null)
        {
            var items = Context.Sessions.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnSessionsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnSessionGet(Ceilapp.Models.ceilapp.Session item);
        partial void OnGetSessionById(ref IQueryable<Ceilapp.Models.ceilapp.Session> items);


        public async Task<Ceilapp.Models.ceilapp.Session> GetSessionById(int id)
        {
            var items = Context.Sessions
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetSessionById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnSessionGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnSessionCreated(Ceilapp.Models.ceilapp.Session item);
        partial void OnAfterSessionCreated(Ceilapp.Models.ceilapp.Session item);

        public async Task<Ceilapp.Models.ceilapp.Session> CreateSession(Ceilapp.Models.ceilapp.Session session)
        {
            OnSessionCreated(session);

            var existingItem = Context.Sessions
                              .Where(i => i.Id == session.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Sessions.Add(session);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(session).State = EntityState.Detached;
                throw;
            }

            OnAfterSessionCreated(session);

            return session;
        }

        public async Task<Ceilapp.Models.ceilapp.Session> CancelSessionChanges(Ceilapp.Models.ceilapp.Session item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnSessionUpdated(Ceilapp.Models.ceilapp.Session item);
        partial void OnAfterSessionUpdated(Ceilapp.Models.ceilapp.Session item);

        public async Task<Ceilapp.Models.ceilapp.Session> UpdateSession(int id, Ceilapp.Models.ceilapp.Session session)
        {
            OnSessionUpdated(session);

            var itemToUpdate = Context.Sessions
                              .Where(i => i.Id == session.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(session);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterSessionUpdated(session);

            return session;
        }

        partial void OnSessionDeleted(Ceilapp.Models.ceilapp.Session item);
        partial void OnAfterSessionDeleted(Ceilapp.Models.ceilapp.Session item);

        public async Task<Ceilapp.Models.ceilapp.Session> DeleteSession(int id)
        {
            var itemToDelete = Context.Sessions
                              .Where(i => i.Id == id)
                              .Include(i => i.Groupes)
                              .Include(i => i.CourseRegistrations)
                              .Include(i => i.AppSettings)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnSessionDeleted(itemToDelete);


            Context.Sessions.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterSessionDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportStatesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/states/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/states/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportStatesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/ceilapp/states/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/ceilapp/states/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnStatesRead(ref IQueryable<Ceilapp.Models.ceilapp.State> items);

        public async Task<IQueryable<Ceilapp.Models.ceilapp.State>> GetStates(Query query = null)
        {
            var items = Context.States.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnStatesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnStateGet(Ceilapp.Models.ceilapp.State item);
        partial void OnGetStateById(ref IQueryable<Ceilapp.Models.ceilapp.State> items);


        public async Task<Ceilapp.Models.ceilapp.State> GetStateById(string id)
        {
            var items = Context.States
                              .AsNoTracking()
                              .Where(i => i.Id == id);

 
            OnGetStateById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnStateGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnStateCreated(Ceilapp.Models.ceilapp.State item);
        partial void OnAfterStateCreated(Ceilapp.Models.ceilapp.State item);

        public async Task<Ceilapp.Models.ceilapp.State> CreateState(Ceilapp.Models.ceilapp.State state)
        {
            OnStateCreated(state);

            var existingItem = Context.States
                              .Where(i => i.Id == state.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.States.Add(state);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(state).State = EntityState.Detached;
                throw;
            }

            OnAfterStateCreated(state);

            return state;
        }

        public async Task<Ceilapp.Models.ceilapp.State> CancelStateChanges(Ceilapp.Models.ceilapp.State item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnStateUpdated(Ceilapp.Models.ceilapp.State item);
        partial void OnAfterStateUpdated(Ceilapp.Models.ceilapp.State item);

        public async Task<Ceilapp.Models.ceilapp.State> UpdateState(string id, Ceilapp.Models.ceilapp.State state)
        {
            OnStateUpdated(state);

            var itemToUpdate = Context.States
                              .Where(i => i.Id == state.Id)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(state);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterStateUpdated(state);

            return state;
        }

        partial void OnStateDeleted(Ceilapp.Models.ceilapp.State item);
        partial void OnAfterStateDeleted(Ceilapp.Models.ceilapp.State item);

        public async Task<Ceilapp.Models.ceilapp.State> DeleteState(string id)
        {
            var itemToDelete = Context.States
                              .Where(i => i.Id == id)
                              .Include(i => i.Municipalities)
                              .Include(i => i.CourseRegistrations)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnStateDeleted(itemToDelete);


            Context.States.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterStateDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}