using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelLockSpriteChanger : MonoBehaviour
{
    public Sprite[] lockedSprites;
    public Sprite[] UnlockedSprites;
    public GameObject[] levelsObjects;
    public LevelsData levelsData;
    
    private void Start()
    {
        
        for (int i = 0; i < levelsObjects.Length; i++)
        {

            if (levelsData.GetIAlocked(i))
            {
                
                levelsObjects[i].GetComponent<Image>().sprite = lockedSprites[i];
                
                levelsObjects[i].GetComponent<Button>().enabled = false;
            }
            else
            {
                
                levelsObjects[i].GetComponent<Image>().sprite = UnlockedSprites[i];
                levelsObjects[i].GetComponent<Button>().enabled = true;
            }
        }
    }
}
