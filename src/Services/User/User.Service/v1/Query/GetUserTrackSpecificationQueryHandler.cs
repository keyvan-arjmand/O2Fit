using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Common;
using Data.Contracts;
using Domain;
using MediatR;
using System.Linq;
using System;

namespace User.Service.v1.Query
{
    public class GetUserTrackSpecificationQueryHandler : IRequestHandler<GetUserTrackSpecificationQuery, UserProfileTrack>, ITransientDependency
    {
        private readonly IRepository<UserProfile> _repositoryProfile;
        private readonly IRepository<UserTrackSpecification> _repositoryTrack;
        private readonly IRepositoryRedis<List<UserTrackSpecification>> _repositoryRedisTrack;
        private readonly IRepositoryRedis<UserProfile> _repositoryRedisProfile;

        public GetUserTrackSpecificationQueryHandler(IRepository<UserProfile> repositoryProfile,
         IRepository<UserTrackSpecification> repositoryTrack,
         IRepositoryRedis<List<UserTrackSpecification>> repositoryRedisTrack,
         IRepositoryRedis<UserProfile> repositoryRedisProfile)
        {
            _repositoryProfile = repositoryProfile;
            _repositoryTrack = repositoryTrack;
            _repositoryRedisTrack = repositoryRedisTrack;
            _repositoryRedisProfile = repositoryRedisProfile;
        }

        public async Task<UserProfileTrack> Handle(GetUserTrackSpecificationQuery request, CancellationToken cancellationToken)
        {

            #region WithError
            //UserProfileTrack userProfileTrack = new UserProfileTrack();

            //userProfileTrack.UserProfile = await _repositoryRedisProfile.GetAsync($"UserProfile_{request.UserId}");

            //// await _repositoryRedisProfile.DeleteAsync($"UserProfile_{request.UserId}");
            //userProfileTrack.TrackSpecifications = await _repositoryRedisTrack.GetAsync($"UserTrackSpecification_{request.UserId}");

            //if (userProfileTrack.UserProfile == null)
            //{
            //    UserProfile profile = await _repositoryProfile.TableNoTracking.SingleOrDefaultAsync(a => a.UserId == request.UserId, cancellationToken).ConfigureAwait(false);
            //    if (profile != null)
            //    {
            //        userProfileTrack.UserProfile = profile;
            //        await _repositoryRedisProfile.UpdateAsync($"UserProfile_{request.UserId}", profile);
            //    }
            //}
            ////if (userProfileTrack.TrackSpecifications != null && userProfileTrack.UserProfile != null)
            ////{
            ////    List<UserTrackSpecification> userTrack = userProfileTrack.TrackSpecifications.Where(u => u._id != null && u._id != "0").ToList();
            ////if (userTrack.Count() == 1)
            ////{
            ////    userTrack.Add(new UserTrackSpecification
            ////    {
            ////        _id = "0",
            ////        UserProfileId = userTrack[0].UserProfileId,
            ////        InsertDate = new DateTime(2020, 01, 01),
            ////        WristSize = userTrack[0].WristSize,
            ////        WeightSize = userTrack[0].WeightSize,
            ////        ArmSize = userTrack[0].ArmSize,
            ////        BustSize = userTrack[0].BustSize,
            ////        HighHipSize = userTrack[0].HighHipSize,
            ////        HipSize = userTrack[0].HipSize,
            ////        NeckSize = userTrack[0].NeckSize,
            ////        ShoulderSize = userTrack[0].ShoulderSize,
            ////        ThighSize = userTrack[0].ThighSize,
            ////        WaistSize = userTrack[0].WaistSize,
            ////    });
            ////}
            ////if (userTrack.Count() == 0)
            ////{
            ////    List<UserTrackSpecification> _userTrack = await _repositoryTrack.TableNoTracking.Where(a => a.UserProfileId == userProfileTrack.UserProfile.Id &&
            ////            a._id != null && a._id != "0")
            ////   .OrderByDescending(a => a.InsertDate)
            ////.Take(2)
            ////.ToListAsync(cancellationToken);
            //// userTrack = _userTrack;
            ////userTrack = _userTrack.Where(u => u._id != null && u._id != "0").ToList();
            ////if (userTrack.Count() == 0)
            ////{
            ////    int i = 1;
            ////    while (userTrack.Count() < 2)
            ////    {
            ////        userTrack.Add(new UserTrackSpecification()
            ////        {
            ////            UserProfileId   = userProfileTrack.UserProfile.Id,
            ////            InsertDate      = new DateTime(2020, 01, i),
            ////            WristSize       = 0,
            ////            WeightSize      = 0,
            ////            ArmSize         = 0,
            ////            BustSize        = 0,
            ////            HighHipSize     = 0,
            ////            HipSize         = 0,
            ////            NeckSize        = 0,
            ////            ShoulderSize    = 0,
            ////            ThighSize       = 0,
            ////            WaistSize       = 0,
            ////            _id             = i.ToString()
            ////        });
            ////        i++;
            ////    }
            ////}
            ////else if (userTrack.Count() == 1)
            ////{
            ////    userTrack.Add(new UserTrackSpecification
            ////    {
            ////        _id = "0",
            ////        UserProfileId = userTrack[0].UserProfileId,
            ////        InsertDate = userTrack[0].InsertDate,
            ////        WristSize = userTrack[0].WristSize,
            ////        WeightSize = userTrack[0].WeightSize,
            ////        ArmSize = userTrack[0].ArmSize,
            ////        BustSize = userTrack[0].BustSize,
            ////        HighHipSize = userTrack[0].HighHipSize,
            ////        HipSize = userTrack[0].HipSize,
            ////        NeckSize = userTrack[0].NeckSize,
            ////        ShoulderSize = userTrack[0].ShoulderSize,
            ////        ThighSize = userTrack[0].ThighSize,
            ////        WaistSize = userTrack[0].WaistSize,
            ////    });
            ////}

            ////    }
            ////    userProfileTrack.TrackSpecifications = userTrack;
            ////    await _repositoryRedisTrack.UpdateAsync($"UserTrackSpecification_{request.UserId}", userTrack);
            ////}
            //if (userProfileTrack.TrackSpecifications == null && userProfileTrack.UserProfile != null)
            //{
            //    List<UserTrackSpecification> _userTrack = await _repositoryTrack.TableNoTracking.Where(a => a.UserProfileId == userProfileTrack.UserProfile.Id &&
            //            a._id != null && a._id != "0")
            //        .OrderByDescending(a => a.InsertDate)
            //        //.Take(2)
            //        .ToListAsync(cancellationToken);
            //    //List<UserTrackSpecification> userTrack = _userTrack.Where(u => u._id != null && u._id !="0").ToList();
            //    //if (userTrack.Count() == 1)
            //    //{
            //    //    userTrack.Add(new UserTrackSpecification
            //    //    {
            //    //        _id = "0",
            //    //        UserProfileId   = userTrack[0].UserProfileId,
            //    //        InsertDate      = new DateTime(2020, 01, 01),
            //    //        WristSize       = userTrack[0].WristSize,
            //    //        WeightSize      = userTrack[0].WeightSize,
            //    //        ArmSize         = userTrack[0].ArmSize,
            //    //        BustSize        = userTrack[0].BustSize,
            //    //        HighHipSize     = userTrack[0].HighHipSize,
            //    //        HipSize         = userTrack[0].HipSize,
            //    //        NeckSize        = userTrack[0].NeckSize,
            //    //        ShoulderSize    = userTrack[0].ShoulderSize,
            //    //        ThighSize       = userTrack[0].ThighSize,
            //    //        WaistSize       = userTrack[0].WaistSize,
            //    //    });
            //    //
            //    //}
            //    //if (userTrack.Count() < 2)
            //    //{
            //    //    int i = 1;
            //    //    while (userTrack.Count() < 2)
            //    //    {
            //    //        userTrack.Add(new UserTrackSpecification()
            //    //        {
            //    //            UserProfileId = userProfileTrack.UserProfile.Id,
            //    //            _id = i.ToString()
            //    //        });
            //    //        i++;
            //    //    }
            //    //}
            //    userProfileTrack.TrackSpecifications = _userTrack;
            //    await _repositoryRedisTrack.UpdateAsync($"UserTrackSpecification_{request.UserId}", _userTrack);
            //}
            //// else if (userProfileTrack.UserProfile == null)
            //// {
            ////     userProfileTrack.UserProfile = new UserProfile()
            ////     {
            ////         UserId = request.UserId,
            ////     };
            ////     if (userProfileTrack.TrackSpecifications == null)
            ////     {
            ////         List<UserTrackSpecification> userTrack = new List<UserTrackSpecification>();
            ////         userProfileTrack.TrackSpecifications = userTrack;
            ////     }
            ////int i = 1;
            ////while (userTrack.Count() < 2)
            ////{
            ////    userTrack.Add(new UserTrackSpecification()
            ////    {
            ////        UserProfileId = 0,
            ////        InsertDate = new DateTime(2020, 01, i),
            ////        WristSize = 0,
            ////        WeightSize = 0,
            ////        ArmSize = 0,
            ////        BustSize = 0,
            ////        HighHipSize = 0,
            ////        HipSize = 0,
            ////        NeckSize = 0,
            ////        ShoulderSize = 0,
            ////        ThighSize = 0,
            ////        WaistSize = 0,
            ////        _id = i.ToString()
            ////    });
            ////    i++;
            ////}

            //// }
            //return userProfileTrack; 
            #endregion

            UserProfileTrack userProfileTrack = new UserProfileTrack();

            userProfileTrack.UserProfile = await _repositoryRedisProfile.GetAsync($"UserProfile_{request.UserId}");

            // await _repositoryRedisProfile.DeleteAsync($"UserProfile_{request.UserId}");
            userProfileTrack.TrackSpecifications = await _repositoryRedisTrack.GetAsync($"UserTrackSpecification_{request.UserId}");

            if (userProfileTrack.UserProfile == null)
            {
                UserProfile _profile = await _repositoryProfile.TableNoTracking.Where(a => a.UserId == request.UserId).SingleOrDefaultAsync(cancellationToken);
                if (_profile != null)
                {
                    userProfileTrack.UserProfile = _profile;
                    await _repositoryRedisProfile.UpdateAsync($"UserProfile_{request.UserId}", _profile);
                }
            }
            if (userProfileTrack.TrackSpecifications != null && userProfileTrack.UserProfile != null)
            {
                List<UserTrackSpecification> userTrack = userProfileTrack.TrackSpecifications.Where(u => u._id != null && u._id != "0").ToList();
                if (userTrack.Count() == 1)
                {
                    userTrack.Add(new UserTrackSpecification
                    {
                        _id = "0",
                        UserProfileId = userTrack[0].UserProfileId,
                        InsertDate = new DateTime(2020, 01, 01),
                        WristSize = userTrack[0].WristSize,
                        WeightSize = userTrack[0].WeightSize,
                        ArmSize = userTrack[0].ArmSize,
                        BustSize = userTrack[0].BustSize,
                        HighHipSize = userTrack[0].HighHipSize,
                        HipSize = userTrack[0].HipSize,
                        NeckSize = userTrack[0].NeckSize,
                        ShoulderSize = userTrack[0].ShoulderSize,
                        ThighSize = userTrack[0].ThighSize,
                        WaistSize = userTrack[0].WaistSize,
                    });
                }
                if (userTrack.Count() == 0)
                {
                    List<UserTrackSpecification> _userTrack = await _repositoryTrack.TableNoTracking.Where(a => a.UserProfileId == userProfileTrack.UserProfile.Id)
                   .OrderByDescending(a => a.InsertDate)
                   .Take(2)
                   .ToListAsync(cancellationToken);
                    userTrack = _userTrack.Where(u => u._id != null && u._id != "0").ToList();
                    if (userTrack.Count() == 0)
                    {
                        int i = 1;
                        while (userTrack.Count() < 2)
                        {
                            userTrack.Add(new UserTrackSpecification()
                            {
                                UserProfileId = userProfileTrack.UserProfile.Id,
                                InsertDate = new DateTime(2020, 01, i),
                                WristSize = 0,
                                WeightSize = 0,
                                ArmSize = 0,
                                BustSize = 0,
                                HighHipSize = 0,
                                HipSize = 0,
                                NeckSize = 0,
                                ShoulderSize = 0,
                                ThighSize = 0,
                                WaistSize = 0,
                                _id = i.ToString()
                            });
                            i++;
                        }
                    }
                    else if (userTrack.Count() == 1)
                    {
                        userTrack.Add(new UserTrackSpecification
                        {
                            _id = "0",
                            UserProfileId = userTrack[0].UserProfileId,
                            InsertDate = userTrack[0].InsertDate,
                            WristSize = userTrack[0].WristSize,
                            WeightSize = userTrack[0].WeightSize,
                            ArmSize = userTrack[0].ArmSize,
                            BustSize = userTrack[0].BustSize,
                            HighHipSize = userTrack[0].HighHipSize,
                            HipSize = userTrack[0].HipSize,
                            NeckSize = userTrack[0].NeckSize,
                            ShoulderSize = userTrack[0].ShoulderSize,
                            ThighSize = userTrack[0].ThighSize,
                            WaistSize = userTrack[0].WaistSize,
                        });
                    }

                }
                userProfileTrack.TrackSpecifications = userTrack;
                await _repositoryRedisTrack.UpdateAsync($"UserTrackSpecification_{request.UserId}", userTrack);
            }
            else if (userProfileTrack.TrackSpecifications == null && userProfileTrack.UserProfile != null)
            {
                List<UserTrackSpecification> _userTrack = await _repositoryTrack.TableNoTracking.Where(a => a.UserProfileId == userProfileTrack.UserProfile.Id)
                    .OrderByDescending(a => a.InsertDate)
                    .Take(2)
                    .ToListAsync(cancellationToken);
                List<UserTrackSpecification> userTrack = _userTrack.Where(u => u._id != null && u._id != "0").ToList();
                if (userTrack.Count() == 1)
                {
                    userTrack.Add(new UserTrackSpecification
                    {
                        _id = "0",
                        UserProfileId = userTrack[0].UserProfileId,
                        InsertDate = new DateTime(2020, 01, 01),
                        WristSize = userTrack[0].WristSize,
                        WeightSize = userTrack[0].WeightSize,
                        ArmSize = userTrack[0].ArmSize,
                        BustSize = userTrack[0].BustSize,
                        HighHipSize = userTrack[0].HighHipSize,
                        HipSize = userTrack[0].HipSize,
                        NeckSize = userTrack[0].NeckSize,
                        ShoulderSize = userTrack[0].ShoulderSize,
                        ThighSize = userTrack[0].ThighSize,
                        WaistSize = userTrack[0].WaistSize,
                    });

                }
                if (userTrack.Count() < 2)
                {
                    int i = 1;
                    while (userTrack.Count() < 2)
                    {
                        userTrack.Add(new UserTrackSpecification()
                        {
                            UserProfileId = userProfileTrack.UserProfile.Id,
                            _id = i.ToString()
                        });
                        i++;
                    }
                }
                userProfileTrack.TrackSpecifications = userTrack;
                await _repositoryRedisTrack.UpdateAsync($"UserTrackSpecification_{request.UserId}", userTrack);
            }
            else if (userProfileTrack.UserProfile == null)
            {
                userProfileTrack.UserProfile = new UserProfile()
                {
                    UserId = request.UserId,
                };
                List<UserTrackSpecification> userTrack = new List<UserTrackSpecification>();
                int i = 1;
                while (userTrack.Count() < 2)
                {
                    userTrack.Add(new UserTrackSpecification()
                    {
                        UserProfileId = 0,
                        InsertDate = new DateTime(2020, 01, i),
                        WristSize = 0,
                        WeightSize = 0,
                        ArmSize = 0,
                        BustSize = 0,
                        HighHipSize = 0,
                        HipSize = 0,
                        NeckSize = 0,
                        ShoulderSize = 0,
                        ThighSize = 0,
                        WaistSize = 0,
                        _id = i.ToString()
                    });
                    i++;
                }
                userProfileTrack.TrackSpecifications = userTrack;
            }
            return userProfileTrack;
        }
    }
}