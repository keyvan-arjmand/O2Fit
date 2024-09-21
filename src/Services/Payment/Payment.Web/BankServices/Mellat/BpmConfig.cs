namespace Payment.Web.BankServices.Mellat;

public static class BpmConfig
{
    public static string TerminalId => "5231565";
    public static string UserName => "Oxygen1398";
    public static string Password => "33321922";
    public static string PostUrl => "https://bpm.shaparak.ir/pgwchannel/startpay.mellat";
    public static string RedirectUrl => "https://banktest.o2fitt.com/Bank/MellatBack";
}