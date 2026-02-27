using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;
using Application.Building.CreateAnnualSnapshot;
using Application.Building.GetAllAnnualSnapshots;
using Application.Building.GetAnnualSnapshotById;
using Application.Building.GetBldJobStatus;
using Application.Building.GetRunningJobs;
using Application.Building.TestBuildings;
using Application.Common.Validator;
using Application.EmailTemplate.CreateEmailTemplate;
using Application.EmailTemplate.GetAllEmailTemplate;
using Application.EmailTemplate.GetAllEmailTemplates;
using Application.EmailTemplate.GetEmailTemplate;
using Application.EmailTemplate.UpdateEmailTemplate;
using Application.FieldWork;
using Application.FieldWork.AssociateEmailTemplateWithFieldWork;
using Application.FieldWork.CanBeClosed;
using Application.FieldWork.ConfirmFieldworkClosure;
using Application.FieldWork.CreateFieldWork;
using Application.FieldWork.ExecuteJob;
using Application.FieldWork.GetActiveFieldWork;
using Application.FieldWork.GetAllFieldWork;
using Application.FieldWork.GetFieldWork;
using Application.FieldWork.GetJobResults;
using Application.FieldWork.GetJobStatus;
using Application.FieldWork.OpenFieldWork;
using Application.FieldWork.SendFieldWorkEmail;
using Application.FieldWork.TestUntestedBld;
using Application.FieldWork.UpdateBldReviewStatus;
using Application.FieldWork.UpdateFieldWork;
using Application.FieldWorkRule.AddFieldWorkRule;
using Application.FieldWorkRule.GetFieldWorkRule;
using Application.FieldWorkRule.GetRuleByFieldWork;
using Application.FieldWorkRule.GetRuleByFieldWorkAndEntity;
using Application.FieldWorkRule.RemoveFieldWorkRule;
using Application.Note.CreateNote;
using Application.Note.DeleteNote;
using Application.Note.GetBuildingNotes;
using Application.Note.GetNote;
using Application.Note.UpdateNote;
using Application.Ports;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingIdAndStatus;
using Application.ProcessOutputLog.GetProcessOutputLogsByDwellingId;
using Application.ProcessOutputLog.GetProcessOutputLogsByEntranceId;
using Application.ProcessOutputLog.PendOutputLog;
using Application.ProcessOutputLog.ResolveProcessOutputLog;
using Application.Quality.AllBuildingsAutomaticRules;
using Application.Quality.AllBuildingsQualityCheck;
using Application.Quality.AutomaticRules;
using Application.Quality.BuildingQualityCheck;
using Application.Quality.RulesExecutor;
using Application.Quality.SetBldToUntested;
using Application.Queries.GetBuildingQualityStats;
using Application.Queries.GetBuildingSummaryStats;
using Application.Queries.GetBuildingWithQuePendingLogs;
using Application.Queries.GetDwellingQualityStats;
using Application.Queries.GetFieldworkProgressByMunicipality;
using Application.Queries.HasBldReviewExecuted;
using Application.Rule.ChangeRuleStatus;
using Application.Rule.CreateRule;
using Application.Rule.GetActiveRules;
using Application.Rule.GetAllRules;
using Application.Rule.GetRule;
using Application.Rule.GetRulesByEntity;
using Application.Rule.GetRulesByEntityAndStatus;
using Application.Rule.GetRulesByQualityAction;
using Application.Rule.GetRulesByVariableAndEntity;
using Application.Rule.UpdateRule;
using FluentValidation;
using Hangfire;
using Infrastructure.BackgroundJobs.Hangfire;
using Infrastructure.Context;
using Infrastructure.Queries.GetBuildingQualityStats;
using Infrastructure.Queries.GetBuildingSummaryStats;
using Infrastructure.Queries.GetBuildingWithQuePendingLogs;
using Infrastructure.Queries.GetDwellingQualityStats;
using Infrastructure.Queries.GetFieldworkProgressByMunicipality;
using Infrastructure.Queries.HasBldReviewExecuted;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using WebApi.Common;
using WebSocketManager = Infrastructure.Services.WebSocketManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("QMSConnectionString");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));

var jwtSettingsConfiguration = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsConfiguration);
var jwtSettings = jwtSettingsConfiguration.Get<JwtSettings>();
var jwtSecret = jwtSettings?.AccessTokenSettings?.SecretKey ?? throw new Exception("JWT secret key missing");

builder.Services.Configure<SmtpOptions>(
    builder.Configuration.GetSection(SmtpOptions.SectionName));

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero
};

builder.Services.AddHangfire((sp, cfg) =>
{
    cfg.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangFireConnectionString"));
    cfg.UseActivator(new Hangfire.AspNetCore.AspNetCoreJobActivator(
        sp.GetRequiredService<IServiceScopeFactory>()));
});
builder.Services.AddHangfireServer();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
//duhet singletone sepse e perdorim ne WebSocketManager 1 instance per gjithe aplikacionin
builder.Services.AddSingleton<IWebSocketBroadcaster, WebSocketManager>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddScoped<IAuthTokenService, JwtService>();
builder.Services.AddScoped<ISendFieldWorkEmail, SendFieldWorkEmailService>();
builder.Services.AddScoped<IRuleRepository, RuleRepository>();
builder.Services.AddScoped<IProcessOutputLogRepository, ProcessOutputLogRepository>();
builder.Services.AddScoped<IFieldWorkRepository, FieldWorkRepository>();
builder.Services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<IFieldWorkRuleRepository, FieldWorkRuleRepository>();
builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();
builder.Services.AddScoped<CreateRule>();
builder.Services.AddScoped<GetAllRules>();
builder.Services.AddScoped<GetActiveRules>();
builder.Services.AddScoped<GetRulesByVariableAndEntity>();
builder.Services.AddScoped<IGetRulesByEntityAndStatus,GetRulesByEntityAndStatus>();
builder.Services.AddScoped<GetRulesByEntity>();
builder.Services.AddScoped<GetRule>();
builder.Services.AddScoped<GetRulesByQualityAction>();
builder.Services.AddScoped<ChangeRuleStatus>();
builder.Services.AddScoped<UpdateRule>();
builder.Services.AddScoped<GetProcessOutputLogsByBuildingId>();
builder.Services.AddScoped<GetProcessOutputLogsByEntranceId>();
builder.Services.AddScoped<GetProcessOutputLogsByDwellingId>();
builder.Services.AddScoped<GetProcessOutputLogsByBuildingIdAndStatus>();
builder.Services.AddScoped<IBuildingQualityCheck, BuildingQualityCheck>();
builder.Services.AddScoped<IResolveProcessOutputLog, ResolveProcessOutputLog>();
builder.Services.AddScoped<IPendProcessOutputLog, PendProcessOutputLog>();
builder.Services.AddScoped<RulesExecutor>();
builder.Services.AddScoped<BuildingQualityCheck>();
builder.Services.AddScoped<AllBuildingsQualityCheck>();
builder.Services.AddScoped<AutomaticRules>();
builder.Services.AddScoped<SetBldToUntested>();
builder.Services.AddScoped<AllBuildingsAutomaticRules>();
builder.Services.AddScoped<IAutomaticRules, AutomaticRules>();
builder.Services.AddScoped<CreateFieldWork>();
builder.Services.AddScoped<GetAllFieldWork>();
builder.Services.AddScoped<GetFieldWork>();
builder.Services.AddScoped<UpdateFieldWork>();
builder.Services.AddScoped<IAssociateEmailTemplateWithFieldWork, AssociateEmailTemplateWithFieldWork>();
builder.Services.AddScoped<GetActiveFieldWork>();
builder.Services.AddScoped<ICreateEmailTemplate, CreateEmailTemplate>();
builder.Services.AddScoped<IGetAllEmailTemplate, GetAllEmailTemplate>();
builder.Services.AddScoped<IGetEmailTemplate, GetEmailTemplate>();
builder.Services.AddScoped<IUpdateEmailTemplate, UpdateEmailTemplate>();
builder.Services.AddScoped<ICreateNote,CreateNote>();
builder.Services.AddScoped<IGetBuildingNotes, GetBuildingNotes>();
builder.Services.AddScoped<IGetNote, GetNote>();
builder.Services.AddScoped<IUpdateNote,UpdateNote>();
builder.Services.AddScoped<IDeleteNote, DeleteNote>();
builder.Services.AddScoped<IAddFieldWorkRule,AddFieldWorkRule>();
builder.Services.AddScoped<IRemoveFieldWorkRule, RemoveFieldWorkRule>();
builder.Services.AddScoped<IGetFieldWorkRule, GetFieldWorkRule>();
builder.Services.AddScoped<IGetRuleByFieldWork, GetRuleByFieldWork>();
builder.Services.AddScoped<IGetRuleByFieldWorkAndEntity, GetRuleByFieldWorkAndEntity>();
builder.Services.AddScoped<IExecuteJob, ExecuteJob>();
builder.Services.AddScoped<ITestUntestedBld, TestUntestedBld>();
builder.Services.AddScoped<ICanBeClosed, CanBeClosed>();
builder.Services.AddScoped<IJobExecutor, JobExecutor>();
builder.Services.AddScoped<IUpdateBldReviewStatus, UpdateBldReviewStatus>();
builder.Services.AddScoped<OpenFieldWork>();
builder.Services.AddScoped<IGetJobStatus, GetJobStatus>();

builder.Services.AddScoped<IGetJobResults, GetJobResults>();
builder.Services.AddScoped<IJobDispatcher, JobDispatcher>();
builder.Services.AddScoped<UpdateFieldworkStatus>();
builder.Services.AddScoped<IGetBuildingSummaryStatsQuery, GetBuildingSummaryStatsQuery>();
builder.Services.AddScoped<IConfirmFieldworkClosure, ConfirmFieldworkClosure>();
builder.Services.AddScoped<IGetFieldworkProgressByMunicipalityQuery, GetFieldworkProgressByMunicipalityQuery>();
builder.Services.AddScoped<FieldWorkJobService>();

builder.Services.AddScoped<ITestBuildings, TestBuildings>();
builder.Services.AddScoped<IHasBldReviewExecutedQuery, HasBldReviewExecutedQuery>();

builder.Services.AddScoped<IGetBldJobStatus, GetBldJobStatus>();
builder.Services.AddScoped<GetRunningJobs>();
builder.Services.AddScoped<IGetDwellingQualityStatsQuery, GetDwellingQualityStatsQuery>();
builder.Services.AddScoped<IGetBuildingQualityStatsQuery, GetBuildingQualityStatsQuery>();
builder.Services.AddScoped<ICreateAnnualSnapshot, CreateAnnualSnapshot>();
builder.Services.AddScoped<IAnnualSnapshotExecutor, AnnualSnapshotExecutor>();
builder.Services.AddScoped<IDownloadJobRepository, DownloadJobRepository>();
builder.Services.AddScoped<IGetAllAnnualSnapshots, GetAllAnnualSnapshots>();
builder.Services.AddScoped<IGetAnnualSnapshotById, GetAnnualSnapshotById>();
builder.Services.AddScoped<IGetBuildingWithQuePendingLogs, GetBuildingWithQuePendingLogsQuery>();

builder.Services.AddValidatorsFromAssemblyContaining<OpenFieldWorkRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateFieldWorkRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GetFieldWorkRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RemoveFieldWorkRuleRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateFieldWorkRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AddFieldWorkRuleRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GetFieldWorkRuleRequestValidator> ();


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {accessToken}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = tokenValidationParameters;
    });

builder.Services.AddHealthChecks();
var app = builder.Build();
app.MapHealthChecks("/health");
// REGISTER MIDDLEWARE HERE

app.UseRouting();

app.UseCors("AllowAll");

app.UseWebSockets();


app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/api/qms/fieldwork/is-active") &&
        context.WebSockets.IsWebSocketRequest &&
        context.Request.Headers.TryGetValue("Authorization", out var authHeader))
    {
        var token = authHeader.ToString().Replace("Bearer ", "");

        var tokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = tokenValidationParameters;
        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            context.User = principal;
        }
        catch
        {
            context.Response.StatusCode = 401;
            return;
        }
    }

    await next();
});




app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();   
}
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QMS.API v1"));

app.UseMiddleware<ErrorHandlerMiddleware>();



app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = [new BasicDashboardAuthorizationFilter()],
    IsReadOnlyFunc = context => true
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.Map("/api/qms/fieldwork/is-active", async context =>
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            var socket = await context.WebSockets.AcceptWebSocketAsync();
            var wsManager = context.RequestServices.GetRequiredService<IWebSocketBroadcaster>();
            await wsManager.HandleConnection(context, socket);
        }
        else
        {
            context.Response.StatusCode = 400;
        }
    });
});

app.Run();

