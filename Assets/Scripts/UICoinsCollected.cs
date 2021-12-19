using TMPro;
using UnityEngine;

public class UICoinsCollected : MonoBehaviour
{
    private TMP_Text _coinsCollected;
    
    // Start is called before the first frame update
    private void Start()
    {
        _coinsCollected = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _coinsCollected.text = Coin.CoinsCollected.ToString();
    }
}
