using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment
{
    public class UpdateCommentDto
    {
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters!")]
        [MaxLength(280, ErrorMessage = "Title can't be over 280 characters!")]
        public string? Title { get; set; }
        [MinLength(5, ErrorMessage = "Content must be at least 5 characters!")]
        [MaxLength(280, ErrorMessage = "Content can't be over 280 characters!")]
        public string? Content { get; set; }
    }
}