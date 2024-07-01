using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.LDTK
{
    public enum TileRenderMode
    { 
        Cover, 
        FitInside, 
        FullSizeCropped, 
        FullSizeUncropped, 
        NineSlice,
        Repeat, 
        Stretch 
    };

    public enum LayerType
    { 
        AutoLayer, 
        Entities, 
        IntGrid, 
        Tiles 
    };

    public enum EmbedAtlas
    { 
        LdtkIcons
    };

    public enum WorldLayout
    { 
        Free, 
        GridVania, 
        LinearHorizontal, 
        LinearVertical
    };

}