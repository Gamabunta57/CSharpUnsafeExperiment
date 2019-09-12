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

        public void OnRegisterEntity(int Id)
        {
            idList.Add(Id);
        }

        public void Update()
        {
            foreach(var id in idList)
            {
                ref var player = ref memory.Get<PlayerEntity>(id);

                Console.WriteLine($"Player {player.baseEntity.Id} before: {player.position}");
                ApplyMovement(ref player.position, ref player.heading);
                Console.WriteLine($"Player {player.baseEntity.Id} after:  {player.position}");
            }
        }

        readonly MasterMemoryAllocator memory;
        readonly IList<int> idList = new List<int>();
    }
}
