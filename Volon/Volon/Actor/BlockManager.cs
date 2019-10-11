using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Volon.Device;

namespace Volon.Actor
{
    class BlockManager
    {
        //フィールド
        private List<Character> blocks;
        private Renderer renderer;
       
        public BlockManager()
        {
            var gameDevice = GameDevice.Instance();
            renderer = gameDevice.GetRenderer();
            Initialize();
        }
        public void Initialize()
        {
            if (blocks != null)
            {
                blocks.Clear();
            }
            else
            {
                blocks = new List<Character>();
            }
        }
        public void Update(GameTime gameTime)
        {
            foreach(Character c in blocks)
            {
                c.Update(gameTime);
            }
        }
        public void Add(Character character)
        {
            if (character.ToString().Contains("Block"))
            {
                blocks.Add(character);
            }
        }
        public void Draw()
        {
            foreach (Character c in blocks)
            {
                c.Draw(renderer);
            }
        }
    }
}
