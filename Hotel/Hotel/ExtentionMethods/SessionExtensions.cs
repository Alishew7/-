
/*using System.Text.Json;

namespace Hotel.ExtentionMethods
{
	public static class SessionExtensions
	{
		public static void SetObject<T>(this ISession session, string key, T value) where T : class
		{
			if (value == null)
			{
				session.Remove(key);
				return;
			}
			else
			{
				string jsonData = JsonSerializer.Serialize(value);
				session.SetString(key, jsonData);
			}
		}

		public static T GetObject<T>(this ISession session, string key)
		{
			var value = session.GetString(key);
			return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
		}
	}
}
*/