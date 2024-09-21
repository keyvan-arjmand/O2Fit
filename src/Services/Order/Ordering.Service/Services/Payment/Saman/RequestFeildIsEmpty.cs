using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.Services.Payment.Saman
{
    public static class RequestFeildIsEmpty
    {
        public static bool Check(out bool isError, out string errorMsg,
            string state, string refNum, string resNum)
        {
            isError = false;
            errorMsg = "";

            if (state.Equals(string.Empty))
            {
                isError = true;
                errorMsg = "خريد شما توسط بانک تاييد شده است اما رسيد ديجيتالي شما تاييد نگشت! مشکلي در فرايند رزرو خريد شما پيش آمده است";
                return true;
            }

            if (refNum.Equals(string.Empty) && state.Equals(string.Empty))
            {
                isError = true;
                errorMsg = "فرايند انتقال وجه با موفقيت انجام شده است اما فرايند تاييد رسيد ديجيتالي با خطا مواجه گشت";
                return true;
            }

            if (resNum.Equals(string.Empty) && state.Equals(string.Empty))
            {
                isError = true;
                errorMsg = "خطا در برقرار ارتباط با بانک";
                return true;
            }

            return false;
        }
    }

}
