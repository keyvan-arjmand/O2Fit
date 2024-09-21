using System.Threading;
using System.Threading.Tasks;
using Blogging.Domain.Entities.Blogs;
using Blogging.Service.Blogs.V1.Commands.SoftDeleteBlog;
using Blogging.Service.SubCategories.V1.Commands.SoftDeleteSubBlog;
using Common;
using Common.Exceptions;
using Data.Contracts;
using MediatR;

namespace Blogging.Service.BlogCategories.V1.Commands.SoftDeleteCategory
{
    public class SoftDeleteCategoryCommandHandler : IRequestHandler<SoftDeleteCategoryCommand>, ITransientDependency
    {
        private readonly IRepository<Category> _repository;

        public SoftDeleteCategoryCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(SoftDeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(cancellationToken, request.Id);
            if (result == null) throw new NotFoundException("Category not found");
            await _repository.DeleteAsync(result, cancellationToken);
            return Unit.Value;
        }
    }
}