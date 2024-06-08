using EtherEngine.Utils.Validate;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EtherEngine.Components
{
    public struct SpriteAnimationComponent
    {
        public string Name;

        public int HFrameCount;
        public int VFrameCount;
        public Vector2 FrameCoordinates;

        public int CurrentFrame = 0;

        public float FrameDuration;//TODO: Replace with a timer.
        public float RemainingTime;

        public bool IsPaused;

        public SpriteAnimationComponent(string name,
                                        int hFrameCount,
                                        int vFrameCount,
                                        int firstHFrame,
                                        int firstVFrame,
                                        float frameDuration)
        {
            Name = name;

            HFrameCount = hFrameCount;
            VFrameCount = vFrameCount;
            FrameCoordinates = new(firstHFrame, firstVFrame);

            FrameDuration = frameDuration;//TODO: Replace with a timer.
            RemainingTime = FrameDuration;

            IsPaused = false;
        }

    }
}
