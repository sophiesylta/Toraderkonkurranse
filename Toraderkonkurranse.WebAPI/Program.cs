using Microsoft.EntityFrameworkCore;
using Toraderkonkurranse.Application;
using Toraderkonkurranse.Application.Contracts;
using Toraderkonkurranse.Application.Contracts.Repository;
using Toraderkonkurranse.Infrastructure.Persistence.Context;
using Toraderkonkurranse.Infrastructure.Persistence.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IArrangementRepository, ArrangementRepository>();
builder.Services.AddScoped<IDeltakerRepository, DeltakerRepository>();
builder.Services.AddScoped<IDommerRepository, DommerRepository>();
builder.Services.AddScoped<IDommerService, DommerService>();
builder.Services.AddScoped<IArrangementService, ArrangementService>();
builder.Services.AddScoped<IDeltakerService, DeltakerService>();
builder.Services.AddScoped<IKonkurranseService, KonkurranseService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof(Program));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ToraderkonkurranseContext>(options => options.UseSqlServer("Data Source = 127.0.0.1, 1401; Initial Catalog = Toraderkonkurranse; User ID = SA; Password = Password9!"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//using (var serviceScope = app.Services.CreateScope())
//{
//    var toraderDbContext = serviceScope.ServiceProvider.GetRequiredService<ToraderkonkurranseContext>();
//    //uttrekkDbContext?.Database.Migrate();


//    //Tømme database
//    toraderDbContext?.Database.EnsureDeleted();

//    //Oppretter database
//    toraderDbContext?.Database.EnsureCreated();

//    //var initDataService = serviceScope.ServiceProvider.GetRequiredService<IInitDataService>();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
