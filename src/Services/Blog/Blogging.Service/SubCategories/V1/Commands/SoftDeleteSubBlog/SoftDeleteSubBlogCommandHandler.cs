using System.Threading;
using System.Threading.Tasks;
using Blogging.Domain.Entities.Blogs;
using Common;
using Common.Exceptions;
using Data.Contracts;
using MediatR;

namespace Blogging.Service.SubCategories.V1.Commands.SoftDeleteSubBlog
{
    public class SoftDeleteSubBlogCommandHandler : IRequestHandler<SoftDeleteSubBlogCommand>, ITransientDependency
    {
        private readonly IRepository<SubCategory> _repository;

        public SoftDeleteSubBlogCommandHandler(IRepository<SubCategory> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(SoftDeleteSubBlogCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(cancellationToken, request.Id);
            if (result == null) throw new NotFoundException("subCategory not found");
            await _repository.DeleteAsync(result, cancellationToken);
            return Unit.Value;
        }
    }
}