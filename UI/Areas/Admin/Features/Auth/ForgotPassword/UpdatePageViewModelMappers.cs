using Application.MediatR.Admin.Page.Commands;
using Application.MediatR.Common.Auth.Commands;
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
