using ECSFoundation.ECS.Entities;
using ECSFoundation.ECS.Systems;
using ECSUnsafeTest.ECS.Component;
using System;
using System.Collections.Generic;

namespace ECSUnsafeTest.ECS.Systems
{

    public interface ICollidable : IEntity
    {
        ref Position Position { get; }
        ref RectCollider Collider { get; }
    }

    class PhysicSystem : ISystem
    {

        public PhysicSystem() => EntityManager.OnNewEntityCreated += OnNewEntityCreated;

        public void Update()
        {
            for (var i = 0; i < entityList.Count - 1; i++)
            {
                var a = entityList[i];
                for (var j = i + 1; j < entityList.Count; j++)
                {
                    var b = entityList[j];
                    var fullExtent = a.Collider.halfExtent + b.Collider.halfExtent;
                    var centerA = a.Position.Value + a.Collider.Center;
                    var centerB = b.Position.Value + b.Collider.Center;

                    var isColliding = centerB.X < centerA.X + fullExtent.X
                        && centerB.X > centerA.X - fullExtent.X
                        && centerB.Y < centerA.Y + fullExtent.Y
                        && centerB.Y > centerA.Y - fullExtent.Y;

                    if (!isColliding) continue;

                    Console.WriteLine($"IsColliding #{a.BaseEntity.Id} | #{b.BaseEntity.Id}");

                    if (a.Collider.type == ColliderType.Player
                        && b.Collider.type == ColliderType.Ball)
                        OnPlayerAndBallCollide(a, b);
                    else if (a.Collider.type == ColliderType.Ball
                        && b.Collider.type == ColliderType.Player)
                        OnPlayerAndBallCollide(b, a);

                    else if (a.Collider.type == ColliderType.Ball
                        && b.Collider.type == ColliderType.Goal)
                        OnBallReachGoal(a, b);
                    else if (a.Collider.type == ColliderType.Goal
                        && b.Collider.type == ColliderType.Ball)
                        OnBallReachGoal(b, a);

                    else if (a.Collider.type == ColliderType.Ball
                        && b.Collider.type == ColliderType.Wall)
                        OnBallTouchWall(a, b);
                    else if (a.Collider.type == ColliderType.Wall
                        && b.Collider.type == ColliderType.Ball)
                        OnBallTouchWall(b, a);

                    if (a.Collider.type == ColliderType.Player
                        && b.Collider.type == ColliderType.Wall)
                        OnPlayerHitsTheWall(a, b);
                    else if (a.Collider.type == ColliderType.Wall
                        && b.Collider.type == ColliderType.Player)
                        OnPlayerHitsTheWall(b, a);
                }
            }
        }

        void OnNewEntityCreated(IEntity entity)
        {
            if (entity is ICollidable o)
                entityList.Add(o);
        }

        void OnPlayerAndBallCollide(ICollidable player, ICollidable ball)
        {
            Console.WriteLine($"Ball before pos: {ball.Position.Value}, heading: {((IMovable)ball).Heading.Value}");
            var penetration = (player.Collider.halfExtent + ball.Collider.halfExtent) * 2 - (ball.Position.Value + player.Position.Value); //TOTALLY WRONG => need sleep 
            if (Math.Abs(penetration.X) <= Math.Abs(penetration.Y))
            {
                if (ball.Collider.Center.X > player.Collider.Center.X)
                    ball.Position.Value.X += Math.Abs(penetration.X);
                else
                    ball.Position.Value.X -= Math.Abs(penetration.X);
                ((IMovable)ball).Heading.Value.X *= -1;
            }
            else
            {
                if (ball.Collider.Center.Y > player.Collider.Center.Y)
                    ball.Position.Value.Y += Math.Abs(penetration.Y);
                else
                    ball.Position.Value.Y -= Math.Abs(penetration.Y);
                ((IMovable)ball).Heading.Value.Y *= -1;
            }
            Console.WriteLine($"Ball after  pos: {ball.Position.Value}, heading: {((IMovable)ball).Heading.Value}");
        }

        void OnBallReachGoal(ICollidable ball, ICollidable goal)
        {
            Console.WriteLine("Ball reach the goal!!");
        }

        void OnBallTouchWall(ICollidable ball, ICollidable wall)
        {
            var penetrationY = (ball.Collider.halfExtent.Y + wall.Collider.halfExtent.Y) * 2 - (wall.Collider.Center.Y + ball.Collider.Center.Y);
            if (penetrationY < 0)
            {
                ball.Position.Value.Y -= penetrationY;
            }
            else
            {
                ball.Position.Value.Y += penetrationY;
            }
            ((IMovable)ball).Heading.Value.Y *= -1;
        }

        void OnPlayerHitsTheWall(ICollidable player, ICollidable wall)
        {
            var penetrationY = (player.Collider.halfExtent.Y + wall.Collider.halfExtent.Y) * 2 - (wall.Collider.Center.Y + player.Collider.Center.Y);
            if (penetrationY < 0)
            {
                player.Position.Value.Y -= penetrationY;
            }
            else
            {
                player.Position.Value.Y += penetrationY;
            }
        }

        readonly IList<ICollidable> entityList = new List<ICollidable>();
    }
}
