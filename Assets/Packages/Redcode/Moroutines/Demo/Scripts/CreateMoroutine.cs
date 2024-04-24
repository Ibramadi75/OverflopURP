using System.Collections;
using UnityEngine;

namespace Redcode.Moroutines.Demo
{
    public class CreateMoroutine : MonoBehaviour
    {
        void Start() => Moroutine.Run(TickEnumerable());

        private IEnumerable TickEnumerable()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                print("Tick!");
            }
        }



    }
}