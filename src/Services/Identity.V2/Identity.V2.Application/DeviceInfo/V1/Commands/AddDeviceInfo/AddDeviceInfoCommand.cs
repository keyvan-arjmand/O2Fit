namespace Identity.V2.Application.DeviceInfo.V1.Commands.AddDeviceInfo;

public record AddDeviceInfoCommand(string UserId , int Os, string? Brand, string? PhoneModel, string? Brightness, string IpAddress, string? FontScale, string? Display, string? ApiLevelSdk,
                                    string? AndroidId, bool? IsTablet, bool? IsEmulator, string? AppVersion, string? Market): IRequest;