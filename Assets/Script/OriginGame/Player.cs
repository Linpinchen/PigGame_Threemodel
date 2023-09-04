using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OriginPlayer
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


       
        public Player(string PlayerName, ref System.Action action)
        {
            Fraction = 0;
            this.PlayerName = PlayerName;
            action += RestFraction;
        }

        /// <summary>
        /// 重置玩家分數
        /// </summary>
        public void RestFraction()
        {

            Fraction = 0;

        }

    }


}

