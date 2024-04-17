using PawMates.Infrastructure.Data.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.Core.Models.PostViewModels
{
    public class PostLikesViewModel
    {
        public int PostId { get; set; }

        public string UserId { get; set; } = string.Empty;

    }
}
