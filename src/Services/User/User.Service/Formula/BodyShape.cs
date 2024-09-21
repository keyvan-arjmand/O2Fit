using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.Enum;

namespace User.Service.Formula
{
   public static class BodyShape
    {
        //public static CalcBodyShapeResponseModel CalcBodyShape(CalcBodyShapeRequestModel model)
        //{
        //    var result = new CalcBodyShapeResponseModel();
        //    //model.Bust = ToInch(model.Bust);
        //    //model.Hips = ToInch(model.Hips);
        //    //model.Waist = ToInch(model.Waist);
        //    //model.HighHip = ToInch(model.HighHip);
        //    var imageUri = $"/system/bodyshape";

        //    switch (model.Gender)
        //    {
        //        case Gender.Female when model.Bust - model.Hips <= 1 * 2.54 && model.Hips - model.Bust < 3.6 * 2.54 && model.Bust - model.Waist >= 9 * 2.54 ||
        //                                model.Hips - model.Waist >= 10 * 2.54:
        //            result.FemaleBodyShape = FemaleBodyShape.Hourglass.ToString();
        //            result.UsefulFoods = "سبزیجات خام و میوه ها، غلات سبوس دار، گوشت گاو ، ماست کم چرب ، جلبک دریایی، غذاهای کامل";
        //            result.HarmfulFoods =
        //                "نوشیدنی های کافئین دار ، کربوهیدرات های پالایش شده، آب حاوی فلوراید و کلر ، انواع کلم ، لوبیا ، سویا ، بادام زمینی";
        //            result.SusceptibleToDiseases = "تیروئید";
        //            result.OverweightInParts = "ذخیره چربی در بازوها، قفسه سینه، زانو و مچ پا";
        //            result.SlimmingSpeed = "سریع ، بهترین سوخت و ساز، استخوانهای قوی";
        //            result.CommonTendencies = " لبنیات و کربوهیدرات های قندی";
        //            result.InappropriateDietOutcome = " احتباس آب در بدن";
        //            result.ImageUri = $"{imageUri}/female/Hour_Glass_W.svg";
        //            break;
        //        case Gender.Female when model.Hips - model.Bust >= 3.6 * 2.54 && model.Hips - model.Bust < 10 * 2.54 && model.Hips - model.Waist >= 9 * 2.54 &&
        //                                model.HighHip / model.Waist < 1.193 * 2.54:
        //            result.FemaleBodyShape = FemaleBodyShape.BottomHourglass.ToString();
        //            result.UsefulFoods = "سبزیجات خام و میوه ها، غلات سبوس دار، گوشت گاو ، ماست کم چرب ، جلبک دریایی، غذاهای کامل";
        //            result.HarmfulFoods =
        //                "کربوهیدرات های نشاسته دار، میوه هایی با قند بالا، نوشیدنی های کافئین دار، غذاهای پر چرب و آماده";
        //            result.SusceptibleToDiseases = "دیابت، سکته مغزی، IBS و مشکلات گوارشی ، یبوست و احتباس آب در بدن";
        //            result.OverweightInParts = "ذخیره چربی در اطراف شکم و سینه";
        //            result.SlimmingSpeed = "متوسط ( توصیه می شود سطوح قند خون خود را کنترل کنند )";
        //            result.CommonTendencies = "غذای نشاسته ای یا قندی، نوشابه های رژیمی و کافئین";
        //            result.InappropriateDietOutcome = "کمبود انرژی و احساس به تمایلات معمول در هنگام صبح و بعد از ناهار";
        //            result.ImageUri = $"{imageUri}/female/W_Apple.svg";
        //            break;
        //        case Gender.Female when model.Bust - model.Hips > 1 * 2.54 && model.Bust - model.Hips < 10 * 2.54 && model.Bust - model.Waist >= 9 * 2.54:
        //            result.FemaleBodyShape = FemaleBodyShape.TopHourglass.ToString();
        //            result.UsefulFoods = "کربوهیدرات های پیچیده، لبنیات کم چرب، سبزیجات تازه و میوه";
        //            result.HarmfulFoods =
        //                "روغن نارگیل، پنیر، کره، روغن کانولا، روغن گیاهی و روغن های دانه ها، گوشت سنگین، اسنک های نمکی، نوشیدنی های کافئین دار";
        //            result.SusceptibleToDiseases = "قلبی و التهاب مفاصل";
        //            result.OverweightInParts = "بیشتر ذخیره چربی در بازوها و قفسه سینه وکمتر در ناحیه ران و مچ پا";
        //            result.SlimmingSpeed = "متوسط";
        //            result.CommonTendencies = "غذاهای نمکی، چربی دار و آماده";
        //            result.InappropriateDietOutcome = "آغاز روز با انرژی خوب، در غروب بدون انرژی و رو آوردن به تمایلات معمول";
        //            result.ImageUri = $"{imageUri}/female/Top_Hour_Glass_W.svg";
        //            break;
        //        case Gender.Female when model.Hips - model.Bust > 2 * 2.54 && model.Hips - model.Waist >= 7 * 2.54 && model.HighHip / model.Waist >= 1.193 * 2.54:
        //            result.FemaleBodyShape = FemaleBodyShape.Spoon.ToString();
        //            result.UsefulFoods =
        //                "سبزیجات و میوه های غنی از فیبر، مقادیر کمی از پروتئین حیوانی،  چربی های غیر لبنیاتی مانند روغن نارگیل، دانه کتان ، جوانه حبوبات و تخم مرغ";
        //            result.HarmfulFoods = "پنیرهای پرچرب، خامه،  سس ها، کافئین و الکل ، گوشت فراوری شده";
        //            result.SusceptibleToDiseases =
        //                "بیماری های مرتبط با سطوح استروژن مانند پروژسترون ها، التهاب مفاصل خصوصأ ران ها بصورت سلولیت، احتباس آب، نفخ";
        //            result.OverweightInParts = "ذخیره چربی در ران و باسن";
        //            result.SlimmingSpeed = "کند";
        //            result.CommonTendencies = "لبنیات پرچرب، دسر های پرکالری، کافه لاته";
        //            result.InappropriateDietOutcome = "تجربه گرسنگی غیر معمولی بین وعده ها یا آخر شب بعد از شام";
        //            result.ImageUri = $"{imageUri}/female/Triangle_Spoon_W.svg";
        //            break;
        //        case Gender.Female when model.Hips - model.Bust >= 3.6 * 2.54 && model.Hips - model.Waist < 9 * 2.54:
        //            result.FemaleBodyShape = FemaleBodyShape.Triangle.ToString();
        //            result.UsefulFoods =
        //                "سبزیجات و میوه های غنی از فیبر، مقادیر کمی از پروتئین حیوانی،  چربی های غیر لبنیاتی مانند روغن نارگیل، دانه کتان ، جوانه حبوبات و تخم مرغ";
        //            result.HarmfulFoods = "پنیرهای پرچرب، خامه،  سس ها، کافئین و الکل ، گوشت های فراوری شده";
        //            result.SusceptibleToDiseases =
        //                "بیماری های مرتبط با سطوح استروژن مانند پروژسترون ها، التهاب مفاصل خصوصأ ران ها بصورت سلولیت، احتباس آب، نفخ";
        //            result.OverweightInParts = "ذخیره چربی در ران و باسن و مچ پا";
        //            result.SlimmingSpeed = "کند";
        //            result.CommonTendencies = "لبنیات پرچرب، دسر های پرکالری، کافه لاته";
        //            result.InappropriateDietOutcome = "تجربه گرسنگی غیر معمولی بین وعده ها یا آخر شب بعد از شام";
        //            result.ImageUri = $"{imageUri}/female/Trangle_W.svg";
        //            break;
        //        case Gender.Female when model.Hips - model.Bust >= 3.6 * 2.54 && model.Hips - model.Waist < 9 * 2.54:
        //            result.FemaleBodyShape = FemaleBodyShape.InvertedTriangle.ToString();
        //            result.UsefulFoods = "کربوهیدرات های پیچیده، لبنیات کم چرب، سبزیجات تازه و میوه";
        //            result.HarmfulFoods =
        //                "روغن نارگیل، پنیر، کره، روغن کانولا، روغن گیاهی و روغن های دانه ها، گوشت سنگین، اسنک های نمکی، نوشیدنی های کافئین دار";
        //            result.SusceptibleToDiseases = "قلبی";
        //            result.OverweightInParts = "ذخیره چربی در بازوها، قفسه سینه";
        //            result.SlimmingSpeed = "متوسط";
        //            result.CommonTendencies = "غذاهای نمکی و چربی دار و آماده";
        //            result.InappropriateDietOutcome = " آغاز روز با انرژی خوب، در غروب بدون انرژی و رو آوردن به تمایلات معمول";
        //            result.ImageUri = $"{imageUri}/female/Inverted_Triangle_W.svg";
        //            break;
        //        case Gender.Female when model.Hips - model.Bust < 3.6 * 2.54 && model.Bust - model.Hips < 3.6 * 2.54 && model.Bust - model.Waist < 9 * 2.54 &&
        //                                model.Hips - model.Waist < 10 * 2.54:
        //            result.FemaleBodyShape = FemaleBodyShape.Rectangle.ToString();
        //            result.UsefulFoods = "غذاهای با کربوهیدرات، چربی ها، پروتئین و فیبر متعادل";
        //            result.HarmfulFoods = "پودر پروتئین، شیک ، تکه های بزرگ گوشت";
        //            result.SusceptibleToDiseases = "مرتبط با کمبود ویتامینها";
        //            result.OverweightInParts = "پراکنده";
        //            result.SlimmingSpeed = "سریع";
        //            result.CommonTendencies = "خوردن غذا پس از سیری";
        //            result.InappropriateDietOutcome = "انواع بیماری ها";
        //            result.ImageUri = $"{imageUri}/female/Rectangle_W.svg";
        //            break;
        //        case Gender.Male when model.Waist / model.Hips >= 1.05 * 2.54:
        //            result.MaleBodyShape = MaleBodyShape.InvertedTriangle.ToString();
        //            result.UsefulFoods = "کربوهیدرات های پیچیده، لبنیات کم چرب، سبزیجات تازه و میوه";
        //            result.HarmfulFoods =
        //                " روغن نارگیل، پنیر، کره، روغن کانولا، روغن گیاهی و روغن های دانه ها، گوشت سنگین، اسنک های نمکی، نوشیدنی های کافئین دار";
        //            result.SusceptibleToDiseases = "قلبی";
        //            result.OverweightInParts = "ذخیره چربی در بازوها، قفسه سینه";
        //            result.SlimmingSpeed = "متوسط";
        //            result.CommonTendencies = "غذاهای نمکی و چربی دار و آماده";
        //            result.InappropriateDietOutcome = "آغاز روز با انرژی خوب، در غروب بدون انرژی و رو آوردن به تمایلات";
        //            result.ImageUri = $"{imageUri}/male/Inverted_Triangle_M.svg";
        //            break;
        //        case Gender.Male when model.Hips - model.Bust >= 3.6 * 2.54 && model.Hips - model.Waist < 9 * 2.54:
        //            result.MaleBodyShape = MaleBodyShape.Triangle.ToString();
        //            result.UsefulFoods =
        //              "سبزیجات و میوه های غنی از فیبر، مقادیر کمی از پروتئین حیوانی،  چربی های غیر لبنیاتی مانند روغن نارگیل، دانه کتان ، جوانه حبوبات و تخم مرغ";
        //            result.HarmfulFoods = "پنیرهای پرچرب، خامه،  سس ها، کافئین و الکل ، گوشت فراوری شده";
        //            result.SusceptibleToDiseases = "التهاب مفاصل خصوصأ ران ها ، احتباس آب، نفخ";
        //            result.OverweightInParts = "ذخیره چربی در ران و باسن و مچ پا";
        //            result.SlimmingSpeed = "کند";
        //            result.CommonTendencies = "لبنیات پرچرب، دسر های پرکالری، کافه لاته";
        //            result.InappropriateDietOutcome = "تجربه گرسنگی غیر معمولی بین وعده ها یا آخر شب بعد از شام";
        //            result.ImageUri = $"{imageUri}/male/Triangle_M.svg";
        //            break;
        //        case Gender.Male when model.Hips - model.Bust < 3.6 * 2.54 && model.Bust - model.Hips < 3.6 * 2.54 && model.Bust - model.Waist < 9 * 2.54 &&
        //                                model.Hips - model.Waist < 10 * 2.54:
        //            result.MaleBodyShape = MaleBodyShape.Rectangle.ToString();
        //            result.UsefulFoods = "غذاهای با کربوهیدرات، چربی ها، پروتئین و فیبر متعادل";
        //            result.HarmfulFoods = "پودر پروتئین، انواع شیک ، تکه های بزرگ گوشت";
        //            result.SusceptibleToDiseases = "مرتبط با کمبود ویتامینها";
        //            result.OverweightInParts = "پراکنده";
        //            result.SlimmingSpeed = "سریع";
        //            result.CommonTendencies = "خوردن غذا پس از سیری";
        //            result.InappropriateDietOutcome = "انواع بیماری ها";
        //            result.ImageUri = $"{imageUri}/male/Rectangle_M.svg";
        //            break;
        //            //default:
        //            //    throw new InvalidArgumentException(MaleBodyShape.NoBodyShape.ToString());

        //            //    //throw new InvalidArgumentException("اطلاعات اندام به درستی وارد نشده");
        //    }
        //    if (result.MaleBodyShape == null && result.FemaleBodyShape == null)
        //    {
        //        result.FemaleBodyShape = MaleBodyShape.NoBodyShape.ToString();
        //        result.MaleBodyShape = MaleBodyShape.NoBodyShape.ToString();
        //        return result;
        //    }
        //    return result;
        //}

    }
}
