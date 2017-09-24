using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyAndDestroyParenObject : MonoBehaviour {
    public GameObject ParentObject;
    CraftItem craft;
    ParticleSystem system;
	// Use this for initialization
	void Start () {
        system = GetComponent<ParticleSystem>();
        craft = ParentObject.GetComponent<CraftItem>();
      
            InvokeRepeating("DestroyPart", 0.0f, 5.0f); 
        
	}
	
    void DestroyPart () {
        if (craft.Built)
        {
            if (!system.IsAlive())
            {
                if (craft.hisEffect)
                {
                    craft.hisEffectPrefabPoolForDestroy.DestroyAPS();
                }
                craft.DefaultOptions();
                craft.DefaultForParticle();
                ParentObject.DestroyAPS();

            }
        } 
    }
}
