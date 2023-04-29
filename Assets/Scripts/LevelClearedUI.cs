using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelClearedUI : MonoBehaviour
{

    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI moneyGainedText;
    public GameState gameState;
    public float countUpTime = 2f;

    public bool shown { get; set; }

    private float killCountAmount;
    private float moneyGainedAmount;

    // Start is called before the first frame update
    void Start()
    {
        shown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!shown) {
            return;
        }

        killCountAmount = Mathf.MoveTowards(killCountAmount, gameState.killCount, gameState.killCount / countUpTime * Time.deltaTime);
        moneyGainedAmount = Mathf.MoveTowards(moneyGainedAmount, gameState.moneyGained, gameState.moneyGained / countUpTime * Time.deltaTime);

        killCountText.text = $"x {Mathf.FloorToInt(killCountAmount)}";
        moneyGainedText.text = $"+ ${Mathf.FloorToInt(moneyGainedAmount)}";
    }
}