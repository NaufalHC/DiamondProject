using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseManager : MonoBehaviour {

    public int val;
    public static bool change;
    public int a;

    public static void changeValue()
    {
        
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        a = val;
        if (change)
        {
            val += 1;
        }
	}


}
