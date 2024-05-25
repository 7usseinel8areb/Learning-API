var builder = WebApplication.CreateBuilder(args);

var ConnectionString = builder.Configuration.GetConnectionString("con")
    ?? throw new InvalidOperationException("No connection string was found");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(ConnectionString);
});
builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddPolicy("HusseinPolicy", corsPolicyBuilder =>
    {
        corsPolicyBuilder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("HusseinPolicy");
app.UseRouting();

app.UseFileServer();
app.UseAuthorization();

app.MapControllers();

app.Run();
