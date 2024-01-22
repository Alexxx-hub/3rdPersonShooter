using System;

namespace _Project.Scripts
{
    public static class PlayerSignals
    {
        public static event Action<int, int> onFire;
        public static event Action<int> onAmmoUpdate;

        public static void Fire(int clipSize, int currentAmo)
        {
            onFire?.Invoke(clipSize, currentAmo);
        }

        public static void AmmoUpdate(int ammo)
        {
            onAmmoUpdate?.Invoke(ammo);
        }
    }
}