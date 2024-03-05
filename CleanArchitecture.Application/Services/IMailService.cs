using CleanArchitecture.Domain.Dtos;

namespace CleanArchitecture.Application.Services;

public interface IMailService
{
    Task SendAsync(SendMailDto sendMailDto);
}
