namespace Payment.Web.BankServices.Saman
{
    public static class TransactionChecking
    {
        public static string Check(out bool isError, int i)
        {
            isError = false;
            string errorMsg = "";

            switch (i)
            {

                case -1:		//TP_ERROR
                    isError = true;
                    errorMsg = "بروز خطا درهنگام بررسي صحت رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-1";
                    break;
                case -2:		//ACCOUNTS_DONT_MATCH
                    isError = true;
                    errorMsg = "بروز خطا در هنگام تاييد رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-2";
                    break;
                case -3:		//BAD_INPUT
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-3";
                    break;
                case -4:		//INVALID_PASSWORD_OR_ACCOUNT
                    isError = true;
                    errorMsg = "خطاي دروني سيستم درهنگام بررسي صحت رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-4";
                    break;
                case -5:		//DATABASE_EXCEPTION
                    isError = true;
                    errorMsg = "خطاي دروني سيستم درهنگام بررسي صحت رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-5";
                    break;
                case -7:		//ERROR_STR_NULL
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-7";
                    break;
                case -8:		//ERROR_STR_TOO_LONG
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-8";
                    break;
                case -9:		//ERROR_STR_NOT_AL_NUM
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-9";
                    break;
                case -10:	//ERROR_STR_NOT_BASE64
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-10";
                    break;
                case -11:	//ERROR_STR_TOO_SHORT
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-11";
                    break;
                case -12:		//ERROR_STR_NULL
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-12";
                    break;
                case -13:		//ERROR IN AMOUNT OF REVERS TRANSACTION AMOUNT
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-13";
                    break;
                case -14:	//INVALID TRANSACTION
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-14";
                    break;
                case -15:	//RETERNED AMOUNT IS WRONG
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-15";
                    break;
                case -16:	//INTERNAL ERROR
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-16";
                    break;
                case -17:	// REVERS TRANSACTIN FROM OTHER BANK
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-17";
                    break;
                case -18:	//INVALID IP
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد" + "-18";
                    break;
            }

            return errorMsg;
        }
    }
}
