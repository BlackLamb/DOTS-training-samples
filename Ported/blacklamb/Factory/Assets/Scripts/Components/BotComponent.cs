using Unity.Entities;
using UnityEngine;

namespace Factory.Components
{
    public struct BotComponent : IComponentData
    {
        public float radius;
        public int hitCount;
        public Entity targetCrafter;
        public bool movingToResource;
        public bool holdingResource;
    }

    public class Bot : MonoBehaviour, IConvertGameObjectToEntity
    {
        public float radius;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            BotComponent bot = new BotComponent() {radius = radius, hitCount = 0};
            dstManager.AddComponentData(entity, bot);
        }
    }
}