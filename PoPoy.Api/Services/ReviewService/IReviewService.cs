using PoPoy.Shared.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace PoPoy.Api.Services.ReviewService
{
    public interface IReviewService
    {
        ValueTask<Review> GetAsync(int id);
        ValueTask<List<Review>> FilterByProductIdAsync(int productId);
        ValueTask<int> PostAsync(Review review, Guid userId);
        ValueTask<int> PutAsync(Review review, Guid userId);
        ValueTask<int> DeleteAsync(int id, Guid userId);
    }
}
