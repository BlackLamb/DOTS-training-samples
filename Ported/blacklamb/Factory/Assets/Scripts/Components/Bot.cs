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
        public Color botColor;
        public float botSpeed;
    }

    namespace Author
    {
        [RequiresEntityConversion]
        public class Bot : MonoBehaviour, IConvertGameObjectToEntity
        {
            public float radius;
            public float botSpeed = 2.0f;

            public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
            {
                BotComponent bot = new BotComponent()
                {
                    radius = radius,
                    hitCount = 0,
                    botSpeed = botSpeed,
                    botColor = Color.white
                };
                dstManager.AddComponentData(entity, bot);
            }
        }
    }
}
