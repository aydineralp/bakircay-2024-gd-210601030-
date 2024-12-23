using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] fruitPrefabs; // Meyve prefablari
    public int numberOfEachFruit = 2; // Her bir meyveden ka� tane �retilecek
    public Transform spawnArea; // Nesnelerin d��ece�i alan
    public Transform placementArea; // Nesnelerin yerle�tirilece�i alan (Placement Area)
    public float spawnHeight = 10f; // Nesnelerin d��ece�i y�kseklik

    void Start()
    {
        // Her meyve prefab'� i�in spawn i�lemi yap
        foreach (GameObject prefab in fruitPrefabs)
        {
            for (int i = 0; i < numberOfEachFruit; i++)
            {
                SpawnObject(prefab);
            }
        }
    }

    // Nesneleri spawn etme i�lemi
    void SpawnObject(GameObject prefab)
    {
        // Spawn alan� i�inde rastgele bir pozisyon belirle
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2, spawnArea.position.x + spawnArea.localScale.x / 2),
            spawnHeight, // Y�ksekli�i sabit tutarak d��mesini sa�la
            Random.Range(spawnArea.position.z - spawnArea.localScale.z / 2, spawnArea.position.z + spawnArea.localScale.z / 2)
        );

        // Prefab'� spawn et
        GameObject spawnedFruit = Instantiate(prefab, randomPosition, Quaternion.identity);

        // Spawn edilen nesneye Rigidbody ve Collider ayarlar�n� yap
        Rigidbody rb = spawnedFruit.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // S�rekli �arp��ma alg�lamas�
            rb.useGravity = true; // Yer�ekimini kullan
        }

        Collider collider = spawnedFruit.GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = false; // Collider'�n trigger olmad���ndan emin ol
        }

        // Nesne yere d��t���nde PlacementArea'ya yerle�tir
        PlaceObjectInArea(spawnedFruit);
    }

    // Nesneyi Placement Area'ya yerle�tir
    void PlaceObjectInArea(GameObject spawnedFruit)
    {
        // PlacementArea i�ine rastgele bir pozisyon
        Vector3 randomPlacementPosition = new Vector3(
            Random.Range(placementArea.position.x - placementArea.localScale.x / 2, placementArea.position.x + placementArea.localScale.x / 2),
            placementArea.position.y, // Placement area'n�n y pozisyonu
            Random.Range(placementArea.position.z - placementArea.localScale.z / 2, placementArea.position.z + placementArea.localScale.z / 2)
        );

        // Yerle�tirilen nesnenin pozisyonunu ayarla
        spawnedFruit.transform.position = randomPlacementPosition;

        // Nesnenin d�n���n� s�f�rla (gerekti�inde)
        spawnedFruit.transform.rotation = Quaternion.identity;
    }
}
