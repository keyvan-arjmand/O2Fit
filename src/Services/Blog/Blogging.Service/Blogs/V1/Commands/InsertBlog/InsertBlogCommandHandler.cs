using System;
using System.Threading;
using System.Threading.Tasks;
using Blogging.Domain.Entities.Blogs;
using Common;
using Common.Exceptions;
using Data.Contracts;
using MediatR;

namespace Blogging.Service.Blogs.V1.Commands.InsertBlog
{
    public class InsertBlogCommandHandler : IRequestHandler<InsertBlogCommand>, ITransientDependency
    {
        private readonly IRepository<Blog> _repository;
        private readonly IRepository<SubCategory> _subRepository;

        public InsertBlogCommandHandler(IRepository<Blog> repository, IRepository<SubCategory> subRepository)
        {
            _repository = repository;
            _subRepository = subRepository;
        }

        public async Task<Unit> Handle(InsertBlogCommand request, CancellationToken cancellationToken)
        {
            var subCat = await _subRepository.GetByIdAsync(cancellationToken, request.SubCategoryId);
            if (subCat == null) throw new NotFoundException("SubCat Not Found");
            await _repository.AddAsync(new Blog
            {
                SubCategoryId = subCat.Id,
                IsDelete = false,
                Status = request.Status,
                Like = request.Like,
                View = 0,
                ImageName = request.Image,
                AltImage = request.AltImage,
                AltBanner = request.AltBanner,
                AltThumb = request.AltThumb,
                ShortDescriptionId = request.ShortDescriptionId,
                TitleId = request.TitleId,
                ThumbName = request.Thumb,
                DescriptionId = request.DescriptionId,
                BannerName = request.Banner,
                InsertDate = DateTime.Now,
                FirstPagePost = request.FirstPagePost,
                KeyWords = request.KeyWords
            }, cancellationToken);
            return Unit.Value;
        }
    }
}