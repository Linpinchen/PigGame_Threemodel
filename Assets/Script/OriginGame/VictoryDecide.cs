using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using OriginPlayer;

namespace OriginVictoryDecide 
{
    public class VictoryDecide : PlayerControl
    {
        int _DiceTempi;
        List<Player> Players;
        Player NowPlayer;
       
        Action Rest_Action;
        Text WinnerText;
        int RoundCount;
        bool OpenRest;


        public VictoryDecide ( Text WinnerText, string[] Names) 
        {
            Players = new List<Player>();

            for (int i = 0; i < Names.Length; i++)
            {
                Players.Add(new Player(Names[i], ref Rest_Action));

            }


            
            this.WinnerText = WinnerText;


        }


        public void VictoryDecide_FC() 
        {

            if (NowPlayer.Fraction >= 100)
            {
                OpenRest = true;
                WinnerText.text = $"贏家:{ NowPlayer.PlayerName }";

            }
            else
            {
                PlayerChange();
            }
        }

        #region 玩家互換
        /// <summary>
        ///  玩家互換
        /// </summary>
        public void PlayerChange()
        {

            Debug.Log("執行PlayerChange");

            if (RoundCount != Players.Count - 1)
            {

                RoundCount++;
            }
            else
            {
                RoundCount = 0;
            }



            if (NowPlayer.Fraction >= 100)
            {
                for (int i = 0; i < Players.Count; i++)
                {
                    Players[i].Fraction = 0;

                }

            }

            NowPlayer = Players[RoundCount];


            //TempFraction_text.text = "暫存分數：";


        }
        #endregion

        public int[] ReSet()
        {
            RoundCount = 0;
            Rest_Action();
            NowPlayer = Players[RoundCount];
            int[] Fractions = new int[Players.Count];
            for (int i = 0; i < Players.Count; i++)
            {

                Fractions[i] = Players[i].Fraction;

            }
            return Fractions;
        }

        public string GetPlayerName()
        {
            return NowPlayer.PlayerName;
        }

        public int SetPlayerFraction(ref int NowRound)
        {
            
            NowRound = RoundCount;

            NowPlayer.Fraction += _DiceTempi;
            _DiceTempi = 0;

            return NowPlayer.Fraction;

        }

        public bool GetRestVAlue()
        {
            if (NowPlayer.Fraction >= 100)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public int GetTempValue(int Tempi)
        {

            if (Tempi == 1)
            {
                _DiceTempi = 0;
                PlayerChange();
                Debug.Log($"骰到1後 : 執行PlayerChange , NowPlayer 的 playerName是 {NowPlayer.PlayerName}");
                return _DiceTempi;
            }
            else
            {
                _DiceTempi += Tempi*10;
                //NowPlayer.Fraction += _DiceTempi*10;
                return _DiceTempi;

            }



        }

        public void OpenRestImage(GameObject OpenRestImage) 
        {
            if (OpenRest)
            {
                OpenRestImage.gameObject.SetActive(true);

                OpenRest = false;
            }

        }
    }


}
