using System;
using System.Collections.Generic;
using ECSFoundation.ECS.Entities;
using ECSUnsafeTest.ECS.Systems;
using ECSUnsafeTest.utils;
using PhysicsBoilerPlate.Component;

namespace PhysicsBoilerPlate.System
{
    public class PhysicSystem
    {
        public PhysicSystem(int layerCount)
        {
            collidableByLayer = new IList<ICollidable>[layerCount];
            collisionMatrix = new IList<Tuple<Layer, Action<IEntity, IEntity, Vector2>>>[layerCount];
        }

        public void Register(Layer layerA, Layer layerB, Action<IEntity, IEntity, Vector2> callback)
        {
            var tuple = new Tuple<Layer, Action<IEntity, IEntity, Vector2>>(layerB, callback);
            collisionMatrix[(int)layerA].Add(tuple);
        }

        public void Update()
        {
            for(var i = 0; i < collisionMatrix.Length; i++)
            {
                Layer layerA = (Layer)i;
                var colliderListA = collidableByLayer[i];
                for(var j = 0; j < collisionMatrix[i].Count; j++)
                {
                    Layer layerB = collisionMatrix[i][j].Item1;
                    var colliderListB = collidableByLayer[j];

                    CheckCollision(colliderListA, colliderListB, collisionMatrix[i][j].Item2);
                }
            }
        }

        void CheckCollision(IList<ICollidable> listA, IList<ICollidable> listB, Action<IEntity, IEntity, Vector2> callback)
        {
            for(var i = 0; i < listA.Count; i++)
            {
                for (var j = 0; j < listB.Count; j++)
                {
                    if (collide(listA[i], listB[j]))
                        callback(listA[i], listB[j], penetration);

                }
            }
        }

        readonly IList<ICollidable>[] collidableByLayer;
        readonly IList<Tuple<Layer, Action<IEntity, IEntity, Vector2>>>[] collisionMatrix;
    }
}
