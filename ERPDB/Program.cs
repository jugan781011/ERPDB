using ERPDB.Model;
using ERPDB.Repositorys;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ERPAPI",
        Description = "New Swagger Document",
        Version = "v0724",
    });
    var filename = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
    var filepath = Path.Combine(AppContext.BaseDirectory, filename);
    options.IncludeXmlComments(filepath);
});

#region Service
builder.Services.AddTransient<Itb_UserMemberRepository, tb_UserMemberRepository>();
#endregion

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddSession();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var connectionString = builder.Configuration.GetConnectionString("MyDbContext");
builder.Services.AddDbContext<MyDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors();
app.UseSession();
app.Run();

