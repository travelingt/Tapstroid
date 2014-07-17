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
    Dictionary<string, ButtonData> AllButtonDats;
    
    public GameObject SpitIndicator;
    public Dictionary<string, UISlate> AlShopUISlates;
    public GameObject ButtonsPrefab;
    public float TopUIScale;

    public List<GameObject> CurrentButtonsAndSplitters;
    public List<string> MenuLine;
    public string CurrentSlate;
    // Use this for initialization

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
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            Time.timeScale = 0;
            transform.localScale = new Vector3(transform.localScale.x + (.1f * OperationSign), transform.localScale.y + (.1f * OperationSign), 1);
            if (transform.localScale.x >= TopUIScale)
            {
                OperationSign = 0;
                if (!IsDisplay)
                {
                    IsDisplay = true;
                    DisplayUISlate("Main");
                    
                }

            }
        }
    }

    public void OnMouseDown()
    {
        if (!on)
        {
            OperationSign = 1;
            on = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
          

        }
        else
        {
            on = false;
            UIStage = 0;
            transform.localScale = new Vector3(0, 0, 1);
            OperationSign = 0;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            IsDisplay = false;
            Time.timeScale = 1;
            for (int i = 0; i < CurrentButtonsAndSplitters.Count; i++)
            {
                Destroy(CurrentButtonsAndSplitters[i]);
            }
            CurrentButtonsAndSplitters.Clear();
        }
    }
    public void OnMouseUp()
    {

    }
    public void DisplayUISlate(string Slate)
    {
        if (CurrentButtonsAndSplitters.Count > 0)
        {
            for (int i = 0; i < CurrentButtonsAndSplitters.Count; i++)
            {
                Destroy(CurrentButtonsAndSplitters[i]);
            }
            CurrentButtonsAndSplitters.Clear();
        }

        UISlate ULS = AlShopUISlates[Slate];
        CurrentSlate = Slate;
        #region SettingUpNewUI_AlsoJustWantedThisToBeShort
        for (int i = 0; i <ULS.ButtonsInThisSlate.Count; i++)
        {
            GameObject NewButton = Instantiate(ButtonsPrefab) as GameObject;
           
            NewButton.GetComponent<SpriteRenderer>().sprite = ULS.ButtonsInThisSlate[i].ThisSpite;
            
            NewButton.GetComponent<Buttons>().Action = ULS.ButtonsInThisSlate[i].Action;
            
            NewButton.GetComponent<Buttons>().Info = ULS.ButtonsInThisSlate[i].Info;

            NewButton.GetComponent<Buttons>().shopUI = this;

            float RoationalPosition = ((i * (360 / ULS.ButtonsInThisSlate.Count))) * (Mathf.PI / 180) +((360 / ULS.ButtonsInThisSlate.Count) / 2 * (Mathf.PI / 180));
            
            NewButton.transform.position = new Vector3(Mathf.Cos(RoationalPosition) * 2, 
                Mathf.Sin(RoationalPosition) * 2, transform.position.z) + transform.position;
           // NewButton.transform.parent = transform;
            CurrentButtonsAndSplitters.Add(NewButton);

            GameObject NewSpli = Instantiate(SpitIndicator) as GameObject;
            NewSpli.transform.position = transform.position;

            NewSpli.GetComponent<SpriteRenderer>().color = Color.black;
            NewSpli.transform.localScale = new Vector3(1, 3.5f, 1);
            NewSpli.transform.eulerAngles = new Vector3(NewSpli.transform.eulerAngles.x,
                NewSpli.transform.eulerAngles.y, i * (360 / ULS.ButtonsInThisSlate.Count) + 90);
            CurrentButtonsAndSplitters.Add(NewSpli);
           // NewSpli.transform.parent = transform;
        }
        #endregion

    }

    public void PreformAction(string Action, string Info)
    {
        switch (Action)
        {
            case "Open":
                MenuLine.Add(CurrentSlate);
                DisplayUISlate(Info);
                break;

            case "Buy":

                break;

            case "Cancel":
                string Shift = MenuLine[MenuLine.Count -1];
                MenuLine.RemoveAt(MenuLine.Count-1);
                DisplayUISlate(Shift);
                break;
        }

    }

    void LoadButtonsInToSlates()
    {
        //Load buttons from text tile into, DICTIONARY?, Maybe
        //Create a load buttons from that Dictionary, load them into slates, load slates into 
        
        TextAsset ShopTA = (TextAsset)Resources.Load("Shop", typeof(TextAsset));
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
                Spr = STR.ReadLine().Split(':')[1];
                Name = STR.ReadLine().Split(':')[1];
                Action = STR.ReadLine().Split(':')[1];
                Info = STR.ReadLine().Split(':')[1];
                Sprite LoadSprite = (Sprite)Resources.Load(Spr, typeof(Sprite));
                HoldButtonData = new ButtonData(Name, Action, Info, LoadSprite);
                AllButtonDats.Add(HoldButtonData.Name, HoldButtonData);
                print("Button" + Time.timeSinceLevelLoad);
            }

        }
        //This take the strings from the shops slates text file, and then creates the slates for the UI, from the 
        //buttons gotten above
        TextAsset SlatesTA = (TextAsset)Resources.Load("ShopSlates", typeof(TextAsset));
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

    public class ButtonData
    {
        public string Name;
        public string Action;
        public string Info;
        public Sprite ThisSpite;

        public ButtonData(string InName, string InAction, string InInfo, Sprite InThisSprite)
        {
            Name = InName;
            Action = InAction;
            Info = InInfo;
            ThisSpite = InThisSprite;
        }

    }
   
}
