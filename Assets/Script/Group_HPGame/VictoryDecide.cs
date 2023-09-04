using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using GroupHpPlayer;


namespace GroupHPVictoryDecide 
{
    public class VictoryDecide : PlayerControl
    {
        int _DiceTempi;

        List<List<Player>> Players;
        Player NowPlayer;
        int[] GroupFraction;
        Action Rest_Action;
        Text WinnerText;
        int RoundCount;//回和數

        int TotalPlayerCount;

        bool OpenRest;

        int NowGroup;//現在的組別

        int NowPlayerCount;//現在是 Players的 第幾元素的成員

       List<List<bool>> HasHP;//判斷是否有血

        public VictoryDecide ( int GroupCount,Text WinnerText,string[] Names,Image[] Hpimage) 
        {
            
            this.WinnerText = WinnerText;

            Players = new List<List<Player>>();
            HasHP = new List<List<bool>>();
            for (int i=0;i<GroupCount;i++) 
            {
                Players.Add(new List<Player>());
                HasHP.Add(new List<bool>());
               
            }

            int Group = Names.Length / GroupCount;
            for (int i=0;i<Names.Length;i++)
            {
                if (i<Group) 
                {
                    Players[0].Add(new Player(Names[i], ref Rest_Action, Hpimage[i], "Group1"));
                    HasHP[0].Add(true);
                }
                else 
                {
                    Players[1].Add(new Player(Names[i], ref Rest_Action, Hpimage[i],"Group2"));
                    HasHP[1].Add(true);
                }
               
            }


            GroupFraction = new int[GroupCount];

            TotalPlayerCount = Names.Length;

            SetEnemy();

        }



        #region 玩家互換
        /// <summary>
        ///  玩家互換
        /// </summary>
        public void PlayerChange()
        {

            //Debug.Log("執行PlayerChange");

            //if (RoundCount != TotalPlayerCount-1)
            //{
            //    RoundCount++;
            //    //if (!HasHP[NowGroup][NowPlayerCount]) 
            //    //{

            //    //    RoundCount++;
            //    //    if (RoundCount>= TotalPlayerCount - 1) 
            //    //    {

            //    //        RoundCount = 0;
                    
            //    //    }
                
            //    //}

            //}
            //else
            //{
            //    RoundCount = 0;
               
            //}

            //NowGroup = GetNowGroup();

            //if (!HasHP[NowGroup][NowPlayerCount]) 
            //{

            //    PlayerChange();

               
            //}


            //if (GroupFraction[NowGroup] >= 100)
            //{

            //    for (int i = 0; i < Players.Count; i++)
            //    {
            //        for (int j = 0; j < Players[i].Count; j++)
            //        {
            //            Players[i][j].Fraction = 0;

            //        }


            //    }

            //}

            //NowPlayer = Players[NowGroup][NowPlayerCount];

            //Debug.Log($"當前玩家: {NowPlayer.PlayerName}");

            //TempFraction_text.text = "暫存分數：";

            do
            {

                Debug.Log("執行PlayerChange");

                if (RoundCount != TotalPlayerCount - 1)
                {
                    RoundCount++;
                 
                }
                else
                {
                    RoundCount = 0;

                }

                NowGroup = GetNowGroup();

                NowPlayer = Players[NowGroup][NowPlayerCount];

                Debug.Log($"當前玩家: {NowPlayer.PlayerName}");

            }
            while (!HasHP[NowGroup][NowPlayerCount]);
        }

        #endregion

        public void VictoryDecide_FC() 
        {
            Debug.Log("執行VictoryDecide_FC() ");

            Debug.Log($" VictoryDecide_FC, NowPlayer : {NowPlayer.PlayerName}");

            Debug.Log($" VictoryDecide_FC, NowPlayer.Enemy : {NowPlayer.Enemy.PlayerName}");

            //NowGroup = GetNowGroup();

            GroupFraction[NowGroup] += NowPlayer.Fraction;

                if (GroupFraction[NowGroup] >= 100)
                {

                    for (int i = 0; i < Players.Count; i++)
                    {
                        for (int j = 0; j < Players[i].Count; j++)
                        {
                            Players[i][j].Fraction = 0;

                        }

                    }

                    OpenRest = true;
                    Debug.Log($"{NowPlayer.GroupName}獲勝");

                    WinnerText.text = $"贏家:{NowPlayer.GroupName }";
                    NowPlayer = Players[0][0];
                    Debug.Log($" NowPlayer : {NowPlayer.PlayerName}的 Fraction :{NowPlayer.Fraction}組別總分數{GroupFraction[NowGroup]} ");
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
            if (GroupFraction[NowGroup] >= 100)
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

            NowRound =NowGroup;
            GroupFraction[NowGroup] += _DiceTempi;
            _DiceTempi = 0;

            return GroupFraction[NowGroup];


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
                       
                        int NowGroup = GetNowGroup();

                        HasHP[NowGroup][NowPlayerCount] = false;
                        Debug.Log($"組別:{NowGroup},當前玩家:{NowPlayer.PlayerName},是否沒血{HasHP[NowGroup][NowPlayerCount]}");

                        if (!HasHP[NowGroup].Contains(true))
                        {
                           
                            OpenRest = true;
                            Debug.Log($"{NowPlayer.Enemy.GroupName}獲勝");
                            WinnerText.text = $"贏家:{NowPlayer.Enemy.GroupName }";
                        }
                        else 
                        {

                            PlayerChange();

                        }


                    }
                    else
                    {

                        PlayerChange();
                        Debug.Log($"執行PlayerChange後當前玩家 : {NowPlayer.PlayerName},血量:{NowPlayer.HP }");
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
            NowPlayer = Players[0][RoundCount];
            int[] Fractions=new int[GroupFraction.Length];
            for (int i = 0; i < Players.Count; i++)
            {
                GroupFraction[i]=0;
                Fractions[i] = GroupFraction[i];
                for (int j=0;j<(TotalPlayerCount/Players.Count);j++) 
                {
                    
                    HasHP[i][j] = true;

                }

            }
            NowGroup = 0;
            NowPlayerCount = 0;

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
                for (int j=0;j<Players[i].Count;j++) 
                {

                    if (i < Players.Count - 1)
                    {
                        Players[i][j].GetEnemy(Players[i + 1][j]);

                    }
                    else
                    {
                        Players[i][j].GetEnemy(Players[i - 1][j]);
                    }

                    Debug.Log($"{ Players[i][j].PlayerName}的敵人{ Players[i][j].Enemy.PlayerName}");

                }
               

            }

        }
        /// <summary>
        /// 取得當前組別
        /// </summary>
        /// <returns></returns>
        int GetNowGroup() 
        {
            NowPlayerCount = (RoundCount / Players.Count);

            int ret = RoundCount % Players.Count;

            return ret;

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
