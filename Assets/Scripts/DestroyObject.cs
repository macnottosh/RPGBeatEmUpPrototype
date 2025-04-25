using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public GameObject player;
    void Start()
    {     
        StartCoroutine(DestroythisObject());
    }

    // Update is called once per frame
    void Update()
    {
        player= GameObject.FindWithTag("Player");
        Vector3 weaponOffset = transform.forward * -1f + transform.right * 0f + transform.up * 0f;
        transform.position = player.transform.position + weaponOffset;
    }
    IEnumerator DestroythisObject()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }
}
