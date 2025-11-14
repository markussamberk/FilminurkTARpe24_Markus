using Filminurk.Data;
using FilminurkTARpe24_Markus.Models.FavoriteLists;
using FilminurkTARpe24_Markus.Models.Movies;
using Microsoft.AspNetCore.Mvc;

namespace FilminurkTARpe24_Markus.Controllers
{
    public class FavoriteListsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        //favoritelistservice add later
        //fileservice add later
        public FavoriteListsController(FilminurkTARpe24Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var resultingLists = _context.FavoriteLists
                .OrderByDescending(y => y.ListCreatedAt)
                .Select(x => new FavoriteListsIndexViewModel
                {
                    FavoriteListID = x.FavoriteListID,
                    ListBelongsToUser = x.ListBelongsToUser,
                    IsMovieOrActor = x.IsMovieOrActor,
                    ListName = x.ListName,
                    ListDescription = x.ListDescription,
                    ListCreatedAt = x.ListCreatedAt,
                    Image = (List<FavoriteListIndexImageViewModel>)_context.FilesToDatabase
                    .Where(ml => ml.ListID == x.FavoriteListID)
                    .Select(li => new FavoriteListIndexImageViewModel
                    {
                        ListID = li.ListID,
                        ImageID = li.ImageID,
                        ImageData = li.ImageData,
                        ImageTitle = li.ImageTitle,
                        Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(li.ImageData)),
                    }) 

                });
            return View(resultingLists);
        }
    }
}
