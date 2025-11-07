using Microsoft.AspNetCore.Mvc;
using Personal.Common;
using Personal.Common.Utils;
using Porter.Application;
using Porter.Dto;
using Personal.Common.EF.Repository;
using Porter.Infra.Postgres.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Where(x => x.Value.Errors.Any())
                                         .Select(x => new { Field = x.Key, Messages = x.Value.Errors.Select(e => e.ErrorMessage) })
            .ToList();

            string messagge = String.Join(", ", errors.FirstOrDefault().Messages);  
            
            ErrorResponseDto error = new ErrorResponseDto("400", messagge);


            return new BadRequestObjectResult(error);
        };
    })
      .AddJsonOptions(options =>
      {
          options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter());
      }); ;

builder.Services.AddSwaggerGen();


builder.Services.AddLogService();
builder.Services.AddAutoMapper();
builder.Services.AddValidators();
builder.Services.AddPostgresDbContext<AppDbContext>(builder.Configuration);
builder.Services.AddRepository(builder.Configuration);
builder.Services.RegisterMediator();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
 
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();


public partial class Program { }