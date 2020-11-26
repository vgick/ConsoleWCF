using ConsoleWCF.WCFService.AuthService;

namespace ConsoleWCF.Interfaces {
	/// <summary>
	/// Интерфейс для работы с БД.
	/// </summary>
	public interface IDB {
		/// <summary>
		/// Если в базе найден пользователь с парой login/password, то возвращается имя пользователя ,в противном случае,
		/// пустое значение.
		/// В идеале в БД должен храниться хэш пароля
		/// </summary>
		/// <param name="user">Логин/пароль пользователя</param>
		/// <returns>Имя пользователя или пустое значение если пользователь не найден</returns>
		string GetUserName(User user);
	}
}