using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class HUD : MonoBehaviour
{

    private Transform _player;
    private PizzaDeliveryTile _deliveryTile;

    public PlayerInventory playerInventory;

    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI currentStageText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI knockText;
    public TextMeshProUGUI nobodyRespondsText;
    public GameObject pepperoniPanel;
    public TextMeshProUGUI pepperoniAmountText;
    public Animator fadePanelAnimator;

    public GameState gameState;

    public bool displayKnockText { get; set; }

    private House correctHouse;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player").transform;  

        if (playerInventory.pepperoniAmount == 0) {
            pepperoniPanel.SetActive(false);
        }
    }

    void Start () {
        correctHouse = GameObject.FindObjectsOfType<House>()
            .Where(house => house.correctHouse)
            .First();
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.FloorToInt(Vector3.Distance(_player.position, correctHouse.transform.position));

        distanceText.text = GameManager.Instance.hasFoundGPS ? $"{distance}m" : "No GPS";
        moneyText.text = $"${gameState.moneyGained}";

        pepperoniAmountText.text = $"{playerInventory.pepperoniAmount}";
        currentStageText.text = $"Stage {gameState.currentStageNumber}";

        knockText.gameObject.SetActive(displayKnockText);

        displayKnockText = false;
    }
}
