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
        public static bool IsDescentFlag;
        private float num = 0f;
        private Timer timer;
        private float playerMoveSeconds = 0;
        private float splashMountainSeconds = 0;
        float power = 0;
        public float firstPower = -2.0f;
        private float specialWidth;
        private float specialHeight;

        //追加 かいと
        private Vector2 previousPos;
        private Vector2 currentPos;
        private float distance;

        private ParticleEmitter emitter;
        private Vector2 assetSize;

        //当たり判定用enum
        private enum Direction
        {
            Down, UP, RIGHT, LEFT
        };

        public Player(IGameMediator mediator)
              : base("Player", 60, 60, 0, 0, mediator)
        {
            position = new Vector2(100, 600);
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
            IsDescentFlag = false;
            isDeadFlag = false;

            //追加
            assetSize = new Vector2(60, 60);
            emitter = new ParticleEmitter();
        }

        public override void Initialize()
        {
            position = new Vector2(150, 0);

            timer = new CountDownTimer(2);
        }

        public override void Update(GameTime gametime)
        {

            float delta = (float)gametime.ElapsedGameTime.TotalSeconds;

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
                //isDeadFlag = true;
            }

            if (IsDescentFlag == true)
            {
                SplashMountain();
            }

            //追加
            if (isDeadFlag == false)
            {
                emitter.Emit("Player", assetSize, position + assetSize / 2, 0.5f, 0.5f, 2f, 1, 600, Color.Black);
            }
            emitter.Update(delta);
        }

        //Playerが昇る
        //Moveのためのメソッド
        /// <summary>
        /// 上昇メソッド
        /// </summary>
        public void PlayerRiseMove()
        {
            ////今と前のポジション受け取り
            //previousPos = currentPos;
            //currentPos = position;

            ////距離
            //distance = Vector2.Distance(currentPos, previousPos);

            ////速さ？
            //float speed = distance / playerMoveSeconds;



            //position.X += 3.0f;
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

                #region 恥ずかしい上昇処理
                if (playerMoveSeconds >= -100 && playerMoveSeconds < 20)
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

            #region　恥ずかしいパーティクル確認用
            //パーティクル確認用
            if (Input.GetKeyTrigger(Keys.F))
            {
                IsDescentFlag = false;
                playerMoveSeconds = 0;
                splashMountainSeconds = 0;
                power = 0;
                firstPower = -15.0f;
            }
            #endregion
        }

        public override void Shutdown()
        {
            sound.StopBGM();
        }

        public override void Hit(Character other)
        {
            if (other is NormalBlock)
            {
                if (other.GetRectangle().Width >= 85 && other.GetRectangle().Width <= 200)
                {
                    Console.WriteLine("Width = " + other.GetRectangle().Width);
                    IsDescentFlag = false;
                    playerMoveSeconds = -50;
                    splashMountainSeconds = 0;
                    power = 0;
                    firstPower = -30.0f;
                }
                else
                {
                    IsDescentFlag = false;
                    playerMoveSeconds = 0;
                    splashMountainSeconds = 0;
                    power = 0;
                    firstPower = -15.0f;
                }
            }
            else if (other is GravityBlock)
            {
                IsDescentFlag = false;
                playerMoveSeconds = 0;
                splashMountainSeconds = 0;
                power = 0;
                firstPower = -5.0f;
            }
            else if (other is SpecialBlock)
            {
                IsDescentFlag = false;
                playerMoveSeconds = -50;
                splashMountainSeconds = 0;
                power = 0;
                firstPower = -30.0f;
            }
            else if (other is ThornsBlock)
            {
                IsDescentFlag = false;
                playerMoveSeconds = 0;
                splashMountainSeconds = 0;
                power = 0;
                firstPower = -15.0f;
            }
            Console.WriteLine("Width = " + other.GetRectangle().Width);

        }
        public override void Draw(Renderer renderer)
        {
            //追加
            emitter.Draw(renderer);

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