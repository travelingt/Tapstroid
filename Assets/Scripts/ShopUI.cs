using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ShopUI : MonoBehaviour
{
    int UIStage;
    bool IsDisplay;
    int OperationSign;
    bool on;

    public static ShopUI Instance;
    
    public GameObject FailBuyText;

    Dictionary<string, ButtonData> AllButtonDats;

    //public GameObject PrefabRingIndicators;
    public GameObject SpitIndicator;
    public Dictionary<string, UISlate> AlShopUISlates;
    public GameObject ButtonsPrefab;
    public float TopUIScale;

    public List<GameObject> CurrentButtonsAndSplitters;
    public List<string> MenuLine;
    public string CurrentSlate;

    public GameObject CurrentBuyItem;

    public List<GameObject> RingIndicator;

    public GameObject BuyCancelButton;

    // Use this for initialization
    void Awake()
    {
        Instance = this;
        GameObject n = Instantiate(BuyCancelButton) as GameObject;
        BuyCancelButton = n;
        BuyCancelButton.SetActive(false);
    }
    void Start()
    {
        MenuLine = new List<string>();
        AllButtonDats = new Dictionary<string, ButtonData>();
        AlShopUISlates = new Dictionary<string, UISlate>();
        on = false;
        OperationSign = 0;
        UIStage = 2;
        LoadButtonsInToSlates();
        CurrentButtonsAndSplitters = new List<GameObject>();
        RingIndicator = new List<GameObject>();

       
      

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnMouseDown()
    {
       
    }
    public void OnMouseUp()
    {

    }
   
    

    
    
   

    //Takes the ShopSlates.txt and creates the menus based on the buttons loaded before
    void LoadButtonsInToSlates()
    {
        //Load buttons from text tile into, DICTIONARY?, Maybe
        //Create a load buttons from that Dictionary, load them into slates, load slates into 
        
        TextAsset ShopTA = (TextAsset)Resources.Load("ShopText/Shop", typeof(TextAsset));
        Stream S = new MemoryStream(ShopTA.bytes);
        StreamReader STR = new StreamReader(S);

        string line;
        while((line = STR.ReadLine()) != null)
        {
            if (line.Contains("{"))
            {
                ButtonData HoldButtonData;
                string Spr;
                string Name;
                string Action;
                string Info;
                float Cost;
                Spr = STR.ReadLine().Split(':')[1];
                Name = STR.ReadLine().Split(':')[1];
                Action = STR.ReadLine().Split(':')[1];
                Info = STR.ReadLine().Split(':')[1];
                Cost = float.Parse(STR.ReadLine().Split(':')[1]);
                Sprite LoadSprite = (Sprite)Resources.Load("Sprites/" + Spr, typeof(Sprite));
                HoldButtonData = new ButtonData(Name, Action, Info, LoadSprite, Cost);
                AllButtonDats.Add(HoldButtonData.Name, HoldButtonData);
                print("Button" + Time.timeSinceLevelLoad);
            }

        }
        //This take the strings from the shops slates text file, and then creates the slates for the UI, from the 
        //buttons gotten above
        TextAsset SlatesTA = (TextAsset)Resources.Load("ShopText/ShopSlates", typeof(TextAsset));
        Stream NS = new MemoryStream(SlatesTA.bytes);
        StreamReader SlatesSTR = new StreamReader(NS);

        string newLine;
        while ((newLine = SlatesSTR.ReadLine()) != null)
        {
            if(newLine.Contains("{"))
            {
                UISlate HoldSlate = new UISlate();
                newLine = SlatesSTR.ReadLine();
                HoldSlate.Name = newLine.Split(':')[1];
                newLine = SlatesSTR.ReadLine();
                int count = int.Parse(newLine.Split(':')[1]);
                for (int i = 0; i < count; i++)
                {
                    newLine = SlatesSTR.ReadLine();
                   HoldSlate.ButtonsInThisSlate.Add(AllButtonDats[newLine.Split(':')[1]]);
                    //I like the line of code above, except for the ]], that just makes me unhappy

                }
                AlShopUISlates.Add(HoldSlate.Name, HoldSlate);
            }
        }
    }

    //Class that is the basics for all menus in the shop you
    public class UISlate
    {
        public string Name;
        public List<ButtonData> ButtonsInThisSlate;
        public UISlate()
        {
            ButtonsInThisSlate = new List<ButtonData>();
        }

        public void CleanUp()
        {
            for (int i = 0; i < ButtonsInThisSlate.Count; i++)
            {

            }
        }
    }

    //Class that holds the defination of a button once it is loaded from the resources
    public class ButtonData
    {
        public string Name;
        public string Action;
        public string Info;
        public Sprite ThisSpite;
        //If this is 0, than the cost is Zero, if it is -1 than that means the button does not have cost, like a goto menu button
        public float CostIfAny;
        public ButtonData(string InName, string InAction, string InInfo, Sprite InThisSprite, float Cost)
        {
            
            Name = InName;
            Action = InAction;
            Info = InInfo;
            ThisSpite = InThisSprite;
            CostIfAny = Cost;
        }

    }
   
}
