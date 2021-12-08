using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GunPicker : MonoBehaviour
{
    public List<GameObject> guns;
    public GameObject lockView;
    public TextMeshProUGUI message;
    public Button startButton;

    private int assaultUnlockPoint = 150;
    private int shotgunUnlockPoint = 100;

    int selectedIndex = 0;
    void Start()
    {
        /*GameObject gun = guns[selectedIndex];
        MainManager.Manager.selectedGun = gun;
        transform.Find("Gun").Find("name").GetComponent<TextMeshProUGUI>().text = gun.name;
        Transform child = transform.Find("Gun").Find("GunParent").GetChild(0);
        GameObject go = Instantiate(gun, child.transform.position, child.transform.rotation);
        go.layer = 5;
        go.transform.localScale = Vector3.one;
        go.transform.SetParent(transform.Find("Gun").Find("GunParent"));
        Destroy(child.gameObject);*/
        changeGun(selectedIndex);
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
        if (IsUnlocked(gun))
        {
            lockView.SetActive(false);
            startButton.interactable = true;
        }
        else
        {
            lockView.SetActive(true);
            startButton.interactable = false;
        }
        MainManager.Manager.selectedGun = gun;
        transform.Find("Gun").Find("name").GetComponent<TextMeshProUGUI>().text = gun.name;
        Transform child = transform.Find("Gun").Find("GunParent").GetChild(0);
        GameObject go = Instantiate(gun, child.transform.position,child.transform.rotation);
        go.layer = 5;
        if (index != 0)
        {
            go.transform.localScale = new Vector3(0.6f,0.6f,0.6f);
        }
        go.transform.SetParent(transform.Find("Gun").Find("GunParent"));
        Destroy(child.gameObject);
    }

    bool IsUnlocked(GameObject gun)
    {
        message.text = "Need highscore > ";

        int highscore = MainManager.Manager.highscore;
        switch (gun.name.ToLower())
        {
            case "assault":
                message.text += assaultUnlockPoint;
                if (highscore >= assaultUnlockPoint) { return true; }
                break;
            case "shotgun":
                message.text += shotgunUnlockPoint;
                if (highscore >= shotgunUnlockPoint) {return true; }
                break;
            default: return true;
        }
        return false;
    }
}
