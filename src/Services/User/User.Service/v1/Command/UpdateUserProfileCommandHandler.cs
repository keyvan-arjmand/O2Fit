using Common;
using Common.Utilities;
using Data.Contracts;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using User.Domain.Enum;

namespace User.Service.v1.Command
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, UserProfile>, ITransientDependency
    {
        private readonly IRepository<UserProfile> _repository;
        private readonly IRepositoryRedis<UserProfile> _repositoryRedis;
        private readonly IWebHostEnvironment _environment;

        public UpdateUserProfileCommandHandler(IRepository<UserProfile> repository, IRepositoryRedis<UserProfile> repositoryRedis, IWebHostEnvironment environment)
        {
            _repository = repository;
            _repositoryRedis = repositoryRedis;
            _environment = environment;
        }
        public async Task<UserProfile> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            UserProfile _profile = await _repository.TableNoTracking.SingleOrDefaultAsync(a => a.UserId == request.UserId, cancellationToken);

            if (_profile != null)
            {

                if (request.FullName != null)
                {
                    _profile.FullName = request.FullName;
                }
                
                if (request.Image != null)
                {
                    var base64EncodedBytes = Convert.FromBase64String(request.Image);

                    string _Path = Path.Combine(_environment.WebRootPath, "UserProfile");

                    DirectoryInfo destination;

                    if (!Directory.Exists(_Path))
                    {
                        destination = Directory.CreateDirectory(_Path);
                    }
                    else
                    {
                        destination = new DirectoryInfo(_Path);
                    }

                    string _FileName = Guid.NewGuid() + ".jpg";

                    var _Address = Path.Combine(_Path, _FileName);

                    if (_profile.ImageUri!=null)
                    {
                        var _oldAdress = Path.Combine(_Path, _profile.ImageUri);
                        if (File.Exists(_Address))
                        {
                            File.Delete(_oldAdress);
                        }
                    }
                    File.WriteAllBytes(_Address, base64EncodedBytes);

                    _profile.ImageUri = _FileName;
                }
                if (request.FoodHabit>0)
                {
                        _profile.FoodHabit =(FoodHabit)request.FoodHabit;
                }
                if (request.DailyActivityRate>0)
                {
                        _profile.DailyActivityRate =(DailyActivityRate)request.DailyActivityRate;
                }
                if (request.Gender!=null)
                {
                    _profile.Gender = (Gender)request.Gender;
                }
                if (request.BirthDate!=null)
                {
                    _profile.BirthDate = request.BirthDate?? _profile.BirthDate;
                }
                if (request.HeightSize>0)
                {
                    _profile.HeightSize = request.HeightSize??_profile.HeightSize;
                }

                await _repository.UpdateAsync(_profile, cancellationToken);
                await _repositoryRedis.UpdateAsync($"UserProfile_{_profile.UserId}", _profile);
            }

            _repository.Detach(_profile);

            return _profile;

        }
    }
}
