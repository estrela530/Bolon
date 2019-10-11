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
    class GamePlay : IScene,IGameMediator
    {
        //フィールド
        // 終了しているかどうか
        private bool isEndFlag;
        // サウンド
        private Sound sound;

        // レンダラー
        private Renderer renderer;
        private GameDevice gameDevice;

        //Playerクラス
        private Player player;

        //IgameMediator
        private IGameMediator igameMediator;

        public GamePlay()
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();

            //レンダラー生成
            renderer = gameDevice.GetRenderer();
            player = new Player(igameMediator);
        }
        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("GamePlaySmall",Vector2.Zero);
            player.Draw(renderer);
            renderer.End();
        }

        public void Initialize()
        {
            Player player = new Player(this);
            isEndFlag = false;
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
        }
    }
}
