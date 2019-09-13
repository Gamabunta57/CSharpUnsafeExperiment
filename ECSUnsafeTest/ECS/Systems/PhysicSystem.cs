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
                for (var j = i + 1; j < entityList.Count; j++)
                {
                    var fullExtent = entityList[i].Collider.halfExtent + entityList[j].Collider.halfExtent;
                    var centerA = entityList[i].Position.Value + entityList[i].Collider.center;
                    var centerB = entityList[j].Position.Value + entityList[j].Collider.center;

                    var isColliding = centerB.X < centerA.X + fullExtent.X
                        && centerB.X > centerA.X - fullExtent.X
                        && centerB.Y < centerA.Y + fullExtent.Y
                        && centerB.Y > centerA.Y - fullExtent.Y;

                    Console.WriteLine($"IsColliding #{entityList[i].BaseEntity.Id} | #{entityList[j].BaseEntity.Id}: {isColliding}");
                }
            }
        }

        void OnNewEntityCreated(IEntity entity)
        {
            if (entity is ICollidable o)
                entityList.Add(o);
        }

        readonly IList<ICollidable> entityList = new List<ICollidable>();
    }
}
