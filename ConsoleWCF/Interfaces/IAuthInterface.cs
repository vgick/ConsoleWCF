using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace ConsoleWCF.Interfaces {
	/// <summary>
	/// Сервис авторизации.
	/// </summary>
	[ServiceContract]
	public interface IAuth {
		/// <summary>
		/// Проверить авторизацию пользователя.
		/// </summary>
		/// <param name="login">Логин пользователя</param>
		/// <param name="password">Пароль пользователя</param>
		/// <returns>Если авторизация прошла успешно, то возвращается имя пользователя,
		/// в противном случае пустое значение</returns>
		[OperationContract]
		[WebInvoke(Method = "GET")]
		string CheckLogin(string login, string password);
	}
}