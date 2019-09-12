using System.Runtime.CompilerServices;
using ECSUnsafeTest.core;

namespace ECSUnsafeTest.Systems.Subsystems
{
    public static class MovableSubsystem
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ApplyMovement(ref Vector2 position, ref Vector2 heading)
        {
            position += heading;
        }
    }
}
