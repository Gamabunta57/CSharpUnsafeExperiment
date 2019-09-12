using System.Runtime.CompilerServices;
using ECSUnsafeTest.core;

namespace ECSUnsafeTest.Systems.Subsystems
{
    public static class CollisionDetectionSubsystem
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsColliding(ref RectCollider collider1, ref RectCollider collider2)
        {
            var bothExtent = collider1.halfExtents + collider2.halfExtents;
            ref var pos2 = ref collider2.center;

            return pos2.X < (collider1.center.X + bothExtent.X)
                && pos2.X > (collider1.center.X - bothExtent.X)
                && pos2.Y < (collider1.center.Y + bothExtent.Y)
                && pos2.Y > (collider1.center.Y - bothExtent.Y)
            ;
        }
    }
}
