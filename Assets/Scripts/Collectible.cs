
using UnityEngine;  
public class Collectible : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            PlatformerControl p = col.GetComponent<PlatformerControl>();
            p.coinsCollected++;
            Debug.Log("My score is: " + p.coinsCollected);
            Destroy(gameObject);
        }
    }
}
