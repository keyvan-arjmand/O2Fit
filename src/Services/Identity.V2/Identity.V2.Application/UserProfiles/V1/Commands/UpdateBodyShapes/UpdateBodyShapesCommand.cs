namespace Identity.V2.Application.UserProfiles.V1.Commands.UpdateBodyShapes;

public record UpdateBodyShapesCommand(string UserId, double TargetChest, double TargetArm, double TargetWaist, double TargetHighHip, double TargetHip, double TargetShoulder,
                                    double TargetWrist, double TargetThighSize, double TargetNeckSize, int HeightSize): IRequest;