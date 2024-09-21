using Common;
using Data.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities.Package;
using Ordering.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Service.v1.Command
{
    public class UpdatePackageCommandHandler : IRequestHandler<UpdatePackageCommand>, IScopedDependency
    {
        private readonly IRepository<Package> _repository;
        private readonly IRepositoryRedis<List<Package>> _repositoryRedis;

        public UpdatePackageCommandHandler(IRepository<Package> repository, IRepositoryRedis<List<Package>> repositoryRedis)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
        }

        public async Task<Unit> Handle(UpdatePackageCommand request, CancellationToken cancellationToken)
        {
            List<Package> _pack = await _repository.Table
                                      .Include(a => a.TranslationName)
                                      .Include(a => a.TranslationDescription)
                                      .Where(a => a.IsActive == true)
                                      .OrderBy(a => a.Sort)
                                      .ToListAsync();
            await _repositoryRedis.UpdateAsync("PackageList", _pack);   
            
            _pack = _pack.Where(a =>a.PackageType == PackageType.CalorieCounting)
                                       .OrderBy(a => a.Sort)
                                       .ToList();

            await _repositoryRedis.UpdateAsync("Package", _pack);

            return Unit.Value;
        }
    }
}
