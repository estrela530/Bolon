﻿using System;
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


        public NormalBlock(Vector2 position, IGameMediator mediator)
            : base("NormalBlock", 200, 50, mediator)
        {
            speed = rnd.Next(3, 5);
            this.position = position;
            isDeadFlag = false;
        }

        public override void Initialize()
        {
            speed = rnd.Next(3, 5);
            isDeadFlag = false;
        }

        public override void Shutdown()
        {
        }

        public override void Update(GameTime gameTime)
        {
            //x軸500以下で死亡
            if (position.X <= 500 || position.Y >= 720)
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
