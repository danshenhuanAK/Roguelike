using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollider : MonoBehaviour
{

    //����������ײ�Ļص�����
    private void OnParticleCollision(GameObject other)
    {
        print(other.name);
    }
    //���Ӵ����Ļص�����
    private void OnParticleTrigger()
    {
        //ֻҪ��ѡ������ϵͳ��trigger���������к��һֱ��ӡ
        print("������");

        //�ٷ�ʾ��������˵��
        ParticleSystem ps = transform.GetComponent<ParticleSystem>();

        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();
        //particleSystemTriggerEventTypeΪö�����ͣ�Enter,Exit,Inside,Outside,��Ӧ����ϵͳ��������ϵ��ĸ�ѡ��
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
        //���봥���������ӱ�Ϊ��ɫ
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            p.startColor = Color.red;
            enter[i] = p;
        }
        //�˳������� ���ӱ�Ϊ����ɫ
        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = exit[i];
            p.startColor = Color.cyan;
            exit[i] = p;
        }

        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    }

}