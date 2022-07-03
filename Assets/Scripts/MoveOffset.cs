using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOffset : MonoBehaviour
{
    // Scroll main texture based on time

    public float scrollSpeed = 0.2f;
    public enum Move
    {
        x,
        y,
        negative_x,
        negative_y
    }
    public Move move;
    Renderer rend;

    void Start()
    {

        rend = GetComponent<Renderer>();

        if (PlayerPrefs.GetInt("LowEndDevice") == 1)
        {
            if (GetComponent<AudioSource>())
                GetComponent<AudioSource>().mute = true;
        }


    }

    void Update()
    {

        float offset = Time.time * scrollSpeed;

        switch (move)
        {
            case Move.x:
                Set_Offset(new Vector2(offset, 0));
                break;
            case Move.y:
                Set_Offset(new Vector2(0, offset));
                break;
            case Move.negative_x:
                Set_Offset(new Vector2(-offset, 0));
                break;
            case Move.negative_y:
                Set_Offset(new Vector2(0/*-0.58f*/, -offset));
                break;
            default:
                break;
        }

    }
    void Set_Offset(Vector2 offset)
    {

        rend.material.SetTextureOffset("_MainTex", offset);

    }
}
