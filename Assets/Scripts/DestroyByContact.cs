using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    public int hp;
    public int damage;
    private GameController gameController;

    

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if((other.tag == "BoundaryTag")||(other.CompareTag("Enemy")))
        {
            return;
        }
 
        if(explosion != null)
        { 
        Instantiate(explosion, this.transform.position, this.transform.rotation);
        }

        if (other.tag == "Player")
        {
           // Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            if (gameController.PlayerHp(damage) < 0)
            {
                gameController.GameOver();
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                Destroy(other.gameObject);
            }
        }
        hp -= 1;
        if(hp < 0)
        {
            gameController.AddScore(scoreValue);
            Destroy(gameObject); //destroy asteroid(obj which is attached to script) marks object to be destroyed and destroys at the end of the frame
        }

        //order doesn't matter because the same frame
        if (other.tag != "Player")
            Destroy(other.gameObject); //destroy laserbolt when hits the asteroid 
    }
}
