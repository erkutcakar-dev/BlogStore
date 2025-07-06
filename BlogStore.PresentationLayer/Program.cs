using BlogStore.BusinessLayer.Abstract;
using BlogStore.BusinessLayer.Concrete;
using BlogStore.BusinessLayer.Container;

var builder = WebApplication.CreateBuilder(args);

// Blog Store servisleri
builder.Services.AddBlogStoreServices();

// Controllers
builder.Services.AddControllersWithViews();

// HttpClient for ToxicDetectionService
builder.Services.AddHttpClient<ToxicDetectionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();

// Routing Configuration
app.MapControllerRoute(
    name: "articleDetail",
    pattern: "Article/ArticleDetail/{slug}",
    defaults: new { controller = "Article", action = "ArticleDetail" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();