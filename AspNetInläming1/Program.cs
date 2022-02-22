using Microsoft.EntityFrameworkCore;
using AspNetInläming1.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
var connectionString = builder.Configuration.GetConnectionString("Blog");
builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();
app.UseStaticFiles();
app.UseMvc(routes =>
{
    routes.MapRoute(
      name: "Default",
      template: "{controller=Post}/{action=ShowAllPosts}"
    );
});

app.Run();