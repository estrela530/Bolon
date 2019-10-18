using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volon.Device;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Volon.Actor;

namespace Volon.Scene
{
    class GameTitle : IScene
    {
        //フィールド
        // 終了しているかどうか
        private bool isEndFlag;
        // サウンド
        private Sound sound;

        // レンダラー
        private Renderer renderer;

        //ここから下追加
        private int num;
        private float num2;//角煮用
        private SceneName nextScene;

        private ParticleEmitter emitter;
        private Vector2 vector;
        private float scale, scale2;
        //ここから上追加

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GameTitle()
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();

            //レンダラー生成
            renderer = gameDevice.GetRenderer();

            //ここから下追加
            nextScene = SceneName.GamePlay;
            emitter = new ParticleEmitter();
            scale = 0.01f;
            vector = new Vector2(scale);
            //ここから上追加

        }
        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("Title", Vector2.Zero);
            #region　数字//角煮用
            if ((int)num2 / 1000 <= 9 && (int)num2 / 1000 >= 1)
            {
                renderer.DrawNumber("number", Vector2.Zero, num2.ToString("f2"), 7);
            }
            if ((int)num2 / 100 <= 9 && (int)num2 / 100 >= 1)
            {
                renderer.DrawNumber("number", Vector2.Zero, num2.ToString("f2"), 6);
            }
            if ((int)num2 / 10 <= 9&& (int)num2 / 10 >= 1)
            {
                renderer.DrawNumber("number", Vector2.Zero, num2.ToString("f2"), 5);
            }
            renderer.DrawNumber("number", Vector2.Zero, num2.ToString("f2"), 4);
            #endregion
            emitter.Draw(renderer);
            renderer.DrawTexture("Circle", new Vector2(450), 0.4f);
            renderer.DrawTexture("Circle", new Vector2(scale2), vector);

            renderer.End();
        }

        public void Initialize()
        {
            num = 0;
            num2 = 0.0f;//角煮用
            nextScene = SceneName.GamePlay;
            isEndFlag = false;

            scale = 0.01f;
            scale2 = 500;
            vector = new Vector2(scale);
        }

        /// <summary>
        /// シーンが終了かどうか
        /// </summary>
        /// <returns>シーン終了ならtrue</returns>
        public bool IsEnd()
        {
            return isEndFlag;
        }

        public SceneName Next()
        {
            return nextScene;
        }

        public void Shutdown()
        {

        }

        public void Update(GameTime gameTime)
        {
            if (Input.GetKeyState(Keys.Space))
            {
                scale += 0.01f;
                scale2 -= 0.5f;
            }
            if (Input.GetKeyRelease(Keys.Space))
            {
                scale = 0.01f;
                scale2 = 500;
            }
            vector = new Vector2(scale);

            sound.PlayBGM("VoLoN BGM");
            num2 += 1/60f;//角煮用
            if (Input.GetKeyState(Keys.Space))
            {
                num++;
            }
            if (Input.GetKeyRelease(Keys.Space) && num <= 10)
            {
                isEndFlag = true;
            }
            if (Input.GetKeyState(Keys.Space) && num >= 100)
            {
                nextScene = SceneName.Tutorial;
                isEndFlag = true;
                return;
            }
            else if (Input.GetKeyRelease(Keys.Space))
            {
                num = 0;
            }

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Input.GetKeyTrigger(Keys.A))
            {
                float scale = 0.5f;
                float shrinkRote = 0.3f;
                int speed = 200;
                emitter.Emit("Player", new Vector2(60, 60),
                    new Vector2(500,300),
                    scale, shrinkRote, 1f, 1, speed, Color.Blue);
            }

            emitter.Update(delta);
        }
    }
}
