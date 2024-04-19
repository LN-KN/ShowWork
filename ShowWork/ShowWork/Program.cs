using ShowWork.BL.Auth;
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

            builder.Services.AddSingleton<IAuthBL, AuthBL>();
            builder.Services.AddSingleton<IEncrypt, Encrypt>();
            builder.Services.AddScoped<ICurrentUser, CurrentUser>(); //Scoped потому что юзеры разные, синглтон для одинаковых данных
            builder.Services.AddSingleton<IAuthDal, AuthDAL>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddMvc().AddSessionStateTempDataProvider();
            builder.Services.AddSession();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
