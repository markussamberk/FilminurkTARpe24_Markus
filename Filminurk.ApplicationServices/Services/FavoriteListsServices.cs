using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Data;
using Microsoft.EntityFrameworkCore;

namespace Filminurk.ApplicationServices.Services
{
    public class FavoriteListsServices
    {
        private readonly FilminurkTARpe24Context _context;
        public FavoriteListsServices(FilminurkTARpe24Context context)
        {
            _context = context;
        }
        public async Task<FavoriteList> Update(FavoriteListDTO updatedList, string typeOfMethod)
        {
            
            FavoriteList updatedListInDB = new();

            updatedListInDB.FavoriteListID = updatedList.FavoriteListID;
            updatedListInDB.ListBelongsToUser = updatedList.ListBelongsToUser;
            updatedListInDB.IsMovieOrActor = updatedList.IsMovieOrActor;
            updatedListInDB.ListName = updatedList.ListName;
            updatedListInDB.ListDescription = updatedList.ListDescription;
            updatedListInDB.IsPrivate = updatedList.IsPrivate;
            updatedListInDB.ListOfMovies = updatedList.ListOfMovies;
            updatedListInDB.ListCreatedAt = updatedList.ListCreatedAt;
            updatedListInDB.ListDeletedAt = updatedList.ListDeletedAt;
            updatedListInDB.ListModifiedAt = updatedList.ListModifiedAt;
            if (typeOfMethod == "Delete")
            {
                _context.FavoriteLists.Attach(updatedListInDB);
                _context.Entry(updatedListInDB).Property(l => l.ListDeletedAt).IsModified = true;
                _context.Entry(updatedListInDB).Property(l => l.ListModifiedAt).IsModified = true;
            }
            else if (typeOfMethod == "Private")
            {
                _context.FavoriteLists.Attach(updatedListInDB);
                _context.Entry(updatedListInDB).Property(l => l.IsPrivate).IsModified = true;
            }
            _context.Entry(updatedListInDB).Property(l => l.ListModifiedAt).IsModified = true;
            await _context.SaveChangesAsync();
            return updatedListInDB;
            

        }
    }
}
