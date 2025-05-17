using AutoMapper;
using EventBooking.Application.Helper.ResponseServices;
using EventBooking.Domain.Entities.IdentityEntities;
using EventBooking.Domain.Enums;
using EventBooking.Domain.IRepositories;
using EventBooking.Domain.Utalities.ResponseModel;
using EventBooking.Infrastructure.Models.Email;
using EventBooking.Infrastructure.Services.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace EventBooking.Application.Features.V1.Account.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailServices _services;
        private readonly ILogger<RegisterCommandHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(IUnitOfWork unitOfWork,
            IEmailServices services,
            ILogger<RegisterCommandHandler> logger,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _services = services;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _mapper.Map<User>(request.RegisterDTO);
                IdentityResult result = await _unitOfWork.Users.UserManager.CreateAsync(user, request.RegisterDTO.Password);

                if (!result.Succeeded)
                    return ResponseHandler.Conflict<string>(message: "Failed to create user.");

                await _unitOfWork.Users.UserManager.AddToRoleAsync(user, UserRole.User.ToString());
                var token = await _unitOfWork.Users.UserManager.GenerateEmailConfirmationTokenAsync(user);

                var url = $"{_httpContextAccessor.HttpContext.Request.Scheme.Trim().ToLower()}://{_httpContextAccessor.HttpContext.Request.Host.ToUriComponent().Trim().ToLower()}/api/v1/Account/ConfirmEmail";

                var parameters = new Dictionary<string, string>
                    {
                        {"Token", token },
                        {"UserId", user.Id}
                    };

                var confirmationLink = new Uri(QueryHelpers.AddQueryString(url, parameters));
                var sendEmailModel = new SendEmailRequest
                {
                    To = user.Email!,
                    Subject = "التحقق من البريد الإلكتروني الخاص بك",
                    Body = $@"
                           <div style='font-family: Arial, sans-serif; direction: rtl; text-align: center;'>
                               <h2 style='color: #333;'>التحقق من البريد الإلكتروني الخاص بك</h2>
                               
                               <p style='font-size: 16px; color: #555;'>
                                   شكرًا لانضمامك إلى <strong>Event Booking</strong>!  
                                   لقد اقتربت من الوصول، فقط انقر على الزر أدناه للتحقق من بريدك الإلكتروني.
                               </p>
                           
                               <a href='{confirmationLink}' 
                                  style='display: inline-block; padding: 12px 24px; font-size: 16px; 
                                         color: #fff; background-color: #1a73e8; text-decoration: none;
                                         border-radius: 10px; font-weight: bold; margin-top: 10px;'>
                                   تأكيد البريد الإلكتروني
                               </a>
                           
                               <p style='font-size: 14px; color: #555; font-weight: bold;'>
                                   مع خالص التحيات، <br>
                                   <strong>Event Booking</strong>
                               </p>
                           </div>"

                };

                var emaiModel = await _services.SendEmailAsync(sendEmailModel);
                if (emaiModel.IsSuccess)
                    return ResponseHandler.Success<string>(message: "The account has been registered successfully.");
                return ResponseHandler.BadRequest<string>(message: "Failed to send confirmation email");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during user register");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }

    }
}
