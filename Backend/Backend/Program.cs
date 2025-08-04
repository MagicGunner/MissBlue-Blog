using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Backend.Application.Implement;
using Backend.Application.Interface;
using Backend.Common;
using Backend.Common.Core;
using Backend.Common.Option;
using Backend.Contracts.IService;
using Backend.Extensions.ServiceExtensions;
using Backend.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
       .ConfigureContainer<ContainerBuilder>(containerBuilder => { containerBuilder.RegisterModule<AutofacModuleRegister>(); })
       .ConfigureAppConfiguration((hostingContext, config) => { hostingContext.Configuration.ConfigureApplication(); });

builder.ConfigureApplication();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
                                   options.SwaggerDoc("v1", new OpenApiInfo {
                                                                                Version = "v1",
                                                                                Title = "Backend API",
                                                                                Description = "站长后台 API 文档",
                                                                                Contact = new OpenApiContact {
                                                                                                                 Name = "MissBlue",
                                                                                                                 Url = new Uri("https://example.com")
                                                                                                             }
                                                                            });

                                   options.EnableAnnotations();
                                   //
                                   // // 修复 SchemaId 重复（避免同名类引起的冲突）
                                   // c.CustomSchemaIds(type => type.FullName);

                                   // 添加 JWT 认证支持
                                   options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                                                                                                         Description = "请输入Token，格式为：Bearer {token}",
                                                                                                         Name = "Authorization",
                                                                                                         In = ParameterLocation.Header,
                                                                                                         Type = SecuritySchemeType.ApiKey,
                                                                                                         Scheme = "Bearer"
                                                                                                     });

                                   options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                                                                                                     {
                                                                                                         new OpenApiSecurityScheme {
                                                                                                                                       Reference = new OpenApiReference {
                                                                                                                                           Type = ReferenceType.SecurityScheme,
                                                                                                                                           Id = "Bearer"
                                                                                                                                       }
                                                                                                                                   },
                                                                                                         Array.Empty<string>()
                                                                                                     }
                                                                                                 });
                               });

// builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
AutoMapperConfig.RegisterMappings();

builder.Services.AddSingleton(new AppSettings(builder.Configuration));
builder.Services.AddAllOptionRegister();

builder.Services.AddCacheSetup();

builder.Services.AddHostedService<VisitCountSyncService>();


builder.Services.AddAuthentication("Bearer")
       .AddJwtBearer("Bearer", options => {
                                   options.TokenValidationParameters = new TokenValidationParameters {
                                                                                                         ValidateIssuer = true,
                                                                                                         ValidateAudience = true,
                                                                                                         ValidateLifetime = true,
                                                                                                         ValidateIssuerSigningKey = true,
                                                                                                         ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                                                                                                         ValidAudience = builder.Configuration["JwtSettings:Audience"],
                                                                                                         IssuerSigningKey = new SymmetricSecurityKey(
                                                                                                          Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]!))
                                                                                                     };
                               });

builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionRequirementHandler>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();

// MinIO 注入
builder.Services.Configure<MinioOptions>(builder.Configuration.GetSection("Minio"));
builder.Services.AddSingleton<IMinIOService, MinIOService>();

// ORM
builder.Services.AddSqlSugarSetup();

// 消息队列
builder.Services.AddRabbitMQSetup();
builder.Services.AddKafkaSetup(builder.Configuration);
builder.Services.AddEventBusSetup();

// 邮件
builder.Services.Configure<MailOptions>(builder.Configuration.GetSection("MailSettings"));

var app = builder.Build();
app.ConfigureApplication();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI(c => {
                         c.SwaggerEndpoint("/swagger/v1/swagger.json", "MissBlue API V1");
                         c.RoutePrefix = string.Empty; // 根目录显示Swagger
                     });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();