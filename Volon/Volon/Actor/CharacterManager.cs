using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Volon.Device;

namespace Volon.Actor
{
    class CharacterManager
    {
        private List<Character> players;
        private List<Character> enemys;
        private List<Character> addNewcharacters;

        public CharacterManager()
        {
            Initialize();
        }


        public void Initialize()
        {
            if (players != null)
            {
                players.Clear();
            }
            else
            {
                players = new List<Character>();
            }          
            if (addNewcharacters != null)
            {
                addNewcharacters.Clear();
            }
            else
            {
                addNewcharacters = new List<Character>();
            }
        }
        public void Add(Character character)
        {
            if (character == null)
            {
                return;
            }
            addNewcharacters.Add(character);
        }
        private void HitToCharacters()
        {
            foreach (var player in players)
            {
                foreach (var enemy in enemys)
                {
                    if (player.IsDead() || enemy.IsDead())
                    {
                        continue;
                    }
                    if (player.IsCollision(enemy))
                    {
                        player.Hit(enemy);
                        enemy.Hit(player);
                    }
                }
            }
        }
        private void RemoveDeadCharacters()
        {
            players.RemoveAll(p => p.IsDead());
            enemys.RemoveAll(e => e.IsDead());
        }
        public void Update(GameTime gameTime)
        {
            foreach (var p in players)
            {
                p.Update(gameTime);
            }
            foreach (var e in enemys)
            {
                e.Update(gameTime);
            }
            foreach (var newChara in addNewcharacters)
            {
                if (newChara is Player)
                {
                    newChara.Initialize();
                    players.Add(newChara);
                }
                
                else
                {
                    newChara.Initialize();
                    enemys.Add(newChara);
                }
            }
            addNewcharacters.Clear();

            HitToCharacters();

            RemoveDeadCharacters();
        }
        public void Draw(Renderer renderer)
        {
            foreach (var e in enemys)
            {
                e.Draw(renderer);
            }
            foreach (var p in players)
            {
                p.Draw(renderer);
            }
        }
    }
}
