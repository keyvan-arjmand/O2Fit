namespace Identity.V2.Domain.Aggregates.UserAggregate;

public class DeviceInformation : BaseEntity
{
    public DeviceInformation()
    {
        
    }
    public DeviceInformation(OsType os, string? brand, string? phoneModel, string? brightness, string? ipAddress, string? fontScale,
        string? display, string? apiLevelSdk, string? androidId, bool? isTablet, bool? isEmulator, string? appVersion, string? market, DateTime createDate,
        bool isProfileComplete)
    {
        Os = os;
        Brand = brand;
        PhoneModel = phoneModel;
        Brightness = brightness;
        IpAddress = ipAddress;
        FontScale = fontScale;
        Display = display;
        ApiLevelSdk = apiLevelSdk;
        AndroidId = androidId;
        IsTablet = isTablet;
        IsEmulator = isEmulator;
        AppVersion = appVersion;
        Market = market;
        CreateDate = createDate;
        IsProfileComplete = isProfileComplete;
    }

    public OsType Os { get; set; }
    public string? Brand { get; set; }
    public string? PhoneModel { get; set; }
    public string? Brightness { get; set; }
    public string? IpAddress { get; set; }
    public string? FontScale { get; set; }
    public string? Display { get; set; }
    public string? ApiLevelSdk { get; set; }
    public string? AndroidId { get; set; }
    public bool? IsTablet { get; set; }
    public bool? IsEmulator { get; set; }
    public string? AppVersion { get; set; }
    public string? Market { get; set; }
    public DateTime CreateDate { get; set; }
    public bool IsProfileComplete { get; set; }

    
}