using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using SimpleApi.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EmpresaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmpresaContext") ?? throw new InvalidOperationException("Connection string 'EmpresaContext' not found.")));

// Default Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy", builder =>
            {
                //for when you're running on localhost
                builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost") 
                .AllowAnyHeader().AllowAnyMethod();
            });
    options.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                  .AllowCredentials();
            });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
