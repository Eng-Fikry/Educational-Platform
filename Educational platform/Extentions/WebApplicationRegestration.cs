using Domain.Contracts;
using Educational_platform.CustomeMiddelwares;

namespace Educational_platform.Extentions
{
    public static class WebApplicationRegestration
    {
        public static async Task DataSeed(this WebApplication app)
        {
            var Scope = app.Services.CreateScope();
            var dataseeding = Scope.ServiceProvider.GetRequiredService<IDataSeed>();
            await dataseeding.IdentityDataSeed();
        }

        public static void AddMiddleWares(this WebApplication app)
        {
            app.UseMiddleware<CustomeExceptionHandler>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
