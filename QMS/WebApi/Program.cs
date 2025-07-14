using Application.Ports;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId;
using Application.ProcessOutputLog.GetProcessOutputLogsByDwellingId;
using Application.ProcessOutputLog.GetProcessOutputLogsByEntranceId;
using Application.Rule.ChangeRuleStatus;
using Application.Rule.CreateRule;
using Application.Rule.GetAllRules;
using Application.Rule.GetRule;
using Application.Rule.GetRulesByEntity;
using Application.Rule.GetRulesByQualityAction;
using Application.Rule.GetRulesByVariableAndEntity;
using Application.Rule.UpdateRule;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Text.Json.Serialization;
using Application.ProcessOutputLog.ResolveProcessOutputLog;
using Application.Quality.BuildingQualityCheck;
using Application.Quality.RulesExecutor;
using WebApi.Common;
using Application.Quality.AutomaticRules;
using Application.ProcessOutputLog.PendOutputLog;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingIdAndStatus;
using Application.FieldWork.GetAllFieldWork;
using Application.FieldWork.CreateFieldWork;
using Application.FieldWork.UpdateFieldWork;
using Application.FieldWork.GetActiveFieldWork;
using Application.EmailTemplate.GetAllEmailTemplates;
using Application.EmailTemplate.UpdateEmailTemplate;
using Application.EmailTemplate.CreateEmailTemplate;
using Application.EmailTemplate.GetEmailTemplate;
using Application.FieldWork.GetFieldWork;
using Application.EmailTemplate.GetAllEmailTemplate;
using Application.Note.CreateNote;
using Application.Note.GetBuildingNotes;
using Application.Note.GetNote;
using Application.Note.UpdateNote;
using Application.Note.DeleteNote;
using Application.FieldWorkRule.AddFieldWorkRule;
using Application.FieldWorkRule.RemoveFieldWorkRule;
using Application.FieldWorkRule.GetFieldWorkRule;
using Application.FieldWorkRule.GetRuleByFieldWork;
using Application.FieldWork.OpenFieldWork;
using Application.Common.Validator;
using FluentValidation;
using Application.FieldWork.UpdateBldReviewStatus;
using Hangfire;
using Hangfire.SqlServer;
using Infrastructure.BackgroundJobs.Hangfire;
using Application.FieldWork.SendFieldWorkEmail;
using Hangfire.MemoryStorage;
using Application.FieldWorkRule.GetRuleByFieldWorkAndEntity;
using Application.Rule.GetActiveRules;
using Application.FieldWork.ExecuteJob;
using Application.FieldWork.GetJobStatus;
using Application.FieldWork.GetJobResults;
using Application.FieldWork.AssociateEmailTemplateWithFieldWork;
using Application.Rule.GetRulesByEntityAndStatus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("QMSConnectionString");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));

var jwtSettingsConfiguration = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsConfiguration);
var jwtSettings = jwtSettingsConfiguration.Get<JwtSettings>();

builder.Services.AddHangfire(x =>
    x.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangFireConnectionString")));

builder.Services.AddHangfireServer();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


builder.Services.AddScoped<IAuthTokenService, JwtService>();
builder.Services.AddScoped<ISendFieldWorkEmail, SendFieldWorkEmailService>();

builder.Services.AddScoped<IRuleRepository, RuleRepository>();
builder.Services.AddScoped<IProcessOutputLogRepository, ProcessOutputLogRepository>();
builder.Services.AddScoped<IFieldWorkRepository, FieldWorkRepository>();
builder.Services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<IFieldWorkRuleRepository, FieldWorkRuleRepository>();


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
builder.Services.AddScoped<AutomaticRules>();
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
builder.Services.AddScoped<IJobExecutor, JobExecutor>();
builder.Services.AddScoped<IUpdateBldReviewStatus, UpdateBldReviewStatus>();
builder.Services.AddScoped<OpenFieldWork>();
builder.Services.AddScoped<IGetJobStatus, GetJobStatus>();
builder.Services.AddScoped<IGetJobResults, GetJobResults>();
builder.Services.AddScoped<IJobDispatcher, JobDispatcher>();

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
builder.Services.AddControllers();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        if (jwtSettings?.AccessTokenSettings.SecretKey != null)
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(jwtSettings.AccessTokenSettings.SecretKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
    });



builder.Services.AddHealthChecks();
var app = builder.Build();
app.MapHealthChecks("/health");
// REGISTER MIDDLEWARE HERE




if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QMS.API v1"));
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new BasicDashboardAuthorizationFilter() }
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

