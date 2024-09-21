using User.Domain.Entities.User;
using WebFramework.Api;

namespace User.API.Models
{
    public class DeviceInformationViewModel : BaseDto<DeviceInformationViewModel, DeviceInformation>
    {
        public int OS { get; set; }
        public string Brand { get; set; }
        public string? PhoneModel { get; set; }
        public string? Market { get; set; }
        public string? AppVersion { get; set; }
    }

    public class DeviceInfoViewModel
    {
        public int OS { get; set; }
        public string? Market { get; set; }
    }
}
