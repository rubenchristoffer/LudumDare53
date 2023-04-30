using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    private Entity _player;
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
    public TextMeshProUGUI ammoText;

    public TextMeshProUGUI healthText;
    public Image healthFillImage;

    public GameState gameState;

    public bool displayKnockText { get; set; }

    private House correctHouse;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Entity>();  

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
        int distance = Mathf.FloorToInt(Vector3.Distance(_player.transform.position, correctHouse.door.position));

        distanceText.text = playerInventory.hasFoundGPS ? $"{distance}m" : "No GPS found";
        moneyText.text = $"${gameState.moneyGained}";

        pepperoniAmountText.text = $"{playerInventory.pepperoniAmount}";
        currentStageText.text = $"Stage {gameState.currentStageNumber}";

        healthText.text = $"{Mathf.CeilToInt(_player.health)}";
        healthFillImage.fillAmount = _player.health / _player.maxHealth;

        knockText.gameObject.SetActive(displayKnockText);

        displayKnockText = false;
    }
}
