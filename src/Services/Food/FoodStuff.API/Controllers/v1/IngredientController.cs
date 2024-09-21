using AutoMapper;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.API.Models.DTOs;
using FoodStuff.Common;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.Translation;
using FoodStuff.Domain.Entities.ViewModels;
using FoodStuff.Domain.Enum;
using FoodStuff.Service.Contracts;
using FoodStuff.Service.Services;
using FoodStuff.Service.v1.Command;
using FoodStuff.Service.v1.Query;
using FoodStuff.Service.v1.Query.GetIngredients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Service.v1.Command;
using Service.v1.Query;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Controllers.v1
{
    [ApiVersion("1")]
    public class IngredientController : BaseController
    {
        private readonly IFoodMeasureUnitRepository _foodMeasurUnitrepository;
        private readonly IFoodRepository _foodRepository;
        private readonly IRepository<IngredientMeasureUnit> IngMessrepository;
        private readonly IIngeredientRepository _ingredientRepository;
        private readonly IFoodIngredientRepository _foodIngredientRepository;
        private readonly IRepository<ExcelTable> _ExcelTable;
        private readonly IWebHostEnvironment _environment;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IRedisCacheClient _redisCacheClient;

        public IngredientController(IIngeredientRepository ingeredientRepository,
           IRepository<IngredientMeasureUnit> IngMessrepository,
           IFoodIngredientRepository foodIngredientRepository,
           IFoodRepository foodRepository, IRepository<ExcelTable> ExcelTable,
           IFoodMeasureUnitRepository foodMeasurUnitrepository,
           IMediator mediator, IMapper mapper, IWebHostEnvironment environment,
           IFileService fileService,
           IRedisCacheClient redisCacheClient)
        {
            _foodMeasurUnitrepository = foodMeasurUnitrepository;
            _foodIngredientRepository = foodIngredientRepository;
            this._ingredientRepository = ingeredientRepository;
            this.IngMessrepository = IngMessrepository;
            _foodRepository = foodRepository;
            _environment = environment;
            _ExcelTable = ExcelTable;
            _mediator = mediator;
            _mapper = mapper;
            _fileService = fileService;
            _redisCacheClient = redisCacheClient;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateIngredientDTO ingredientDto, CancellationToken cancellationToken)
        {
            try
            {

                await _mediator.Send(new CreateIngredientCommand
                {
                    ThumbUri = ingredientDto.ThumbUri,
                    DefaultMeasureUnitId = ingredientDto.DefaultMeasureUnitId,
                    Code = ingredientDto.Code,
                    MeasureUnitIds = ingredientDto.MeasureUnitIds,
                    TranslationDto = new Translation
                    {
                        Arabic = ingredientDto.Name.Arabic,
                        English = ingredientDto.Name.English,
                        Persian = ingredientDto.Name.Persian
                    },
                    NutrientValue = ingredientDto.NutrientValue,
                    Tag = ingredientDto.Tag,
                    TagAr = ingredientDto.TagAr,
                    TagEn = ingredientDto.TagEn,
                });

                return Ok();
            }
            catch (Exception e)
            {
                throw new AppException(ApiResultStatusCode.ServerError, e.Message);
            }

        }
        [HttpGet("GetByCode")]
        [Authorize(Roles = "Admin")]
        public async Task<List<GetByCodeIngredientAdminViewModel>> GetByCode(string code)
        {
            var result = await _mediator.Send(new GetIngredientByCodeQuery
            {
                Code = code
            });
            return result;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Put(UpdateIngredientDTO ingredientDto, CancellationToken cancellationToken)
        {
            try
            {

                Ingredient ingredient = await _ingredientRepository.Table.
                    Include(a => a.IngredientMeasureUnits)
                    .FirstAsync(a => a.Id == ingredientDto.Id,
                        cancellationToken: cancellationToken);


                var IngImgFolder = Path.Combine("wwwroot", "Ingimg");
                var IngImgPath = Path.Combine(Directory.GetCurrentDirectory(), IngImgFolder);

                DeleteFile deleteFile = new DeleteFile(_environment);

                if (!string.IsNullOrEmpty(ingredient.ThumbUri))
                {
                    var fileName = ingredient.ThumbUri;
                    var fullPath = Path.Combine(IngImgPath, fileName);

                    if (System.IO.File.Exists(fullPath))
                    {
                        if (ingredient.ThumbUri != "noimage.jpg" &&
                            ingredient.ThumbUri != "DefaultIngPic.png" &&
                            ingredient.ThumbUri != "o2fit image.jpg")
                        {
                            deleteFile.DeleteFiles(ingredient.ThumbUri, IngImgPath);
                        }

                    }
                }



                if (!string.IsNullOrEmpty(ingredientDto.ThumbUri))
                {
                    ingredientDto.ThumbUri = _fileService.AddImage(
                    ingredientDto.ThumbUri, "Ingimg", ingredientDto.Code);
                }


                ingredientDto.Name.Id = ingredient.NameId;
                await _mediator.Send(new UpdateTranslationCommand
                {
                    Translation = ingredientDto.Name.ToEntity(_mapper)
                });

                ingredient.Code = ingredientDto.Code;

                ingredient.NutrientValue = StringConvertor.DoubleToString(ingredientDto.NutrientValue);
                ingredient.Tag = ingredientDto.Tag == null ? null : ingredientDto.Tag.ToLower();
                ingredient.TagArEn = "";
                ingredient.IsFood = false;
                ingredient.DefaultMeasureUnitId = ingredientDto.DefaultMeasureUnitId;
                ingredient.ThumbUri = ingredientDto.ThumbUri != null ? ingredientDto.ThumbUri : null;
                ingredient.TagAr = ingredientDto.TagAr;
                ingredient.TagEn = ingredientDto.TagEn;


                await _ingredientRepository.UpdateAsync(ingredient, cancellationToken);


                if (ingredient.IngredientMeasureUnits != null)
                {
                    await IngMessrepository.DeleteRangeAsync(ingredient.IngredientMeasureUnits, cancellationToken);
                }


                List<IngredientMeasureUnit> IngMeaslist = new List<IngredientMeasureUnit>();
                foreach (var item in ingredientDto.MeasureUnitIds)
                {
                    IngredientMeasureUnit ingredientMeasureUnit = new IngredientMeasureUnit();
                    ingredientMeasureUnit.IngredientId = ingredient.Id;
                    ingredientMeasureUnit.MeasureUnitId = item;
                    IngMeaslist.Add(ingredientMeasureUnit);


                }
                await IngMessrepository.AddRangeAsync(IngMeaslist, cancellationToken);

                return Ok();

            }
            catch (Exception e)
            {
                throw new AppException(ApiResultStatusCode.ServerError, e.Message);
            }
        }


        [HttpGet("GetById")]
        public async Task<ApiResult<IngredientAdminDTO>> GetByIdAsync(int Id, CancellationToken cancellationToken)
        {
            //var listIng = repository.Table;
            try
            {

                var Ing = await _ingredientRepository.Table
                                               .Include(it => it.Translation)
                                               .Include(a => a.IngredientMeasureUnits).ThenInclude(a => a.MeasureUnit)
                                               .ThenInclude(t => t.Translation)
                                               .Where(i => i.Id == Id).FirstAsync(cancellationToken);


                List<MeasureUnitModelDTO> _Measureunits = new List<MeasureUnitModelDTO>
                {
                    new MeasureUnitModelDTO()
                    {
                        Id = 36,
                        Value = 1,
                        Persian = "گرم",
                        English = "Gram",
                        Arabic = "غرام",
                        MeasureUnitCategory = MeasureUnitCategory.weight
                    },
                    new MeasureUnitModelDTO()
                    {
                        Id = 37,
                        Value = 28.35,
                        Persian = "اونس",
                        English = "Ounces",
                        Arabic = "Ounces",
                        MeasureUnitCategory = MeasureUnitCategory.weight
                    },
                    new MeasureUnitModelDTO()
                    {
                        Id = 58,
                        Value = 453.6,
                        Persian = "پوند",
                        English = "Pounds",
                        Arabic = "Pounds",
                        MeasureUnitCategory = MeasureUnitCategory.weight
                    }
                };

                if (Ing.IngredientMeasureUnits.Any())
                {
                    var _measureunits = Ing.IngredientMeasureUnits.Where(m => m.MeasureUnitId != 36 && m.MeasureUnitId != 37 && m.MeasureUnitId != 58).Select(m => new MeasureUnitModelDTO()
                    {
                        Id = m.MeasureUnitId,
                        Value = m.MeasureUnit.Value,
                        Persian = m.MeasureUnit.Translation.Persian,
                        English = m.MeasureUnit.Translation.English,
                        Arabic = m.MeasureUnit.Translation.Arabic,
                        MeasureUnitCategory = m.MeasureUnit.MeasureUnitCategory
                    }).ToList();
                    _Measureunits.AddRange(_measureunits);
                }

                var Ingredient = new IngredientAdminDTO()
                {
                    Id = Ing.Id,
                    Name = new Translation()
                    {
                        Id = Ing.NameId,
                        Persian = Ing.Translation.Persian,
                        English = Ing.Translation.English,
                        Arabic = Ing.Translation.Arabic
                    },
                    Code = Convert.ToInt32(Ing.Code),
                    ThumbUri = CommonStrings.CommonUrl + "Ingimg/" + Ing.ThumbUri,
                    NutrientValue = Ing.NutrientValue,
                    MessureUnits = _Measureunits,

                };
                return Ingredient;
            }
            catch (Exception ex)
            {

                throw new AppException(ApiResultStatusCode.ServerError, ex.Message);
            }
        }


        [HttpGet("GetMeasurunitsByIngId")]
        public async Task<List<MeasureUnitModelDTO>> GetMeasurunitAsync(int Id, CancellationToken cancellationToken)
        {
            //var listIng = repository.Table;

            var Ing = await _ingredientRepository.Table.Include(a => a.IngredientMeasureUnits)
                                           .ThenInclude(a => a.MeasureUnit)
                                           .Where(i => i.Id == Id).SingleAsync(cancellationToken);

            var nameIds = new List<int>();
            foreach (var item in Ing.IngredientMeasureUnits)
            {
                nameIds.Add(item.MeasureUnit.NameId);
            }
            var translations = await _mediator.Send(new GetTranslationQuery()
            {
                Ids = nameIds
            });
            List<MeasureUnitModelDTO> MessureUnits = Ing.IngredientMeasureUnits.Select(m => new MeasureUnitModelDTO()
            {
                Id = m.MeasureUnit.Id,
                Persian = translations.Find(n => n.Value == m.MeasureUnit.NameId).Text,
                Value = m.MeasureUnit.Value
            }).ToList();

            return MessureUnits;
        }

        [HttpGet("GetByIdAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<GetByIdIngredientAdminViewModel> GetByIdAdmin(int Id, CancellationToken cancellationToken)
        {
            var result = await _ingredientRepository.TableNoTracking
                 .Include(a => a.IngredientMeasureUnits)
                                            .Include(t => t.Translation)
                                            .Select(s => new GetByIdIngredientAdminViewModel
                                            {
                                                Id = s.Id,
                                                DefaultMeasureUnitId = s.DefaultMeasureUnitId,
                                                Code = s.Code,
                                                IsFood = s.IsFood,
                                                NutrientValue = StringConvertor.ToNumber(s.NutrientValue),
                                                Tag = s.Tag,
                                                TagAr = s.TagAr,
                                                TagArEn = s.TagArEn,
                                                TagEn = s.TagEn,
                                                ThumbUri = s.ThumbUri,
                                                Name = new TranslationViewModel
                                                {
                                                    Persian = s.Translation.Persian,
                                                    Arabic = s.Translation.Arabic,
                                                    English = s.Translation.English
                                                },
                                                MeasureUnitIds = s.IngredientMeasureUnits.Select(s => s.MeasureUnitId).ToList()

                                            }).FirstAsync(i => i.Id == Id);


            if (!string.IsNullOrEmpty(result.ThumbUri))
            {
                result.ThumbUri = await ConvertImage.GetImageAsBase64Url("Ingimg/" + result.ThumbUri);
            }

            return result;


        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<PageResult<GetAllIngredientAdminViewModel>> GetAll(int? Page, int PageSize
            , CancellationToken cancellationToken)
        {

            List<GetAllIngredientAdminViewModel> _paging = await _ingredientRepository.TableNoTracking
                .Include(a => a.Translation)
                                           .OrderByDescending(i => i.Id)
                                           .Skip((Page - 1 ?? 0) * PageSize)
                                           .Take(PageSize)
                                           .Select(s => new GetAllIngredientAdminViewModel
                                           {
                                               Id = s.Id,
                                               Code = s.Code,
                                               Name = s.Translation.Persian,
                                           })
                                           .ToListAsync(cancellationToken);

            var result = new PageResult<GetAllIngredientAdminViewModel>
            {
                Count = _paging.Count,
                PageIndex = Page ?? 1,
                PageSize = PageSize,
                Items = _paging
            };

            return result;
        }


        [HttpGet("Search")]
        public async Task<PageResult<IngSearchQueryDTO>> SearchAsync(string name, int? Page, int PageSize
            , CancellationToken cancellationToken)
        {
            List<IngSearchResultDTO> _paging = await _mediator.Send(new GetIngredientQuery
            {
                name = name,
                Page = Page,
                PageSize = PageSize,
                LanguageName = LanguageName
            });

            List<IngSearchQueryDTO> ingSearchQueryDTOs = new List<IngSearchQueryDTO>();
            foreach (var item in _paging)
            {
                IngSearchQueryDTO ingSearchQueryDTO = new IngSearchQueryDTO
                {
                    Id = item.Id,
                    Name = item.NameTranslate,
                    Code = item.Code
                };

                ingSearchQueryDTOs.Add(ingSearchQueryDTO);
            }

            ingSearchQueryDTOs = ingSearchQueryDTOs.Distinct().ToList();

            var result = new PageResult<IngSearchQueryDTO>
            {
                PageIndex = Page ?? 1,
                PageSize = PageSize,
                Items = ingSearchQueryDTOs
            };

            return result;
        }

        [HttpGet("SearchAdmin")]
        public async Task<PageResult<IngSearchQueryDTO>> SearchAdmin(string name, int? Page, int PageSize
            , CancellationToken cancellationToken)
        {
            List<IngSearchResultDTO> _paging = await _mediator.Send(new GetIngredientAdminQuery
            {
                name = name,
                Page = Page,
                PageSize = PageSize,
                LanguageName = LanguageName == null ? "Persian" : LanguageName
            });

            List<IngSearchQueryDTO> ingSearchQueryDTOs = new List<IngSearchQueryDTO>();
            foreach (var item in _paging)
            {
                IngSearchQueryDTO ingSearchQueryDTO = new IngSearchQueryDTO
                {
                    Id = item.Id,
                    Name = item.NameTranslate,
                    Code = item.Code
                };

                ingSearchQueryDTOs.Add(ingSearchQueryDTO);
            }

            ingSearchQueryDTOs = ingSearchQueryDTOs.Distinct().ToList();

            var result = new PageResult<IngSearchQueryDTO>
            {
                PageIndex = Page ?? 1,
                PageSize = PageSize,
                Items = ingSearchQueryDTOs
            };

            return result;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {

            bool exist = _foodIngredientRepository.TableNoTracking.Any(fi => fi.IngredientId == id);
            if (exist)
            {
                throw new AppException("This Ingredient Used In Food");
            }

            Ingredient Ing = await _ingredientRepository.Table
                .Include(a => a.IngredientMeasureUnits).FirstAsync(a => a.Id == id);


            await IngMessrepository.DeleteRangeAsync(Ing.IngredientMeasureUnits, cancellationToken);

            await _ingredientRepository.DeleteAsync(Ing, cancellationToken);

            List<int> _list = new List<int>
            {
                Ing.NameId
            };

            await _mediator.Send(new DeleteTranslationCommand
            {
                Ids = _list
            });


            if (string.IsNullOrEmpty(Ing.ThumbUri))
            {
                _fileService.RemoveImage(Ing.ThumbUri, "Ingimg");
            }

            return Ok();
        }

        //[HttpDelete("DeleteReng")]
        //public async Task<ApiResult> DeleteReng(CancellationToken cancellationToken)
        //{

        //    DeleteFile DeleteFile = new DeleteFile(_environment);
        //    List<ExcelTable> exIds = new List<ExcelTable>(); //await _ExcelTable.TableNoTracking.ToListAsync(cancellationToken);
        //    foreach (var exId in exIds) { _ExcelTable.Detach(exId); }
        //    foreach (var item in exIds)
        //    {
        //        // Ingredient Ing = await ingeredientRepository.Table.Include(a => a.IngredientMeasureUnits).FirstAsync(a => a.Id == item.Id);
        //        var foods = await _foodIngredientRepository.Table.Where(f => f.IngredientId == item.Id).ToListAsync(cancellationToken);
        //        if (foods.Count() == 0)
        //        {
        //            Ingredient Ing = await ingeredientRepository.Table.Include(a => a.IngredientMeasureUnits).FirstOrDefaultAsync(a => a.Id == item.Id);

        //            if (Ing != null)
        //            {
        //                List<int> _list = new List<int>();
        //                _list.Add(Ing.NameId);

        //                await ingeredientRepository.DeleteAsync(Ing, cancellationToken);
        //                await _mediator.Send(new DeleteTranslationCommand
        //                {
        //                    Ids = _list
        //                });
        //                ingeredientRepository.Detach(Ing);
        //            }

        //        }
        //    }
        //    return Ok();
        //}


        ////[HttpGet]
        ////public async Task<PageResult<IEnumerable<SelectOption<int>>>> SearchAsync(int languageid, int? Page, int PageSize
        ////    , CancellationToken cancellationToken)
        ////{
        ////    //var listIng = repository.Table;

        ////    List<int> nameid = new List<int>();

        ////    List<Ingredient> Ing = await ingeredientRepository.Table.Include(a => a.IngredientMeasureUnits)
        ////                                   .ThenInclude(a => a.MeasureUnit)
        ////                                   .Skip((Page - 1 ?? 0) * PageSize)
        ////                                   .Take(PageSize)
        ////                                   .ToListAsync(cancellationToken);

        ////    var countDetails = Ing.Count();

        ////    //Get NameIds
        ////    foreach (var item in Ing)
        ////    {
        ////        var mes = item.IngredientMeasureUnits.Select(s => s.MeasureUnit.NameId);
        ////        foreach (var item1 in mes)
        ////        {
        ////            nameid.Add(item1);
        ////        }
        ////        nameid.Add(item.NameId);
        ////    }

        ////    //for get translation ingredient
        ////    var translations = await _mediator.Send(new GetTranslationQuery
        ////    {
        ////        Ids = nameid,
        ////        Language = LanguageName
        ////    });


        ////    List<IngredientSelectDTO> _paging = new List<IngredientSelectDTO>();

        ////    if (Ing.Count > 0)
        ////    {
        ////        foreach (var item in Ing)
        ////        {
        ////            IngredientSelectDTO invoicePaging = new IngredientSelectDTO()
        ////            {
        ////                Name = translations.Where(a => a.Value == item.NameId).Select(a => a.Text).First(),
        ////                Code = item.Code,
        ////                ThumbUri = "",
        ////                Id = item.Id,
        ////                MessureUnits = item.IngredientMeasureUnits.Select(s => new SelectOption<int>
        ////                {
        ////                    Value = s.MeasureUnitId,
        ////                    Text = translations.Find(n => n.Value == s.MeasureUnit.NameId).Text
        ////                }).ToList()
        ////            };

        ////            _paging.Add(invoicePaging);
        ////        }
        ////    }

        ////    var result = new PageResult<IngredientSelectDTO>
        ////    {
        ////        Count = countDetails,
        ////        PageIndex = Page ?? 1,
        ////        PageSize = PageSize,
        ////        Items = _paging
        ////    };

        ////    return result;
        ////}



        //[HttpPost("AddIngFromExcel")]
        //[Authorize(Roles = "Admin")]
        //public async Task<ApiResult<List<string>>> PostIngfromexcel(CancellationToken cancellationtoken)
        //{
        //    List<string> addedIngCodeList = new List<string>();
        //    var ings = await _ExcelTable.Table.ToListAsync(cancellationtoken); //new List<ExcelTable>(); //await _ExcelTable.Table.ToListAsync(cancellationtoken);
        //    foreach (var item in ings)
        //    {
        //        double[] nutrients = new double[34];

        //        nutrients[0] = item.V1;
        //        nutrients[1] = item.V2;
        //        nutrients[2] = item.V3;
        //        nutrients[3] = item.V4;
        //        nutrients[4] = item.V5;
        //        nutrients[5] = item.V6;
        //        nutrients[6] = item.V7;
        //        nutrients[7] = item.V8;
        //        nutrients[8] = item.V9;
        //        nutrients[9] = item.V10;
        //        nutrients[10] = item.V11;
        //        nutrients[11] = item.V12;
        //        nutrients[12] = item.V13;
        //        nutrients[13] = item.V14;
        //        nutrients[14] = item.V15;
        //        nutrients[15] = item.V16;
        //        nutrients[16] = item.V17;
        //        nutrients[17] = item.V18;
        //        nutrients[18] = item.V19;
        //        nutrients[19] = item.V20;
        //        nutrients[20] = item.V21;
        //        nutrients[21] = item.V22;
        //        nutrients[22] = item.V23;
        //        nutrients[23] = item.V24;
        //        nutrients[24] = item.V25;
        //        nutrients[25] = item.V26;
        //        nutrients[26] = item.V27;
        //        nutrients[27] = item.V28;
        //        nutrients[28] = item.V29;
        //        nutrients[29] = item.V30;
        //        nutrients[30] = item.V31;
        //        nutrients[31] = item.V32;
        //        nutrients[32] = item.V33;
        //        nutrients[33] = item.V34;

        //        var ingredient = new Ingredient();
        //        if (nutrients.Length == 34)
        //        {
        //            Translation _Name = new Translation()
        //            {
        //                Persian = item.PersianName,
        //                English = item.EnglishName,
        //                Arabic = item.ArabicName
        //            };
        //            var name = await _mediator.Send(new CreateTranslationCommand
        //            {
        //                Translation = _Name,
        //            });
        //            ingredient.NameId = name.Id;
        //            ingredient.IngredientCategory = IngredientCategory.LambAndVealAndGameProducts;
        //            ingredient.Code = item.Code.ToString();
        //            ingredient.IsFood = false;
        //            ingredient.Tag = null;
        //            ingredient.ThumbUri = "DefaultIngPic.png";
        //            ingredient.NutrientValue = StringConvertor.DoubleToString(nutrients.ToList());
        //            await ingeredientRepository.AddAsync(ingredient, cancellationtoken);
        //            addedIngCodeList.Add(item.Code.ToString() + "_Add");
        //        }
        //        else
        //        {
        //            addedIngCodeList.Add(item.Code.ToString() + "_notAdd");
        //        }


        //    }
        //    return addedIngCodeList;
        //}


        ///// <summary>
        ///// ویرایش نوتریشن مواد اولیه و غذاهای تک ماده ای از داده های اکسل
        ///// </summary>
        ///// <param name="cancellationtoken"></param>
        ///// <returns></returns>
        //[HttpPut("PutIngAndOneIngFood")]
        //[Authorize(Roles = "Admin")]
        //public async Task<ApiResult<List<string>>> putfromexcel(CancellationToken cancellationtoken)
        //{
        //    List<string> addedIngCodeList = new List<string>();
        //    var ings = await _ExcelTable.Table.ToListAsync(cancellationtoken); //new List<ExcelTable>(); //
        //    foreach (var item in ings)
        //    {
        //        double[] nutrients = new double[34];

        //        nutrients[0] = item.V1;
        //        nutrients[1] = item.V2;
        //        nutrients[2] = item.V3;
        //        nutrients[3] = item.V4;
        //        nutrients[4] = item.V5;
        //        nutrients[5] = item.V6;
        //        nutrients[6] = item.V7;
        //        nutrients[7] = item.V8;
        //        nutrients[8] = item.V9;
        //        nutrients[9] = item.V10;
        //        nutrients[10] = item.V11;
        //        nutrients[11] = item.V12;
        //        nutrients[12] = item.V13;
        //        nutrients[13] = item.V14;
        //        nutrients[14] = item.V15;
        //        nutrients[15] = item.V16;
        //        nutrients[16] = item.V17;
        //        nutrients[17] = item.V18;
        //        nutrients[18] = item.V19;
        //        nutrients[19] = item.V20;
        //        nutrients[20] = item.V21;
        //        nutrients[21] = item.V22;
        //        nutrients[22] = item.V23;
        //        nutrients[23] = item.V24;
        //        nutrients[24] = item.V25;
        //        nutrients[25] = item.V26;
        //        nutrients[26] = item.V27;
        //        nutrients[27] = item.V28;
        //        nutrients[28] = item.V29;
        //        nutrients[29] = item.V30;
        //        nutrients[30] = item.V31;
        //        nutrients[31] = item.V32;
        //        nutrients[32] = item.V33;
        //        nutrients[33] = item.V34;



        //        var ingredientlist = await ingeredientRepository.TableNoTracking.Where(f => f.Code == item.Code.ToString()).ToListAsync(cancellationtoken);

        //        foreach (var ingredient in ingredientlist)
        //        {
        //            //if (nutrients.Length == 34)
        //            //{

        //            var Name = new TranslationDto()
        //            {
        //                Arabic = item.ArabicName,
        //                Persian = item.PersianName,
        //                English = item.EnglishName,
        //                Id = ingredient.NameId
        //            };

        //            await _mediator.Send(new UpdateTranslationCommand
        //            {
        //                Translation = Name.ToEntity(_mapper)
        //            });
        //            //ingredient.NutrientValue = StringConvertor.DoubleToString(nutrients.ToList());
        //            //ingeredientRepository.Detach(ingredient);
        //            //await ingeredientRepository.UpdateAsync(ingredient, cancellationtoken);
        //            addedIngCodeList.Add(item.Code.ToString() + "_Add");
        //            //}
        //            //else
        //            //{
        //            //    addedIngCodeList.Add(item.Code.ToString() + "_notAdd");
        //            //}
        //        }

        //        //========================================================غذای تک ماده ای==========================
        //        //var foodList = await _foodRepository.TableNoTracking.Where(f => f.FoodCode == item.Code).ToListAsync(cancellationtoken);

        //        //foreach (var food in foodList)
        //        //{
        //        //    if (nutrients.Length == 34)
        //        //    {
        //        //        food.NutrientValue = StringConvertor.DoubleToString(nutrients.ToList());
        //        //        _foodRepository.Detach(food);
        //        //        await _foodRepository.UpdateAsync(food, cancellationtoken);
        //        //        addedIngCodeList.Add(item.Code.ToString() + "_Add");
        //        //    }
        //        //    else
        //        //    {
        //        //        addedIngCodeList.Add(item.Code.ToString() + "_notAdd");
        //        //    }
        //        //}

        //    }
        //    return addedIngCodeList;
        //}

        //[HttpPut("RoundIngNutrients")]
        //[Authorize(Roles = "Admin")]
        //public async Task<ApiResult> RoundIngNutrients(CancellationToken cancellationtoken)
        //{


        //    var ingredientlist = await ingeredientRepository.TableNoTracking.ToListAsync(cancellationtoken);

        //    foreach (var ingredient in ingredientlist)
        //    {

        //        List<double> NutrientValue = StringConvertor.ToNumber(ingredient.NutrientValue);
        //        ingredient.NutrientValue = StringConvertor.DoubleToString(NutrientValue);
        //        ingeredientRepository.Detach(ingredient);
        //        await ingeredientRepository.UpdateAsync(ingredient, cancellationtoken);

        //    }

        //    return Ok();
        //}


        //[HttpPut("TesEditTagArEnIng")]
        //public async Task<PageResult<FoodSelectAdminDTO>> TesEditTagArEnIng(string password, int FromId, int ToId, CancellationToken cancellationToken)
        //{
        //    int countDetails = 0;
        //    var IngList = await _ExcelTable.TableNoTracking.Where(f => f.Id >= FromId && f.Id <= ToId).ToListAsync(cancellationToken); //new List<ExcelTable>(); //;
        //    if (password == "alireza007")
        //    {
        //        List<Ingredient> Ings = new List<Ingredient>();
        //        foreach (var item in IngList)
        //        {
        //            Ingredient Ing = new Ingredient();
        //            Ing = ingeredientRepository.Table.Where(f => f.Code == item.Code.ToString()).FirstOrDefault();
        //            if (Ing != null)
        //            {
        //                Ing.TagArEn = item.ArabicName.ToLower();
        //                Ing.Tag = item.EnglishName.ToLower();
        //                ingeredientRepository.Detach(Ing);
        //                // Ings.Add(Ing);
        //                await ingeredientRepository.UpdateAsync(Ing, cancellationToken);
        //                countDetails++;
        //            }
        //        }

        //        PageResult<FoodSelectAdminDTO> result = new PageResult<FoodSelectAdminDTO>()
        //        {
        //            Count = countDetails,

        //            Items = null,
        //        };
        //        return result;
        //    }
        //    return null;
        //}

        //[HttpGet("GetIngsWithOutMeasureUnit")]
        //[Authorize(Roles = "Admin")]
        //public async Task<List<IdValue>> GetIngsWithOutMeasureUnitAsync(CancellationToken cancellationToken)
        //{
        //    //var listIng = repository.Table;

        //    List<IdValue> result = new List<IdValue>();

        //    List<Ingredient> Ings = await ingeredientRepository.Table.Include(a => a.Translation).Include(a => a.IngredientMeasureUnits)
        //                                   .ThenInclude(a => a.MeasureUnit).OrderByDescending(i => i.Id)
        //                                   .ToListAsync(cancellationToken);

        //    //Get NameIds
        //    foreach (var item in Ings)
        //    {
        //        var mes = item.IngredientMeasureUnits.Select(s => s.MeasureUnit.NameId);
        //        if (mes.Count() == 3)
        //        {
        //            var ing = new IdValue()
        //            {
        //                Id = Convert.ToInt32(item.Code),
        //                Value = item.Translation.Persian
        //            };
        //            result.Add(ing);
        //        }
        //    }
        //    return result;
        //}

        //[HttpDelete("ClearIngredientSearchCache")]
        //public async Task ClearIngredientSearchCache()
        //{
        //    await _redisCacheClient.Db3.FlushDbAsync();
        //}

    }
}
