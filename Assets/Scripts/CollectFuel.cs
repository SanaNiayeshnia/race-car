using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFuel : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("RedFuel"))
            {
                FuelController.instance.AddFuel(30f);
            }
            else if (gameObject.CompareTag("BlueFuel"))
            {
                FuelController.instance.AddFuel(50f);
            }
            else if (gameObject.CompareTag("YellowFuel"))
            {
                FuelController.instance.FillFuel();
            }

            if (_audioSource != null)
            {
                _audioSource.Play();
            }

            Destroy(gameObject, _audioSource != null ? 0.2f : 0f);
        }
    }
}
