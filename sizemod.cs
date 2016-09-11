using Terraria.ModLoader;

namespace sizemod
{
	class sizemod : Mod
	{
		public sizemod()
		{
			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
		}
	}
}
