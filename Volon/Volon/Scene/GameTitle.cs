﻿using System;
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
        private float num2;//角煮用
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
            nextScene = SceneName.GamePlay;
            //ここから上追加

        }
        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("Title", Vector2.Zero);
            //うんち

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

            renderer.End();
        }

        public void Initialize()
        {
            num = 0;
            num2 = 0.0f;//角煮用
            nextScene = SceneName.GamePlay;
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
            return nextScene;
        }

        public void Shutdown()
        {

        }

        public void Update(GameTime gameTime)
        {
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
            if (Input.GetKeyRelease(Keys.Space) && num >= 60)
            {
                nextScene = SceneName.Tutorial;
                isEndFlag = true;
                return;
            }
            else if (Input.GetKeyRelease(Keys.Space))
            {
                num = 0;
            }
        }
    }
}
