using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface PlayerControl
{
    void PlayerChange();
    void VictoryDecide_FC();

    int[] ReSet();

    string GetPlayerName();

    int SetPlayerFraction(ref int NowRound);

    bool GetRestVAlue();

    void OpenRestImage(GameObject OpenRestImage);

    int GetTempValue(int Tempi);

}
