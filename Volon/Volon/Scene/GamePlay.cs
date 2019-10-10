using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Volon.Device;

namespace Volon.Scene
{
    class GamePlay : IScene
    {

        private GameDevice gameDevice;
        private Renderer renderer;

        public GamePlay()
        {
            gameDevice = GameDevice.Instance();
        }
        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.End();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public bool IsEnd()
        {
            throw new NotImplementedException();
        }

        public SceneName Next()
        {
            throw new NotImplementedException();
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
