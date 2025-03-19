using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int HP = 100;
    public GameObject bloodyScreen;
    public TextMeshProUGUI playerhealthUI;
    public GameObject gameOverUI;
    public GameObject blackoutUI; // Thêm biến Blackout UI
    public Animator cameraAnimator; // Thêm biến Animator của Camera
    public bool isDead;

    private void Start()
    {
        // Hiển thị Health ngay khi bắt đầu game
        UpdateHealthUI();
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return; // Nếu đã chết thì không nhận thêm sát thương

        HP -= damageAmount;
        if (HP <= 0)
        {
            HP = 0;
            isDead = true;
            PlayerDead();
        }
        else
        {
            StartCoroutine(BloodyScreenEffect());
            UpdateHealthUI();
            SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerHurt);
        }
    }

    private void PlayerDead()
    {
        // Bật âm thanh chết
        SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerDie);

        // Vô hiệu hóa điều khiển
        GetComponent<MouseMovement>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;

        // Bật Animator trong Camera khi chết
        if (cameraAnimator != null)
        {
            cameraAnimator.enabled = true;
            cameraAnimator.Play("DeathAnimation"); // Chỉnh tên animation nếu cần
        }

        // Ẩn UI health
        playerhealthUI.gameObject.SetActive(false);

        // Bật hiệu ứng blackout
        blackoutUI.SetActive(true);

        // Bắt đầu hiệu ứng fade màn hình
        GetComponent<ScreenFader>().StartFade();

        // Hiển thị GameOver UI sau một giây
        StartCoroutine(ShowGameOverUI());
    }

    private IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(1f);
        gameOverUI.SetActive(true);
    }

    private IEnumerator BloodyScreenEffect()
    {
        if (!bloodyScreen.activeInHierarchy)
        {
            bloodyScreen.SetActive(true);
        }

        var image = bloodyScreen.GetComponentInChildren<Image>();
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bloodyScreen.SetActive(false);
    }

    private void UpdateHealthUI()
    {
        playerhealthUI.text = $"Health: {HP}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zomhand"))
        {
            if (!isDead)
            {
                TakeDamage(other.gameObject.GetComponent<Zomhand>().damage);
            }
        }
    }
}
