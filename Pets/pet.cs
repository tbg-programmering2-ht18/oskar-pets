using System;
using System.Collections.Generic;
using System.Text;

namespace Pets
{
    class Pet
    {
        public string Name { get; set; }
        public string Sound { get; set; }
        public bool CanFly { get; set; }
        public string AnimalType { get; set; }

        public Pet(string animaltype, string name, string sound, bool canfly)
        {
            Name = name;
            Sound = sound;
            CanFly = canfly;
            AnimalType = animaltype;
        }
        public string Show()
        {
            if (CanFly == true)
            {
                return string.Format("Name: {0}\nSound: {1}\nType and can fly!!", Name, Sound, AnimalType);
            }
            else
            {
                return string.Format("Name: {0}\nSound: {1}\nType", Name, Sound, AnimalType);
            }
        }
    }
}
