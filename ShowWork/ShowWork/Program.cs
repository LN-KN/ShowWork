using ShowWork.BL.Auth;
using ShowWork.BL.General;
using ShowWork.BL.Profile;
using ShowWork.BL.Resume;
using ShowWork.DAL_MSSQL;

namespace ShowWork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });


            builder.Services.AddScoped<IAuth, Auth>();
            builder.Services.AddSingleton<IEncrypt, Encrypt>();
            builder.Services.AddScoped<ICurrentUser, CurrentUser>(); //Scoped потому что юзеры разные, синглтон для одинаковых данных
            builder.Services.AddSingleton<IAuthDal, AuthDAL>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddSingleton<IUserTokenDAL, UserTokenDAL>();
            builder.Services.AddSingleton<IDbSessionDAL, DbSessionDAL>();
            builder.Services.AddScoped<IDbSession, DbSession>();
            builder.Services.AddScoped<IWebCookie, WebCookie>();
            builder.Services.AddSingleton<IProfileDAL, ProfileDAL>();
            builder.Services.AddSingleton<IProfile, Profile>();
            builder.Services.AddSingleton<IResume, Resume>();
            builder.Services.AddSingleton<IWork, Work>();
            builder.Services.AddSingleton<IWorkDAL, WorkDAL>();
            builder.Services.AddSingleton<IGradesDAL, GradesDAL>();
            builder.Services.AddSingleton<IFollowDAL, FollowDAL>();
            builder.Services.AddSingleton<ICommentDAL, CommentDAL>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                //app.UseWebAssemblyDebugging();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Use CORS middleware
            app.UseCors("AllowAllOrigins");

            app.UseRouting();

            app.UseAuthorization();
            app.UseBlazorFrameworkFiles();

            app.MapControllerRoute(
                name: "area",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            DbHelper.connString = app.Configuration["ConnectionStrings:Default"] ?? "";
            app.Run();
        }
    }
}
