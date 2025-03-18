using UnityEngine;

namespace WeaponObilities
{
	public class SaveWeaponSetting
    {
		private int cur = 0;
		private string id;

        public SaveWeaponSetting(string id, int cur)
        {
            this.id = id;
			this.cur = cur;
		}

		#region Сохранение пуль PlayerPrefs
		//сохранение пуль
		public void SaveCountButllets(int cur)
		{
			if (PlayerPrefs.HasKey(id))
			{
				PlayerPrefs.SetInt(id, cur);
			}
			else
			{
				PlayerPrefs.SetInt(id, this.cur);
			}
			PlayerPrefs.Save();
		}

		//взять сохранненое количество пуль
		public int GetCountBullets()
		{
			if (PlayerPrefs.HasKey(id))
			{
				return PlayerPrefs.GetInt(id);
			}

			return PlayerPrefs.GetInt(id, 0);
		}
		#endregion

	}
}
