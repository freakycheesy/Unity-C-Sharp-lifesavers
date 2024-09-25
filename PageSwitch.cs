using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageSwitch : MonoBehaviour
{
    [SerializeField] bool startWithPageOpened = true;
    private void Start() {
        if (startWithPageOpened) {
            SwitchCategory(0);
        }
    }

    public void SwitchCategory(int selectedCategory) {
        int i = 0;
        foreach (Transform Category in transform) {
            if (i == selectedCategory) {
                Category.gameObject.SetActive(true);
            }
            else {
                Category.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
