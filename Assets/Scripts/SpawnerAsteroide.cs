using UnityEngine;

public class SpawnerAsteroide : MonoBehaviour
{
    public GameObject asteroidePrefab;
    public float spawnRatePorMinuto = 30;
    public float spawnRateIncremento = 1f;
    public float x_limite;
    public float maxTiempoVida = 4f;

    private float spawnSiguiente = 0;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnSiguiente)
        {
            spawnSiguiente = Time.time + 60 / spawnRatePorMinuto;

            spawnRatePorMinuto += spawnRateIncremento;

            float rand = Random.Range(-x_limite, x_limite);

            Vector3 spawnPosicion = new Vector3(rand , 8f, -0.5f);

            GameObject asteroide = Instantiate(asteroidePrefab, spawnPosicion, Quaternion.identity);
            Destroy(asteroide, maxTiempoVida);
        }
    }
}
