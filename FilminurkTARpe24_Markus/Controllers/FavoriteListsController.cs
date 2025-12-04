using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Data;
using FilminurkTARpe24_Markus.Models.FavoriteLists;
using FilminurkTARpe24_Markus.Models.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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
                    ListDeletedAt = (DateTime)x.ListDeletedAt,
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
        [HttpGet]
        public async Task<IActionResult> UserDetails(Guid id, Guid thisuserid)
        {
            if (id == null || thisuserid == null)
            {
                return BadRequest();
                //TODO: return corresponding errorviews. id not found for list, and user login error for userid
            }
            var thisList = _context.FavoriteLists
                .Where(tl => tl.FavoriteListID == id && tl.ListBelongsToUser == thisuserid.ToString())
                .Select(
                stl => new FavoriteListUserDetailsViewModel
                {
                    FavoriteListID = stl.FavoriteListID,
                    ListBelongsToUser = stl.ListBelongsToUser,
                    IsMovieOrActor = stl.IsMovieOrActor,
                    ListName = stl.ListName,
                    ListDescription = stl.ListDescription,
                    IsPrivate = stl.IsPrivate,
                    ListOfMovies = stl.ListOfMovies,
                    IsReported = stl.IsReported,
                    /* Image = _context.FilesToDatabase
                     .Where(i => i.ListID == stl.FavoriteListID)
                     .Select(si => new FavoriteListIndexImageViewModel
                     {
                         ImageID = si.ImageID,
                         ListID = si.ListID,
                         ImageData = si.ImageData,
                         ImageTitle = si.ImageTitle,
                         Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(si.ImageData))
                     }).ToList().First()      */
                }).First();
            /*if (thisList == null)
            {
                return NotFound();
            }*/
            Console.BackgroundColor
            return View("UserTogglePrivacy", thisList);
        }
        [HttpPost]
        public async Task<IActionResult> UserTogglePrivacy(Guid id)
        {
            FavoriteList thisList = await _favoriteListsServices.DetailsAsync(id);

            FavoriteListDTO updatedList = new FavoriteListDTO();
            updatedList.FavoriteListID = thisList.FavoriteListID;
            updatedList.ListBelongsToUser = thisList.ListBelongsToUser;
            updatedList.ListName = thisList.ListName;
            updatedList.ListDescription = thisList.ListDescription;
            updatedList.IsPrivate = thisList.IsPrivate;
            updatedList.ListOfMovies = thisList.ListOfMovies;
            updatedList.IsReported = thisList.IsReported;
            updatedList.IsMovieOrActor = thisList.IsMovieOrActor;
            updatedList.ListCreatedAt = thisList.ListCreatedAt;
            updatedList.ListModifiedAt = DateTime.Now;
            updatedList.ListDeletedAt = thisList.ListDeletedAt;

            var result = await _favoriteListsServices.Update(updatedList, "Private");
            if (result == null)
            {
                return NotFound();
            }
            /*if (result.IsPrivate != !result.IsPrivate)
            {
                return BadRequest();
            }*/
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> UserDelete(Guid id)
        {
            var deletedList = await _favoriteListsServices.DetailsAsync(id);

            var dto = new FavoriteListDTO();
            dto.FavoriteListID = deletedList.FavoriteListID;
            dto.ListBelongsToUser = deletedList.ListBelongsToUser;
            dto.ListName = deletedList.ListName;
            dto.ListDescription = deletedList.ListDescription;
            dto.IsPrivate = deletedList.IsPrivate;
            dto.ListOfMovies = deletedList.ListOfMovies;
            dto.IsReported = deletedList.IsReported;
            dto.IsMovieOrActor = deletedList.IsMovieOrActor;
            dto.ListCreatedAt = deletedList.ListCreatedAt;
            dto.ListModifiedAt = DateTime.Now;
            dto.ListDeletedAt = DateTime.Now;
            ViewData["UpdateServiceType"] = "Delete";
            
            var result = await _favoriteListsServices.Update(dto);
            if (result == null) 
            {
                NotFound();
            }
            
            return RedirectToAction("Index");
        }
    }
}
