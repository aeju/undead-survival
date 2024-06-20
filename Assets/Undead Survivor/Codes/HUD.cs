using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }
    public InfoType type;

    private Text myText;
    private Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.nextExp[GameManager.instance.level];
                mySlider.value = curExp / maxExp; // 슬라이더 : 현재 경험치 / 최대 경험치 
                break;
            case InfoType.Level:
                
                break;
            case InfoType.Kill:
                
                break;
            case InfoType.Time:
                
                break;
            case InfoType.Health:
                
                break;
        }
    }
}
