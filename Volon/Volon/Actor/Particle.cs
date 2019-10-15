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
    class Particle
    {
        public string _name;
        public Vector2 _position;
        public Vector2 _direction;
        public Vector2 _origin;
        public float _duration;
        public float _scale;
        public float _shrinkRate;
        public float _speed;
        public bool _isActive;
        public Color _color;

        public Particle(string name,Vector2 size,Vector2 pos,float speed,float angle,float scale,float shrinkRate,float duration,Color color)
        {
            _name = name;
            _position = pos;
            _scale = scale;
            _shrinkRate = shrinkRate;
            _isActive = true;
            _duration = duration;
            _color = color;
            _speed = speed;
            _origin = new Vector2(size.X/2,size.Y/2);

            angle = MathHelper.ToRadians(angle);

            Vector2 up = new Vector2(0, -1.0f);
            Matrix rot = Matrix.CreateRotationZ(angle);
            _direction = Vector2.Transform(up, rot);

        }
        
        public void Update(float delta)
        {
            _position += _direction * _speed * delta;

            _scale -= _shrinkRate * delta;

            _duration -= delta;

            if(_scale <= 0.0f || _duration <= 0.0f)
            {
                _isActive = false;
                _position = new Vector2(-100, -100);
            }
        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture(_name, _position, _color, 0, _origin, _scale);
        }
    }
}
