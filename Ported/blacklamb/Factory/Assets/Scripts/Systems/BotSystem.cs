using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace Factory.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public class BotSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            throw new System.NotImplementedException();
        }
    }
}