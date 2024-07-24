using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Text;
using TestAbsensi.Middlewares;
using TestAbsensi.Models;
using TestAbsensi.Repositories;
using TestAbsensi.Seeds;
using TestAbsensi.Services.Absensi;
using TestAbsensi.Services.Auth;
using TestAbsensi.Services.Karyawan;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

});
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ini secret key JWT yang panjang sekali hardcode")),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("redis-11143.c1.asia-northeast1-1.gce.redns.redis-cloud.com:11143,abortConnect=False,password=BtyFW6PO3uUue6zROGCfkYeZ0C2ChAu9"));
builder.Services.AddTransient<ErrorHandling>();
#region Repository inject
builder.Services.AddTransient<IRepository<KaryawanModel>, Repository<KaryawanModel>>();
builder.Services.AddTransient<IRepository<AbsensiModel>, Repository<AbsensiModel>>();
builder.Services.AddTransient<IRepository<UserModel>, Repository<UserModel>>();
#endregion

#region Services inject
builder.Services.AddTransient<IKaryawanService, KaryawanService>();
builder.Services.AddTransient<IAbsensiService, AbsensiService>();
builder.Services.AddTransient<IAuthService, AuthService>();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,

        },
        new List<string>()
    }
});
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    UserSeeder.Initialize(context);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseMiddleware<TokenValidation>();
app.UseAuthorization();

app.UseMiddleware<ErrorHandling>();


app.MapControllers();

app.Run();
