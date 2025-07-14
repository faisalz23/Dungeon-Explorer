using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip footstepClip;
    public AudioClip attackClip;
    public AudioClip pickupClip;
    public AudioClip openChestClip;
    public AudioClip levelCompleteClip;
    public AudioClip destroyClip;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Hapus atau aktifkan baris ini jika kamu ingin/menghapus pengaturan volume SFX
        // audioSource.volume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
    }

    public void PlayFootstepSound()
    {
        PlaySound(footstepClip);
    }

    public void PlayAttackSound()
    {
        PlaySound(attackClip);
    }

    public void PlayPickupSound()
    {
        PlaySound(pickupClip);
    }

    public void PlayChestOpenSound()
    {
        PlaySound(openChestClip);
    }

    public void PlayLevelCompleteSound()
    {
        PlaySound(levelCompleteClip);
    }

    public void PlayDestroyObjectSound()
    {
        PlaySound(destroyClip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("⚠️ AudioSource atau AudioClip belum diisi!");
        }
    }
}
