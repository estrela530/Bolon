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
    class GravityBlock : Character
    {

        private float speed;
        private Random rnd;

        public GravityBlock(Vector2 position, IGameMediator mediator)
            : base("GravityBlock", mediator)
        {
            Random rnd = new Random();
            speed = rnd.Next(1, 5);

            this.position = position;
        }

        public override void Initialize()
        {
            speed = rnd.Next(1, 5);
        }

        public override void Shutdown()
        {
        }

        public override void Update(GameTime gameTime)
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
