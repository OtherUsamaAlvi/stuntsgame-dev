using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

[CreateAssetMenu]
public class CarData : ScriptableObject
{
    [System.Serializable]
    public class Cars
    {
        public string carIndex;
        public string carName;
        public string carDescription;



        public float speed = 50f;
        public float acceleration = 50f;
        public float handeling = 1f;
        
        [HideInInspector]
        public bool isUsed = false;
        public bool isLocked = false;

        public int price;


    }
    public List<Cars> carData;


    public bool reward_Achieved = false;



    public int GetLastUnlockedCar()

    {
        int unlocked=1;

        for (int i = 0; i < carData.Count; i++)
        {
            if (carData[i].isLocked ==false)
            {
                unlocked= i;
                    

            }
            

           
        }
        return unlocked+1;
        
    }

    public void SetIsLocked(int carNo,bool locked)
    {
        carData[carNo].isLocked = locked;
    }
    public void SetIsLocked( bool locked, int carNo)
    {
        carData[carNo].isLocked = locked;
    }


    public int GetPrice(int carNo)
    {
        return carData[carNo].price;
    }
    public void Used(int carNo)
    {
        for (int i = 0; i < carData.Count; i++)
        {
            if (i == carNo)
            {
                carData[i].isUsed = true;
            }
            else
            {
                carData[i].isUsed = false;
            }
        }
    }
    public void UnUsed()
    {

        try
        {
            for (int i = 0; i < carData.Count; i++)
            {
                carData[i].isUsed = false;
            }
        }
        catch (System.Exception)
        {


        }

    }
    public bool isUsed(int carNo)
    {
        bool isused = false;

        isused = carData[carNo].isUsed;

        return isused;
    }
    public int Selected_Car()
    {
        int selectedCar = 0;

        for (int i = 0; i < carData.Count; i++)
        {
            if (carData[i].isUsed)
            {
                selectedCar = i;
                break;
            }
        }
        return selectedCar;
    }

    public bool IsLocked(int carNo)
    {
        return carData[carNo].isLocked;
    }
    public void SetLockedUnlocked(int carNo)
    {
        carData[carNo].isLocked = false;
    }

    public void Set_Prices()
    {

        try
        {
            carData[1].price = 1000;
            carData[2].price = 5000;
            carData[3].price = 10000;
            carData[4].price = 15000;
            
        }
        catch (System.Exception)
        {


        }

    }
    public void Set_Used()
    {
        try
        {
            carData[0].isUsed = true;
            carData[1].isUsed = false;
            carData[2].isUsed = false;
            carData[3].isUsed = false;
            carData[4].isUsed = false;
            
        }
        catch (System.Exception)
        {


        }

    }
    public void UnlockAll()
    {

        try
        {
            for (int i = 0; i < carData.Count; i++)
            {
                carData[i].isLocked = false;
            }
        }
        catch (System.Exception)
        {


        }

    }
    public void Set_Locked()
    {
        try
        {
            carData[0].isLocked = false;
            carData[1].isLocked = true;
            carData[2].isLocked = true;
            carData[3].isLocked = true;
            carData[4].isLocked = true;
            
        }
        catch (System.Exception)
        {


        }

    }
    public bool Is_Car_Available()
    {
        bool isCarAvailable = false;

        for (int i = 0; i < carData.Count; i++)
        {
            if (carData[i].isLocked)
            {
                isCarAvailable = true;
                break;
            }
        }

        return isCarAvailable;
    }
    public int Get_Available_Car()
    {
        int get_Available_Car = 0;

        for (int i = 0; i < carData.Count; i++)
        {
            if (carData[i].isLocked)
            {
                get_Available_Car = i;
                break;
            }
        }

        return get_Available_Car;
    }
    public int Get_Next_Car(int selectedCar)
    {
        int nextCar = selectedCar;

        if (selectedCar < carData.Count - 1)
        {
            selectedCar++;
            nextCar = selectedCar;
        }
        return nextCar;
    }
    public int Get_Previous_Car(int selectedCar)
    {

        int previousCar = selectedCar;

        if (selectedCar > 0)
        {
            selectedCar--;
            previousCar = selectedCar;
        }
        return previousCar;
    }
    public float Get_Handeling(int carNumber)
    {
        return carData[carNumber].handeling;
    }
    public float Get_Speed(int carNumber)
    {
        return carData[carNumber].speed;
    }
    public float Get_Acceleration(int carNumber)
    {
        return carData[carNumber].acceleration;
    }
    public string Get_Name(int carNumber)
    {
        return carData[carNumber].carName;
    }
}
