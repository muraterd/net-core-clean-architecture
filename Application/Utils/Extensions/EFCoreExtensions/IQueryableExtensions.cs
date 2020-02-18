using Data.Entities;
using System.Linq;

namespace Application.Utils.Extensions.EFCoreExtensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<UserEntity> IncludeProfilePhoto(this IQueryable<UserEntity> query)
        {
            return query.Select(w => new UserEntity()
            {
                Id = w.Id,
                CreatedAt = w.CreatedAt,
                Email = w.Email,
                FirstName = w.FirstName,
                IsActive = w.IsActive,
                IsDeleted = w.IsDeleted,
                LastName = w.LastName,
                Password = w.Password,
                Roles = w.Roles,
                Photos = w.Photos.Where(w => w.IsProfilePhoto).ToList()
            });
        }
    }
}
