using ECSFoundation.ECS.Entities;
using ECSFoundation.ECS.Systems;
using ECSImplementation.ECS.Component;
using ECSImplementation.Global;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace ECSImplementation.ECS.Systems
{
    /*TODO: 
     * - migrate the code to have a better architecture
     *      - this should check for the collisions only
     *      - when it detects one, it should not process it by itself (event system ?)
     * - integrate more than one shape for the collision detection (circle + polygon?)
     * - now it's AABB only => should add the possibility to rotate
     */
    public interface ICollidable : IEntity
    {
        ref Position Position { get; }
        ref RectCollider Collider { get; }
    }

    class PhysicSystem : ISystem
    {

        public PhysicSystem(uint layerCount)
        {
            collidableByLayer = new IList<ICollidable>[layerCount];
            collisionMatrix = new IList<Tuple<CollisionLayer, Action<ICollidable, ICollidable, Vector2>>>[layerCount];

            for (var i = 0; i < layerCount; i++)
            {
                collidableByLayer[i] = new List<ICollidable>();
                collisionMatrix[i] = new List<Tuple<CollisionLayer, Action<ICollidable, ICollidable, Vector2>>>();
            }
        }

        public void SetLayersAsCollidableTogether(CollisionLayer layerA, CollisionLayer layerB, Action<ICollidable, ICollidable, Vector2> callback)
        {
            var tuple = new Tuple<CollisionLayer, Action<ICollidable, ICollidable, Vector2>>(layerB, callback);
            collisionMatrix[(int)layerA].Add(tuple);
        }
        public void NotifyNewEntity(ICollidable entity) => collidableByLayer[(uint)entity.Collider.type].Add(entity);

        public void Update(GameTime gameTime)
        {
            for (var i = 0; i < collisionMatrix.Length; i++)
            {
                var collidingWithLayerA = collisionMatrix[i];
                var entityListA = collidableByLayer[i];

                for (var j = 0; j < collidingWithLayerA.Count; j++)
                {
                    var layerB = collidingWithLayerA[j].Item1;
                    var entityListB = collidableByLayer[(uint)layerB];

                    for (var k = 0; k < entityListA.Count; k++)
                    {
                        var a = entityListA[k];
                        for (var l = 0; l < entityListB.Count; l++)
                        {
                            var b = entityListB[l];
                            var fullExtent = a.Collider.halfExtent + b.Collider.halfExtent;
                            ref var centerA = ref a.Collider.Center;
                            var centerB = b.Position.Value + b.Collider.Center - a.Position.Value;

                            var isColliding = centerB.X < centerA.X + fullExtent.X
                                && centerB.X > centerA.X - fullExtent.X
                                && centerB.Y < centerA.Y + fullExtent.Y
                                && centerB.Y > centerA.Y - fullExtent.Y;

                            if (!isColliding) continue;

                            Console.WriteLine($"IsColliding #{a.BaseEntity.Id} | #{b.BaseEntity.Id}");

                            var penetrationVector = new Vector2
                            {
                                X = fullExtent.X - Math.Abs(centerB.X) - Math.Abs(centerA.X),
                                Y = fullExtent.Y - Math.Abs(centerB.Y) - Math.Abs(centerA.Y)
                            };

                            collisionMatrix[i][j].Item2.Invoke(a, b, penetrationVector);
                        }
                    }
                }
            }
        }

        readonly IList<ICollidable>[] collidableByLayer;
        readonly IList<Tuple<CollisionLayer, Action<ICollidable, ICollidable, Vector2>>>[] collisionMatrix;
    }
}
