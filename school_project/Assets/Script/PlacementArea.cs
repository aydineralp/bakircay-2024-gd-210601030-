using UnityEngine;

public class PlacementArea : MonoBehaviour
{
    public Transform placementPoint;  // Platform �zerindeki sabit nokta
    private GameObject currentObject; // Platformda bulunan mevcut nesne

    void OnTriggerEnter(Collider other)
    {
        // Meyve nesnesi mi? (Tag kontrol� yap�yoruz)
        if (other.CompareTag("Fruit"))
        {
            // E�er platformda bir nesne yoksa, yeni meyve ekle
            if (currentObject == null)
            {
                currentObject = other.gameObject;
                currentObject.transform.position = placementPoint.position; // Nesneyi placementPoint konumuna ta��
                currentObject.GetComponent<Collider>().enabled = false; // �arp��may� devre d��� b�rak
            }
            else
            {
                // E�er nesneler ayn� t�rdeyse, her ikisini yok et
                if (currentObject.name == other.gameObject.name)
                {
                    Destroy(currentObject);  // Eski nesneyi yok et
                    Destroy(other.gameObject);  // Yeni nesneyi yok et
                    currentObject = null;  // currentObject s�f�rlan�r
                }
                else
                {
                    // Farkl� t�rde nesne geldi�inde, ikinci nesne platformdan f�rlat�l�r
                    Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        Vector3 forceDirection = (other.transform.position - placementPoint.position).normalized;
                        rb.AddForce(forceDirection * 500f);  // Kuvvet uygulama
                    }
                    currentObject = null;  // currentObject s�f�rlan�r, platform bo�alt�l�r
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        // Nesnenin placementArea'ya yak�nken "�ekilme" etkisini ekleyelim
        if (other.CompareTag("Fruit"))
        {
            other.transform.position = Vector3.Lerp(other.transform.position, placementPoint.position, Time.deltaTime * 10f);  // Lerp ile yava��a yerle�tir
        }
    }

    void OnTriggerExit(Collider other)
    {
        // E�er nesne platformdan ��karsa, currentObject s�f�rlan�r
        if (other.CompareTag("Fruit"))
        {
            currentObject = null;
        }
    }
}
