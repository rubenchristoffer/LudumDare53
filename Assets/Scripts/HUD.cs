using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class HUD : MonoBehaviour
{

    private Transform _player;
    private PizzaDeliveryTile _deliveryTile;

    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI knockText;
    public TextMeshProUGUI nobodyRespondsText;
    public Animator fadePanelAnimator;

    public PlayerInventory playerInventory;

    public bool displayKnockText { get; set; }

    private House correctHouse;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player").transform;  
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
        moneyText.text = $"${playerInventory.money}";

        knockText.gameObject.SetActive(displayKnockText);

        displayKnockText = false;
    }
}
