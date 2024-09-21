using System.Threading;
using System.Threading.Tasks;
using Blogging.Domain.Entities.Blogs;
using Common;
using Common.Exceptions;
using Data.Contracts;
using MediatR;

namespace Blogging.Service.Blogs.V1.Commands.UpdateBlog
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand>, ITransientDependency
    {
        private readonly IRepository<Blog> _repository;
        private readonly IRepository<SubCategory> _subRepository;

        public UpdateBlogCommandHandler(IRepository<Blog> repository, IRepository<SubCategory> subRepository)
        {
            _repository = repository;
            _subRepository = subRepository;
        }

        public async Task<Unit> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await _repository.GetByIdAsync(cancellationToken, request.Id);
            if (blog == null) throw new NotFoundException("blog not found");
            var subCategory = await _subRepository.GetByIdAsync(cancellationToken, request.SubCategoryId);
            if (subCategory == null) throw new NotFoundException("subCategory not found");
            blog.AltImage = request.AltImage;
            blog.ImageName = request.Image;
            blog.BannerName = request.Banner;
            blog.ThumbName = request.Thumb;
            blog.Status = request.Status;
            blog.AltBanner = request.AltBanner;
            blog.AltThumb = request.AltThumb;
            blog.SubCategoryId = subCategory.Id;
            blog.FirstPagePost = request.FirstPagePost;
            blog.KeyWords = request.KeyWords;
            await _repository.UpdateAsync(blog, cancellationToken);
            return Unit.Value;
        }
    }
}