using Unity.Entities;
using Factory.Types;

// ReSharper disable once CheckNamespace
namespace Factory.Components
{
    public struct TileComponent : IComponentData
    {
        public int moveCost;
        public bool isResourceSpawner;
        public Types.PathType pathType;
    }
}