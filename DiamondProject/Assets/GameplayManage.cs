using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManage : MonoBehaviour {

    [SerializeField] Text RollText;
    [SerializeField] Text playerturn;
    [SerializeField] Text[] PlayerText = new Text[2];
    [SerializeField] int[] PlayerDice = new int[2];
    [SerializeField] int DiceRoll;
    [SerializeField] bool goRoll,End;
    [SerializeField] int Player;
    float timer = 2;
    int thewinner;
    [SerializeField] GameManager Manage;

	// Use this for initialization
	void Start () {
        Manage = GetComponent<GameManager>();
        Player = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (!End)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                doRoll();
            }


            if (Player == 0)
            {
                Player = 0;
                playerturn.text = "Player 1 Turn";
            }
            else if (Player == 1)
            {
                playerturn.text = "Player 2 Turn";
                Player = 1;
            }

            if (goRoll)
            {
                Debug.Log("rollstart");
                timer = timer - Time.deltaTime;
                DiceRoll = Random.Range(1, 6);
                RollText.text = "" + DiceRoll;
                if (timer <= 0)
                {
                    RollText.text = "" + DiceRoll;
                    goRoll = false;
                    timer = 2;
                    updatePlayer(Player, DiceRoll);
                }
            }

        }

    }

    void doRoll()
    {
            goRoll = true;
    }

    void updatePlayer(int Turn, int Dice)
    {
        PlayerDice[Turn] = Dice;
        PlayerText[Turn].text = "" + PlayerDice[Turn];
        Player += 1;
        if (Player>1)
        {
            End = true;

            if (PlayerDice[0] > PlayerDice[1])
            {
                thewinner = 1;
            }
            else if(PlayerDice[1] > PlayerDice[0])
            {
                thewinner = 2;
            }
            else
            {
                thewinner = 0;
            }
            theWinnerOFGame(thewinner);

        }


    }
    public void GoingRoll()
    {
        goRoll = true;
    }

    void theWinnerOFGame(int x)
    {
        if(x == 0)
        {
            RollText.text = "Tie";

        }
        else
        {
            RollText.text = "PLayer " + x + " Won";

        }
    }

}
