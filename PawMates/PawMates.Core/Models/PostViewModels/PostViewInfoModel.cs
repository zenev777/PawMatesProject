namespace PawMates.Core.Models.PostViewModels
{
    public class PostViewInfoModel
    {
        public int Id { get; set; }

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string Creator { get; set; } = string.Empty;

        public int Likes { get; set; }

        //public List<PostLikesViewModel> Likes { get; set; } = new List<PostLikesViewModel>();
    }
}
