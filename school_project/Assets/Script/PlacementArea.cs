using UnityEngine;

public class PlacementArea : MonoBehaviour
{
    public Transform placementPoint;  // Platform üzerindeki sabit nokta
    private GameObject currentObject; // Platformda bulunan mevcut nesne

    void OnTriggerEnter(Collider other)
    {
        // Meyve nesnesi mi? (Tag kontrolü yapýyoruz)
        if (other.CompareTag("Fruit"))
        {
            // Eðer platformda bir nesne yoksa, yeni meyve ekle
            if (currentObject == null)
            {
                currentObject = other.gameObject;
                currentObject.transform.position = placementPoint.position; // Nesneyi placementPoint konumuna taþý
                currentObject.GetComponent<Collider>().enabled = false; // Çarpýþmayý devre dýþý býrak
            }
            else
            {
                // Eðer nesneler ayný türdeyse, her ikisini yok et
                if (currentObject.name == other.gameObject.name)
                {
                    Destroy(currentObject);  // Eski nesneyi yok et
                    Destroy(other.gameObject);  // Yeni nesneyi yok et
                    currentObject = null;  // currentObject sýfýrlanýr
                }
                else
                {
                    // Farklý türde nesne geldiðinde, ikinci nesne platformdan fýrlatýlýr
                    Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        Vector3 forceDirection = (other.transform.position - placementPoint.position).normalized;
                        rb.AddForce(forceDirection * 500f);  // Kuvvet uygulama
                    }
                    currentObject = null;  // currentObject sýfýrlanýr, platform boþaltýlýr
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        // Nesnenin placementArea'ya yakýnken "çekilme" etkisini ekleyelim
        if (other.CompareTag("Fruit"))
        {
            other.transform.position = Vector3.Lerp(other.transform.position, placementPoint.position, Time.deltaTime * 10f);  // Lerp ile yavaþça yerleþtir
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Eðer nesne platformdan çýkarsa, currentObject sýfýrlanýr
        if (other.CompareTag("Fruit"))
        {
            currentObject = null;
        }
    }
}
