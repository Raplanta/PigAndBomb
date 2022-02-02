using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float timeBeforeBang;
    [SerializeField] private GameObject bangPrefab;

    GameObject bangInstantiate;

    void Start()
    {
        StartCoroutine(CreateBang());
    }

    private IEnumerator CreateBang()
    {
        yield return new WaitForSeconds(timeBeforeBang);

        bangInstantiate = (GameObject)Instantiate(bangPrefab, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
