using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectChara : MonoBehaviour {

    public int thisPlayer,value,plus;
    public string Xaxis,Yaxis,selectBtn, cancelBtn;
    public static bool change;
    
    // Use this for initialization
    void Start () {
        
        //a = ChooseManager.value;
        if (thisPlayer == 1)
        {
            Xaxis = "HorizontalP1";
            Yaxis = "VerticalP1";
            cancelBtn = "joystick " + thisPlayer + " button 1";
            selectBtn = "joystick " + thisPlayer + " button 0";
        }
        if (thisPlayer == 2)
        {
            Xaxis = "HorizontalP2";
            Yaxis = "VerticalP2";
            cancelBtn = "joystick " + thisPlayer + " button 1";
            selectBtn = "joystick " + thisPlayer + " button 0";
        }
        if (thisPlayer == 3)
        {
            Xaxis = "HorizontalP3";
            Yaxis = "VerticalP2";
            cancelBtn = "joystick " + thisPlayer + " button 1";
            selectBtn = "joystick " + thisPlayer + " button 0";
        }
        if (thisPlayer == 4)
        {
            Xaxis = "HorizontalP4";
            Yaxis = "VerticalP4";
            cancelBtn = "joystick " + thisPlayer + " button 1";
            selectBtn = "joystick " + thisPlayer + " button 0";
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis(Yaxis) > 0)
        {
            plus = 6;

            value += -plus;
            change = true;
            //if (value == 0)
            //{
            //    value = 4;
            //}
        }
        if (Input.GetAxis(Yaxis) < 0)
        {
            plus = 6;
            value += plus;
            change = true;

        }
        if (Input.GetAxis(Xaxis) > 0)
        {
            plus = 1;
            value += plus;
            change = true;
            //ChooseManager.changeValue();
            // ChooseManager.hori();
        }
        if (Input.GetAxis(Xaxis) < 0)
        {
            plus = 1;
            value += -plus;
            change = true;
            // ChooseManager.hori();
        }
        if (change == true)
        {
           // movingBtn();
            plus = 0;
        }
        else
        {
           // plus = 1;

        }

    }
}
