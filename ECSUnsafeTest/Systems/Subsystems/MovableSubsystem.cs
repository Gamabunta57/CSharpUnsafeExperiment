using System.Runtime.CompilerServices;
using ECSUnsafeTest.core;

namespace ECSUnsafeTest.Systems.Subsystems
{
    public static class MovableSubsystem
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ApplyMovement(Vector2 position, Vector2 heading) => position + heading;
    }
}
