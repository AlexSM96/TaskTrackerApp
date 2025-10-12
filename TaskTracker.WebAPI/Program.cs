var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddControllers();

builder
    .AddSwagger()
    .AddCORS()
    .AddTaskTrackerDb()
    .AddApplicationServices()
    .AddOptions()
    .AddBearerAuthentication()
    ;

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Services.CreateDbIfNotExist();
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
