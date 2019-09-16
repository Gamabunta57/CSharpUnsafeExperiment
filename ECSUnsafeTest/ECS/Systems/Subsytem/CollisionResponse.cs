using ECSUnsafeTest.Global;
using ECSUnsafeTest.utils;
using System;

namespace ECSUnsafeTest.ECS.Systems.Subsytem
{
    public static class CollisionResponse
    {
        public static void OnPlayerAndBallCollide(ICollidable player, ICollidable ball, Vector2 penetration)
        {
            var movableBall = (IMovable)ball;
            Console.WriteLine($"Ball before pos: {movableBall.Position.Value}, heading: {movableBall.Heading.Value}");
            
            if (Math.Abs(penetration.X) <= Math.Abs(penetration.Y))
            {
                movableBall.Position.Value.X += player.Position.Value.X < movableBall.Position.Value.X ? penetration.X : -penetration.X;
                movableBall.Heading.Value.X *= -1;
            }
            else
            {
                movableBall.Position.Value.Y += player.Position.Value.Y < movableBall.Position.Value.Y ? penetration.Y : -penetration.Y;
                movableBall.Heading.Value.Y *= -1;
            }
            Console.WriteLine($"Ball after  pos: {movableBall.Position.Value}, heading: {movableBall.Heading.Value}");
        }

        public static void OnBallReachGoalPlayer1(ICollidable ball, ICollidable goal, Vector2 penetration)
        {
            Console.WriteLine("Player 1 win the match!!");
            GameState.Player1Scored = true;
        }
        public static void OnBallReachGoalPlayer2(ICollidable ball, ICollidable goal, Vector2 penetration)
        {
            Console.WriteLine("Player 2 win the match!!");
            GameState.Player2Scored = true;
        }

        public static void OnBallTouchWall(ICollidable ball, ICollidable wall, Vector2 penetration)
        {
            var movableBall = ((IMovable)ball);
            Console.WriteLine($"Ball before pos: {movableBall.Position.Value}, heading: {movableBall.Heading.Value}");

            movableBall.Position.Value.Y += movableBall.Position.Value.Y < wall.Position.Value.Y ? -penetration.Y : penetration.Y;
            movableBall.Heading.Value.Y *= -1;

            Console.WriteLine($"Ball after  pos: {movableBall.Position.Value}, heading: {movableBall.Heading.Value}");
        }

        public static void OnPlayerHitsTheWall(ICollidable player, ICollidable wall, Vector2 penetration) =>
            player.Position.Value.Y += (penetration.Y < 0) ? -penetration.Y : penetration.Y;
    }
}
