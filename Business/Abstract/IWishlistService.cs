using Core.Utilities.Results.Abstract;
using Entities.DTOs.WishListDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IWishlistService
    {
        IResult AddWishlist(int userId, AddWishlistDTO addWishlistDTO);
    }
}
