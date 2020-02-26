using Application.MediatR.Common.Auth.Commands;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Areas.Admin.Features.Auth.Login
{
    public static class LoginViewModelMappers
    {
        public static LoginCommand ToLoginCommand(this LoginViewModel viewModel)
        {
            return Mapper.Map<LoginCommand>(viewModel);
        }
    }
}
