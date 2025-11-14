using FilminurkTARpe24_Markus.Models.UserComments;
using Filminurk.Data;
using Microsoft.AspNetCore.Mvc;
using Filminurk.Core.Dto;
using Filminurk.ApplicationServices.Services;
using Filminurk.Core.ServiceInterface;
using System.Reflection.Metadata.Ecma335;

namespace FilminurkTARpe24_Markus.Controllers
{
    public class UserCommentsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IUserCommentsServices _userCommentsServices;
        public UserCommentsController
            (
                FilminurkTARpe24Context context,
                IUserCommentsServices userCommentsServices
            )
        {
            _context = context;
            _userCommentsServices = userCommentsServices;
        }
        public IActionResult Index()
        {
            var result = _context.UserComments
                .Select(c => new UserCommentsIndexViewModel
                {
                    CommentID = c.CommentID,
                    CommentBody = c.CommentBody,
                    IsHarmful = (int)c.IsHarmful,
                    CommentCreatedAt = c.CommentCreatedAt,
                }
            );
            return View(result);
        }
        [HttpGet]
        public IActionResult NewComment()
        {
            //TODO: erista kas tegemist on admini, või tavakasutajaga
            UserCommentsCreateViewModel newcomment = new();
            return View(newcomment);
        }
        [HttpPost, ActionName("NewComment")]
        //meetodile ei tohi panna allowanonymous
        public async Task<IActionResult> NewCommentPost(UserCommentsCreateViewModel newcommentVM)
        {
            // check dto
            //newcommentVM.CommenterUserId = "00000000-0000-0000-000000000001";
            //TODO: newcommenti manuaalne seadmine, asenda pärast kasutaja id-ga
            Console.WriteLine(newcommentVM.CommenterUserId);
            if (ModelState.IsValid)
            {
                var dto = new UserCommentDTO() { };
                dto.CommentID = newcommentVM.CommentID;
                dto.CommentBody = newcommentVM.CommentBody;
                dto.CommenterUserID = newcommentVM.CommenterUserId;
                dto.CommentedScore = newcommentVM.CommentedScore;
                dto.CommentCreatedAt = newcommentVM.CommentCreatedAt;
                dto.CommentModifiedAt = newcommentVM.CommentModifiedAt;
                dto.CommentDeletedAt = newcommentVM.CommentDeletedAt;
                dto.IsHelpful = newcommentVM.IsHelpful;
                dto.IsHarmful = newcommentVM.IsHarmful;
            
                var result = await _userCommentsServices.NewComment(dto);
                if (result == null)
                {
                    return NotFound();
                }
                //TODO: erista ära kas tegu on admini või
                //kasutajaga, admin tagastub admin-comments-index,
                //kasutaja aga vastava filmi juurde
                return RedirectToAction(nameof(Index));
                //return RedirectToAction("Details", "Movies", id);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> DetailsAdmin(Guid id)
        {
            var requestedComment = await _userCommentsServices.DetailAsync(id);
            if (requestedComment == null)
            {
                return NotFound();
            }
            var commentVM = new UserCommentsIndexViewModel();

            commentVM.CommentID = requestedComment.CommentID;
            commentVM.CommentBody = requestedComment.CommentBody;
            commentVM.CommenterUserID = requestedComment.CommenterUserID;
            commentVM.CommentCreatedAt = requestedComment.CommentCreatedAt;
            commentVM.CommentModifiedAt = requestedComment.CommentModifiedAt;
            commentVM.CommentDeletedAt = requestedComment.CommentDeletedAt;
            commentVM.CommentedScore = requestedComment.CommentedScore;

            return View(commentVM);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var deleteEntry = await _userCommentsServices.DetailAsync(id);

            if (deleteEntry == null)
            {
                return NotFound();
            }

            var commentVM = new UserCommentsIndexViewModel();
            commentVM.CommentID = deleteEntry.CommentID;
            commentVM.CommentBody = deleteEntry.CommentBody;
            commentVM.CommenterUserID = deleteEntry.CommenterUserID;
            commentVM.CommentCreatedAt = deleteEntry.CommentCreatedAt;
            commentVM.CommentModifiedAt = deleteEntry.CommentModifiedAt;
            commentVM.CommentDeletedAt = deleteEntry.CommentDeletedAt;
            commentVM.CommentedScore = deleteEntry.CommentedScore;
            return View("DeleteAdmin",commentVM);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAdminPost(Guid id)
        {
            var deleteThisComment = await _userCommentsServices.Delete(id);
            if (deleteThisComment == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}
