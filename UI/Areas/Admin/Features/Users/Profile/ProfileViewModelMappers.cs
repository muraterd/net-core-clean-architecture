using Application.MediatR.Admin.User.Commands.UpdateUser;
using AutoMapper;
using Data.Entities;

namespace WebCMS.Areas.Admin.Features.Users.Profile
{
    public static class ProfileViewModelMappers
    {
        public static UpdateUserCommand ToUpdateUserCommand(this ProfileViewModel viewModel) {
            return Mapper.Map<UpdateUserCommand>(viewModel);
        }

        public static ProfileViewModel ToProfileViewModel(this UserEntity user)
        {
            return Mapper.Map<ProfileViewModel>(user);
        }
    }
}
