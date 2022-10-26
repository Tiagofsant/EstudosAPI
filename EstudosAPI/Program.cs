using AutoWrapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


//configura��o do cors 
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
    builder => builder
    .SetIsOriginAllowedToAllowWildcardSubdomains()
    .SetIsOriginAllowed((host) => true)
    .AllowAnyMethod()
    .AllowAnyHeader());
});

var app = builder.Build();

//using AutoWrapper - Envelopamento - //mostra a reposta no console do erro que aparece na lista - por padr�o ele nao mostra
app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions { IsDebug = true });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy"); //libera��o do cors para acesso externo a esta API

app.UseHttpsRedirection();

app.MapControllers();

app.Run();