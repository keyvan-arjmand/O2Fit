using System.Threading;
using System.Threading.Tasks;
using Blogging.Domain.Entities.Blogs;
using Common;
using Common.Exceptions;
using Data.Contracts;
using MediatR;

namespace Blogging.Service.SubCategories.V1.Commands.UpdateSubBlog
{
    public class UpdateSubBlogCommandHandler : IRequestHandler<UpdateSubBlogCommand>, ITransientDependency
    {
        private readonly IRepository<SubCategory> _repository;
        private readonly IRepository<Category> _catRepository;

        public UpdateSubBlogCommandHandler(IRepository<SubCategory> repository, IRepository<Category> catRepository)
        {
            _repository = repository;
            _catRepository = catRepository;
        }

        public async
            Task<Unit> Handle(UpdateSubBlogCommand request, CancellationToken cancellationToken)
        {
            var subCategory = await _repository.GetByIdAsync(cancellationToken, request.Id);
            if (subCategory == null) throw new NotFoundException("subCategory not found");
            var cat = await _catRepository.GetByIdAsync(cancellationToken, request.Id);
            if (subCategory == null) throw new NotFoundException("Category not found");
            subCategory.CategoryId = cat.Id;
            subCategory.ImageName = request.ImageName;
            subCategory.AltImage = request.AltImage;
            await _repository.UpdateAsync(subCategory, cancellationToken);
            return Unit.Value;
        }
    }
}