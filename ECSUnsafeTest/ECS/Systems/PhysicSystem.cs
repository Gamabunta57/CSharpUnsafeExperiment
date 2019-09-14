using ECSFoundation.ECS.Entities;
using ECSFoundation.ECS.Systems;
using ECSUnsafeTest.ECS.Component;
using ECSUnsafeTest.Global;
using ECSUnsafeTest.utils;
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
                    ref var centerA = ref a.Collider.Center;
                    var centerB = b.Position.Value + b.Collider.Center - a.Position.Value;

                    var isColliding = centerB.X < centerA.X + fullExtent.X
                        && centerB.X > centerA.X - fullExtent.X
                        && centerB.Y < centerA.Y + fullExtent.Y
                        && centerB.Y > centerA.Y - fullExtent.Y;

                    if (!isColliding) continue;

                    var penetration = new Vector2{
                        X =  fullExtent.X - Math.Abs(centerB.X) - Math.Abs(centerA.X),
                        Y = fullExtent.Y - Math.Abs(centerB.Y) - Math.Abs(centerA.Y)
                    };

                    Console.WriteLine($"IsColliding #{a.BaseEntity.Id} | #{b.BaseEntity.Id}");

                    if (a.Collider.type == ColliderType.Player
                        && b.Collider.type == ColliderType.Ball)
                        OnPlayerAndBallCollide(a, b, penetration);
                    else if (a.Collider.type == ColliderType.Ball
                        && b.Collider.type == ColliderType.Player)
                        OnPlayerAndBallCollide(b, a, penetration);

                    else if (a.Collider.type == ColliderType.Ball
                        && b.Collider.type == ColliderType.GoalPlayer1
                        || a.Collider.type == ColliderType.GoalPlayer1
                        && b.Collider.type == ColliderType.Ball)
                        OnBallReachGoalPlayer1();
                    else if (a.Collider.type == ColliderType.Ball
                        && b.Collider.type == ColliderType.GoalPlayer2
                        || a.Collider.type == ColliderType.GoalPlayer2
                        && b.Collider.type == ColliderType.Ball)
                        OnBallReachGoalPlayer2();

                    else if (a.Collider.type == ColliderType.Ball
                        && b.Collider.type == ColliderType.Wall)
                        OnBallTouchWall(a, b, penetration);
                    else if (a.Collider.type == ColliderType.Wall
                        && b.Collider.type == ColliderType.Ball)
                        OnBallTouchWall(b, a, penetration);

                    if (a.Collider.type == ColliderType.Player
                        && b.Collider.type == ColliderType.Wall)
                        OnPlayerHitsTheWall(a, b, penetration);
                    else if (a.Collider.type == ColliderType.Wall
                        && b.Collider.type == ColliderType.Player)
                        OnPlayerHitsTheWall(b, a, penetration);
                }
            }
        }

        void OnNewEntityCreated(IEntity entity)
        {
            if (entity is ICollidable o)
                entityList.Add(o);
        }

        void OnPlayerAndBallCollide(ICollidable player, ICollidable ball, Vector2 penetration)
        {
            Console.WriteLine($"Ball before pos: {ball.Position.Value}, heading: {((IMovable)ball).Heading.Value}");
            if (Math.Abs(penetration.X) <= Math.Abs(penetration.Y))
            {
                ball.Position.Value.X += player.Position.Value.X < ball.Position.Value.X ? penetration.X : -penetration.X;
                ((IMovable)ball).Heading.Value.X *= -1;
            }
            else
            {
                ball.Position.Value.Y += penetration.Y;
                ((IMovable)ball).Heading.Value.Y *= -1;
            }
            Console.WriteLine($"Ball after  pos: {ball.Position.Value}, heading: {((IMovable)ball).Heading.Value}");
        }

        void OnBallReachGoalPlayer1()
        {
            Console.WriteLine("Player 1 win the match!!");
            GameState.Player1Scored = true;
        }
        void OnBallReachGoalPlayer2()
        {
            Console.WriteLine("Player 2 win the match!!");
            GameState.Player2Scored = true;
        }

        void OnBallTouchWall(ICollidable ball, ICollidable wall, Vector2 penetration)
        {
            Console.WriteLine($"Ball before pos: {ball.Position.Value}, heading: {((IMovable)ball).Heading.Value}");
           
            ball.Position.Value.Y += ball.Position.Value.Y < wall.Position.Value.Y ? -penetration.Y : penetration.Y;
            ((IMovable)ball).Heading.Value.Y *= -1;

            Console.WriteLine($"Ball after  pos: {ball.Position.Value}, heading: {((IMovable)ball).Heading.Value}");
        }

        void OnPlayerHitsTheWall(ICollidable player, ICollidable wall, Vector2 penetration) => 
            player.Position.Value.Y += (penetration.Y < 0) ? -penetration.Y : penetration.Y;

        readonly IList<ICollidable> entityList = new List<ICollidable>();
    }
}
