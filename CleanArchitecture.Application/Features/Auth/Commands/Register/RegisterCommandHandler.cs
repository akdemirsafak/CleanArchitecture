using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Features.Auth.Commands.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, MessageResponse>
{
    private readonly IAuthService _authService;
    private readonly IMailService _mailService;

    public RegisterCommandHandler(IAuthService authService, IMailService mailService)
    {
        _authService = authService;
        _mailService = mailService;
    }

    public async Task<MessageResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user=await _authService.RegisterAsync(request);
        var sendMailModel=new SendMailDto(user.Email,"Aramıza Hoşgeldin.","Burası body kısmı");
        //await _mailService.SendAsync(sendMailModel);
        return new("Kullanıcı başarıyla kaydedildi.");
    }
}
