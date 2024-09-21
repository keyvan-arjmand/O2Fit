using System.Threading;
using System.Threading.Tasks;
using Blogging.Domain.Entities.Blogs;
using Common;
using Common.Exceptions;
using Data.Contracts;
using MediatR;

namespace Blogging.Service.SubCategories.V1.Commands.InsertSubBlog
{
    public class InsertSubBlogCommandHandler : IRequestHandler<InsertSubBlogCommand>, ITransientDependency
    {
        private readonly IRepository<SubCategory> _repository;
        private readonly IRepository<Category> _catRepository;

        public InsertSubBlogCommandHandler(IRepository<SubCategory> repository, IRepository<Category> catRepository)
        {
            _repository = repository;
            _catRepository = catRepository;
        }

        public async Task<Unit> Handle(InsertSubBlogCommand request, CancellationToken cancellationToken)
        {
            var cat = await _catRepository.GetByIdAsync(cancellationToken, request.CategoryId);
            if (cat == null) throw new NotFoundException("category not found");
            await _repository.AddAsync(new SubCategory
            {
                ImageName = request.ImageName,
                CategoryId = cat.Id,
                Category = cat,
                TitleId = request.TitleId,
                AltImage = request.AltImage,
                IsDelete = false,
                
            }, cancellationToken);
            return Unit.Value;
        }
    }
}