﻿namespace Identity.V2.Application.DeviceInfo.V1.Commands.ChangeIsProfileCompleteToTrueByUserId;

public record ChangeIsProfileCompleteToTrueByUserIdCommand(string UserId): IRequest;