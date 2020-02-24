using Application.MediatR.Admin.Auth.Commands;
using Application.MediatR.Admin.Page.Commands;
using Application.MediatR.Common.Auth.Queries.IsSuperAdminExist;
using AutoMapper;

namespace UI.Areas.Admin.Features.Auth.ResetPassword
{
    public static class ResetPasswordViewModelMappers
    {
        public static ValidatePasswordResetTokenQuery ToValidatePasswordResetTokenQuery(this ResetPasswordViewModel viewModel)
        {
            return Mapper.Map<ValidatePasswordResetTokenQuery>(viewModel);
        }

        public static ResetPasswordCommand ToResetPasswordCommand(this ResetPasswordViewModel viewModel)
        {
            return Mapper.Map<ResetPasswordCommand>(viewModel);
        }
    }
}
