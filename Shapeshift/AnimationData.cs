using System;
using System.Collections.Generic;

namespace Shapeshift
{
    class AnimationMetadata
    {
        public AnimationMeta meta;
    }

    class AnimationMeta
    {
        public List<FrameTagData> frameTags { get; set; }
    }

    class FrameTagData
    {
        public string name { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public string direction { get; set; }

        public Animation toAnimation()
        {
            switch (direction)
            {
                case "forward":
                case "pingpong":
                case "reverse":
                    return new Animation(from, to, (direction == "pingpong"), (direction == "reverse"));
                default:
                    throw new Exception("Invalid direction.");
            }
        }
    }

}
