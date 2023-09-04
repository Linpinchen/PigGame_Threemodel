﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using OriginVictoryDecide;
using OriginPlayer;


namespace OriginGame 
{
    public class Manager : MonoBehaviour
    {
        public Text NowPlayer_text;
        public Text TempFraction_text;
        public Text[] PlayersFraction_text;
        public Text WinnerText;
        public Button RollBtn;
        public Button HoldBtn;
        public Button Rest_Btn;
        public Image Diceimage;
        public Image Restimage;
        public Sprite[] Dice_Sprites;
        public Button MenuBtn;

        string[] PlayerNames;
        int Tempi;
        int RoundCount;
        Tweener tweener;
        IEnumerator enumerator;
        PlayerControl _PlayerControl;

        // Start is called before the first frame update
        void Start()
        {



            init();

            RollBtn.onClick.AddListener(delegate ()
            {
                RollBtn.interactable = false;
                HoldBtn.interactable = false;

                int TempDice = Random.Range(1, Dice_Sprites.Length + 1);

                enumerator = DiceRoll(TempDice);

                StartCoroutine(enumerator);

            });



            HoldBtn.onClick.AddListener(delegate ()
            {
                //Debug.Log($"NowPlateR的名子:{NowPlayer.PlayerName}");
                int PlayersFraction = _PlayerControl.SetPlayerFraction(ref RoundCount);
                PlayersFraction_text[RoundCount].text = PlayersFraction.ToString();
                bool isB = _PlayerControl.GetRestVAlue();
                if (isB)
                {
                    for (int i = 0; i < PlayersFraction_text.Length; i++)
                    {
                        PlayersFraction_text[i].text = "0";
                    }

                }
                Tempi = 0;
                _PlayerControl.VictoryDecide_FC();
                _PlayerControl.OpenRestImage(Restimage.gameObject);
                NowPlayer_text.text = "當前玩家: " + _PlayerControl.GetPlayerName();
                TempFraction_text.text = "暫存分數: " + Tempi;
            });


            Rest_Btn.onClick.AddListener(delegate ()
            {

                Rest();

            });


            MenuBtn.onClick.AddListener(delegate() 
            {

                SceneManager.LoadScene("PigGameMenu_Scene");
            
            
            });



        }


        #region 遊戲初始設定
        /// <summary>
        /// 遊戲初始設定
        /// </summary>
        void init()
        {

            PlayerNames = new string[] { "PlayerOne", "PlayerTwo" };
            _PlayerControl = new VictoryDecide(  WinnerText, PlayerNames);

         
            tweener = Diceimage.transform.DOShakePosition(10, new Vector3(50, 50, 0));
            tweener.timeScale = 10;
            tweener.SetAutoKill(false);
            tweener.Pause();


            Rest();
        }
        #endregion

        #region 遊戲重置設定
        /// <summary>
        /// 遊戲重置設定
        /// </summary>
        void Rest()
        {
            Restimage.gameObject.SetActive(false);

            Tempi = 0;

            //RoundCount = 0;

            int[] Fractions = _PlayerControl.ReSet();
            for (int i = 0; i < Fractions.Length; i++)
            {

                PlayersFraction_text[i].text = Fractions[i].ToString();

            }

            string NowPlayerName = _PlayerControl.GetPlayerName();

            NowPlayer_text.text = $"當前玩家 ：{ NowPlayerName}";

            TempFraction_text.text = $"暫存分數：{Tempi}";



        }
        #endregion

       

        #region 搖骰
        /// <summary>
        /// 搖骰
        /// </summary>
        /// <param name="Temp"></param>
        /// <returns></returns>
        IEnumerator DiceRoll(int Temp)
        {

            Diceimage.transform.DORestart();

            for (int t = 0; t < 20; t++)
            {
                int i = Random.Range(0, Dice_Sprites.Length);
                Diceimage.sprite = Dice_Sprites[i];
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(0.05f);

            Diceimage.sprite = Dice_Sprites[Temp - 1];
          
           

            TempFraction_text.text = $"暫存分數: {_PlayerControl.GetTempValue(Temp)}";
            NowPlayer_text.text = "當前玩家: " + _PlayerControl.GetPlayerName();
            //Debug.Log($"骰到1後 : 執行PlayerChange , NowPlayer 的 playerName是 {_PlayerControl.GetPlayerName()}");



            RollBtn.interactable = true;
            HoldBtn.interactable = true;

        }



    }



}
#endregion
