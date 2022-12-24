
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PoPoy.Api.Data;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Paging;

namespace PoPoy.Api.Services.ReviewService
{
    public class ReviewService : IReviewService
    {
        private readonly DataContext context;

        public ReviewService(DataContext factory)
            => context = factory;

        public async ValueTask<Review> GetAsync(int id)
        {
            using (context)
            {
                return await context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async ValueTask<PagedList<Review>> FilterByProductIdAsync(int productId, ProductParameters productParameters)
        {
            using (context)
            {
                var list_review = await context.Reviews
                    .Where(x => x.ProductId == productId)
                    .OrderByDescending(x => x.CreateDate)
                    .Include(x=>x.User)
                    .ToListAsync();
                foreach (var item in list_review)
                {
                    item.User = new User()
                    {
                        FirstName = item.User.FirstName,
                        LastName = item.User.LastName,
                        AvatarPath = item.User.AvatarPath
                    };
                }
                return PagedList<Review>
                            .ToPagedList(list_review, productParameters.PageNumber, productParameters.PageSize);
            }
        }

        public async ValueTask<int> PostAsync(Review review, Guid userId)
        {
            using (context)
            {
                var now = DateTime.UtcNow;
                review.CreateDate = now;
                review.UpdateDate = now;
                review.UserId = userId;

                await context.Reviews.AddAsync(review);
                await context.SaveChangesAsync();
                await UpdateRatingAvergare(review);
                return review.Id;
            }
        }

        public async ValueTask<int> PutAsync(Review review, Guid userId)
        {
            using (context)
            {
                var dbReview = await context.Reviews.FirstOrDefaultAsync(x => x.Id == review.Id);
                if (dbReview is null)
                    return StatusCodes.Status404NotFound;

                if (dbReview.UserId != userId)
                    return StatusCodes.Status400BadRequest;

                dbReview.Rating = review.Rating;
                dbReview.Title = review.Title;
                dbReview.ReviewText = review.ReviewText;
                dbReview.UpdateDate = DateTime.UtcNow;
                await context.SaveChangesAsync();
                await UpdateRatingAvergare(review);
                return StatusCodes.Status204NoContent;
            }
        }

        public async ValueTask<int> DeleteAsync(int id, Guid userId)
        {
            using (context)
            {
                var dbReview = await context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
                if (dbReview is null)
                    return StatusCodes.Status404NotFound;

                if (dbReview.UserId != userId)
                    return StatusCodes.Status400BadRequest;

                context.Reviews.Remove(dbReview);
                await context.SaveChangesAsync();

                return StatusCodes.Status204NoContent;
            }
        }
        public async Task UpdateRatingAvergare(Review review)
        {
            try
            {
                using (context)
                {
                    var getAllReview = await context.Reviews
                    .Where(x => x.ProductId == review.ProductId)
                    .ToListAsync();
                    var product = await context.Products.FirstOrDefaultAsync(x => x.Id == review.ProductId);
                    if (product != null)
                    {
                        product.ReviewAverage = getAllReview.Count == 0 ? 0 : (decimal)getAllReview.Average(x => x.Rating);
                        await context.SaveChangesAsync();
                    }
                }
                
            }
            catch
            {

            }

        }
    }
}





