using System;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class Nave : MonoBehaviour
{

    public float fuerzaEmpuje = 100f;
    public float velocidadRotacion = 110f;

    public float limiteX, limiteY;

    public GameObject lanzador, balaPrefab, panel;

    private Rigidbody _rigid;

    public static int PUNTOS = 0;
    private Boolean pausa = false; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = transform.position;
        if (newPos.x > limiteX) newPos.x = -limiteX + 1;
        else if (newPos.x < -limiteX) newPos.x = limiteX - 1;
        else if (newPos.y > limiteY) newPos.y = -limiteY ;
        else if (newPos.y < -limiteY) newPos.y = limiteY ;
        transform.position = newPos;
        //Movimiento
        float empuje = Input.GetAxis("Empuje") * Time.deltaTime;
        Vector3 direccionEmpuje = transform.right;
        _rigid.AddForce(direccionEmpuje * empuje * fuerzaEmpuje);

        //Rotacion
        float rotacion = Input.GetAxis("Rotacion") * Time.deltaTime;
        transform.Rotate(Vector3.forward, -rotacion * velocidadRotacion);

        //Disparos
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 spawn = lanzador.transform.position;
            GameObject bala = Instantiate(balaPrefab, spawn, transform.rotation);

            //Bala scriptBala = bala.GetComponent<Bala>();
            //scriptBala.targetVector = transform.right;
        }

        if (Input.GetKeyDown(KeyCode.Escape))        {
            if (!pausa)
            {
                Pausar();
            } else
            {
                Continuar();
            }
        }
    }

    private void Pausar()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;
        pausa = !pausa;
    }

    public void Continuar()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        pausa = !pausa;
    }
    public void Reiniciar()
    {
        PUNTOS = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Continuar();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")){
            PUNTOS = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
