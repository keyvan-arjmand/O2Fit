using FoodStuff.Domain.Entities.Food;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodStuff.Domain.Filter
{
    public static class FoodQueryParameters
    {
        public static IQueryable<Food> GetQueryable(IQueryable<Food> _query, string LanguageName, FoodInputParameters Parameter)
        {
            IQueryable<Food> _food = _query;

            var searchWord = Parameter.Name.Split(' ');

            if (!string.IsNullOrEmpty(Parameter.Name))
            {
                switch (LanguageName)
                {
                    case "Persian":
                        {
                            _food = _food.Where(a => a.Tag.Contains(Parameter.Name.ToLower()) || a.TranslationName.Persian.ToLower().Contains(Parameter.Name.ToLower()) ||
                            a.TranslationName.Persian.ToLower().StartsWith(Parameter.Name.ToLower()) || a.TranslationName.Persian.ToLower().EndsWith(Parameter.Name.ToLower())).OrderBy(f => (int)f.FoodType).ThenBy(a => a.TranslationName.Persian.Length);
                            break;
                        }
                    case "English":
                        {
                            _food = _food.Where(a =>a.TagArEn.ToLower().Contains(Parameter.Name.ToLower()) || a.TranslationName.English.ToLower().Contains(Parameter.Name.ToLower()) ||
                            a.TranslationName.English.ToLower().StartsWith(Parameter.Name.ToLower()) || a.TranslationName.English.ToLower().EndsWith(Parameter.Name.ToLower())).OrderBy(f => (int)f.FoodType).ThenBy(a => a.TranslationName.English.Length);
                            break;
                        }
                    case "Arabic":
                        {
                            _food = _food.Where(a =>a.TagArEn.ToLower().Contains(Parameter.Name.ToLower()) || a.TranslationName.Arabic.ToLower().Contains(Parameter.Name.ToLower()) ||
                            a.TranslationName.Arabic.ToLower().StartsWith(Parameter.Name.ToLower()) || a.TranslationName.Arabic.ToLower().EndsWith(Parameter.Name.ToLower())).OrderBy(f => (int)f.FoodType).ThenBy(a => a.TranslationName.Arabic.Length);
                            break;
                        }
                    default:
                        {
                            //_food = from fit in _food where
                            //        fit.TranslationName.English.ToLower().Contains(Parameter.Name.ToLower()) ||
                            //        fit.TranslationName.English.ToLower().Any(a => searchWord.Contains(a)) 
                            //        select fit;

                            //.Where(x => stringsToSearchFor.Any(s => DbFunctions.Like(x.name, $"%{s}%")))

                            //_food = _food.Where(a => a.TagArEn.ToLower().Contains(Parameter.Name.ToLower()) ||
                            //a.TranslationName.English.ToLower().Contains(Parameter.Name.ToLower()) ||
                            //a.TranslationName.English.ToLower().StartsWith(Parameter.Name.ToLower()) || a.TranslationName.English.ToLower().EndsWith(Parameter.Name.ToLower())).OrderBy(f => (int)f.FoodType).ThenBy(a => a.TranslationName.English.Length);

                           //_food = _food.Where(r => searchWord.Any(p => r.TranslationName.English.Contains(p)));


                            break;
                        }
                }
            }

            //if (!string.IsNullOrEmpty(Parameter.Barcode))
            //{
            //    if (Parameter.Barcode.Length < 14)
            //    {
            //        _food = _food.Where(a =>a.BarcodeGs1 == Parameter.Barcode);
            //    }
            //    else
            //    {
            //        _food = _food.Where(a => a.BarcodeNational == Parameter.Barcode);
            //    }
            //}

            //if (Parameter.FoodType != null)
            //{
            //    _food = _food.Where(a => a.FoodType == Parameter.FoodType);
            //}

            //if (_food.Count()>0)
            //{
            //    _food = _food.Where(f=>f.IsActive==true);
            //}    
            
            //if (Parameter.FoodCode != null)
            //{
            //    _food = _food.Where(a => a.FoodCode == Parameter.FoodCode);
            //}

            return _food;
        }
    }
}
