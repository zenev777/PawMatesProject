﻿using Microsoft.EntityFrameworkCore;
using PawMates.Core.Contracts.PostInterface;
using PawMates.Core.Models.EventViewModels;
using PawMates.Core.Models.PostViewModels;
using PawMates.Infrastructure.Data.Common;
using PawMates.Infrastructure.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.Core.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IRepository repository;

        public PostService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<bool> CreatePostAsync(PostFormViewModel model, string creatorId)
        {
            if (await repository.AlreadyExistAsync<Post>(p => p.Creator.UserName == model.Creator)) throw new ApplicationException("Event already exists");

            var entity = new Post()
            {
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                CreatorId = creatorId,
                Id = model.Id,
            };

            await repository.AddAsync(entity);
            await repository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<PostViewInfoModel>> GetAllPostsAsync()
        {
            return await repository
              .AllReadOnly<Post>()
              .Select(p => new PostViewInfoModel()
              {
                Id = p.Id,
                Creator = p.Creator.UserName,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
              })
              .ToListAsync();
        }
    }
}
