using System.Threading;
using System.Threading.Tasks;
using Blogging.Domain.Entities.Blogs;
using Common;
using Data.Contracts;
using MediatR;

namespace Blogging.Service.BlogCategories.V1.Commands.InsertCategoryBlog
{
    public class InsertCategoryBlogCommandHandler : IRequestHandler<InsertCategoryBlogCommand>, ITransientDependency
    {
        private readonly IRepository<Category> _repository;

        public InsertCategoryBlogCommandHandler(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(InsertCategoryBlogCommand request, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(new Category
            {
                ImageName = request.ImageName,
                IsDelete = false,
                AltImage = request.AltImage,
                TitleId = request.TitleId,
                Type = request.Type
            }, cancellationToken);
            return Unit.Value;
        }
    }
}