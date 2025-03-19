 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombies : MonoBehaviour
{
    // Start is called before the first frame update
   public Zomhand zomhand;

    public int zombieDamage;

    private void Start()
    {
        zomhand.damage = zombieDamage;
    }
}
