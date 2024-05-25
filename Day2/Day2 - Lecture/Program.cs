using Microsoft.AspNetCore.Cors.Infrastructure;

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
//from appCors
builder.Services.AddCors(corsOptions =>
{
    corsOptions.AddPolicy("MyPolicy", corsPolicyBuilder =>
    {
        /*corsPolicyBuilder.WithOrigins("http://localhost:4200")*/
        /*corsPolicyBuilder.AllowAnyHeader()*/
        /*corsPolicyBuilder.AllowAnyOrigin()*/
        corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader();
    });

    corsOptions.AddPolicy("MyPloicy2", corsPolicyBuilder =>
    {
        //???? ???? ???? ?? ??? ??????? ???? ?????
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();//to use HTML and images

//?? ???? ???? ?????? ?????HTML, Angular, .....
//????? ??? ???? ????? 
//http, https ????

/*app.UseCors();//for all */
//to customize policy open for only company 1 2 3 declare configureservice method =>>> go to the service up
app.UseCors("MyPolicy");

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();