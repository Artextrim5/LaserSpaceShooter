using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    WaveConfig waveConfig;                    // ссылка на другой класс
    List<Transform> wayPoints;                // лист содержащий координаты
    int wayPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        wayPoints = waveConfig.GetWayPoints();
        transform.position = wayPoints[wayPointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    public void SetWaveConfig(WaveConfig waveConfig) // создаем метод который должен возвращать нам определенную переменную определенного типа
    {
        this.waveConfig = waveConfig;  // приравниваем конкретно этой нашей переменной переменную которую получили
    }


    private void EnemyMove()
    {
        if (wayPointIndex <= wayPoints.Count - 1)
        {
            var targetPosition = wayPoints[wayPointIndex].transform.position;
            var movementThisFraim = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFraim);

            if (transform.position == targetPosition)
            {
                wayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
