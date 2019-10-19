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

        private float speed;
        private Random rnd = new Random();
        private Player player;

        public NormalBlock(Vector2 position, IGameMediator mediator)
            : base("NormalBlock", 200, 50,30,50, mediator)
        {
            this.position = position;
            isDeadFlag = false;
        }

        public override void Initialize()
        {
            speed = rnd.Next(5,7);
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
            //Move();
        }
        public override void Hit(Character other)
        {
            //Console.WriteLine(other);
            //if (other.GetRectangle().Width >= 60 && other.GetRectangle().Width <= 115)
            //{
            //    Console.WriteLine("Width = " + GetRectangle().Width);

            //    player.firstPower = -30.0f;
            //}
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
