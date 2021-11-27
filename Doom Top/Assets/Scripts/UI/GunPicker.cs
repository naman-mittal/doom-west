using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunPicker : MonoBehaviour
{
    public List<GameObject> guns;

    int selectedIndex = 0;
    void Start()
    {
        GameObject gun = guns[selectedIndex];
        MainManager.Manager.selectedGun = gun;
        transform.Find("Gun").Find("name").GetComponent<TextMeshProUGUI>().text = gun.name;
        Transform child = transform.Find("Gun").Find("GunParent").GetChild(0);
        GameObject go = Instantiate(gun, child.transform.position, child.transform.rotation);
        go.layer = 5;
        go.transform.SetParent(transform.Find("Gun").Find("GunParent"));
        Destroy(child.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextGun()
    {
        selectedIndex = (selectedIndex + 1) % guns.Count;
        changeGun(selectedIndex);
    }

    public void prevGun()
    {
        if (selectedIndex == 0)
        {
            selectedIndex = guns.Count - 1;
        }
        else
        {
            selectedIndex--;
        }
        changeGun(selectedIndex);
    }

    void changeGun(int index)
    {
        GameObject gun = guns[index];
        MainManager.Manager.selectedGun = gun;
        transform.Find("Gun").Find("name").GetComponent<TextMeshProUGUI>().text = gun.name;
        Transform child = transform.Find("Gun").Find("GunParent").GetChild(0);
        GameObject go = Instantiate(gun, child.transform.position,child.transform.rotation);
        go.layer = 5;
        go.transform.SetParent(transform.Find("Gun").Find("GunParent"));
        Destroy(child.gameObject);
    }
}
