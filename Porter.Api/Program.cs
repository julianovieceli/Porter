using Microsoft.AspNetCore.Mvc;
using Porter.Application;
using Porter.Common;
using Porter.Common.Utils;
using Porter.Dto;
using Porter.Infra.Postgres.Repository;
using System.Linq;

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


builder.Services.AddApplicationServices();
builder.Services.AddAutoMapper();
builder.Services.AddValidators();
builder.Services.ConfigurePostGresDbContext(builder.Configuration);

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
