using PoPoy.Shared.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using PoPoy.Shared.Paging;

namespace PoPoy.Api.Services.ReviewService
{
    public interface IReviewService
    {
        ValueTask<Review> GetAsync(int id);
        ValueTask<PagedList<Review>> FilterByProductIdAsync(int productId, ProductParameters productParameters);
        ValueTask<int> PostAsync(Review review, Guid userId);
        ValueTask<int> PutAsync(Review review, Guid userId);
        ValueTask<int> DeleteAsync(int id, Guid userId);
    }
}
