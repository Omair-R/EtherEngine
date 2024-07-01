using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EtherEngine.Entities;

namespace EtherEngine.Components.Relations
{
    public struct ChildOf
    {
        public EtherEntity Target;
    }

    public struct ParentOf
    {
        public EtherEntity Target;
    }
}
