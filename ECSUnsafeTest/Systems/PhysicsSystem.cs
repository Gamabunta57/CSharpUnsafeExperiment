using System;
using System.Collections.Generic;
using ECSUnsafeTest.MemoryAllocators;

namespace ECSUnsafeTest.Systems
{
    public class PhysicsSystem : ISystem
    {
        public PhysicsSystem(MasterMemoryAllocator allocator)
        {
            memory = allocator;
        }

        public void OnRegisterEntity(int Id, Type type)
        {
            idList.Add(new Tuple<int, Type>(Id, type));
        }

        public void Update()
        {
            foreach (var id in idList)
            {
                ref var player = ref memory.Get(id.Item1, id.Item2);

                Console.WriteLine($"Player {player.baseEntity.Id} before: {player.position}");
                ApplyMovement(ref player.position, ref player.heading);
                Console.WriteLine($"Player {player.baseEntity.Id} after:  {player.position}");
            }
        }

        readonly MasterMemoryAllocator memory;
        readonly IList<Tuple<int, Type>> idList = new List<Tuple<int, Type>>();
    }
}
