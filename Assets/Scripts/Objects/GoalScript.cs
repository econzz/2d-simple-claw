using Game2D.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game2D.ObjectScripts
{
    public class GoalScript : MonoBehaviour
    {
        private GameController gameController;
        // Start is called before the first frame update
        void Awake()
        {
            this.gameController = FindObjectOfType<GameController>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.transform.tag == "ClawObject")           
                return;

            Destroy(collision.gameObject);
            this.gameController.OnPrizeGet();

        }
    }

}
