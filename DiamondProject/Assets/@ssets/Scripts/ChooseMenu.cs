using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class ChooseMenu : MonoBehaviour {

    [SerializeField] tweener[] btn;

    public int val = 0,batasAtas,BatasBawah;
    int plus;
    
    public bool change = false;


    [System.Serializable]
    public class tweener
    {
        public Transform obj;
        public Vector3 posisiAkhir;
        public Vector3 posisiAwal;
        public float waktu = 0.2f;
        public float delay = 0;
        public Ease ease = Ease.OutBack;
    }
    void Start()
    {
        val = 0;
        for (int i = 0; i < btn.Length; i++)
        {
            if (btn[i].ease == Ease.Unset)
            {
                btn[i].ease = Ease.OutBack;
                
            }
            btn[i].obj.DOMove(btn[i].posisiAwal, btn[i].waktu).SetDelay(btn[i].delay).SetEase(btn[i].ease);
            
           
        }
        //change = true;

        for (int i = 0; i < btn.Length; i++)
        {
            btn[i].obj.DOScale(0.95f, .9f).SetLoops(-1, LoopType.Yoyo);
        }

        
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
           
            val += -plus;
            change = true;
            if(val == 0)
            {
                val = 4;
            }
        }
        if (Input.GetAxis("Vertical") < 0)
        {
           
           val += plus; 
            change = true;

        }

        if (val > batasAtas)
        {
            val = 1;
        }
        //if (val < BatasBawah)
        //{
        //    val = BatasBawah;
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            shake();
            //    string inScene = "";
            //    if (val == 1)
            //    {
            //        inScene = "choseChara";
            //    }
            //    if (val == 2)
            //    {
            //        inScene = "Setting";
            //    }
            //    if (val == 3)
            //    {
            //        inScene = "Credit";
            //    }
            //    if (val == 4)
            //    {
            //        inScene = "Exit";
            //    }
            //    choseMenu(inScene);
        }

        if (Input.GetAxis("Vertical") == 0)
        {
            change = false;
        }

        if (change == true)
        {
            movingBtn();
            plus = 0;
        }
        else
        {
            plus = 1;

        }

        
       
    }

    void shake()
    {
        for (int i = 0; i < btn.Length; i++)
        {
            btn[i].obj.DOShakeScale(1, 1, 10, 90, false);
        }
    }
    public void choseMenu(string scene)
    {
        if (scene != "Exit")
        {
            Application.Quit();
        }
        else {
            SceneManager.LoadScene(scene);

        }
    }

    void movingBtn()
    {
        
        for(int i = 1;i<5; i++ )
        {
            if (i == val)
            {
                btn[i].obj.DOMove(btn[i].posisiAkhir, 0.2f);
            }
            else
            {
                btn[i].obj.DOMove(btn[i].posisiAwal, 0.2f);
            }
            
        }

    }
}
