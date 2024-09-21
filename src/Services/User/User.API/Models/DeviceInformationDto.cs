using User.Domain.Entities.User;
using WebFramework.Api;

namespace User.API.Models
{
    public class DeviceInformationDto : BaseDto<DeviceInformationDto, DeviceInformation>
    {
        public int UserId { get; set; }
        public OsType OS { get; set; }
        public string Brand { get; set; }
        public string? PhoneModel { get; set; }
        public string? Brightness { get; set; }
        public string? IPAddress { get; set; }
        public string? FontScale { get; set; }
        public string? Display { get; set; }
        public string? ApiLevelSdk { get; set; }
        public string? AndroidId { get; set; }
        public bool? IsTablet { get; set; }
        public bool? IsEmulator { get; set; }
        public string? AppVersion { get; set; }
        public string? Market { get; set; }
    }
}
