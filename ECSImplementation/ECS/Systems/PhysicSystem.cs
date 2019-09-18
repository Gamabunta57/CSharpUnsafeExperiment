using ECSFoundation.ECS.Entities;
using ECSFoundation.ECS.Systems;
using ECSImplementation.ECS.Component;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

    public class PhysicSystem : ISystem
    {

        public PhysicSystem(uint layerCount)
        {
            collidableByLayer = new IList<ICollidable>[layerCount];
            collisionMatrix = new IList<Tuple<CollisionLayer, Action<ICollidable, ICollidable, Vector2>>>[layerCount];
            singleLayerCollisionMatrix = new List<Tuple<CollisionLayer, Action<ICollidable, ICollidable, Vector2>>>();

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

        public void SetLayersAsCollidable(CollisionLayer layer, Action<ICollidable, ICollidable, Vector2> callback)
        {
            var tuple = new Tuple<CollisionLayer, Action<ICollidable, ICollidable, Vector2>>(layer, callback);
            singleLayerCollisionMatrix.Add(tuple);
        }

        public void NotifyNewEntity(ICollidable entity)
        {
            Console.WriteLine($"New physics: #{entity.BaseEntity.Id}, {(int)entity.Collider.type}: {entity.Collider.type.ToString()}");
            collidableByLayer[(uint)entity.Collider.type].Add(entity);
        }

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
                    var callback = collisionMatrix[i][j].Item2;
                    for (var k = 0; k < entityListA.Count; k++)
                        for (var l = 0; l < entityListB.Count; l++)
                            CheckCollision(entityListA[k], entityListB[l], callback);
                }
            }

            for (var i = 0; i < singleLayerCollisionMatrix.Count; i++)
            {
                var entityList = collidableByLayer[(int)singleLayerCollisionMatrix[i].Item1];
                var callback = singleLayerCollisionMatrix[i].Item2;
                for (var k = 0; k < entityList.Count - 1; k++)
                    for (var l = k + 1; l < entityList.Count; l++)
                        CheckCollision(entityList[k], entityList[l], callback);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void CheckCollision(ICollidable a, ICollidable b, Action<ICollidable, ICollidable, Vector2> callback)
        {
            var fullExtent = a.Collider.halfExtent + b.Collider.halfExtent;
            var centerB = b.Position.Value + b.Collider.Center - a.Position.Value - a.Collider.Center;

            var isColliding = centerB.X < fullExtent.X
                && centerB.X > -fullExtent.X
                && centerB.Y < fullExtent.Y
                && centerB.Y > -fullExtent.Y;

            if (!isColliding) return;

            var penetrationVector = new Vector2
            {
                X = fullExtent.X - Math.Abs(centerB.X),
                Y = fullExtent.Y - Math.Abs(centerB.Y)
            };

            callback.Invoke(a, b, penetrationVector);
        }

        readonly IList<ICollidable>[] collidableByLayer;
        readonly IList<Tuple<CollisionLayer, Action<ICollidable, ICollidable, Vector2>>>[] collisionMatrix;
        readonly IList<Tuple<CollisionLayer, Action<ICollidable, ICollidable, Vector2>>> singleLayerCollisionMatrix;
    }
}
