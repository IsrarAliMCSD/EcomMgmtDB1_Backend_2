using Code_EcomMgmtDB1.JwtHelper;
using Code_EcomMgmtDB1.JWTToken1;
using Code_EcomMgmtDB1.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Your API", Version = "v1" });

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token.\r\n\r\nExample: \"Bearer eyJhbGci...\""
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      builder =>
                      {
                          //builder.WithOrigins("http://localhost:3000",
                          //                    "https://localhost:3000")
                          //.WithHeaders()
                          //.WithMethods("GET", "POST", "DELETE");
                          ////.AllowAnyHeader()
                          ////.AllowAnyOrigin()
                          ////.AllowAnyMethod();

                          //you can configure your custom policy
                          builder.AllowAnyMethod().AllowAnyOrigin()
                                              .AllowAnyHeader()
                                              .AllowAnyMethod();

                      });


});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    //string keyToken = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
    //var Key = Encoding.UTF8.GetBytes(keyToken);
    var Key = Encoding.UTF8.GetBytes("s3cure_and_l0ng_256bit_key_example_12345678");

    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://localhost:7075/",
        ValidAudience = "localhost:7075",//"Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});


builder.Services.AddScoped<JWTToken, JWTToken>();
builder.Services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();

builder.Services.AddDbContext<EcomMgmtDb1Context>(options =>
//options.UseSqlServer(_configuration.GetConnectionString("CompanyDB")));
options.UseSqlServer("data source=.;Initial Catalog=EcomMgmtDB1;Integrated Security=True;"));


builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseCors("_myAllowSpecificOrigins");
app.UseRouting();
//app.UseCors();
app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();




app.Run();
