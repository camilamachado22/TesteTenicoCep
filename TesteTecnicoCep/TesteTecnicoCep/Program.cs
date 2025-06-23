using Microsoft.EntityFrameworkCore;
using TesteTecnicoCep.Mapping;
using TesteTecnicoCep.Mapping;
using TesteTecnicoCep.Services;






var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddDbContext<TesteTecnicoCep.Data.TesteTecnicoCepDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddAutoMapper(typeof(MappingTesteProfile));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddHttpClient<CepService>();



builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
  
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
