using Application.MediatR.Admin.Page.Commands;
using AutoMapper;
using Data.Entities;

namespace WebCMS.Areas.Admin.Features.Pages.Create
{
    public static class CreatePageViewModelMappers
    {
        public static CreatePageCommand ToCreatePageCommand(this CreatePageViewModel viewModel)
        {
            return Mapper.Map<CreatePageCommand>(viewModel);
        }

        public static CreatePageViewModel ToCreatePageViewModel(this PageEntity entity)
        {
            return Mapper.Map<CreatePageViewModel>(entity);
        }
    }
}
