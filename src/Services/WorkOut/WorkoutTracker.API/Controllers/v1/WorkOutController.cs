using AutoMapper;
using Common;
using Data.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.v1.Command;
using Service.v1.Query;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;
using WorkoutTracker.Common;
using WorkoutTracker.Common.Utilities;
using WorkoutTracker.Data.Contracts;
using WorkoutTracker.Data.Repositories;
using WorkoutTracker.Domain.Entities.Translation;
using WorkoutTracker.Domain.Entities.WorkOut;
using WorkoutTracker.Domain.Enum;

namespace WorkoutTracker.API.Controllers.v1
{
    [ApiVersion("1")]
    public class WorkOutController : BaseController
    {
        private readonly IWorkOutRepository _workOutRepository;
        private readonly IRepository<WorkoutBodyMuscles> _workoutBodyMusclesRepository;
        private readonly IUserFavoriteWorkOutRepository _userFavoriteWorkOutRepository;
        private readonly IRepository<BodyMuscle> _bodymuscleRepository;
        private readonly IWorkoutAttributeRepository _workoutAttributeRepository;
        private readonly IRepository<WorkOutAttributeValue> _workOutAttributeValueRepository;
        private readonly IUserTrackWorkOutRepository _userTrackWorkoutRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;


        public WorkOutController(IWorkOutRepository workOutRepository, IMediator mediator, IMapper mapper,
            IRepository<WorkoutBodyMuscles> workoutBodyMusclesRepository, IWorkoutAttributeRepository workoutAttributeRepository,
            IRepository<WorkOutAttributeValue> workOutAttributeValueRepository,
            IRepository<BodyMuscle> bodymuscleRepository, IWebHostEnvironment environment,
            IUserTrackWorkOutRepository userTrackWorkoutRepository,
            IUserFavoriteWorkOutRepository userFavoriteWorkOutRepository
            )
        {
            _mapper = mapper;
            _mediator = mediator;
            _workOutRepository = workOutRepository;
            _workoutAttributeRepository = workoutAttributeRepository;
            _workOutAttributeValueRepository = workOutAttributeValueRepository;
            _workoutBodyMusclesRepository = workoutBodyMusclesRepository;
            _userTrackWorkoutRepository = userTrackWorkoutRepository;
            _userFavoriteWorkOutRepository = userFavoriteWorkOutRepository;
            _bodymuscleRepository = bodymuscleRepository;
            _environment = environment;
        }


        [HttpPost, DisableRequestSizeLimit]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Post(Models.CreateModelDTO workOutDto, CancellationToken cancellationToken)
        {
            try
            {
                var _imageFile = workOutDto.FileUpload.workoutImg;
                var _iconFile = workOutDto.FileUpload.workoutIcon;
                var _generalVideoFile = workOutDto.FileUpload.GeneralV;
                var _maleVideoFile = workOutDto.FileUpload.MenV;
                var _femaleVideoFile = workOutDto.FileUpload.WomenV;

                WorkOutDTO workOutDTO = workOutDto.WorkOutDTO;

                var VideoFolderName = Path.Combine("wwwroot", "videos");
                var VideoPathToSave = Path.Combine(Directory.GetCurrentDirectory(), VideoFolderName);

                WorkOut workOut = workOutDTO.ToEntity(_mapper);
                if ((int)workOut.Gender > 1 || workOut.Gender == null)
                {
                    workOut.Gender = null;
                }
                else
                {
                    workOut.Gender = (Gender)workOut.Gender;
                }
                var imageName = "";
                if (_imageFile != null)
                {
                    var ImageFolderName = Path.Combine("wwwroot", "images");
                    var ImagePathToSave = Path.Combine(Directory.GetCurrentDirectory(), ImageFolderName);
                    imageName = ContentDispositionHeaderValue.Parse(_imageFile.ContentDisposition).FileName.Trim('"').ToLower();
                    var fullPath = Path.Combine(ImagePathToSave, imageName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        _imageFile.CopyTo(stream);
                    }
                    workOut.ImageUri = imageName;
                }

                var iconName = "";
                if (_iconFile != null)
                {
                    var IconFolderName = Path.Combine("wwwroot", "icons");
                    var IconPathToSave = Path.Combine(Directory.GetCurrentDirectory(), IconFolderName);
                    iconName = ContentDispositionHeaderValue.Parse(_iconFile.ContentDisposition).FileName.Trim('"').ToLower();
                    var fullPath = Path.Combine(IconPathToSave, iconName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        _iconFile.CopyTo(stream);
                    }
                    workOut.IconUri = iconName.ToLower();
                }

                var generalVideoName = "";
                if (_generalVideoFile != null)
                {
                    generalVideoName = ContentDispositionHeaderValue.Parse(_generalVideoFile.ContentDisposition).FileName.Trim('"').ToLower();
                    var fullPath = Path.Combine(VideoPathToSave, generalVideoName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        _generalVideoFile.CopyTo(stream);
                    }
                    workOut.VideoUrl = generalVideoName.ToLower();
                }
                var maleVideoName = "";
                if (_maleVideoFile != null)
                {
                    maleVideoName = ContentDispositionHeaderValue.Parse(_maleVideoFile.ContentDisposition).FileName.Trim('"').ToLower();
                    maleVideoName = maleVideoName.Split('.')[0].ToLower() + "_male" + Path.GetExtension(maleVideoName);
                    var fullPath = Path.Combine(VideoPathToSave, maleVideoName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        _maleVideoFile.CopyTo(stream);
                    }
                    workOut.VideoUrl = maleVideoName.ToLower();
                }

                var femaleVideoName = "";
                if (_femaleVideoFile != null)
                {
                    femaleVideoName = ContentDispositionHeaderValue.Parse(_femaleVideoFile.ContentDisposition).FileName.Trim('"').ToLower();
                    femaleVideoName = femaleVideoName.Split('.')[0].ToLower() + "_female" + Path.GetExtension(femaleVideoName);
                    var fullPath = Path.Combine(VideoPathToSave, femaleVideoName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        _femaleVideoFile.CopyTo(stream);
                    }
                    workOut.VideoUrl = femaleVideoName.ToLower();
                }


                if (workOutDTO.CardioCategory > 0)
                {
                    workOut.CardioCategory = (CardioCategory)workOutDTO.CardioCategory;
                }
                var workoutName = await _mediator.Send(new CraeteTranslationCommand
                {
                    Translation = workOutDTO.Name.ToEntity(_mapper)
                });
                workOut.NameId = workoutName.Id;

                if (workOutDTO.Recommandation.Persian != null)
                {
                    var recommandation = await _mediator.Send(new CraeteTranslationCommand
                    {
                        Translation = workOutDTO.Recommandation.ToEntity(_mapper)
                    });
                    workOut.RecommandationId = recommandation.Id;
                }

                if (workOutDTO.HowToDo.Persian != null)
                {
                    var howToDo = await _mediator.Send(new CraeteTranslationCommand
                    {
                        Translation = workOutDTO.HowToDo.ToEntity(_mapper)
                    });
                    workOut.HowToDoId = howToDo.Id;
                }

                try
                {

                    workOut.Id = await _workOutRepository.AddAsync(workOut, cancellationToken);

                }
                catch (Exception ex)
                {

                    throw;
                }
                //---------------------Add BodyMuscle------------------------
                List<WorkoutBodyMuscles> bodyMuscleList = new List<WorkoutBodyMuscles>();
                if (workOutDTO.BodyMuscles != null)
                {
                    foreach (var item in workOutDTO.BodyMuscles)
                    {
                        var bodyMuscle = new WorkoutBodyMuscles()
                        {
                            BodyMuscleId = item,
                            WorkoutId = workOut.Id
                        };
                        bodyMuscleList.Add(bodyMuscle);
                    }
                    try
                    {
                        await _workoutBodyMusclesRepository.AddRangeAsync(bodyMuscleList, cancellationToken);
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }

                //-----------------------------------Attributes------------------
                if (workOutDTO.Attributes != null)
                {
                    foreach (var item in workOutDTO.Attributes)
                    {
                        var attribute = new WorkOutAttribute()
                        {
                            WorkOutId = workOut.Id,
                        };
                        var attrName = await _mediator.Send(new CraeteTranslationCommand
                        {
                            Translation = item.Name.ToEntity(_mapper)
                        });
                        attribute.NameId = attrName.Id;
                        attribute.Id = await _workoutAttributeRepository.AddAsync(attribute, cancellationToken);

                        var _attributeValues = new List<WorkOutAttributeValue>();
                        foreach (var attrVal in item.AttributeValues)
                        {
                            var _attrVal = new WorkOutAttributeValue()
                            {
                                BurnedCalories = attrVal.BurnedCalories
                            };
                            var attrValName = await _mediator.Send(new CraeteTranslationCommand
                            {
                                Translation = attrVal.Name.ToEntity(_mapper)
                            });
                            _attrVal.NameId = attrValName.Id;
                            _attrVal.WorkOutAttributeId = attribute.Id;
                            _attributeValues.Add(_attrVal);
                        };
                        await _workOutAttributeValueRepository.AddRangeAsync(_attributeValues, cancellationToken);
                    }
                }
                return new ApiResult(true, ApiResultStatusCode.Success);
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ApiResultStatusCode.BadRequest, ex.Message);
            }
        }



        [HttpPut, DisableRequestSizeLimit]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Put(Models.CreateModelDTO workOutDto, CancellationToken cancellationToken)
        {
            try
            {
                var _imageFile = workOutDto.FileUpload.workoutImg;
                var _iconFile = workOutDto.FileUpload.workoutIcon;
                var _generalVideoFile = workOutDto.FileUpload.GeneralV;
                var _maleVideoFile = workOutDto.FileUpload.MenV;
                var _femaleVideoFile = workOutDto.FileUpload.WomenV;

                WorkOutDTO workOutDTO = workOutDto.WorkOutDTO;
                var oldWorkout = await _workOutRepository.Table.Include(b => b.WorkoutBodyMuscles).Where(w => w.Id == workOutDTO.Id).FirstOrDefaultAsync(cancellationToken);
                var VideoFolderName = Path.Combine("wwwroot", "videos");
                var VideoPathToSave = Path.Combine(Directory.GetCurrentDirectory(), VideoFolderName);

                WorkOut workOut = workOutDTO.ToEntity(_mapper);
                workOut.IconUri = oldWorkout.IconUri;
                workOut.ImageUri = oldWorkout.ImageUri;
                workOut.VideoUrl = oldWorkout.VideoUrl;
                if ((int)workOut.Gender > 1 || workOut.Gender == null)
                {
                    workOut.Gender = null;
                }
                else
                {
                    workOut.Gender = (Gender)workOut.Gender;
                }
                DeleteFile deleteFile = new DeleteFile(_environment);
                var imageName = "";
                if (_imageFile != null)
                {
                    var ImageFolderName = Path.Combine("wwwroot", "images");
                    var ImagePathToSave = Path.Combine(Directory.GetCurrentDirectory(), ImageFolderName);
                    imageName = ContentDispositionHeaderValue.Parse(_imageFile.ContentDisposition).FileName.Trim('"').ToLower();
                    var fullPath = Path.Combine(ImagePathToSave, imageName);
                    if (oldWorkout.ImageUri != null)
                    {
                        deleteFile.DeleteFiles(oldWorkout.ImageUri, ImagePathToSave);
                    }
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        _imageFile.CopyTo(stream);
                    }
                    workOut.ImageUri = imageName;
                }

                var iconName = "";
                if (_iconFile != null)
                {
                    var IconFolderName = Path.Combine("wwwroot", "icons");
                    var IconPathToSave = Path.Combine(Directory.GetCurrentDirectory(), IconFolderName);
                    iconName = ContentDispositionHeaderValue.Parse(_iconFile.ContentDisposition).FileName.Trim('"').ToLower();
                    var fullPath = Path.Combine(IconPathToSave, iconName);
                    if (oldWorkout.IconUri != null)
                    {
                        deleteFile.DeleteFiles(oldWorkout.IconUri, IconPathToSave);
                    }
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        _iconFile.CopyTo(stream);
                    }
                    workOut.IconUri = iconName.ToLower();
                }

                var generalVideoName = "";
                if (_generalVideoFile != null)
                {
                    generalVideoName = ContentDispositionHeaderValue.Parse(_generalVideoFile.ContentDisposition).FileName.Trim('"').ToLower();
                    var fullPath = Path.Combine(VideoPathToSave, generalVideoName);
                    if (oldWorkout.VideoUrl != null)
                    {
                        deleteFile.DeleteFiles(oldWorkout.VideoUrl, VideoPathToSave);
                    }
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        _generalVideoFile.CopyTo(stream);
                    }
                    workOut.VideoUrl = generalVideoName.ToLower();
                }
                var maleVideoName = "";
                if (_maleVideoFile != null)
                {
                    if (oldWorkout.VideoUrl != null)
                    {
                        deleteFile.DeleteFiles(oldWorkout.VideoUrl, VideoPathToSave);
                    }
                    maleVideoName = ContentDispositionHeaderValue.Parse(_maleVideoFile.ContentDisposition).FileName.Trim('"').ToLower();
                    maleVideoName = maleVideoName.Split('.')[0].ToLower() + "_male" + Path.GetExtension(maleVideoName);
                    var fullPath = Path.Combine(VideoPathToSave, maleVideoName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        _maleVideoFile.CopyTo(stream);
                    }
                    workOut.VideoUrl = maleVideoName.ToLower();
                }

                var femaleVideoName = "";
                if (_femaleVideoFile != null)
                {
                    if (oldWorkout.VideoUrl != null)
                    {
                        deleteFile.DeleteFiles(oldWorkout.VideoUrl, VideoPathToSave);
                    }
                    femaleVideoName = ContentDispositionHeaderValue.Parse(_femaleVideoFile.ContentDisposition).FileName.Trim('"').ToLower();
                    femaleVideoName = femaleVideoName.Split('.')[0].ToLower() + "_female" + Path.GetExtension(femaleVideoName);
                    var fullPath = Path.Combine(VideoPathToSave, femaleVideoName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        _femaleVideoFile.CopyTo(stream);
                    }
                    workOut.VideoUrl = femaleVideoName.ToLower();
                }

                if (workOutDTO.CardioCategory > 0)
                {
                    workOut.CardioCategory = (CardioCategory)workOutDTO.CardioCategory;
                }
                workOutDTO.Name.Id = oldWorkout.NameId;
                var workoutName = await _mediator.Send(new UpdateTranslationCommand
                {
                    Translation = workOutDTO.Name.ToEntity(_mapper)
                });
                workOut.NameId = workoutName.Id;

                if (workOutDTO.Recommandation.Persian != null || workOutDTO.Recommandation.English != null || workOutDTO.Recommandation.Arabic != null)
                {
                    if (oldWorkout.RecommandationId == null)
                    {
                        var recommandation = await _mediator.Send(new CraeteTranslationCommand
                        {
                            Translation = workOutDTO.Recommandation.ToEntity(_mapper)
                        });
                        workOut.RecommandationId = recommandation.Id;
                    }
                    else if (oldWorkout.RecommandationId > 0)
                    {
                        workOutDTO.Recommandation.Id = oldWorkout.RecommandationId ?? 0;
                        var recommandation = await _mediator.Send(new UpdateTranslationCommand
                        {
                            Translation = workOutDTO.Recommandation.ToEntity(_mapper)
                        });
                        workOut.RecommandationId = recommandation.Id;
                    }

                }

                if (workOutDTO.HowToDo.Persian != null || workOutDTO.HowToDo.English != null || workOutDTO.HowToDo.Arabic != null)
                {
                    if (oldWorkout.HowToDoId == null)
                    {
                        var howToDo = await _mediator.Send(new CraeteTranslationCommand
                        {
                            Translation = workOutDTO.HowToDo.ToEntity(_mapper)
                        });
                        workOut.HowToDoId = howToDo.Id;
                    }
                    else if (oldWorkout.HowToDoId > 0)
                    {
                        workOutDTO.HowToDo.Id = oldWorkout.HowToDoId ?? 0;
                        var howToDo = await _mediator.Send(new UpdateTranslationCommand
                        {
                            Translation = workOutDTO.HowToDo.ToEntity(_mapper)
                        });
                        workOut.HowToDoId = howToDo.Id;
                    }
                }

                try
                {
                    _workOutRepository.Detach(oldWorkout);
                    await _workOutRepository.UpdateAsync(workOut, cancellationToken);

                }
                catch (Exception ex)
                {

                    throw;
                }
                //---------------------Add BodyMuscle------------------------
                if (oldWorkout.WorkoutBodyMuscles != null)
                {
                    await _workoutBodyMusclesRepository.DeleteRangeAsync(oldWorkout.WorkoutBodyMuscles, cancellationToken);
                }
                List<WorkoutBodyMuscles> bodyMuscleList = new List<WorkoutBodyMuscles>();
                if (workOutDTO.BodyMuscles != null)
                {
                    foreach (var item in workOutDTO.BodyMuscles)
                    {
                        var bodyMuscle = new WorkoutBodyMuscles()
                        {
                            BodyMuscleId = item,
                            WorkoutId = workOut.Id
                        };
                        bodyMuscleList.Add(bodyMuscle);
                    }
                    try
                    {
                        await _workoutBodyMusclesRepository.AddRangeAsync(bodyMuscleList, cancellationToken);
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }

                //-----------------------------------Attributes------------------
                if (workOutDTO.Attributes != null)
                {
                    foreach (var item in workOutDTO.Attributes)
                    {
                        var attribute = new WorkOutAttribute()
                        {
                            WorkOutId = workOut.Id,
                        };
                        if (item.Id == 0)
                        {
                            var attrName = await _mediator.Send(new CraeteTranslationCommand
                            {
                                Translation = item.Name.ToEntity(_mapper)
                            });
                            attribute.NameId = attrName.Id;
                            attribute.Id = await _workoutAttributeRepository.AddAsync(attribute, cancellationToken);

                        }
                        else if (item.Id > 0)
                        {
                            var attrName = await _mediator.Send(new UpdateTranslationCommand
                            {
                                Translation = item.Name.ToEntity(_mapper)
                            });
                            attribute.Id = item.Id;
                        }
                        var _attributeValues = new List<WorkOutAttributeValue>();
                        foreach (var attrVal in item.AttributeValues)
                        {
                            var _attrVal = new WorkOutAttributeValue()
                            {
                                BurnedCalories = attrVal.BurnedCalories
                            };
                            var attrValName = await _mediator.Send(new CraeteTranslationCommand
                            {
                                Translation = attrVal.Name.ToEntity(_mapper)
                            });
                            _attrVal.NameId = attrValName.Id;
                            _attrVal.WorkOutAttributeId = attribute.Id;
                            _attributeValues.Add(_attrVal);
                        };
                        await _workOutAttributeValueRepository.AddRangeAsync(_attributeValues, cancellationToken);
                    }
                }
                return new ApiResult(true, ApiResultStatusCode.Success);
            }
            catch (Exception ex)
            {
                return new ApiResult(false, ApiResultStatusCode.BadRequest, ex.Message);
            }
        }



        [HttpGet]
        public async Task<ApiResult<WorkOutSelectDTO>> Get(int Id, CancellationToken cancellationToken)
        {
            var _workout = await _workOutRepository.Table.Include(a => a.WorkOutAttribute).ThenInclude(a => a.WorkOutAttributeValue)
                .Include(a => a.WorkoutBodyMuscles).ThenInclude(a => a.BodyMuscle).FirstAsync(a => a.Id == Id);
            //------------------------------------Translation --------------
            var translationIds = new List<int>();
            translationIds.Add(_workout.NameId);
            if (_workout.HowToDoId > 0) { translationIds.Add(_workout.HowToDoId ?? 0); }
            if (_workout.RecommandationId > 0) { translationIds.Add(_workout.RecommandationId ?? 0); }

            foreach (var item in _workout.WorkOutAttribute)
            {
                translationIds.Add(item.NameId);
                foreach (var attrValue in item.WorkOutAttributeValue)
                {
                    translationIds.Add(attrValue.NameId);
                }
            }
            foreach (var item in _workout.WorkoutBodyMuscles)
            {
                translationIds.Add(item.BodyMuscle.NameId);
            }
            if (_workout.TargetMuscle > 0)
            {
                translationIds.Add(_workout.TargetMuscle ?? 0);
            }
            var names = await _mediator.Send(new GetTranslationQuery() { Ids = translationIds, Language = LanguageName });
            //--------------------------------WorkoutAttributes------------------------------

            var Attributes = new List<AttributeModel>();
            foreach (var item in _workout.WorkOutAttribute)
            {
                var WoAtVal = new List<AttributeSelectModel>();
                foreach (var attributeValue in item.WorkOutAttributeValue)
                {
                    var attrVal = new AttributeSelectModel();
                    attrVal.Attribute = new SelectOption<int> { Text = names.Find(n => n.Value == attributeValue.NameId).Text, Value = attributeValue.Id };
                    attrVal.BurnedCalori = attributeValue.BurnedCalories;
                    WoAtVal.Add(attrVal);
                };
                var _AttributModel = new AttributeModel()
                {
                    WorkOutAttribute = new SelectOption<int> { Text = names.Find(n => n.Value == item.NameId).Text, Value = item.Id },
                    WorkOutAttributeValue = WoAtVal,
                };
                Attributes.Add(_AttributModel);
            }

            //---------------------------------WorkoutBodyMuscles-----------------------------
            var bodyMuscles = new List<string>();
            foreach (var item in _workout.WorkoutBodyMuscles)
            {
                bodyMuscles.Add(names.Find(n => n.Value == item.BodyMuscle.NameId).Text);
            }
            var body = await _bodymuscleRepository.GetByIdAsync(cancellationToken, _workout.TargetMuscle);
            //------------------------------------------------------------------------------
            var workoutSelect = new WorkOutSelectDTO()
            {
                Id = _workout.Id,
                Name = names.Find(n => n.Value == _workout.NameId).Text,
                HowToDo = (_workout.HowToDoId > 0) ? names.Find(n => n.Value == _workout.HowToDoId).Text : null,
                Recommandation = (_workout.RecommandationId > 0) ? names.Find(n => n.Value == _workout.RecommandationId).Text : null,
                BurnedCalories = _workout.BurnedCalories,
                IconUri = (_workout.IconUri != null) ? CommonStrings.CommonUrl + "icons/" + _workout.IconUri : null,
                ImageUri = (_workout.ImageUri != null) ? CommonStrings.CommonUrl + "images/" + _workout.ImageUri : null,
                VideoUrl = (_workout.VideoUrl != null) ? CommonStrings.CommonUrl + "videos/" + _workout.VideoUrl : null,
                Attributes = Attributes,
                BodyMuscles = bodyMuscles,
                Classification = (int)_workout.Classification,
                Level = (int)_workout.Level,

            };
            if (_workout.Gender != null)
            {
                workoutSelect.Gender = ((int)_workout.Gender < 2) ? (int?)_workout.Gender : null;
            }
            if (_workout.Level > 0)
            {

                workoutSelect.Level = (int)_workout.Level;
            }
            else { workoutSelect.Level = 0; }
            if (_workout.TargetMuscle > 0)
            {
                workoutSelect.TargetMuscle = (body != null) ? names.Find(n => n.Value == body.NameId).Text : "";
            }
            else { workoutSelect.TargetMuscle = null; }
            return workoutSelect;
        }



        [HttpGet("GetFullWorkout")]
        public async Task<WorkOutDTO> GetFullWorkoutAsync(int Id, CancellationToken cancellationToken)
        {
            var _workout = await _workOutRepository.Table
                .Include(n => n.Translation)
                .Include(r => r.TranslationRecommandation)
                .Include(h => h.TranslationHowToDo)
                .Include(a => a.WorkOutAttribute).ThenInclude(v => v.Translation)
                .Include(a => a.WorkOutAttribute).ThenInclude(av => av.WorkOutAttributeValue).ThenInclude(tv => tv.Translation)
                .Include(b => b.WorkoutBodyMuscles)
                .Where(w => w.Id == Id)
                .SingleAsync(cancellationToken);

            var Attributes = new List<AttributeDtoModel>();
            if (_workout.WorkOutAttribute.Count() > 0)
            {
                foreach (var item in _workout.WorkOutAttribute)
                {
                    //--------------------------------WorkoutAttributes------------------------------             
                    var WoAtVal = new List<AttributeValueModel>();
                    foreach (var attributeValue in item.WorkOutAttributeValue)
                    {

                        var attrVal = new AttributeValueModel();
                        attrVal.Name = new Models.TranslationDto
                        {
                            Id = attributeValue.Translation.Id,
                            Persian = attributeValue.Translation.Persian,
                            English = attributeValue.Translation.English,
                            Arabic = attributeValue.Translation.Arabic
                        };
                        attrVal.BurnedCalories = attributeValue.BurnedCalories;
                        attrVal.Id = attributeValue.Id;
                        WoAtVal.Add(attrVal);
                    }
                    var Attribut = new AttributeDtoModel()
                    {
                        Id = item.Id,
                        Name = new Models.TranslationDto
                        {
                            Id = item.Translation.Id,
                            Persian = item.Translation.Persian,
                            English = item.Translation.English,
                            Arabic = item.Translation.Arabic
                        },
                        AttributeValues = WoAtVal,
                    };
                    Attributes.Add(Attribut);
                }
            }
            //------------------------------------------------------------------------------
            var workoutSelect = new WorkOutDTO();

            workoutSelect.Id = _workout.Id;
            workoutSelect.Name = new Models.TranslationDto
            {
                Id = _workout.Translation.Id,
                Persian = _workout.Translation.Persian,
                English = _workout.Translation.English,
                Arabic = _workout.Translation.Arabic
            };
            workoutSelect.HowToDo = (_workout.HowToDoId > 0) ? new Models.TranslationDto
            {
                Id = _workout.TranslationHowToDo.Id,
                Persian = _workout.TranslationHowToDo.Persian,
                English = _workout.TranslationHowToDo.English,
                Arabic = _workout.TranslationHowToDo.Arabic
            } : null;
            workoutSelect.Recommandation = (_workout.RecommandationId > 0) ? new Models.TranslationDto
            {
                Id = _workout.TranslationRecommandation.Id,
                Persian = _workout.TranslationRecommandation.Persian,
                English = _workout.TranslationRecommandation.English,
                Arabic = _workout.TranslationRecommandation.Arabic
            } : null;
            workoutSelect.BurnedCalories = _workout.BurnedCalories;
            workoutSelect.IconUri = _workout.IconUri == null ? null : CommonStrings.CommonUrl + "icons/" + _workout.IconUri;
            workoutSelect.ImageUri = (_workout.ImageUri == null) ? null : CommonStrings.CommonUrl + "images/" + _workout.ImageUri;
            workoutSelect.VideoUrl = (_workout.VideoUrl == null) ? null : CommonStrings.CommonUrl + "videos/" + _workout.VideoUrl;
            workoutSelect.Attributes = Attributes;
            workoutSelect.BodyMuscles = _workout.WorkoutBodyMuscles.Select(i => i.BodyMuscleId).ToList();
            workoutSelect.Tag = _workout.Tag;
            workoutSelect.CardioCategory = _workout.CardioCategory;
            workoutSelect.Gender = _workout.Gender;
            workoutSelect.Classification = _workout.Classification;
            workoutSelect.Level = _workout.Level;

            if (_workout.TargetMuscle > 0)
            {
                workoutSelect.TargetMuscle = (int)_workout.TargetMuscle;
            }
            else
            {
                workoutSelect.TargetMuscle = null;
            }

            return workoutSelect;
        }



        /// <summary>
        /// Get All Workout of a Class with Translation
        /// </summary>
        /// <param name="classificationId"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetByClassification")]
        public async Task<PageResult<WorkOutSelectDTO>> GetByClassificationId(int classificationId, int? Page, int PageSize,
            CancellationToken cancellationToken)
        {
            var workouts = await _workOutRepository.GetByClassificationIdAsync(classificationId, cancellationToken);

            List<WorkOut> _listW = await _workOutRepository.Table
                                          .Skip((Page - 1 ?? 0) * PageSize)
                                          .Take(PageSize)
                                          .ToListAsync(cancellationToken);

            var countDetails = _listW.Count();

            //------------------------------------Translation --------------
            var translationIds = new List<int>();
            foreach (var item in workouts)
            {
                translationIds.Add(item.NameId);
            }
            var names = await _mediator.Send(new GetTranslationQuery() { Ids = translationIds, Language = LanguageName });
            //------------------------------------------------------------------------------
            List<WorkOutSelectDTO> _paging = new List<WorkOutSelectDTO>();

            var workoutList = new List<WorkOutSelectDTO>();
            if (_listW.Count > 0)
            {
                foreach (var workout in workouts)
                {
                    WorkOutSelectDTO invoicePaging = new WorkOutSelectDTO()
                    {
                        Id = workout.Id,
                        Name = names.Find(n => n.Value == workout.NameId).Text,
                        IconUri = workout.IconUri,
                        ImageUri = workout.ImageUri,
                        Gender = (workout.Gender == Gender.Male) ? 1 : 0,
                        Classification = (int)workout.Classification,
                        Level = (int)workout.Level,
                    };
                    _paging.Add(invoicePaging);
                }
            }


            var result = new PageResult<WorkOutSelectDTO>
            {
                Count = countDetails,
                PageIndex = Page ?? 1,
                PageSize = PageSize,
                Items = _paging
            };

            return result;
        }

        [HttpGet("GetAll")]
        public async Task<ApiResult<List<WorkOutSelectFullDTO>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var Workouts = new List<WorkOutSelectFullDTO>();
            List<WorkOut> WorkoutList = await _workOutRepository.Table
                .Include(n => n.Translation)
                .Include(r => r.TranslationRecommandation)
                .Include(h => h.TranslationHowToDo)
                .Include(a => a.WorkOutAttribute).ThenInclude(v => v.Translation)
                .Include(a => a.WorkOutAttribute).ThenInclude(av => av.WorkOutAttributeValue).ThenInclude(tv => tv.Translation)
                .Include(b => b.WorkoutBodyMuscles)
                .ToListAsync(cancellationToken);
            var countDetails = WorkoutList.Count();
            foreach (var _workout in WorkoutList)
            {
                var Attributes = new List<AttributeFullModel>();
                if (_workout.WorkOutAttribute != null)
                {
                    foreach (var item in _workout.WorkOutAttribute)
                    {
                        //--------------------------------WorkoutAttributes------------------------------             
                        var WoAtVal = new List<AttributeSelectFullModel>();
                        foreach (var attributeValue in item.WorkOutAttributeValue)
                        {

                            var attrVal = new AttributeSelectFullModel();
                            attrVal.Name = new Translation()
                            {
                                Id = attributeValue.Translation.Id,
                                Persian = attributeValue.Translation.Persian,
                                English = attributeValue.Translation.English,
                                Arabic = attributeValue.Translation.Arabic
                            };
                            attrVal.BurnedCalori = attributeValue.BurnedCalories;
                            attrVal.Id = attributeValue.Id;
                            WoAtVal.Add(attrVal);
                        }
                        var Attribut = new AttributeFullModel()
                        {
                            Id = item.Id,
                            Name = new Translation()
                            {
                                Id = item.Translation.Id,
                                Persian = item.Translation.Persian,
                                English = item.Translation.English,
                                Arabic = item.Translation.Arabic
                            },
                            AttributeValue = WoAtVal,
                        };
                        Attributes.Add(Attribut);
                    }
                }
                //------------------------------------------------------------------------------
                var workoutSelect = new WorkOutSelectFullDTO();

                workoutSelect.Id = _workout.Id;
                workoutSelect.Name = new Translation()
                {
                    Id = _workout.Translation.Id,
                    Persian = _workout.Translation.Persian,
                    English = _workout.Translation.English,
                    Arabic = _workout.Translation.Arabic
                };
                workoutSelect.HowToDo = (_workout.HowToDoId > 0) ? new Translation()
                {
                    Id = _workout.TranslationHowToDo.Id,
                    Persian = _workout.TranslationHowToDo.Persian,
                    English = _workout.TranslationHowToDo.English,
                    Arabic = _workout.TranslationHowToDo.Arabic
                } : null;
                workoutSelect.Recommandation = (_workout.RecommandationId > 0) ? new Translation()
                {
                    Id = _workout.TranslationRecommandation.Id,
                    Persian = _workout.TranslationRecommandation.Persian,
                    English = _workout.TranslationRecommandation.English,
                    Arabic = _workout.TranslationRecommandation.Arabic
                } : null;
                workoutSelect.BurnedCalories = _workout.BurnedCalories;
                workoutSelect.IconUri = ((int)_workout.Classification == 1) ? "#.. / .. / res/img/sports/" + _workout.IconUri + "#" : CommonStrings.CommonUrl + "icons/" + _workout.IconUri;
                workoutSelect.ImageUri = (_workout.ImageUri != null) ? CommonStrings.CommonUrl + "images/" + _workout.ImageUri : null;
                workoutSelect.Video = ((int)_workout.Classification != 1) ? CommonStrings.CommonUrl + "videos/" + _workout.VideoUrl : null;
                //workoutSelect.MaleVideo = ((int)_workout.Classification != 1) ? CommonStrings.CommonUrl + "videos/" + _workout.VideoUrl.Split('.')[0].ToLower() + "_male" + Path.GetExtension(_workout.VideoUrl) : null;
                //workoutSelect.FemaleVideo = ((int)_workout.Classification != 1) ? CommonStrings.CommonUrl + "videos/" + _workout.VideoUrl.Split('.')[0].ToLower() + "_female" + Path.GetExtension(_workout.VideoUrl) : null;
                workoutSelect.Attributes = Attributes;
                workoutSelect.BodyMuscles = _workout.WorkoutBodyMuscles.Select(i => i.BodyMuscleId).ToList();
                workoutSelect.Gender = (_workout.Gender != null) ? (int?)_workout.Gender : null;
                workoutSelect.Classification = (int)_workout.Classification;
                if (_workout.Level > 0)
                {

                    workoutSelect.Level = (int)_workout.Level;
                }
                else { workoutSelect.Level = null; }
                if (_workout.TargetMuscle > 0)
                {
                    workoutSelect.TargetMuscle = (int)_workout.TargetMuscle;

                }
                else { workoutSelect.TargetMuscle = null; }

                Workouts.Add(workoutSelect);
            }
            return Workouts;
        }

        /// <summary>
        /// تمرینات Exercise Search
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("Search")]
        public async Task<PageResult<WorkOutSelectFullDTO>> SearchAsync(string name, int? Page,
      int PageSize, CancellationToken cancellationToken)
        {
            List<WorkOut> workouts = new List<WorkOut>();
            switch (LanguageName)
            {
                case "English":
                    {
                        workouts = await _workOutRepository.Table.Include(n => n.Translation).Where(w => w.Tag.Contains(name.ToLower()) || w.Translation.English.Contains(name.ToLower()))
                                         .OrderBy(w => w.Translation.English.Length)
                                         .Skip((Page - 1 ?? 0) * PageSize)
                                         .Take(PageSize)
                                         .ToListAsync(cancellationToken);
                        break;
                    }
                case "Arabic":
                    {
                        workouts = await _workOutRepository.Table.Include(n => n.Translation).Where(w => w.Tag.Contains(name) || w.Translation.Arabic.Contains(name))
                                                                .OrderBy(w => w.Translation.Arabic.Length)
                                                                .Skip((Page - 1 ?? 0) * PageSize)
                                                                .Take(PageSize)
                                                                .ToListAsync(cancellationToken);
                        break;
                    }
                case "Persian":
                    {
                        workouts = await _workOutRepository.Table.Include(n => n.Translation).Where(w => w.Tag.Contains(name) || w.Translation.Persian.Contains(name))
                                        .OrderBy(w => w.Translation.Persian.Length)
                                        .Skip((Page - 1 ?? 0) * PageSize)
                                        .Take(PageSize)
                                        .ToListAsync(cancellationToken);
                        break;
                    }
                default:
                    {
                        workouts = await _workOutRepository.Table.Include(n => n.Translation).Where(w => w.Tag.Contains(name.ToLower()) || w.Translation.English.Contains(name.ToLower()))
                                        .OrderBy(w => w.Translation.English.Length)
                                        .Skip((Page - 1 ?? 0) * PageSize)
                                        .Take(PageSize)
                                        .ToListAsync(cancellationToken);
                        break;
                    }
            }
            var countDetails = workouts.Count();

            //------------------------------------Translation --------------
            //var translationIds = new List<int>();
            //foreach (var item in workouts)
            //{
            //    translationIds.Add(item.NameId);
            //}
            //var names = await _mediator.Send(new GetTranslationQuery() { Ids = translationIds });
            //------------------------------------------------------------------------------
            List<WorkOutSelectFullDTO> _paging = new List<WorkOutSelectFullDTO>();

            //var workoutList = new List<WorkOutSelectDTO>();
            if (workouts.Count > 0)
            {
                foreach (var workout in workouts)
                {
                    WorkOutSelectFullDTO invoicePaging = new WorkOutSelectFullDTO()
                    {
                        Id = workout.Id,
                        Name = new Translation
                        {
                            Id = workout.Translation.Id,
                            Persian = workout.Translation.Persian,
                            Arabic = workout.Translation.Arabic,
                            English = workout.Translation.English
                        }, //names.Find(n => n.Value == workout.NameId).Text,
                        IconUri = workout.IconUri,
                        ImageUri = workout.ImageUri,
                        Classification = (int)workout.Classification,
                        Level = (int)workout.Level,
                    };
                    if (workout.Gender != null) { invoicePaging.Gender = (int)workout.Gender; }
                    _paging.Add(invoicePaging);
                }
            }
            var result = new PageResult<WorkOutSelectFullDTO>
            {
                Count = countDetails,
                PageIndex = Page ?? 1,
                PageSize = PageSize,
                Items = _paging
            };
            return result;
        }



        [HttpGet("GetAllPagingAsync")]
        public async Task<PageResult<WorkOutSelectFullDTO>> GetAllPagingAsync(int? clasificationId, int? Page, int PageSize,
            CancellationToken cancellationToken)
        {
            var Workouts = new List<WorkOutSelectFullDTO>();
            List<WorkOut> _workoutList = await _workOutRepository.Table
                .Include(n => n.Translation)
                .Include(r => r.TranslationRecommandation)
                .Include(h => h.TranslationHowToDo)
                .Include(a => a.WorkOutAttribute).ThenInclude(v => v.Translation)
                .Include(a => a.WorkOutAttribute).ThenInclude(av => av.WorkOutAttributeValue).ThenInclude(tv => tv.Translation)
                .Include(b => b.WorkoutBodyMuscles)
                .OrderByDescending(w => w.Id).ToListAsync(cancellationToken);


            List<WorkOut> WorkoutList = new List<WorkOut>();
            if (clasificationId > 0)
            {
                WorkoutList = _workoutList.Where(w => (int)w.Classification == clasificationId).Skip((Page - 1 ?? 0) * PageSize)
                .Take(PageSize).ToList();
            }
            else
            {
                WorkoutList = _workoutList.Skip((Page - 1 ?? 0) * PageSize).Take(PageSize).ToList();
            }

            var countDetails = WorkoutList.Count();
            List<WorkOutSelectFullDTO> _paging = new List<WorkOutSelectFullDTO>();
            if (WorkoutList.Count > 0)
            {
                foreach (var _workout in WorkoutList)
                {
                    var Attributes = new List<AttributeFullModel>();
                    if (_workout.WorkOutAttribute.Count() > 0)
                    {
                        foreach (var item in _workout.WorkOutAttribute)
                        {
                            //--------------------------------WorkoutAttributes------------------------------             
                            var WoAtVal = new List<AttributeSelectFullModel>();
                            foreach (var attributeValue in item.WorkOutAttributeValue)
                            {

                                var attrVal = new AttributeSelectFullModel();
                                attrVal.Name = new Translation()
                                {
                                    Id = attributeValue.Translation.Id,
                                    Persian = attributeValue.Translation.Persian,
                                    English = attributeValue.Translation.English,
                                    Arabic = attributeValue.Translation.Arabic
                                };
                                attrVal.BurnedCalori = attributeValue.BurnedCalories;
                                attrVal.Id = attributeValue.Id;
                                WoAtVal.Add(attrVal);
                            }
                            var Attribut = new AttributeFullModel()
                            {
                                Id = item.Id,
                                Name = new Translation()
                                {
                                    Id = item.Translation.Id,
                                    Persian = item.Translation.Persian,
                                    English = item.Translation.English,
                                    Arabic = item.Translation.Arabic
                                },
                                AttributeValue = WoAtVal,
                            };
                            Attributes.Add(Attribut);
                        }
                    }
                    //------------------------------------------------------------------------------
                    var workoutSelect = new WorkOutSelectFullDTO();

                    workoutSelect.Id = _workout.Id;
                    workoutSelect.Name = new Translation()
                    {
                        Id = _workout.Translation.Id,
                        Persian = _workout.Translation.Persian,
                        English = _workout.Translation.English,
                        Arabic = _workout.Translation.Arabic
                    };
                    workoutSelect.HowToDo = (_workout.HowToDoId > 0) ? new Translation()
                    {
                        Id = _workout.TranslationHowToDo.Id,
                        Persian = _workout.TranslationHowToDo.Persian,
                        English = _workout.TranslationHowToDo.English,
                        Arabic = _workout.TranslationHowToDo.Arabic
                    } : null;
                    workoutSelect.Recommandation = (_workout.RecommandationId > 0) ? new Translation()
                    {
                        Id = _workout.TranslationRecommandation.Id,
                        Persian = _workout.TranslationRecommandation.Persian,
                        English = _workout.TranslationRecommandation.English,
                        Arabic = _workout.TranslationRecommandation.Arabic
                    } : null;
                    workoutSelect.BurnedCalories = _workout.BurnedCalories;
                    workoutSelect.IconUri = ((int)_workout.Classification == 1) ? "#.. / .. / res/img/sports/" + _workout.IconUri + "#" : CommonStrings.CommonUrl + "icons/" + _workout.IconUri;
                    workoutSelect.ImageUri = (_workout.ImageUri != null) ? CommonStrings.CommonUrl + "images/" + _workout.ImageUri : null;
                    workoutSelect.Video = (_workout.VideoUrl != null) ? CommonStrings.CommonUrl + "videos/" + _workout.VideoUrl : null;
                    //workoutSelect.MaleVideo = ((int)_workout.Classification != 1) ? CommonStrings.CommonUrl + "videos/" + _workout.VideoUrl.Split('.')[0].ToLower() + "_male" + Path.GetExtension(_workout.VideoUrl) : null;
                    //workoutSelect.FemaleVideo = ((int)_workout.Classification != 1) ? CommonStrings.CommonUrl + "videos/" + _workout.VideoUrl.Split('.')[0].ToLower()+ "_female" + Path.GetExtension(_workout.VideoUrl) : null;
                    workoutSelect.Attributes = Attributes;
                    workoutSelect.BodyMuscles = _workout.WorkoutBodyMuscles.Select(i => i.BodyMuscleId).ToList();
                    if (_workout.Gender != null)
                    {
                        workoutSelect.Gender = ((int)_workout.Gender < 2) ? (int?)_workout.Gender : null;
                    }
                    workoutSelect.Classification = (int)_workout.Classification;
                    if (_workout.Level > 0)
                    {
                        workoutSelect.Level = (int)_workout.Level;
                    }
                    else { workoutSelect.Level = null; }
                    if (_workout.TargetMuscle > 0)
                    {
                        workoutSelect.TargetMuscle = (int)_workout.TargetMuscle;
                    }
                    else
                    {
                        workoutSelect.TargetMuscle = null;
                    }

                    _paging.Add(workoutSelect);
                }
            }

            var result = new PageResult<WorkOutSelectFullDTO>
            {
                Count = countDetails,
                PageIndex = Page ?? 1,
                PageSize = PageSize,
                Items = _paging
            };

            return result;
        }


        [HttpGet("GetAllByClassification")]
        public async Task<ApiResult<List<WorkOutSelectFullDTO>>> GetAllByClassificationAsync(int clasificationId, CancellationToken cancellationToken)
        {
            var Workouts = new List<WorkOutSelectFullDTO>();
            List<WorkOut> WorkoutList = await _workOutRepository.Table
                .Include(n => n.Translation)
                .Include(r => r.TranslationRecommandation)
                .Include(h => h.TranslationHowToDo)
                .Include(a => a.WorkOutAttribute).ThenInclude(v => v.Translation)
                .Include(a => a.WorkOutAttribute).ThenInclude(av => av.WorkOutAttributeValue).ThenInclude(tv => tv.Translation)
                .Include(b => b.WorkoutBodyMuscles)
                .Where(w => (int)w.Classification == clasificationId)
                .ToListAsync(cancellationToken);
            var countDetails = WorkoutList.Count();
            try
            {
                foreach (var _workout in WorkoutList)
                {
                    var Attributes = new List<AttributeFullModel>();
                    if (_workout.WorkOutAttribute.Count() > 0)
                    {
                        foreach (var item in _workout.WorkOutAttribute)
                        {
                            //--------------------------------WorkoutAttributes------------------------------             
                            var WoAtVal = new List<AttributeSelectFullModel>();
                            foreach (var attributeValue in item.WorkOutAttributeValue)
                            {

                                var attrVal = new AttributeSelectFullModel();
                                attrVal.Name = new Translation()
                                {
                                    Id = attributeValue.Translation.Id,
                                    Persian = attributeValue.Translation.Persian,
                                    English = attributeValue.Translation.English,
                                    Arabic = attributeValue.Translation.Arabic
                                };
                                attrVal.BurnedCalori = attributeValue.BurnedCalories;
                                attrVal.Id = attributeValue.Id;
                                WoAtVal.Add(attrVal);
                            }
                            var Attribut = new AttributeFullModel()
                            {
                                Id = item.Id,
                                Name = new Translation()
                                {
                                    Id = item.Translation.Id,
                                    Persian = item.Translation.Persian,
                                    English = item.Translation.English,
                                    Arabic = item.Translation.Arabic
                                },
                                AttributeValue = WoAtVal,
                            };
                            Attributes.Add(Attribut);
                        }
                    }
                    //------------------------------------------------------------------------------
                    try
                    {
                        var workoutSelect = new WorkOutSelectFullDTO();
                        workoutSelect.Id = _workout.Id;
                        workoutSelect.Name = new Translation()
                        {
                            Id = _workout.Translation.Id,
                            Persian = _workout.Translation.Persian,
                            English = _workout.Translation.English,
                            Arabic = _workout.Translation.Arabic
                        };
                        workoutSelect.HowToDo = (_workout.HowToDoId > 0) ? new Translation()
                        {
                            Id = _workout.TranslationHowToDo.Id,
                            Persian = _workout.TranslationHowToDo.Persian,
                            English = _workout.TranslationHowToDo.English,
                            Arabic = _workout.TranslationHowToDo.Arabic
                        } : null;
                        workoutSelect.Recommandation = (_workout.RecommandationId > 0) ? new Translation()
                        {
                            Id = _workout.TranslationRecommandation.Id,
                            Persian = _workout.TranslationRecommandation.Persian,
                            English = _workout.TranslationRecommandation.English,
                            Arabic = _workout.TranslationRecommandation.Arabic
                        } : null;
                        workoutSelect.BurnedCalories = _workout.BurnedCalories;
                        // workoutSelect.IconUri = ((int)_workout.Classification == 1) ? "#.. / .. / res/img/sports/" + _workout.IconUri + "#" : CommonStrings.CommonUrl + "icons/" + _workout.IconUri;
                        TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
                        workoutSelect.IconUri = ((int)_workout.Classification == 1) ? myTI.ToTitleCase(Path.GetFileNameWithoutExtension(_workout.IconUri)) : CommonStrings.CommonUrl + "icons/" + _workout.IconUri;
                        workoutSelect.ImageUri = (_workout.ImageUri != null) ? CommonStrings.CommonUrl + "images/" + _workout.ImageUri : null;
                        workoutSelect.Video = ((int)_workout.Classification != 1) ? CommonStrings.CommonUrl + "videos/" + _workout.VideoUrl : null;
                        //workoutSelect.MaleVideo = ((int)_workout.Classification != 1) ? CommonStrings.CommonUrl + "videos/" + _workout.VideoUrl.Split('.')[0].ToLower() + "_male" + Path.GetExtension(_workout.VideoUrl) : null;
                        //workoutSelect.FemaleVideo = ((int)_workout.Classification != 1) ? CommonStrings.CommonUrl + "videos/" + _workout.VideoUrl.Split('.')[0].ToLower() + "_female" + Path.GetExtension(_workout.VideoUrl) : null;
                        workoutSelect.Attributes = Attributes;
                        if (_workout.CardioCategory > 0)
                        {
                            workoutSelect.CardioCategory = (int)_workout.CardioCategory;
                        }
                        workoutSelect.BodyMuscles = (_workout.WorkoutBodyMuscles.Count() > 0) ? _workout.WorkoutBodyMuscles.Select(i => i.BodyMuscleId).ToList() : null;
                        workoutSelect.Gender = (_workout.Gender != null) ? (int?)_workout.Gender : null;
                        workoutSelect.Classification = (int)_workout.Classification;
                        if (_workout.Level > 0) { workoutSelect.Level = (int)_workout.Level; }
                        if (_workout.TargetMuscle > 0) { workoutSelect.TargetMuscle = (int)_workout.TargetMuscle; }

                        Workouts.Add(workoutSelect);
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Workouts;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var userTrackWorkouts = await _userTrackWorkoutRepository.TableNoTracking.AnyAsync(w => w.WorkOutId == Id, cancellationToken);
            if (userTrackWorkouts)
            {
                return null;
            }

            WorkOut workout = await _workOutRepository.Table
               .Include(b => b.WorkoutBodyMuscles)
               .Include(a => a.WorkOutAttribute)
               .Where(w => w.Id == Id).FirstAsync(cancellationToken);

            var nameIds = new List<int>();

            nameIds.Add(workout.NameId);
            if (workout.RecommandationId > 0)
            {
                nameIds.Add(workout.RecommandationId ?? 0);
            }
            if (workout.HowToDoId > 0)
            {
                nameIds.Add(workout.HowToDoId ?? 0);
            }

            if (workout.WorkOutAttribute.Count() > 0)
            {
                List<int> attNameIds = workout.WorkOutAttribute.Select(w => w.NameId).ToList();
                nameIds.AddRange(attNameIds);

                foreach (var item in workout.WorkOutAttribute)
                {
                    var attValNameIds = await _workOutAttributeValueRepository.Table.Where(v => v.WorkOutAttributeId == item.Id).Select(v => v.NameId).ToListAsync(cancellationToken);
                    nameIds.AddRange(attValNameIds);
                }
            }

            var attValues = await _workOutAttributeValueRepository.TableNoTracking.Where(v => v.WorkOutAttribute.WorkOut.Id == Id).ToListAsync(cancellationToken);
            await _workOutAttributeValueRepository.DeleteRangeAsync(attValues, cancellationToken);

            var favWorkout = await _userFavoriteWorkOutRepository.TableNoTracking.Where(f => f.WorkOutId == Id).FirstOrDefaultAsync(cancellationToken);
            if (favWorkout != null)
            {
                await _userFavoriteWorkOutRepository.DeleteAsync(favWorkout, cancellationToken);
            }
            DeleteFile deleteFile = new DeleteFile(_environment);
            if (workout.ImageUri != null)
            {
                deleteFile.DeleteFiles(workout.ImageUri, "images");
                //if (workout.ImageUri != workout.IconUri && workout.IconUri != null)
                //{
                //    d.DeleteFiles(workout.IconUri, "icons");
                //}
            }
            if (workout.IconUri != null)
            {
                deleteFile.DeleteFiles(workout.IconUri, "icons");
            }
            if (workout.VideoUrl != null)
            {
                deleteFile.DeleteFiles(workout.VideoUrl, "videos");
            }


            await _workoutBodyMusclesRepository.DeleteRangeAsync(workout.WorkoutBodyMuscles, cancellationToken);
            await _workoutAttributeRepository.DeleteRangeAsync(workout.WorkOutAttribute, cancellationToken);

            await _workOutRepository.DeleteAsync(workout, cancellationToken);

            await _mediator.Send(new DeleteTranslationCommand
            {
                Ids = nameIds
            });

            return Ok();
        }


        [HttpDelete("DeleteAttributeValue")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> DeleteAttributeValue(int Id, CancellationToken cancellationToken)
        {
            var nameIds = new List<int>();
            var attValue = await _workOutAttributeValueRepository.TableNoTracking.Where(v => v.Id == Id).FirstOrDefaultAsync(cancellationToken);
            nameIds.Add(attValue.NameId);
            await _workOutAttributeValueRepository.DeleteAsync(attValue, cancellationToken);
            await _mediator.Send(new DeleteTranslationCommand
            {
                Ids = nameIds
            });
            return Ok();
        }



        [HttpPut("EditAttributeValue")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> EditAttributeValue(WorkOutAttributeValueDTO AttrValuedto, CancellationToken cancellationToken)
        {

            WorkOutAttributeValue oldAttr = await _workOutAttributeValueRepository.GetByIdAsync(cancellationToken, AttrValuedto.WorkOutAttributeId);
            AttrValuedto.Name.Id = oldAttr.NameId;
            var name = await _mediator.Send(new UpdateTranslationCommand
            {
                Translation = AttrValuedto.Name.ToEntity(_mapper)
            });

            oldAttr.NameId = name.Id;
            oldAttr.BurnedCalories = AttrValuedto.BurnedCalories;
            await _workOutAttributeValueRepository.UpdateAsync(oldAttr, cancellationToken);
            return Ok();
        }
    }
}
