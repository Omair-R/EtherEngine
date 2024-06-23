using System;

namespace EtherEngine.Components
{
    public enum FollowType
    {
        Instant,
        PID,
    }
    public struct FollowComponent
    {
        public Guid EntityUID;
        public FollowType FollowType;
    }

}
