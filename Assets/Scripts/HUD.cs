using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{

    private Transform _player;
    private PizzaDeliveryTile _deliveryTile;

    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI moneyText;

    public PlayerInventory playerInventory;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player").transform;  
        _deliveryTile = GameObject.FindObjectOfType<PizzaDeliveryTile>();
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.FloorToInt(Vector3.Distance(_player.position, _deliveryTile.transform.position));

        distanceText.text = GameManager.Instance.hasFoundGPS ? $"{distance}m" : "No GPS";
        moneyText.text = $"${playerInventory.money}";
    }
}
