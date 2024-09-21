using System.Threading;
using System.Threading.Tasks;
using Blogging.Domain.Entities.Blogs;
using Common;
using Common.Exceptions;
using Data.Contracts;
using MediatR;

namespace Blogging.Service.Blogs.V1.Commands.UpdateBlogStatus
{
    public class UpdateBlogStatusCommandHandler : IRequestHandler<UpdateBlogStatusCommand>, ITransientDependency
    {
        private readonly IRepository<Blog> _repository;

        public UpdateBlogStatusCommandHandler(IRepository<Blog> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateBlogStatusCommand request, CancellationToken cancellationToken)
        {
            var blog = await _repository.GetByIdAsync(cancellationToken, request.Id);
            if (blog == null) throw new NotFoundException("blog not found");
            blog.Status = request.Status;
            await _repository.UpdateAsync(blog, cancellationToken);
            return Unit.Value;
        }
    }
}