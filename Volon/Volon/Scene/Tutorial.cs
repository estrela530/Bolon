using System;
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
    class Tutorial : IScene
    {
        //フィールド
        // 終了しているかどうか
        private bool isEndFlag;
        // サウンド
        private Sound sound;

        // レンダラー
        private Renderer renderer;
        //Playerクラス
        private Player player;
        //IgameMediator
        private IGameMediator igameMediator;
        private BlockManager blockManager;
        private int back, back2, back3, back4, back5, back6;
        private int flameTime, interval;
        private float num = 0;

        public Tutorial()
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
            renderer = gameDevice.GetRenderer();
            player = new Player(igameMediator);
            blockManager = new BlockManager();
        }
        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            //renderer.DrawTexture("Tutorial", Vector2.Zero);
            #region　背景
            renderer.DrawTexture("background1", new Vector2(back, 0));
            renderer.DrawTexture("background1", new Vector2(back4, 0));
            renderer.DrawTexture("cloud", new Vector2(back2, 0), 0.5f);
            renderer.DrawTexture("cloud", new Vector2(back5, 0), 0.5f);
            renderer.DrawTexture("cloud2", new Vector2(back3, 0), 0.3f);
            renderer.DrawTexture("cloud2", new Vector2(back6, 0), 0.3f);
            #endregion 
            blockManager.Draw(renderer);
            renderer.DrawTexture("しらす", Vector2.Zero, num);
            renderer.End();
        }

        public void Initialize()
        {
            Player player = new Player(igameMediator);
            isEndFlag = false;
            blockManager = new BlockManager();//ブロック管理者を生成
            blockManager.Add(player);

            blockManager.Add(new NormalBlock(new Vector2(200, 550), igameMediator));//テンプレート
            blockManager.Add(new NormalBlock(new Vector2(450, 350), igameMediator));//佐々木

            flameTime = 0;
            #region 背景
            back = 0;
            back2 = 0;
            back3 = 0;
            back4 = 1280;
            back5 = 1280;
            back6 = 1280;
            #endregion
        }

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
            if (flameTime == 1)
            {
                num = 0.4f;
                if (Input.GetKeyTrigger(Keys.D))
                {
                    player.Update(gameTime);
                    flameTime++;
                    num = 0;
                    return;
                }
                return;
            }
            if (flameTime == 100)
            {
                num = 0.4f;
                if (Input.GetKeyTrigger(Keys.D))
                {
                    player.Update(gameTime);
                    flameTime++;
                    num = 0;
                    return;
                }
                return;
            }
            player.Update(gameTime);
            blockManager.Update(gameTime);
            flameTime++;
            //if (Input.GetKeyTrigger(Keys.D))
            //{
            //    isEndFlag = true;
            //}
            if (Input.GetKeyState(Keys.Space))
            {
                interval++;
                if (interval >= 100)
                {
                    isEndFlag = true;
                }
            }
            if (Input.GetKeyRelease(Keys.Space))
            {
                interval = 0;
            }
            #region 背景
            back -= 1;
            if (back <= -1280)
            {
                back = 0;
            }
            back2 -= 3;
            if (back2 <= -1280)
            {
                back2 = 0;
            }
            back3 -= 2;
            if (back3 <= -1280)
            {
                back3 = 0;
            }
            back4 -= 1;
            if (back4 <= 0)
            {
                back4 = 1280;
            }
            back5 -= 3;
            if (back5 <= 0)
            {
                back5 = 1280;
            }
            back6 -= 2;
            if (back6 <= 0)
            {
                back6 = 1280;
            }
            #endregion
        }
    }
}
