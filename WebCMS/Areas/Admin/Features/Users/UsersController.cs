using System.Threading.Tasks;
using Application.MediatR.Admin.User.Commands;
using Application.MediatR.Admin.User.Queries.GetUserById;
using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using WebCMS.Areas.Admin.Features.Base;
using WebCMS.Areas.Admin.Features.Users.Profile;
using WebCMS.Areas.Admin.Models.Base;

namespace WebCMS.Areas.Admin.Features.Users
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class UsersController : AuthorizedController
    {
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] GetAllUsersQuery query)
        {
            var result = await Mediator.Send(query);
            var viewModel = Mapper.Map<ListPageViewModel<UserEntity>>(result);

            return View(viewModel);
        }

        [HttpGet("{id}/profile")]
        public async Task<IActionResult> Profile(long id)
        {
            var getUserByIdQuery = new GetUserByIdQuery() { Id = id };

            UserEntity user = await Mediator.Send(getUserByIdQuery);

            return View(user.ToProfileViewModel());
        }

        [HttpPost("{id}/profile")]
        public async Task<IActionResult> Profile(ProfileViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                UserEntity user = await Mediator.Send(new GetUserByIdQuery() { Id = viewModel.Id });

                return View(user.ToProfileViewModel());
            }

            if (viewModel.ProfilePicture != null)
            {
                var deleteAvatarCommand = new DeleteAvatarCommand() { UserId = viewModel.Id };
                await Mediator.Send(deleteAvatarCommand);

                var updateAvatarCommand = new UpdateAvatarCommand() { UserId = viewModel.Id, Image = viewModel.ProfilePicture };
                await Mediator.Send(updateAvatarCommand);
            }

            await Mediator.Send(viewModel.ToUpdateUserCommand());

            return RedirectToAction("profile", new { id = viewModel.Id });
        }
    }
}
