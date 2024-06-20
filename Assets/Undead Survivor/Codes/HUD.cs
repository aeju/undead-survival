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
                myText.text = string.Format("Lv.{0:F0}", GameManager.instance.level);
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.instance.kill);
                break;
            case InfoType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime; // 남은 시간
                int min = Mathf.FloorToInt(remainTime / 60); // 소수점 버리기
                int sec = Mathf.FloorToInt(remainTime % 60);
                //myText.text = string.Format("{0:F0}:{1:F0}", min, sec); // 0:7
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec); // 00:07 (항상 두 자리) D0, D1 ... : 자리수 지정
                break;
            case InfoType.Health:
                float curHealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.maxHealth;
                mySlider.value = curHealth / maxHealth; // 슬라이더 : 현재 경험치 / 최대 경험치 
                break;
        }
    }
}
