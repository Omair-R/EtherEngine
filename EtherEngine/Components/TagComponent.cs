using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Components
{
    public struct TagComponent
    {
        public string Tag;

        public TagComponent()
        {
            Tag = "Entity";
        }

        public TagComponent(string tag)
        {
            Tag = tag;
        }
    }
}
