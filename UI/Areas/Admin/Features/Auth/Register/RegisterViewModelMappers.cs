using Application.MediatR.Admin.Auth.Commands.CreateSuperAdmin;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Areas.Admin.Features.Auth.Register
{
    public static class RegisterViewModelMappers
    {
        public static CreateSuperAdminCommand ToCreateSuperAdminCommand(this RegisterViewModel viewModel)
        {
            return Mapper.Map<CreateSuperAdminCommand>(viewModel);
        }
    }
}
