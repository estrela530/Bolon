using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Volon.Def;
using Volon.Device;
using Volon.Dvice;
using Volon.Scene;
using Volon.Util;

namespace Volon.Actor
{
    class Player : Character
    {
        //フィールド
        private Sound sound;
        private float riseSpeed;
        private bool blockJumpNow;

        //当たり判定用enum
        private enum Direction
        {
            Down, UP, RIGHT, LEFT
        };
        private Direction direction;

        private Dictionary<Direction, Range> directionRange;

        public Player(IGameMediator mediator)
              : base("Player", mediator)

        {
            position = Vector2.Zero;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();

        }

        public override void Initialize()
        {
            position = new Vector2(300, 400);
            directionRange = new Dictionary<Direction, Range>()
            {
                {Direction.Down,new Range(0,3) },
                {Direction.UP,new Range(4,7) },
                {Direction.RIGHT,new Range(8,11) },
                {Direction.LEFT,new Range(12,15) }
            };

        }


        public override void Update(GameTime gametime)
        {
            Vector2 velocity = Input.Velocity();
            //移動量
            float speed = 20.0f;
            position = position + Input.Velocity() * speed;
            //当たり判定
            var min = Vector2.Zero;
            var max = new Vector2(Screen.Width - 64, Screen.Height - 64);
            position = Vector2.Clamp(position, min, max);

           

            //Screenからはみ出さないようにする処理
            position = position + Input.Velocity() * speed;
            if (position.X < 0.0f)
            {
                position.X = 0.0f;
            }
            if (position.X >= Screen.Width - 64)
            {
                position.X = Screen.Width - 64;
            }
            if (position.Y < 0.0f)
            {
                position.Y = 0.0f;
            }
            if (position.Y > Screen.Height - 64)
            {
                position.Y = Screen.Height - 64;
            }

            position = Vector2.Clamp(position, Vector2.Zero, new Vector2(735, 535));
            UpdateMotion();
           
        }

        //Playerが昇る動きと押したら降下
        //するMoveのためのメソッド
        /// <summary>
        /// 上下メソッド
        /// </summary>
        public void PlayerRiseMove()
        {
            

            if (Input.GetKeyTrigger(Keys.Space))
            {

            }
        }

        

        public override void Shutdown()
        {
            sound.StopBGM();
        }

        public override void Hit(Character other)
        {
           
        }
        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }

        private void ChangeMotion(Direction direction)
        {
            this.direction = direction;
        }
        private void UpdateMotion()
        {
            Vector2 velocity = Input.Velocity();
            if (velocity.Length() <= 0.0f)
            {
                return;
            }
            if ((velocity.Y > 0.0f) && (direction != Direction.Down))
            {
                ChangeMotion(Direction.Down);
            }
            else if ((velocity.Y < 0.0f) && (direction != Direction.UP))
            {
                ChangeMotion(Direction.UP);
            }
            else if ((velocity.X > 0.0f) && (direction != Direction.RIGHT))
            {
                ChangeMotion(Direction.RIGHT);
            }
            else if ((velocity.X < 0.0f) && (direction != Direction.LEFT))
            {
                ChangeMotion(Direction.LEFT);
            }
        }
    }
}