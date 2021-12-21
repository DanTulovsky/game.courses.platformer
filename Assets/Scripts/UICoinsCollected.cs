using TMPro;
using UnityEngine;

public class UICoinsCollected : MonoBehaviour
{
    private TMP_Text _text;
    
    // Start is called before the first frame update
    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _text.SetText(Coin.CoinsCollected.ToString());
    }
}