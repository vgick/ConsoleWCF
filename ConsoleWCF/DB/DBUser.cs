using System;
using ConsoleWCF.WCFService.AuthService;

namespace ConsoleWCF.DB {
	/// <summary>
	/// Данные пользователя в БД.
	/// </summary>
	public class DBUser: User {
		/// <summary>
		/// Разделитель в строке.
		/// </summary>
		private static char _separate = '\t';
		
		/// <summary>
		/// Создать объект на основе строки.
		/// </summary>
		/// <param name="rawData">Строка с данными</param>
		public DBUser(string rawData) {
			string[] data	= rawData.Split(_separate);
			if (data == default || data.Length != 3) throw new Exception($"Не корректные данные{rawData}");
			
			Login		= data[0];
			Password	= data[1];
			Name		= data[2];
		}
		
		/// <summary>
		/// Имя пользователя.
		/// </summary>
		public string Name { get; }
	}
}