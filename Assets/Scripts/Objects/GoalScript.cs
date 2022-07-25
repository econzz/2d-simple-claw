using Game2D.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game2D.ObjectScripts
{
    public class GoalScript : MonoBehaviour
    {

        public static Action<PrizeScript> OnPrizeGet;


        /// <summary>
        /// ゴールの中に何かを入るとき
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.gameObject.GetComponent<PrizeScript>())
            {
                PrizeScript prize = collision.gameObject.GetComponent<PrizeScript>();
                OnPrizeGet?.Invoke(prize);
            }

        }
    }

}
