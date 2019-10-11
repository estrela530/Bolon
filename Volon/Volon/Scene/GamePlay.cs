﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Volon.Device;
using Volon.Actor;

namespace Volon.Scene
{
    class GamePlay : IScene, IGameMediator
    {
        //フィールド
        // 終了しているかどうか
        private bool isEndFlag;
        // サウンド
        private Sound sound;

        // レンダラー
        private Renderer renderer;
        //private GameDevice gameDevice;

        //Playerクラス
        private Player player;

        //IgameMediator
        private IGameMediator igameMediator;

        private List<Character> characters;

        //ここから下追加
        private BlockManager blockManager;
        private int back, back2, back3, back4, back5, back6;
        //ここから上追加

        public GamePlay()
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();

            //レンダラー生成
            renderer = gameDevice.GetRenderer();
            player = new Player(igameMediator);
            //ここから下追加
            blockManager = new BlockManager();
            //ここから上追加
        }
        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            //renderer.DrawTexture("GamePlaySmall",Vector2.Zero);
            renderer.DrawTexture("background1", new Vector2(back, 0));
            renderer.DrawTexture("background1", new Vector2(back4, 0));
            renderer.DrawTexture("cloud", new Vector2(back2, 0), 0.5f);
            renderer.DrawTexture("cloud", new Vector2(back5, 0), 0.5f);
            renderer.DrawTexture("cloud2", new Vector2(back3, 0), 0.3f);
            renderer.DrawTexture("cloud2", new Vector2(back6, 0), 0.3f);
            player.Draw(renderer);
            //ここから下追加
            blockManager.Draw();
            //ここから上追加
            renderer.End();
        }

        public void Initialize()
        {
            Player player = new Player(this);
            isEndFlag = false;
            //ここから下追加
            blockManager = new BlockManager();
            blockManager.Add(new NormalBlock(new Vector2(600, 300), igameMediator));//生成確認用

            back = 0;
            back2 = 0;
            back3 = 0;
            back4 = 1280;
            back5 = 1280;
            back6 = 1280;
            //ここから上追加
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public SceneName Next()
        {
            return SceneName.GameEnding;
        }

        public void Shutdown()
        {

        }

        public void Update(GameTime gameTime)
        {
            if (Input.GetKeyTrigger(Keys.Space))
            {
                isEndFlag = true;
            }

            player.Update(gameTime);

            //ここから下追加
            blockManager.Update(gameTime);

            back -= 1;
            if (back <= -1280)
            {
                back = 0;
            }
            back2 -= 2;
            if (back2 <= -1280)
            {
                back2 = 0;
            }
            back3 -= 3;
            if (back3 <= -1280)
            {
                back3 = 0;
            }
            back4 -= 1;
            if (back4 <= 0)
            {
                back4 = 1280;
            }
            back5 -= 2;
            if (back5 <= 0)
            {
                back5 = 1280;
            }
            back6 -= 3;
            if (back6 <= 0)
            {
                back6 = 1280;
            }
            //ここから上追加
        }
    }
}
