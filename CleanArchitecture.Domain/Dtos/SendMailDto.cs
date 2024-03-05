using System.Net.Mail;

namespace CleanArchitecture.Domain.Dtos;

public sealed record SendMailDto(
    string to, 
    string subject, 
    string body
    );
