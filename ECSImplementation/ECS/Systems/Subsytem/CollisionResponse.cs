using ECSImplementation.Global;
using Microsoft.Xna.Framework;
using System;

namespace ECSImplementation.ECS.Systems.Subsytem
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

        public static void OnBallTouchWallFull(ICollidable ball, ICollidable wall, Vector2 penetration)
        {
            var movableBall = (IMovable)ball;

            if (Math.Abs(penetration.X) <= Math.Abs(penetration.Y))
            {
                var isBallOnLeft = movableBall.Position.Value.X < wall.Position.Value.X;
                var direction = Math.Abs(movableBall.Heading.Value.X);

                movableBall.Position.Value.X += isBallOnLeft ? -penetration.X : penetration.X;
                movableBall.Heading.Value.X = isBallOnLeft && movableBall.Heading.Value.X > 0 ? -direction : direction;
            }
            else
            {
                var isBallOnTop = movableBall.Position.Value.Y < wall.Position.Value.Y;
                var direction = Math.Abs(movableBall.Heading.Value.Y);

                movableBall.Position.Value.Y += isBallOnTop ? -penetration.Y : penetration.Y;
                movableBall.Heading.Value.Y = isBallOnTop && movableBall.Heading.Value.Y > 0 ? -direction : direction;
            }
        }

        public static void OnBallsCollide(ICollidable ballA, ICollidable ballB, Vector2 penetration)
        {
            var movableBallA = (IMovable)ballA;
            var movableBallB = (IMovable)ballB;

            if (Math.Abs(penetration.X) <= Math.Abs(penetration.Y))
            {
                var isBallAOnLeft = movableBallA.Position.Value.X < movableBallB.Position.Value.X;
                var directionA = Math.Abs(movableBallA.Heading.Value.X);
                var directionB = Math.Abs(movableBallB.Heading.Value.X);

                movableBallA.Position.Value.X += (isBallAOnLeft ? -penetration.X : penetration.X) * .5f;
                movableBallB.Position.Value.X += (isBallAOnLeft ? penetration.X :-penetration.X) *.5f;

                movableBallA.Heading.Value.X = isBallAOnLeft && movableBallA.Heading.Value.X > 0 ? -directionA : directionA;
                movableBallB.Heading.Value.X = isBallAOnLeft && movableBallB.Heading.Value.X > 0 ? directionB : -directionB;
            }
            else
            {
                var isBallAOnTop = movableBallA.Position.Value.Y < movableBallB.Position.Value.Y;
                var directionA = Math.Abs(movableBallA.Heading.Value.X);
                var directionB = Math.Abs(movableBallB.Heading.Value.X);

                movableBallA.Position.Value.Y += (isBallAOnTop ? -penetration.Y : penetration.Y) * .5f;
                movableBallB.Position.Value.Y += (isBallAOnTop ? penetration.Y : -penetration.Y) * .5f;

                movableBallA.Heading.Value.Y = isBallAOnTop && movableBallA.Heading.Value.Y > 0 ? -directionA : directionA;
                movableBallB.Heading.Value.Y = isBallAOnTop && movableBallB.Heading.Value.Y > 0 ? directionB : -directionB;
            }
        }

        public static void OnPlayerHitsTheWall(ICollidable player, ICollidable wall, Vector2 penetration) =>
            player.Position.Value.Y += player.Position.Value.Y < wall.Position.Value.Y ? -penetration.Y : penetration.Y;
    }
}
