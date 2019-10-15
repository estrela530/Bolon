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
        private int interval, num, num2;
        private Random rnd = new Random();
        private List<int> numbers;
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
            blockManager.Draw(renderer);
            //ここから下追加
            numbers = new List<int>();
            for (int i = 2; i < 6; i++)
            {
                numbers.Add(i);
            }
            //ここから上追加
            renderer.End();
        }

        public void Initialize()
        {
            Player player = new Player(this);
            isEndFlag = false;
            //ここから下追加
            blockManager = new BlockManager();//ブロック管理者を生成
            blockManager.Add(player);
            //blockManager.Add(new NormalBlock(new Vector2(1000, 100), igameMediator));//生成確認用
            //blockManager.Add(new GravityBlock(new Vector2(1000, 200), igameMediator));//生成確認用
            //blockManager.Add(new SpecialBlock(new Vector2(1000, 300), igameMediator));//生成確認用
            //blockManager.Add(new ThornsBlock(new Vector2(1000, 400), igameMediator));//生成確認用

            back = 0;
            back2 = 0;
            back3 = 0;
            back4 = 1280;
            back5 = 1280;
            back6 = 1280;

            //rnd = new Random();//ランダム生成
            interval = 0;
            num2 = 0;
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
            #region ランダム生成
            if (numbers.Count == 0)
            {
                for (int i = 2; i < 6; i++)
                {
                    numbers.Add(i);
                }
            }
            interval++;
            if (interval >= rnd.Next(150, 250) && num2 == 0 ||
                interval >= rnd.Next(150, 250) && num2 == 2)
            {
                num = numbers[rnd.Next(numbers.Count)];
                blockManager.Add(new NormalBlock(new Vector2(1280, (num * 100)), igameMediator));
                numbers.RemoveAll(c => c.ToString().Contains(num.ToString()));
                interval = 0;
                num2+=rnd.Next(0,2);
            }
            if (interval >= rnd.Next(150, 250) && num2 == 1)
            {
                num = numbers[rnd.Next(numbers.Count)];
                blockManager.Add(new ThornsBlock(new Vector2(1280, (num * 100)), igameMediator));
                numbers.RemoveAll(c => c.ToString().Contains(num.ToString()));
                interval = 0;
                num2++;
            }
            if (interval >= rnd.Next(150, 250) && num2 == 3)
            {
                num = numbers[rnd.Next(numbers.Count)];
                blockManager.Add(new GravityBlock(new Vector2(1280, (num * 100)), igameMediator));
                numbers.RemoveAll(c => c.ToString().Contains(num.ToString()));
                interval = 0;
                num2++;
            }
            if (num2==4)
            {
                num = numbers[rnd.Next(numbers.Count)];
                blockManager.Add(new SpecialBlock(new Vector2(1280, (num * 100)), igameMediator));
                numbers.RemoveAll(c => c.ToString().Contains(num.ToString()));
                num2 = 0;
            }
            
            #endregion
            #region 背景
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
            #endregion
            //ここから上追加
            Console.WriteLine("num = " + num);
            //Console.WriteLine("num2 = " + num2);

        }
    }
}
