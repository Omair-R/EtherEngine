using System;

namespace EtherEngine.Components
{
    public struct IdComponent
    {
        public readonly Guid Id;

        public IdComponent()
        {
            Id = Guid.NewGuid();
        }

        public IdComponent(Guid id)
        {
            Id = id;
        }
    }
}
