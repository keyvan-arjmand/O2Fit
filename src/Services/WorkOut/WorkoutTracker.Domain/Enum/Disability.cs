using System;
using System.Collections.Generic;
using System.Text;

namespace WorkoutTracker.Domain.Enum
{
    public enum Disability
    {
        RightArm = 2,//بازو
        LeftArm = 4,
        RightForearm = 8,//ساعد
        LeftForearm = 16,
        RightWrist = 32,
        LeftWrist = 64,
        RightThigh = 128,//ران
        LeftThigh = 256,
        RightLeg = 512, //ساق
        LeftLeg = 1024,
        RightAnkle = 2048,//مچ پا
        LeftAnkle = 4096
    }
}
