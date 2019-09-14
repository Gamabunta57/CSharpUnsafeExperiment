using System;
using ECSUnsafeTest.utils;

namespace PhysicsBoilerPlate.Component
{
    public struct RectCollider
    {
        public Vector2 center;
        public Vector2 halfExtent;
        public Layer layer;
    }

    public enum Layer{
        Player,
        Ball,
        Wall,
        Goal1,
        Goal2
    }
}
