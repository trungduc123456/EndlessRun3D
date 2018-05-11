using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    public Animator anim;
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }
	void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.GetCoin();
            anim.SetTrigger("Colected");
        }
    }
}
