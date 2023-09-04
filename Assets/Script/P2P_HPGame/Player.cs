using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace HpPlayer
{
    /// <summary>
    ///角色模板 
    /// </summary>
    public class Player
    {
        /// <summary>
        /// 玩家分數
        /// </summary>
        public int Fraction;

        /// <summary>
        /// 玩家名稱
        /// </summary>
        public string PlayerName;


        public int HP;

        public Player Enemy;

        public Image HPimage;

        public Player(string PlayerName, ref System.Action action, Image HPimage)
        {
            Fraction = 0;
            HP = 5;
            this.PlayerName = PlayerName;
            this.HPimage = HPimage;
            action += RestFraction;

        }

        public void GetEnemy(Player Enemy) 
        {
            this.Enemy = Enemy;
        
        }

        /// <summary>
        /// 重置玩家分數
        /// </summary>
        public void RestFraction()
        {

            Fraction = 0;
            HP = 5;
            HPimage.rectTransform.localScale = new Vector3(1,1,1);
        }

    }


}

