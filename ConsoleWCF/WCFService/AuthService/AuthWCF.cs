using System.Linq;
using System.ServiceModel.Web;
using ConsoleWCF.Interfaces;

namespace ConsoleWCF.WCFService.AuthService {
	public class AuthWCF : IAuth {
		/// <summary>
		/// Конструктор.
		/// </summary>
		AuthWCF() => DB	= ConsoleWCF.DB.DB.CreateDB(Program.LoggerFactory);
		
		/// <summary>
		/// База данных.
		/// </summary>
		private IDB DB { get; }

		/// <summary>
		/// Проверить авторизацию пользователя.
		/// </summary>
		/// <param name="login">Логин пользователя</param>
		/// <param name="password">Пароль пользователя</param>
		/// <returns>Если авторизация прошла успешно, то возвращается имя пользователя,
		/// в противном случае пустое значение</returns>
		public string CheckLogin(string login, string password) {
			AddCorsHeaders();

			if (login == default || password == default) return default;
			
			return DB.GetUserName(new User() {Login = login, Password = password});
		}
		
		/// <summary>
		/// Добавить заголовки для CORS.
		/// </summary>
		private void AddCorsHeaders() {
			var allowedOrigins	= new [] { "http://localhost:4200"};
			var request			= WebOperationContext.Current.IncomingRequest;
			var response		= WebOperationContext.Current.OutgoingResponse;
			var origin	= request.Headers["Origin"];

			if (origin != null && allowedOrigins.Any(x => x == origin)) {
				response.Headers.Add("Access-Control-Allow-Origin", origin);
				response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
				response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, X-Requested-With");
				response.Headers.Add("Access-Control-Allow-Credentials", "true");
			}
		}
	}
}