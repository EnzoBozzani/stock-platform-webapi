using api.Dtos.Comment;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(Guid id);
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment?> UpdateAsync(Guid id, UpdateCommentDto commentDto);
        Task<Comment?> DeleteAsync(Guid id);
    }
}