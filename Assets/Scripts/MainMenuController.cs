using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenuPanel; // Ekrana gelecek panel
    private bool isPanelVisible = false; // Panelin açık/kapalı durumunu takip edecek

    void Start()
    {
        // Başlangıçta panelin kapalı olduğundan emin olun ve oyunu başlatın
        mainMenuPanel.SetActive(isPanelVisible);
        Time.timeScale = 1f; // Oyun hızını normal başlatın
        Cursor.visible = false; // Başlangıçta mouse imlecini gizleyin
        Cursor.lockState = CursorLockMode.Locked; // Mouse'u ekrana kilitleyin
    }

    void Update()
    {
        // "T" tuşuna basıldığında panelin durumunu değiştir
        if (Input.GetKeyDown(KeyCode.T))
        {
            isPanelVisible = !isPanelVisible; // Durumu tersine çevir
            mainMenuPanel.SetActive(isPanelVisible); // Paneli aç veya kapat

            if (isPanelVisible)
            {
                // Panel açıldığında oyunu durdur
                Time.timeScale = 0f; // Oyun zamanını durdurun
                Cursor.visible = true; // Mouse imlecini görünür yapın
                Cursor.lockState = CursorLockMode.None; // Mouse'u serbest bırakın
            }
            else
            {
                // Panel kapatıldığında oyuna devam et
                Time.timeScale = 1f; // Oyunu tekrar başlatın
                Cursor.visible = false; // Mouse imlecini gizleyin
                Cursor.lockState = CursorLockMode.Locked; // Mouse'u ekrana kilitleyin
            }
        }
    }
}
