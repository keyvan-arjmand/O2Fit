using System.Threading;
using System.Threading.Tasks;
using Blogging.Domain.Entities.Blogs;
using Blogging.Service.Dtos;
using Common;
using Common.Exceptions;
using Data.Contracts;
using MediatR;

namespace Blogging.Service.Blogs.V1.Commands.SoftDeleteBlog
{
    public class SoftDeleteBlogCommandHandler : IRequestHandler<SoftDeleteBlogCommand>, ITransientDependency
    {
        private readonly IRepository<Blog> _repository;

        public SoftDeleteBlogCommandHandler(IRepository<Blog> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(SoftDeleteBlogCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(cancellationToken, request.Id);
            if (result == null) throw new NotFoundException("blog not found");
            await _repository.DeleteAsync(result, cancellationToken);
            return Unit.Value;
        }
    }
}