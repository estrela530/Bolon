using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Volon.Device;
using Volon.Scene;
using Volon.Def;

namespace Volon.Actor
{
    class NormalBlock : Character
    {

        float speed;
        Random rnd;

        public NormalBlock(IGameMediator mediator)
            : base("NormalBlock", mediator)
        {
            var gameDevice = GameDevice.Instance();
            rnd = gameDevice.GetRandom();
            speed = rnd.Next(1, 5);

            position = new Vector2(Screen.Width, Screen.Height);
        }

        public override void Initialize()
        {
        }

        public override void Shutdown()
        {
        }

        public override void Update(GameTime gametime)
        {
            Move();
        }
        public override void Hit(Character other)
        {
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }

        public void Move()
        {
            position.X -= speed;
        }
    }
}
