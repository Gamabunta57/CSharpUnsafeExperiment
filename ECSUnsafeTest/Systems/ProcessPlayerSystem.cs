using System;
using System.Collections.Generic;
using ECSUnsafeTest.Entities;
using ECSUnsafeTest.MemoryAllocators;

using static ECSUnsafeTest.Systems.Subsystems.MovableSubsystem;

namespace ECSUnsafeTest.Systems
{
    public class ProcessPlayerSystem : ISystem
    {

        public ProcessPlayerSystem(MasterMemoryAllocator allocator){
            memory = allocator;
        }

        public void OnRegisterEntity(IEntity entity)
        {
            idList.Add(entity);
        }

        public void Update()
        {
            foreach(var entity in idList)
            {
                var player = (PlayerEntity) entity;

                Console.WriteLine($"Player {player.BaseEntity.Id} before: {player.Position.Value}");

                ApplyMovement(ref player.Position.Value , ref player.Heading.Value);

                Console.WriteLine($"Player {player.BaseEntity.Id} after:  {player.Position.Value}");
            }
        }

        readonly MasterMemoryAllocator memory;
        readonly IList<IEntity> idList = new List<IEntity>();
    }
}
