using BlogStore.BusinessLayer.Abstract;
using BlogStore.BusinessLayer.Concrete;
using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using BlogStore.DataAccessLayer.EntityFramework;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BlogStore.BusinessLayer.Container
{
    public static class ServiceRegistration
    {
        public static void AddBlogStoreServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<IArticleService, ArticleManager>();

            services.AddScoped<ICategoryDal, EfCategoryDAL>();
            services.AddScoped<ICommentDal, EfCommentDAL>();
            services.AddScoped<IArticleDal, EfArticleDal>();

            services.AddDbContext<BlogContext>();

            services.AddIdentity<AppUser, IdentityRole>()
                   .AddEntityFrameworkStores<BlogContext>();

            
            services.AddScoped<IToxicDetectionService, ToxicDetectionService>();
        }
    }
}