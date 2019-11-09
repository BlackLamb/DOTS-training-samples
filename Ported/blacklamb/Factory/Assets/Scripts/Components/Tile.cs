using Unity.Entities;
using Factory.Types;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Factory.Components
{
    public struct TileComponent : IComponentData
    {
        public int moveCost;
        public bool isResourceSpawner;
        public Types.PathType pathType;
    }

    namespace Author
    {
        public class Tile : MonoBehaviour, IConvertGameObjectToEntity
        {
            public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
