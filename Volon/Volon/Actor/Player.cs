using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Volon.Def;
using Volon.Device;
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
        private float gravity = 5.0f;
        public static bool IsDescentFlag;
        private float num = 0f;
        private Timer timer;
        private GameTime gameTime;
        private float seconds = 0;
        float Acc = 0.0f;//加速度
        float PosY = 0.0f;//y座標
        float JP = 100.0f;
        float GV = 0.98f;
        float power = 0;
        float firstPower = -2.0f;
        float descentPower = 0;

        //当たり判定用enum
        private enum Direction
        {
            Down, UP, RIGHT, LEFT
        };
        private Direction direction;

        private Dictionary<Direction, Range> directionRange;

        public Player(IGameMediator mediator)
              : base("Player", 60, 60, mediator)

        {
            position = new Vector2(100, 600);
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
            IsDescentFlag = false;

        }

        public override void Initialize()
        {
            position = new Vector2(0, 0);
            
            timer = new CountDownTimer(2);
            descentPower = 0;
        }


        public override void Update(GameTime gametime)
        {
            //timer.Update(gameTime);

            //Vector2 velocity = Input.Velocity();
            //移動量
            //float speed = 20.0f;
            //position = position + Input.Velocity() * speed;
            //当たり判定
            var min = Vector2.Zero;
            var max = new Vector2(Screen.Width - 64, Screen.Height - 64);
            //position = Vector2.Clamp(position, min, max);

            //移動用メソッド実装
            PlayerRiseMove();
            //Console.WriteLine("position.Y = " + position.Y);

            //if (Input.GetKeyState(Keys.D))
            //{
            //    IsDescentFlag = true;
            //    descentPower = 10.0f;
            //    position.Y += descentPower;
            //}
            //else if (Input.GetKeyRelease(Keys.D))
            //{
            //    IsDescentFlag = false;
            //    seconds = 0;
            //    power = 0;
            //}

            if (Input.GetKeyState(Keys.D))
            {
                IsDescentFlag = true;
                descentPower = 10.0f;
                position.Y += descentPower;
            }
            if (position.Y >= Screen.Height - 64)
            {
                IsDescentFlag = false;
                seconds = 0;
                power = 0;
                firstPower = -15.0f;
            }

            
        }


        //Playerが昇る動きと押したら降下
        //するMoveのためのメソッド
        /// <summary>
        /// 上下メソッド
        /// </summary>
        public void PlayerRiseMove()
        {
            position.X += 3.0f;
            seconds += 1;

            //IsDesceentFlagがfalseで
            if (IsDescentFlag == false)
            {
                //position.Y += -10.0f;
                //if (timer.IsTime())
                //{
                //    position.Y += -20.0f;
                //}

                //今のところの完成
                //if (seconds >= 0 && seconds < 100)
                //{
                //    power += -0.1f;
                //    position.Y += firstPower;
                //    position.Y += power;
                //}
                //if (seconds >= 100)
                //{
                //    power += 0.3f;
                //    position.Y += power;
                //}
                if (seconds >= 0 && seconds < 20)
                {
                    power = firstPower;
                    power += -0.2f;
                    position.Y += power;
                }
                else if (seconds >= 20)
                {
                    power += 0.3f;
                    position.Y += power;
                }
            }
            //else if (isDeadFlag == true)
            //{
            //    descentPower = 10.0f;
            //    position.Y += descentPower;
            //}
        }



        public override void Shutdown()
        {
            sound.StopBGM();
        }

        public override void Hit(Character other)
        {
            IsDescentFlag = false;
            seconds = 0;
            power = 0;
            firstPower = -15.0f;
        }
        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }

        private void ChangeMotion(Direction direction)
        {
            this.direction = direction;
        }
        
    }
}