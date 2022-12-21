using UnityEngine;

namespace Movlution
{
    public class Spike : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                col.gameObject.SetActive(false);
            }
        }
    }
}
