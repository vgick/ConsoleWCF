using System.Runtime.Serialization;

namespace ConsoleWCF.WCFService.AuthService {
	/// <summary>
	/// Данные пользователя для авторизации.
	/// </summary>
	[DataContract]
	public class User {
		/// <summary>
		/// Логин пользователя.
		/// </summary>
		public string Login { get; set; }
		
		/// <summary>
		/// Пароль пользователя.
		/// </summary>
		public string Password { get; set; }
	}
}