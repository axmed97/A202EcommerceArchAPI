using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.WishListDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class WishListManager : IWishlistService
    {
        private readonly IWishlistDAL _wishlistDAL;
        private readonly IMapper _mapper;

        public WishListManager(IWishlistDAL wishlistDAL, IMapper mapper)
        {
            _wishlistDAL = wishlistDAL;
            _mapper = mapper;
        }

        public IResult AddWishlist(int userId, AddWishlistDTO addWishlistDTO)
        {
            var map = _mapper.Map<WishList>(addWishlistDTO);
            map.CreatedDate = DateTime.Now;
            map.UserId = userId;
            _wishlistDAL.Add(map);
            return new SuccessResult();
        }
    }
}
