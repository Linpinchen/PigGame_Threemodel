using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using HpPlayer;


namespace P2PHPVictoryDecide 
{
    public class VictoryDecide2 : PlayerControl
    {
        int _DiceTempi;

        List<Player> Players;
        Player NowPlayer;
        Action Rest_Action;
        Text WinnerText;
        int RoundCount;
        bool OpenRest;




        public VictoryDecide2 ( Text WinnerText,string[] Names,Image[] Hpimage) 
        {
            
            this.WinnerText = WinnerText;

            Players = new List<Player>();

            for (int i=0;i<Names.Length;i++)
            {
                Players.Add(new Player(Names[i],ref Rest_Action, Hpimage[i]));
            
            }

            SetEnemy();

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

        public void VictoryDecide_FC() 
        {
            Debug.Log("執行VictoryDecide_FC() ");

            Debug.Log($" VictoryDecide_FC, NowPlayer : {NowPlayer.PlayerName}");

            Debug.Log($" VictoryDecide_FC, NowPlayer.Enemy : {NowPlayer.Enemy.PlayerName}");


                if (NowPlayer.Fraction >= 100)
                {
                    OpenRest = true;
                    Debug.Log($"{NowPlayer.PlayerName}獲勝");

                    WinnerText.text = $"贏家:{NowPlayer.PlayerName }";
                    NowPlayer = Players[0];
                    Debug.Log($" NowPlayer : {NowPlayer.PlayerName}的 Fraction :{NowPlayer.Fraction} ");
                    
                    
                }
                else
                {

                    PlayerChange();
                    //manager.PlayerChange();
                    Debug.Log($"當前NowPlayer是誰{NowPlayer.PlayerName}");
                }

           
        }

        /// <summary>
        /// 取得當前玩家是否分數超過100,事就重制 顯示的分數文字
        /// </summary>
        /// <returns></returns>
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

        public int SetPlayerFraction(ref int NowRound) 
        {

            NowRound = RoundCount;
            NowPlayer.Fraction += _DiceTempi;
            _DiceTempi = 0;

            return NowPlayer.Fraction;


        }

        public int GetTempValue(int Tempi) 
        {

            if (Tempi == 1)
            {
                _DiceTempi = 0;

                if (NowPlayer.HP > 0)
                {
                    Debug.Log($"NowPlayer 的血量 :{NowPlayer.HP}");

                    NowPlayer.HP -= 1;

                    Debug.Log($"NowPlayer 扣血後的血量 :{NowPlayer.HP}");


                    float temp = (((100 / 5) * 0.01f) * NowPlayer.HP);

                    NowPlayer.HPimage.rectTransform.localScale = new Vector3(temp, 1, 1);

                    if (NowPlayer.HP <= 0)
                    {
                        OpenRest = true;
                        Debug.Log($"{NowPlayer.Enemy.PlayerName}獲勝");

                        WinnerText.text = $"贏家:{NowPlayer.Enemy.PlayerName }";
                        NowPlayer = Players[0];
                    }
                    else
                    {

                        PlayerChange();
                        Debug.Log($"執行PlayerChange後當前玩家 : {NowPlayer.PlayerName},敵人血量:{NowPlayer.Enemy.HP }");
                    }

                }

                Debug.Log($"骰到1後 : 執行PlayerChange , NowPlayer 的 playerName是 {NowPlayer.PlayerName}");
                return _DiceTempi;
            }
            else 
            {
                _DiceTempi += Tempi * 10;
                return _DiceTempi;
            
            }
           

        
        }


        public int[] ReSet() 
        {
            RoundCount = 0;
            Rest_Action();
            NowPlayer = Players[RoundCount];
            int[] Fractions=new int[Players.Count];
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

        public void SetEnemy()
        {

            for (int i = 0; i < Players.Count; i++)
            {
                if (i < Players.Count - 1)
                {
                    Players[i].GetEnemy(Players[i + 1]);

                }
                else
                {
                    Players[i].GetEnemy(Players[0]);
                }

                Debug.Log($"{ Players[i].PlayerName}的敵人{ Players[i].Enemy.PlayerName}");

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
