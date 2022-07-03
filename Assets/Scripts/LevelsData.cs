
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

[CreateAssetMenu]
public class LevelsData : ScriptableObject
{
    [System.Serializable]
    public class CareerMode
    {
        public string levelName;
        public string firstName;
        public string secondName;
        

        public string levelDescription;
        public string sceneName;
        public string trophyName;
        public int levelNumber;
        public int requiredCar;
        public int goldSeconds;
        public int silverSeconds;
        public int reward = 1500;
        public int goldReward;
        public int silverReward;
        public int bronzeReward;
        //public Sprite sprite;
        public bool isLocked = true;
        public bool isCompleted = false;



        [Header("OVER PANEL THINGS")]
        public string shortReason;
        public string longReason;

        [Header("PAUSE SCRENE TEXTS")]
        public string shortDescription;

    }
    public List<CareerMode> careerMode;

    [System.Serializable]
    public class SkatePark
    {
        public string levelName;
        public string firstName;
        public string secondName;
       
        public string levelDescription;
        public string sceneName;
        public string trophyName;
        public int levelNumber;
        public int requiredCar;
        public int reward = 1500;
        //public Sprite sprite;
        public bool isLocked = true;
        public bool isCompleted = false;

        [Header("OVER PANEL THINGS")]
        public string shortReason;
        public string longReason;

        [Header("PAUSE SCRENE TEXTS")]
        public string shortDescription;
    }
    public List<SkatePark> skatePark;

  
    public enum ModeSelected
    {
        skatePark,
        careerMode

    }
    [Header("MODE SELECTION")]
    public ModeSelected modeSelected;
    public int levelSelected = 1;

    [Header("Game Currency")]
    public int totalCoins;

    [Header("Player and Huggy Points")]
    [SerializeField]
    private int huggy=0;
    [SerializeField]
    private int player=0;

    [Header("Sound Settings")]
    public bool soundOn = true;
    public bool musicOn = true;

    [Header("Start Up Panels")]
    public bool showPrivacy = true;
    public bool openLevels = false;

    [SerializeField]
    private string shortDescription;

    [Tooltip("value is from 0 to 3\n0: Buttons\n1: Gyro\n2: SteeringWheel")]
    public int controllsSettings;
    
    private int rotationpoints;



    public void SetcontrollsSettings(int setting)
    {

       
            controllsSettings = setting;
        
    }
    public int GetControllSettings()
    {
        
        return controllsSettings;
    }
    public void AddRotationpoints()
    {
        rotationpoints += 100;
    }    

    public int GetRotationPoints()
    {
        return rotationpoints;
    }

    public void removeRotationPoints()
    {
        rotationpoints = 0;
    }

    public int GetRoundsCompleted()
    {
        int count = 0;
        for(int i=0;i< careerMode.Count;i++)
        {
            if(careerMode[i].isCompleted==true)
            {
                count++;
            }
        }

        return count;
    }
    public string GetLevelName(int index)
    {
        return careerMode[index].levelName;
    }
    public int GetRequiredCar(int index)
    {
        return careerMode[index].requiredCar;
    }
    public void SetHuggyWinPoints(int points)
    {
        huggy = points;
    }
    public void SetPlayerWinpoints(int points)
    {
        player = points;
    }

    public void PlayerWon()
    {
        player++;
    }
    public void HuggyWon()
    {
        huggy++;
    }
    public int GetHuggyWinPoints()
    {
        return huggy;
    }

    public int GetPlayerWinPoints()
    {
        return player;
    }


    public int GetCoins()
    {
        return totalCoins;
    }


    public void SetCoins(int money)
    {
        totalCoins = money;
    }

    public void AddCoins(int money)
    {
        totalCoins += money;
    }
    public int GetGoldReward(int index)
    {
        return careerMode[index].goldReward;
    }

    public int GetSilverReward(int index)
    {
        return careerMode[index].silverReward;
    }

    public int GetBronzeReward(int index)
    {
        return careerMode[index].bronzeReward;
    }

    public int GetGoldSeconds(int index)
    {
        return careerMode[index].goldSeconds;
    }
    public int GetSilverSeconds(int index)
    {
        return careerMode[index].silverSeconds;
    }

    public string GetSceneName()
    {
        string sceneName = "";
        if (modeSelected == ModeSelected.skatePark)
            sceneName = skatePark[levelSelected].sceneName;
        else if (modeSelected == ModeSelected.careerMode)
            sceneName = careerMode[levelSelected].sceneName;
        
        return sceneName;
    }
    
    public int GetLevelSelected()
    {
        int _levelSelected = 0;
        if (modeSelected == ModeSelected.skatePark)
            _levelSelected = skatePark[levelSelected].levelNumber;
        else if (modeSelected == ModeSelected.careerMode)
            _levelSelected = careerMode[levelSelected].levelNumber;
       
        return _levelSelected;
    }
    public int GetRequiredCar()
    {
        int _requiredCar = 0;
        if (modeSelected == ModeSelected.skatePark)
            _requiredCar = skatePark[levelSelected].requiredCar;
        else if (modeSelected == ModeSelected.careerMode)
            _requiredCar = careerMode[levelSelected].requiredCar;
        return _requiredCar;
    }
    public void OpenNextLevel()
    {
        AnalyticsResult analyticsResult = Analytics.CustomEvent(
            "Level Completed", new Dictionary<string, object> { { "Mode", modeSelected.ToString() }, { "Level", levelSelected } }
            );


        if (modeSelected == ModeSelected.skatePark)
        {
            if (levelSelected < 4)
            {
                levelSelected++;
                
            }
            else
            {
                levelSelected = 0;
                modeSelected = ModeSelected.careerMode;
            }
        }
        else if (modeSelected == ModeSelected.careerMode)
        {
            if (levelSelected < 4)
            {
                levelSelected++;
                UnlockLevel_StampedeHunting(levelSelected);
            }
          
        }
       
    }
    public bool GetIAlocked(int levelSelected)
    {
        return careerMode[levelSelected].isLocked;
    }
    public void SetLevelComplete()
    {
        if (modeSelected == ModeSelected.skatePark)
            skatePark[levelSelected].isCompleted = true;
        else if (modeSelected == ModeSelected.careerMode)
            careerMode[levelSelected].isCompleted = true;
        
    }
    public int GetCompletedLevels()
    {
        int number = 0;
        for (int i = 0; i < skatePark.Count; i++)
        {
            if (skatePark[i].isCompleted)
                number++;
        }
        for (int i = 0; i < careerMode.Count; i++)
        {
            if (careerMode[i].isCompleted)
                number++;
        }
        

        return number;
    }

    public string Get_Mode()
    {
        string sceneName = "";
        if (modeSelected == ModeSelected.skatePark)
            sceneName = "Skate Park";
        else if (modeSelected == ModeSelected.careerMode)
            sceneName = "Career Mode";
        
        return sceneName;
    }
    public string Get_Trophy()
    {
        string sceneName = "";
        if (modeSelected == ModeSelected.skatePark)
            sceneName = skatePark[levelSelected].trophyName;
        else if (modeSelected == ModeSelected.careerMode)
            sceneName = careerMode[levelSelected].trophyName;
        
        return sceneName;
    }
    public string Get_Reward()
    {
        string sceneName = "";
        if (modeSelected == ModeSelected.skatePark)
            sceneName = skatePark[levelSelected].reward.ToString();
        else if (modeSelected == ModeSelected.careerMode)
            sceneName = careerMode[levelSelected].reward.ToString();
       
        return sceneName;
    }
    public void AddCoins()
    {
        if (modeSelected == ModeSelected.skatePark)
            totalCoins = totalCoins + skatePark[levelSelected].reward;
        else if (modeSelected == ModeSelected.careerMode)
            totalCoins = totalCoins + careerMode[levelSelected].reward;
      
    }
    public string FailedReasonShort()
    {
        string sceneName = "";
        if (modeSelected == ModeSelected.skatePark)
            sceneName = skatePark[levelSelected].shortReason;
        else if (modeSelected == ModeSelected.careerMode)
            sceneName = careerMode[levelSelected].shortReason;
   
        return sceneName;
    }
    public string FailedReasonLong()
    {
        string sceneName = "";
        if (modeSelected == ModeSelected.skatePark)
            sceneName = skatePark[levelSelected].longReason;
        else if (modeSelected == ModeSelected.careerMode)
            sceneName = careerMode[levelSelected].longReason;
      
        return sceneName;
    }

    public string LevelDescription()
    {
        string sceneName = "";
        if (modeSelected == ModeSelected.skatePark)
            sceneName = skatePark[levelSelected].levelDescription;
        else if (modeSelected == ModeSelected.careerMode)
            sceneName = careerMode[levelSelected].levelDescription;
      
        return sceneName;
    }
    public string Level_Short_Description()
    {
        string sceneName = "";
        if (modeSelected == ModeSelected.skatePark)
            sceneName = skatePark[levelSelected].shortDescription;
        else if (modeSelected == ModeSelected.careerMode)
            sceneName = careerMode[levelSelected].shortDescription;
       
        return sceneName;
    }

   

    #region careerMode
    public int OpenedLevel_CareerMode()
    {
        int openLevel = 0;

        for (int i = 0; i < careerMode.Count; i++)
        {
            if (!careerMode[i].isLocked)
                openLevel = i;
        }

        return openLevel;
    }
    /*public Sprite GetSprite_StampedeHunting(int levelNumber)
    {
        return careerMode[levelNumber].sprite;
    }*/
    public bool IsLocked_StampedeHunting(int levelNumber)
    {
        return careerMode[levelNumber].isLocked;
    }
    public void UnlockLevel_StampedeHunting(int levelNumber)
    {
        careerMode[levelNumber].isLocked = false;
    }
    public int Next_Level_StampedeHunting(int selectedLevel)
    {
        int nextLevel = selectedLevel;

        if (selectedLevel < careerMode.Count - 1)
        {
            selectedLevel++;
            nextLevel = selectedLevel;
        }
        return nextLevel;
    }
    public int Previous_Level_StampedeHunting(int selectedLevel)
    {

        int previousLevel = selectedLevel;

        if (selectedLevel > 0)
        {
            selectedLevel--;
            previousLevel = selectedLevel;
        }
        return previousLevel;
    }
    public string FirstName_StampedeHunting(int selectedLevel)
    {
        return careerMode[selectedLevel].firstName;
    }
    public string SecondName_StampedeHunting(int selectedLevel)
    {
        return careerMode[selectedLevel].secondName;
    }
    public string LevelDescription_StampedeHunting(int selectedLevel)
    {
        return careerMode[selectedLevel].levelDescription;
    }
    public int Reward_StampedeHunting(int selectedLevel)
    {
        return careerMode[selectedLevel].reward;
    }
    public void Set_Level_StampedeHunting(int selectedLevel)
    {
        levelSelected = selectedLevel;
        modeSelected = ModeSelected.careerMode;
    }
    #endregion



}