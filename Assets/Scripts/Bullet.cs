using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int bulletDamage;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision objectWehit)
    {
        if(objectWehit.gameObject.CompareTag("Target"))
        {
             print("hit " + objectWehit.gameObject.name + " !");


            CreateBulletImpactEffect(objectWehit);

             Destroy(gameObject);


        }
        if (objectWehit.gameObject.CompareTag("Wall"))
        {
            print("hit a wall");


            CreateBulletImpactEffect(objectWehit);


            Destroy(gameObject);
        }

        if (objectWehit.gameObject.CompareTag("Zombie"))
        {
            print("hit zombie");

            if (objectWehit.gameObject.GetComponent<Zombie>().isDead == false)
            {
                objectWehit.gameObject.GetComponent<Zombie>().TakeDamage(bulletDamage);
            }

            objectWehit.gameObject.GetComponent<Zombie>().TakeDamage(bulletDamage);

            CreateBloodSprayEffect(objectWehit);

            Destroy(gameObject);
        }

        void CreateBulletImpactEffect(Collision objectWehit)
        {
            ContactPoint contact = objectWehit.contacts[0];

            GameObject hole = Instantiate(
                GlobalReferences.Instance.bulletImpactEffectPrefab,
                contact.point,
                Quaternion.LookRotation(contact.normal) 
                
                );


            hole.transform.SetParent(objectWehit.gameObject.transform);

        }
    }

    private void CreateBloodSprayEffect(Collision objectWehit)
    {
        ContactPoint contact = objectWehit.contacts[0];

        GameObject bloodSprayPrefab = Instantiate(
            GlobalReferences.Instance.bloodSprayEffect,
            contact.point,
            Quaternion.LookRotation(contact.normal)

            );


        objectWehit.transform.SetParent(objectWehit.gameObject.transform);
    }
}
