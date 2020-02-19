using Application.MediatR.Admin.Page.Commands;
using Application.MediatR.Admin.User.Commands.UpdateUser;
using AutoMapper;
using Data.Entities;

namespace WebCMS.Areas.Admin.Features.Pages.Update
{
    public static class UpdatePageViewModelMappers
    {
        public static UpdatePageCommand ToUpdatePageCommand(this UpdatePageViewModel viewModel)
        {
            return Mapper.Map<UpdatePageCommand>(viewModel);
        }

        public static UpdatePageViewModel ToUpdatePageViewModel(this PageEntity entity)
        {
            return Mapper.Map<UpdatePageViewModel>(entity);
        }
    }
}
