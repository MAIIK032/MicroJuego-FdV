using System;
using UnityEngine;
using UnityEngine.UI;

public class Bala : MonoBehaviour
{

    public float velocidad = 7f;
    public float maxTiempoVida = 3f;
    public Vector3 targetVector;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, maxTiempoVida);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocidad * targetVector * Time.deltaTime);
    }

   
    void OnTriggerEnter(Collider other)
    {
        var colision = other.gameObject;
        if (colision.CompareTag("Enemy"))
        {
            IncrementarPuntuacion();
            Destroy(colision);
            Destroy(gameObject);
        } 
    }

    private void IncrementarPuntuacion()
    {
        Nave.PUNTOS++;
        UpdateTextoPuntos();
    }

    private void UpdateTextoPuntos()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "SCORE: " + Nave.PUNTOS;
    }
}
