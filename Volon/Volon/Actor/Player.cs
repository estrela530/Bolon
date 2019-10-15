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
        private float playerMoveSeconds = 0;
        private float splashMountainSeconds = 0;
        float power = 0;
        float firstPower = -2.0f;

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

        }


        public override void Update(GameTime gametime)
        {
            //当たり判定
            var min = Vector2.Zero;
            var max = new Vector2(Screen.Width - 64, Screen.Height - 64);

            //移動用メソッド実装
            PlayerRiseMove();

            if (Input.GetKeyTrigger(Keys.D))
            {
                IsDescentFlag = true;
            }
            if (position.Y >= Screen.Height - 64)
            {
                IsDescentFlag = false;
                playerMoveSeconds = 0;
                splashMountainSeconds = 0;
                power = 0;
                firstPower = -15.0f;
            }

            if (IsDescentFlag == true)
            {
                SplashMountain();
            }
        }


        //Playerが昇る
        //Moveのためのメソッド
        /// <summary>
        /// 上昇メソッド
        /// </summary>
        public void PlayerRiseMove()
        {
            position.X += 3.0f;
            playerMoveSeconds += 1;

            if (IsDescentFlag == false)
            {
                #region IsTimeお試し
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
                #endregion

                #region 恥ずかしい落下処理
                if (playerMoveSeconds >= 0 && playerMoveSeconds < 20)
                {
                    power = firstPower;
                    power += -0.2f;
                    position.Y += power;
                }
                else if (playerMoveSeconds >= 20)
                {
                    power += 0.3f;
                    position.Y += power;
                }
                #endregion
            }

        }



        public override void Shutdown()
        {
            sound.StopBGM();
        }

        public override void Hit(Character other)
        {
            IsDescentFlag = false;
            playerMoveSeconds = 0;
            splashMountainSeconds = 0;
            power = 0;
            firstPower = -15.0f;
        }
        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }
        

        public void SplashMountain()
        {
            #region 急降下
            splashMountainSeconds += 1;
            //初速
            if (splashMountainSeconds >= 0 && splashMountainSeconds < 20)
            {
                power += 5.0f;
                position.Y += power;
            }
            //加速
            else if (splashMountainSeconds >= 20)
            {
                power += 2.5f;
                position.Y += power;
            }
            #endregion
        }
    }
}