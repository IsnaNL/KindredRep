using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CoinCol
{
    private static CoinCol CoinColinstance = null;
    private static readonly object padlock = new object();
    private int CoinsCollected;

    CoinCol()
    {
       
    }

    public static CoinCol Instance
    {
        get
        {
            lock (padlock)
            {
                if (CoinColinstance == null)
                {
                    CoinColinstance = new CoinCol();
                }
                return CoinColinstance;
            }
        }
       
    }
    public void AddToCoinsCollected()
    {
        CoinsCollected++;
    }
}
