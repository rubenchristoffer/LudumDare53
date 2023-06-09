using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelClearedUI : MonoBehaviour
{

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI failedText;
    public TextMeshProUGUI deliveryFeeText;
    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI moneyGainedText;
    public Button continueButton;
    public Button quitButton;

    public GameState gameState;
    public PlayerInventory playerInventory;
    public float countUpTime = 2f;

    public bool shown { get; set; }

    private float deliveryFeeAmount;
    private float killCountAmount;
    private float moneyGainedAmount;

    void Awake () {
        continueButton.onClick.AddListener(() => {
            FindObjectOfType<HUD>()?.gameObject.SetActive(false);

            if (!GameManager.Instance.hasLevelFailed) {
                playerInventory.Save();
                gameState.Save();
            }

            SceneManager.LoadScene("Shop");
        });

        quitButton.onClick.AddListener(() => {
            if (!GameManager.Instance.hasLevelFailed) {
                gameState.Save();
                playerInventory.Save();
            }

            SceneManager.LoadScene("MainMenu");
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!shown) {
            return;
        }

        failedText.gameObject.SetActive(GameManager.Instance.hasLevelFailed);
        deliveryFeeText.gameObject.SetActive(!GameManager.Instance.hasLevelFailed);
        titleText.text = GameManager.Instance.hasLevelFailed ? "You died!" : "Pizza delivered!";

        deliveryFeeAmount = Mathf.MoveTowards(deliveryFeeAmount, gameState.currentStage.jobPrice, gameState.currentStage.jobPrice / countUpTime * Time.deltaTime);
        killCountAmount = Mathf.MoveTowards(killCountAmount, gameState.killCount, gameState.killCount / countUpTime * Time.deltaTime);
        moneyGainedAmount = Mathf.MoveTowards(moneyGainedAmount, gameState.moneyGained, gameState.moneyGained / countUpTime * Time.deltaTime);

        deliveryFeeText.text = $"Delivery fee: + ${Mathf.FloorToInt(deliveryFeeAmount)}";
        killCountText.text = $"x {Mathf.FloorToInt(killCountAmount)}";
        moneyGainedText.text = $"+ ${Mathf.FloorToInt(moneyGainedAmount)}";
    }
}