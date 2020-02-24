using Application.MediatR.Admin.Auth.Commands;
using Application.MediatR.Admin.Page.Commands;
using AutoMapper;

namespace UI.Areas.Admin.Features.Auth.ForgotPassword
{
    public static class ForgotPasswordViewModelMappers
    {
        public static SendPasswordResetMailCommand ToSendPasswordResetMailCommand(this ForgotPasswordViewModel viewModel)
        {
            return Mapper.Map<SendPasswordResetMailCommand>(viewModel);
        }
    }
}
