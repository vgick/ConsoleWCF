using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConsoleWCF.Interfaces;
using ConsoleWCF.WCFService.AuthService;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ConsoleWCF.DB {
	public class DB: IDB {
		/// <summary>
		/// Синглтон.
		/// </summary>
		private static IDB _DB;
		
		/// <summary>
		/// Имя файла в котором содержатся данные пользователей.
		/// </summary>
		private static string _UserFileName = @"../../users.txt";

		/// <summary>
		/// Инициализировать список пользователей.
		/// Первая строка в файле - заголовок.
		/// </summary>
		private DB(ILoggerFactory loggerFactory) {
			try { LoadData().Wait(); }
			catch (AggregateException exception) {
				loggerFactory.CreateLogger<DB>().
					LogError(
						exception.InnerException,
						"Не удалось загрузить базу пользователей файл: {sourceFile}",
						_UserFileName
					);
				throw;
			}
			catch (Exception exception) {
				loggerFactory.CreateLogger<DB>().
					LogError(
						exception,
						"Не удалось загрузить базу пользователей файл: {sourceFile}",
						_UserFileName
					);
				throw;
			}
		}

		/// <summary>
		/// Синглтон.
		/// </summary>
		/// <param name="loggerFactory">Фабрика логгера.</param>
		/// <returns>База</returns>
		public static IDB CreateDB(ILoggerFactory loggerFactory) {
			return _DB ?? (_DB = new DB(loggerFactory));
		}

		/// <summary>
		/// Загрузить базу из файла асинхронно.
		/// </summary>
		/// <returns>Задача</returns>
		private static async Task LoadData() {
			List<DBUser> users	= new List<DBUser>();
			
			string record;
			bool headerHasRead	= false;
			StreamReader file	= new StreamReader(_UserFileName);
			while((record = await file.ReadLineAsync()) != default) {
				if (!headerHasRead) {
					headerHasRead	= true;
					continue;
				};
				
				users.Add(new DBUser(record));
			}  
  
			file.Close();
			_DBUsers	= users;
		}
		
		/// <summary>
		/// Список пользователей.
		/// </summary>
		private static IEnumerable<DBUser> _DBUsers;
		
		/// <summary>
		/// Если в базе найден пользователь с парой login/password, то возвращается имя пользователя, в противном случае,
		/// пустое значение.
		/// В идеале в БД должен храниться хэш пароля.
		/// </summary>
		/// <param name="user">Логин/пароль пользователя</param>
		/// <returns>Имя пользователя или пустое значение если пользователь не найден</returns>
		public string GetUserName(User user) =>
			_DBUsers.
				FirstOrDefault(u => u.Login.Equals(user.Login) && u.Password.Equals(user.Password))?.Name;
	}
}