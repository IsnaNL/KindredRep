using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager a_Instance;
    public AudioSource audioSource;
    public List<AudioClip> Alixclips;
    public List<AudioClip> BennyClips;
    public List<AudioClip> JetSwordClips;
    public List<AudioClip> ShotGunClips;
    public List<AudioClip> PickaxeClips;
    public List<AudioClip> AmbianceClips;
    public List<AudioClip> MenuClips;
    void Start()
    {
        a_Instance = this;
    }
    public void AlyxJumpAudio()
    {
      audioSource.PlayOneShot(Alixclips[2]);
    }
    public void AlyxHitAudio()
    {
        audioSource.PlayOneShot(Alixclips[3]);
    }
    public void AlyxLandAudio()
    {
        audioSource.PlayOneShot(Alixclips[3]);
    }
    public void AlyxJetSwordAttackAudio()
    {
        
        
        audioSource.PlayOneShot(JetSwordClips[0], 0.5f);
    }
    public void AlyxJetSwordDashAudio()
    {
        audioSource.PlayOneShot(JetSwordClips[3]);
    }
    public void AlyxJetSwordClashWithTerrainAudio()
    {
        audioSource.PlayOneShot(JetSwordClips[2], 0.5f);
    }
    public void AlyxShotGunMobilityAudio()
    {
        audioSource.PlayOneShot(ShotGunClips[1]);
    }
    public void AlyxShotGunShotAudio()
    {
        audioSource.PlayOneShot(ShotGunClips[2]);
    }
    public void AlyxShotGunclashAudio()
    {
        audioSource.PlayOneShot(ShotGunClips[0]);

    }
    public void AlyxPickaxeClashWithTerrain()
    {
        audioSource.PlayOneShot(PickaxeClips[0]);
    }
    public void BennyJumpAudio()
    {
       audioSource.PlayOneShot(BennyClips[0]);
    }
    public  void BennyAttackAudio()
    {
        audioSource.PlayOneShot(BennyClips[1]);

    }
    public void BennyIdleAudio()
    {
        audioSource.PlayOneShot(BennyClips[2]);

    }
    public void BennyPeekAudio()
    {
        audioSource.PlayOneShot(BennyClips[3]);

    }
    public void BennyHurtAudio()
    {
        audioSource.PlayOneShot(BennyClips[4]);
    }
}

