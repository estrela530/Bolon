using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volon.Device;

namespace Volon.Actor
{
    class ParticleEmitter
    {
        protected List<Particle> particles;
        protected Random rnd;

        public ParticleEmitter()
        {
            particles = new List<Particle>();
            rnd = new Random();
        }

        public void Clear()
        {
            particles.Clear();
        }

        public void Update(float delta)
        {
            List<Particle> toRemove = new List<Particle>();

            for(int i = 0;i < particles.Count; i++)
            {
                particles[i].Update(delta);

                if (!particles[i]._isActive)
                {
                    toRemove.Add(particles[i]);
                }
            }

            for(int i = 0;i < toRemove.Count; i++)
            {
                particles.Remove(toRemove[i]);
            }
        }

        public void Draw(Renderer renderer)
        {
            for(int i = 0;i < particles.Count; i++)
            {
                particles[i].Draw(renderer);
            }
        }

        public void Emit(string name,Vector2 size,Vector2 pos,float scale,float shrinkRate,
            float duration,int amount,int maxSpeed,Color color)
        {
            Particle p;
            for(int i = 0;i < amount; i++)
            {
                int angle = rnd.Next(0, 360);

                float speed = rnd.Next(1, maxSpeed);

                p = new Particle(name,size, pos, speed, angle, scale, shrinkRate, duration, color);

                particles.Add(p);
            }
        }
    }
}
