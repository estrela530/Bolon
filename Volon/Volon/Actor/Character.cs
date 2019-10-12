using System;
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
        protected enum State
        {
            preparation,
            Alive,
            Dying,
            Dead
        };

        public Character(string name, IGameMediator mediator)
        {
            this.name = name;
            position = Vector2.Zero;
            isDeadFlag = false;
            this.mediator = mediator;
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
        public bool IsCollision(Character other)
        {
            float length = (position - other.position).Length();

            float radiusSum = 64f;
            if (length <= radiusSum)
            {
                return true;
            }
            return false;
        }
        public void SetPosition(ref Vector2 other)
        {
            other = position;
        }
    }

}

