using System.Threading.Tasks;
using Application.MediatR.Admin.User.Commands;
using Application.MediatR.Admin.User.Queries.GetUserById;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCMS.Areas.Admin.Features.Base;
using WebCMS.Areas.Admin.Features.Users.Profile;

namespace WebCMS.Areas.Admin.Features.Users
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class UsersController : AuthorizedController
    {
        [HttpGet]
        public async Task<IActionResult> List([FromQuery] GetAllUsersQuery query)
        {
            return View(await Mediator.Send(query));
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
