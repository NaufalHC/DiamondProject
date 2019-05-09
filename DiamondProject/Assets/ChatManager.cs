using Assets.Scripts.ServerShared.Hubs;
using Assets.Scripts.ServerShared.MessagePackObjects;
using Assets.Scripts.ServerShared.Services;
using Grpc.Core;
using MagicOnion.Client;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class ChatManager : MonoBehaviour, IChatHubReceiver
    {
        private IChatHub streamingClient;
        private IChatService client;

        private bool isJoin;

        public Text ChatText;

        public Button JoinOrLeaveButton;

        public Text JoinOrLeaveButtonText;

        public Button SendMessageButton;

        public InputField fieldInput;

        [SerializeField] Text RollText;

        [SerializeField] Text playerturn;

        [SerializeField] Text[] PlayerText = new Text[2];

        [SerializeField] Text[] playerName = new Text[2];

        [SerializeField] int[] PlayerDice = new int[2];

        [SerializeField] int DiceRoll;

        [SerializeField] bool goRoll, End;

        [SerializeField] int Player;

        private float timer = 2;

        private int thewinner;

        private bool startGame = false;

        [SerializeField] GameManager Manage;

        void Start()
        {
            Manage = GetComponent<GameManager>();
            //Player = 0;
            this.InitializeClient();
            this.InitializeUi();
        }

        void Update()
        {
            if (startGame == true)
            {

                if (Player == 0)
                {
                    playerturn.text = "your Turn";

                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        doRoll();
                    }
                }
                else
                {
                    playerturn.text = "enemy Turn";
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
                        SendMessage();
                        updatePlayer(Player, DiceRoll);
                    }
                }

            }
        }


        private void InitializeClient()
        {
            // Initialize the Hub
            var channel = new Channel("localhost:12345", ChannelCredentials.Insecure);
            this.streamingClient = StreamingHubClient.Connect<IChatHub, IChatHubReceiver>(channel, this);
            this.client = MagicOnionClient.Create<IChatService>(channel);
        }

        private void InitializeUi()
        {
            this.isJoin = false;

            this.SendMessageButton.gameObject.SetActive(false);
            this.ChatText.text = string.Empty;
            this.fieldInput.text = string.Empty;
            this.fieldInput.placeholder.GetComponent<Text>().text = "Please enter your name.";
            this.JoinOrLeaveButtonText.text = "Enter the room";
            this.startGame = false;
        }


        #region Client -> Server (Streaming)
        public async void JoinOrLeave()
        {
            if (this.isJoin)
            {
                await this.streamingClient.LeaveAsync();

                this.InitializeUi();
            }
            else
            {
                var request = new JoinRequest { RoomName = "Room", UserName = this.fieldInput.text };

                this.startGame = true;
                this.playerName[0].text = this.fieldInput.text;

                await this.streamingClient.JoinAsync(request);

                this.isJoin = true;
                this.SendMessageButton.gameObject.SetActive(true);
                this.JoinOrLeaveButtonText.text = "Leave";
                this.fieldInput.text = string.Empty;
                this.fieldInput.placeholder.GetComponent<Text>().text = "Please enter a text..";
            }
        }


        public async void SendMessage()
        {
            if (!this.isJoin)
                return;

            await this.streamingClient.SendMessageAsync(this.DiceRoll.ToString());

            this.fieldInput.text = string.Empty;
        }
        #endregion


        #region Server -> Client (Streaming)
        public void OnJoin(string name)
        {
            this.ChatText.text += $"\n<color=grey>{name} joined.</color>";
            Player = 0;
        }


        public void OnLeave(string name)
        {
            this.ChatText.text += $"\n<color=grey>{name} left.</color>";
        }

        public void OnSendMessage(MessageResponse message)
        {
            this.ChatText.text += $"\n{message.UserName}：{message.Message}";
            Player += 1;
            playerName[1].text = " ";
            goRoll = true;
        }
        #endregion

        void doRoll()
        {
            goRoll = true;
        }

        void updatePlayer(int Turn, int Dice)
        {
            PlayerDice[Turn] = Dice;
            PlayerText[Turn].text = "" + PlayerDice[Turn];
            Player += 1;
            if (Player > 1)
            {
                //End = true;
                startGame = false;

                if (PlayerDice[0] > PlayerDice[1])
                {
                    thewinner = 1;
                }
                else if (PlayerDice[1] > PlayerDice[0])
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
            if (x == 0)
            {
                RollText.text = "Tie";

            }
            else
            {
                RollText.text = playerName[x-1].text + " Win";

            }
        }

    }
}
