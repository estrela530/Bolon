using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volon.Device;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

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
        private SceneName nextScene;
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
            num = 0;
            nextScene = SceneName.GamePlay;
            //ここから上追加

        }
        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("Title", Vector2.Zero);
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
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
            return SceneName.GamePlay;
        }

        public void Shutdown()
        {

        }

        public void Update(GameTime gameTime)
        {
            if (Input.GetKeyRelease(Keys.Space))
            {
                isEndFlag = true;
            }
            //if (Input.GetKeyState(Keys.Space))
            //{

            //    //num++;
            //    //if (Input.GetKeyRelease(Keys.Space) && num <= 10)
            //    //{
            //    //    isEndFlag = true;
            //    //}
            //    //if (Input.GetKeyRelease(Keys.Space) && num >= 60)
            //    //{
            //    //    nextScene = SceneName.Tutorial;
            //    //    isEndFlag = true;
            //    //}
            //    //else if(Input.GetKeyRelease(Keys.Space))
            //    //{
            //    //    num = 0;
            //    //}
            //}
        }
    }
}
