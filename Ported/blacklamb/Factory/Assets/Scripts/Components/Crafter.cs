using Unity.Entities;
using UnityEngine;

namespace Factory.Components
{
    public struct CrafterComponent : IComponentData
    {
        public int requiredResourceCount;
        public int inventory;
        public int workerCount;
        public bool destroyed;
    }

    namespace Author
    {
        public class Crafter : MonoBehaviour, IConvertGameObjectToEntity
        {
            public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
            {
                CrafterComponent crafter = new CrafterComponent()
                {
                    requiredResourceCount = 10,
                    inventory = 0,
                    workerCount = 0,
                    destroyed = false
                };

                dstManager.AddComponentData(entity, crafter);
            }
        }
    }
}