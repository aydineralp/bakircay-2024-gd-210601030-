using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] fruitPrefabs; // Meyve prefablari
    public int numberOfEachFruit = 2; // Her bir meyveden kaç tane üretilecek
    public Transform spawnArea; // Nesnelerin düþeceði alan
    public Transform placementArea; // Nesnelerin yerleþtirileceði alan (Placement Area)
    public float spawnHeight = 10f; // Nesnelerin düþeceði yükseklik

    void Start()
    {
        // Her meyve prefab'ý için spawn iþlemi yap
        foreach (GameObject prefab in fruitPrefabs)
        {
            for (int i = 0; i < numberOfEachFruit; i++)
            {
                SpawnObject(prefab);
            }
        }
    }

    // Nesneleri spawn etme iþlemi
    void SpawnObject(GameObject prefab)
    {
        // Spawn alaný içinde rastgele bir pozisyon belirle
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2, spawnArea.position.x + spawnArea.localScale.x / 2),
            spawnHeight, // Yüksekliði sabit tutarak düþmesini saðla
            Random.Range(spawnArea.position.z - spawnArea.localScale.z / 2, spawnArea.position.z + spawnArea.localScale.z / 2)
        );

        // Prefab'ý spawn et
        GameObject spawnedFruit = Instantiate(prefab, randomPosition, Quaternion.identity);

        // Spawn edilen nesneye Rigidbody ve Collider ayarlarýný yap
        Rigidbody rb = spawnedFruit.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // Sürekli çarpýþma algýlamasý
            rb.useGravity = true; // Yerçekimini kullan
        }

        Collider collider = spawnedFruit.GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = false; // Collider'ýn trigger olmadýðýndan emin ol
        }

        // Nesne yere düþtüðünde PlacementArea'ya yerleþtir
        PlaceObjectInArea(spawnedFruit);
    }

    // Nesneyi Placement Area'ya yerleþtir
    void PlaceObjectInArea(GameObject spawnedFruit)
    {
        // PlacementArea içine rastgele bir pozisyon
        Vector3 randomPlacementPosition = new Vector3(
            Random.Range(placementArea.position.x - placementArea.localScale.x / 2, placementArea.position.x + placementArea.localScale.x / 2),
            placementArea.position.y, // Placement area'nýn y pozisyonu
            Random.Range(placementArea.position.z - placementArea.localScale.z / 2, placementArea.position.z + placementArea.localScale.z / 2)
        );

        // Yerleþtirilen nesnenin pozisyonunu ayarla
        spawnedFruit.transform.position = randomPlacementPosition;

        // Nesnenin dönüþünü sýfýrla (gerektiðinde)
        spawnedFruit.transform.rotation = Quaternion.identity;
    }
}
