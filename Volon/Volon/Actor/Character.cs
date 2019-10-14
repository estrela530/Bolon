﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Volon.Device;
using Volon.Def;
using Volon.Scene;

namespace Volon.Actor
{


    abstract class Character
    {
        protected Vector2 position;
        protected string name;
        protected bool isDeadFlag;
        protected IGameMediator mediator;
        protected int width;//幅
        protected int height;//高さ

        public Character(string name, int width, int height, IGameMediator mediator)
        {
            this.name = name;
            position = Vector2.Zero;
            isDeadFlag = false;
            this.mediator = mediator;
            this.width = width;
            this.height = height;

        }
        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Shutdown();
        public abstract void Hit(Character other);
        public bool IsDead()
        {
            return isDeadFlag;
        }
        public virtual void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }
        //public bool IsCollision(Character other)
        //{
        //    float length = (position - other.position).Length();

        //    float radiusSum = 64f;
        //    if (length <= radiusSum)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        public void SetPosition(ref Vector2 other)
        {
            other = position;
        }

        public Rectangle GetRectangle()
        {
            //矩形の生成
            Rectangle area = new Rectangle();

            //位置と幅、高さを設定
            area.X = (int)position.X;
            area.Y = (int)position.Y;
            area.Height = height;
            area.Width = width;

            return area;
        }

        public bool IsCollision(Character other)
        {
            //RectangleクラスのIntersectsメソッドで重なり判定
            return this.GetRectangle().Intersects(other.GetRectangle());
        }
    }

}

