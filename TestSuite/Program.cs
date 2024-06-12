using Narya.Email.Smtp;
using Narya.Email.Sendgrid;
using Narya.Email.Core;
using Narya.Email.Core.Enums;
using Narya.Email.Core.Interfaces;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEmailUsingSmtp();
builder.Services.AddEmailUsingSendgrid();
builder.Services.AddEmailProvider(provider =>
{
    var emailProvider = new EmailProvider();

    emailProvider.AddProvider(EmailProvidersEnum.SendGrid, provider.GetRequiredService<Narya.Email.Sendgrid.Services.EmailService>());
    emailProvider.AddProvider(EmailProvidersEnum.Smtp, provider.GetRequiredService<Narya.Email.Smtp.Services.EmailService>());

    return emailProvider;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();