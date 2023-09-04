using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Button OriginBtn;
    public Button PvPBtn;
    public Button GroupBtn;
    
    // Start is called before the first frame update
    void Start()
    {
        OriginBtn.onClick.AddListener(delegate() 
        {

            SceneManager.LoadScene("IfElse_HellScene");
        
        });

        PvPBtn.onClick.AddListener(delegate() 
        {


            SceneManager.LoadScene("P2P_HP_PigGameScene");


        });

        GroupBtn.onClick.AddListener(delegate() 
        {


            SceneManager.LoadScene("Group_HP_Pigame");

        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
