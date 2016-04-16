using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapeshift
{

    class Animation
    {
        public List<int> indices = new List<int>();

        public Animation(int start, int end, bool pingpong = false, bool reverse = false)
        {
            indices.AddRange(Enumerable.Range(start, end + 1 - start));
            if (reverse)
            {
                indices.Reverse();
            }
            if (pingpong)
            {
                indices.AddRange(indices.GetRange(1, indices.Count - 2).Reverse<int>());
            }
        }
    }

    class Sprite
    {
        string filename;
        TextureAtlas atlas;
        Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        Animation currentAnimation;
        int frame = 0;
        int subframe = 0;
        int frameFrequency = 6;

        public Sprite(string filename, int width, int height)
        {
            this.filename = filename;
            this.atlas = new TextureAtlas(filename, width, height);
        }

        public void LoadContent()
        {
            atlas.LoadContent();
            LoadAnimations();
            if(animations.Count > 0)
                SetAnimation(animations.First().Key);
        }

        void LoadAnimations()
        {
            string animationFile = "Content\\" + filename + ".json";
            if (!File.Exists(animationFile))
                return;
            using (StreamReader streamReader = new StreamReader(animationFile))
            {
                var serializer = new JsonSerializer();
                AnimationMetadata aData = (AnimationMetadata)serializer.Deserialize(streamReader, typeof(AnimationMetadata));
                foreach(var frameTag in aData.meta.frameTags)
                {
                    AddAnimation(frameTag);
                }
            }
        }

        void AddAnimation(FrameTagData frameTag)
        {
            animations.Add(frameTag.name, frameTag.toAnimation());
        }

        public bool SetAnimation(string s)
        {
            if (!animations.ContainsKey(s))
                return false;
            Animation a = animations[s];
            if (a == null || currentAnimation == a)
                return false;
            frame = 0;
            currentAnimation = a;
            return true;
        }

        public void Draw(Vector2 pos)
        {
            int atlasIndex = (currentAnimation != null) ? currentAnimation.indices[frame] : 0;
            atlas.Draw(pos, atlasIndex);
            if (currentAnimation != null)
            {
                subframe = (subframe + 1) % frameFrequency;
                if (subframe == 0)
                    frame = (frame + 1) % currentAnimation.indices.Count;
            }
        }
    }
}
