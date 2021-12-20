using UnityEngine;

public class CoinBox : HittableFromBelow
{
    [SerializeField] private int totalCoins = 3;

    protected override bool CanUse => _remainingCoins > 0;

    private int _remainingCoins;

    protected override void Start()
    {
        base.Start();
        _remainingCoins = totalCoins;
    }

    protected override void Use()
    {
        base.Use();

        if (_remainingCoins <= 0) return;
        
        _remainingCoins--;
        Coin.CoinsCollected++;
    }
}