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
    class SpecialBlock : Character
    {

        private float speed;

        public SpecialBlock(Vector2 position, IGameMediator mediator)
            : base("SpecialBlock", 120, 35,0,0, mediator)
        {
            speed = 9;

            this.position = position;
        }

        public override void Initialize()
        {
            isDeadFlag = false;
        }

        public override void Shutdown()
        {
        }

        public override void Update(GameTime gameTime)
        {
            //x軸0以下で死亡
            if (position.X <= 0 || position.Y >= 720)
            {
                isDeadFlag = true;
            }
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
