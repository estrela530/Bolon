using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Volon.Device;
using Microsoft.Xna.Framework.Input;
using Volon.Actor;

namespace Volon.Scene
{
    /// <summary>
    /// エンディングクラス
    /// </summary>
    class GameEnding : IScene
    {
        private bool IsEndFlag;//終了フラグ
        private bool on;

        private SceneName nextScene;

        private Random rnd;
        private ParticleEmitter emitter;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GameEnding()
        {
            IsEndFlag = false;
            on = false;

            nextScene = SceneName.GamePlay;
            emitter = new ParticleEmitter();

        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            renderer.Begin();

            renderer.DrawTexture("Ending", Vector2.Zero);
            emitter.Draw(renderer);

            renderer.End();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            IsEndFlag = false;
            on = false;
        }

        /// <summary>
        /// シーン終了か？
        /// </summary>
        /// <returns>シーン終了してたらtrue</returns>
        public bool IsEnd()
        {
            return IsEndFlag;
        }

        /// <summary>
        /// 次のシーンへ
        /// </summary>
        /// <returns>次のシーン名</returns>
        public SceneName Next()
        {
            return SceneName.GameTitle;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Shutdown()
        {
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public void Update(GameTime gameTime)
        {
            if (Input.GetKeyTrigger(Keys.Space))
            {
                on = true;
            }
            if (Input.GetKeyRelease(Keys.Space)&&on==true)
            {
                IsEndFlag = true;
            }

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Input.GetKeyTrigger(Keys.A))
            {
                float scale = 0.2f;
                float shrinkRote = 0.3f;
                int speed = 200;
                emitter.Emit("Player", new Vector2(60, 60),
                    new Vector2(500, 300),
                    scale, shrinkRote, 0.7f, 10, speed, Color.Blue);
            }

            emitter.Update(delta);
        }
    }
}
