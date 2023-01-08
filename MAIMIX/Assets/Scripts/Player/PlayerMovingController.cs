using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovingController : MonoBehaviour
{
    private TargetsSpawnManager targets_spawn_manager;
    [SerializeField] private float player_offset = -3;
    private float move_period = 1f;
    private void Start()
    {
        targets_spawn_manager = TargetsSpawnManager.instance;
        targets_spawn_manager.OnTargetSpawned += Move;
       /* Camera.main.GetComponent<CinemachineBrain>().
                    ActiveVirtualCamera.VirtualCameraGameObject.
                    GetComponent<CinemachineVirtualCamera>().
                    GetCinemachineComponent<CinemachineTransposer>().
                    m_FollowOffset += new Vector3(0, -player_offset, 0);*/
    }
    private void OnEnable()
    {
        if (targets_spawn_manager == null)
            return;
        targets_spawn_manager.OnTargetSpawned += Move;
    }
    private void OnDisable()
    {
        targets_spawn_manager.OnTargetSpawned -= Move;
    }
    public void Move(float dist)
    {
        StartCoroutine(MoveCoroutine(move_period, dist));
    }
    private IEnumerator MoveCoroutine(float period, float dist)
    {
        float whole_time = period;
        while(period > 0)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, dist + player_offset, 0), (whole_time - period) / whole_time);
            period -= Time.deltaTime;
            yield return null;
        }
        yield break;
    }
}
