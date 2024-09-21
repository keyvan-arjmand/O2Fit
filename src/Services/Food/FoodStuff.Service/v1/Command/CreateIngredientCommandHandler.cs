using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Common.Utilities;
using FoodStuff.Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using FoodStuff.Service.Contracts;
using MediatR;
using Service.v1.Command;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStuff.Service.v1.Command
{
    public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, Unit>, ITransientDependency
    {
        private readonly IIngeredientRepository _ingredientRepository;
        private readonly IRepository<IngredientMeasureUnit> _ingredientMeasureUnitRepository;
        private readonly IFileService _fileService;
        private readonly IMediator _mediator;

        public CreateIngredientCommandHandler(IIngeredientRepository ingredientRepository,
            IRepository<IngredientMeasureUnit> ingredientMeasureUnitRepository,
            IFileService fileService,
            IMediator mediator)
        {
            _ingredientRepository = ingredientRepository;
            _ingredientMeasureUnitRepository = ingredientMeasureUnitRepository;
            _fileService = fileService;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {

            var isExist = _ingredientRepository.TableNoTracking.Any(i => i.Code == request.Code);
            if (isExist)
                throw new AppException(ApiResultStatusCode.BadRequest, "Food Code Is Duplicate In Ingredient");

            if (request.NutrientValue.Count != 34)
                throw new AppException("Number Of NutrientValue Is Wrong  In Ingredient");

            var name = await _mediator.Send(new CreateTranslationCommand
            {
                Translation = new Domain.Entities.Translation.Translation
                {
                    Arabic = request.TranslationDto.Arabic,
                    English = request.TranslationDto.English,
                    Persian = request.TranslationDto.Persian,
                }
            });


            string imageName = null;
            if (!string.IsNullOrEmpty(request.ThumbUri))
            {
                //var IngImgFolder = Path.Combine("wwwroot", "Ingimg");
                //var IngImgPath = Path.Combine(Directory.GetCurrentDirectory(), IngImgFolder);

                //using (var fileStream = new FileStream(IngImgPath, FileMode.Create))
                //{
                //    await request.ThumbUri.CopyToAsync(fileStream);
                //}
                imageName = _fileService.AddImage(request.ThumbUri, "Ingimg", request.Code);
            }

            var ingredient = new Ingredient
            {
                NameId = name.Id,
                NutrientValue = StringConvertor.DoubleToString(request.NutrientValue),
                Tag = request.Tag,
                TagArEn = "",
                Code = request.Code,
                ThumbUri = imageName,
                TagEn = request.TagEn,
                TagAr = request.TagAr,
                DefaultMeasureUnitId = request.DefaultMeasureUnitId,
                IsFood = false
            };

            ingredient.Id = await _ingredientRepository.AddAsync(ingredient, cancellationToken);


            List<IngredientMeasureUnit> IngMeaslist = new List<IngredientMeasureUnit>();
            request.MeasureUnitIds.Add(36);
            request.MeasureUnitIds.Add(37);
            request.MeasureUnitIds.Add(58);
            request.MeasureUnitIds = request.MeasureUnitIds.Distinct().ToList();



            foreach (var item in request.MeasureUnitIds)
            {
                IngredientMeasureUnit ingredientMeasureUnit = new IngredientMeasureUnit();
                ingredientMeasureUnit.IngredientId = ingredient.Id;
                ingredientMeasureUnit.MeasureUnitId = item;
                IngMeaslist.Add(ingredientMeasureUnit);
            }
            await _ingredientMeasureUnitRepository.AddRangeAsync(IngMeaslist, cancellationToken);

            return Unit.Value;
        }
    }
}
