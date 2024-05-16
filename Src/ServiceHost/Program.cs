using System.Text.Encodings.Web;
using System.Text.Unicode;
using _0_Framework.Application;
using _0_Framework.Application.Sms;
using _0_Framework.Application.ZarinPal;
using _0_Framework.AutoMapperProfiles;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.Configuration;
using BlogManagement.Infrastructure.Configuration;
using ChatManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Configuration;
using EdixleQuery.Contracts;
using EdixleQuery.Query;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.SignalR;
using PlanManagement.Infrastructure.Configuration;
using ReviewManagement.Infrastructure.Configuration;
using ServiceHost;
using ServiceHost.Hubs;
using WalletManagement.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("EditorLand");

builder.Services.AddHttpContextAccessor();

BlogManagementBootstrapper.Configure(builder.Services, connectionString);
AccountManagementBootstrapper.Configure(builder.Services, connectionString);
CommentManagementBootstrapper.Configure(builder.Services, connectionString);
ReviewManagementBootstrapper.Configure(builder.Services, connectionString);
ChatManagementBootstrapper.Configure(builder.Services, connectionString);
PlanManagementBootstrapper.Configure(builder.Services, connectionString);
WalletManagementBootstrapper.Configure(builder.Services, connectionString);

builder.Services.AddTransient<IAuthHelper, AuthHelper>();
builder.Services.AddTransient<IGoogleRecaptcha, GoogleRecaptcha>();
builder.Services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<IUserIdProvider, CustomUserIdProvider>();
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
builder.Services.AddTransient<ISharedQuery, SharedQuery>();
builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddTransient<IFileHostUploader, FileHostUploader>();
builder.Services.AddAutoMapper(typeof(BaseRepositoryProfile));
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
    options.Secure = CookieSecurePolicy.Always;
});
builder.Services.AddTransient<ISmsService, SmsService>();

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new System.IO.DirectoryInfo("."))
    //.ProtectKeysWithCertificate(new X509Certificate2());
    .SetDefaultKeyLifetime(TimeSpan.FromDays(90));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
    {
        o.LoginPath = new PathString("/Login");
        o.LogoutPath = new PathString("/Login");
        o.AccessDeniedPath = new PathString("/AccessDenied");
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminArea", builder =>
        builder.RequireRole(new List<string> { Roles.Admin, Roles.ArticleUploader, Roles.ContentController }));

    options.AddPolicy("PersonalPages", builder => builder.RequireRole(new List<string> { Roles.Admin, Roles.ContentController }));
    options.AddPolicy("Account", builder => builder.RequireRole(new List<string> { Roles.Admin }));
    options.AddPolicy("PortfolioUsageCategory", builder => builder.RequireRole(new List<string> { Roles.Admin }));
    options.AddPolicy("PortfolioCategory", builder => builder.RequireRole(new List<string> { Roles.Admin }));
    options.AddPolicy("Comments", builder => builder.RequireRole(new List<string> { Roles.Admin, Roles.ContentController }));
    options.AddPolicy("Portfolios", builder => builder.RequireRole(new List<string> { Roles.Admin, Roles.ContentController }));
    options.AddPolicy("TextSlider", builder => builder.RequireRole(new List<string> { Roles.Admin, Roles.ContentController }));
    options.AddPolicy("Blog", builder => builder.RequireRole(new List<string> { Roles.Admin, Roles.ArticleUploader }));
    options.AddPolicy("Feature", builder => builder.RequireRole(new List<string> { Roles.Admin, Roles.ContentController }));
    options.AddPolicy("Role", builder => builder.RequireRole(new List<string> { Roles.Admin }));

});
// Add Hangfire services.
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    }));
// Add the processing server as IHostedService
builder.Services.AddHangfireServer();


// Add services to the container.
builder.Services.AddRazorPages().AddMvcOptions(options => options.Filters.Add<SecurityPageFilter>())
    .AddRazorPagesOptions(options =>
    {

        options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
        options.Conventions.AuthorizeAreaFolder("Administration", "/PersonalPages", "PersonalPages");
        options.Conventions.AuthorizeAreaFolder("Administration", "/PortfolioUsageCategory", "PortfolioUsageCategory");
        options.Conventions.AuthorizeAreaFolder("Administration", "/PortfolioCategory", "PortfolioCategory");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Portfolio", "Portfolios");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");
        options.Conventions.AuthorizeAreaFolder("Administration", "/TextSlider", "TextSlider");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Blog", "Blog");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Feature", "Feature");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Role", "Role");
    });

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();

}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// HangFire Dashboard
app.UseHangfireDashboard("/Administration/Hangfire", new DashboardOptions
{
    Authorization = new[] { new HangfireAuthorizationFilter() }
});

app.MapRazorPages();

// HangFire Dashboard endpoint
app.MapHangfireDashboard("/Administration/Hangfire");

// SignalR Hubs
app.MapHub<ChatHub>("/ChatHub");
app.MapHub<UploaderHub>("/UploaderHub");

app.Run();
