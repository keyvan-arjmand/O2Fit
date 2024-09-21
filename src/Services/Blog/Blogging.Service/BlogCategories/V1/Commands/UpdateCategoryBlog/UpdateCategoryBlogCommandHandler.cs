using System.Threading;
using System.Threading.Tasks;
using Blogging.Domain.Entities.Blogs;
using Common;
using Common.Exceptions;
using Data.Contracts;
using MediatR;

namespace Blogging.Service.BlogCategories.V1.Commands.UpdateCategoryBlog
{
    public class UpdateCategoryBlogCommandHandler : IRequestHandler<UpdateCategoryBlogCommand>, ITransientDependency
    {
        private readonly IRepository<Category> _repository;

        public UpdateCategoryBlogCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateCategoryBlogCommand request, CancellationToken cancellationToken)
        {
            var cat = await _repository.GetByIdAsync(cancellationToken, request.Id);
            if (cat == null) throw new NotFoundException("subCategory not found");
            cat.AltImage = request.AltImage;
            cat.Type = request.Type;
            cat.ImageName = request.ImageName ?? cat.ImageName;
            await _repository.UpdateAsync(cat, cancellationToken);
            return Unit.Value;
        }
    }
}