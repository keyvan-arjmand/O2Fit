using Track.Domain.Enums;

namespace Track.Application.Common.Utilities;

public static class Disabilities
{
    /// <summary>
    /// نقص عضو ها
    /// </summary>
    public static IEnumerable<(Domain.Enums.Disability Disability, double Percent)> DisabilityPercents
    {
        get
        {
            yield return (Disability.LeftAnkle, 1.7);
            yield return (Disability.LeftArm, 5);
            yield return (Disability.LeftForearm, 3.2);
            yield return (Disability.LeftLeg, 8);
            yield return (Disability.LeftThigh, 16);
            yield return (Disability.LeftWrist, 0.7);

            yield return (Disability.RightAnkle, 1.7);
            yield return (Disability.RightArm, 5);
            yield return (Disability.RightForearm, 3.2);
            yield return (Disability.RightLeg, 8);
            yield return (Disability.RightThigh, 16);
            yield return (Disability.RightWrist, 0.7);
        }
    }

    /// <summary>
    /// محاسبه وزن کاربر قبل از نقص عضو
    /// </summary>
    /// <param name="weight"></param>
    /// <param name="disabilities"></param>
    /// <returns></returns>
    public static double CalcWeightBeforeDisability(double weight, IEnumerable<Disability> disabilities) =>
        100 * weight / (100 - DisabilityPercents.Where(x => disabilities.Contains(x.Disability)).Sum(x => x.Percent));
}