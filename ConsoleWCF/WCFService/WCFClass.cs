using System;
using System.Collections.Generic;
using System.ServiceModel;
using ConsoleWCF.WCFService.AuthService;
using Microsoft.Extensions.Logging;

namespace ConsoleWCF.WCFService {
	public class WCFClass: IDisposable {
		/// <summary>
		/// Список запущенных сервисов.
		/// </summary>
		private readonly List<ServiceHost> _Hosts = new List<ServiceHost>();
		
		/// <summary>
		/// Запустить сервисы.
		/// </summary>
		public void StartServices(ILoggerFactory loggerFactory) {
			try { _Hosts.Add(StartService(typeof(AuthWCF))); }
			catch (Exception exception) {
				loggerFactory.CreateLogger<WCFClass>().
					LogError(
						exception,
						"Ошибка запуска службы. Служба: {serviceName}",
						nameof(WCFClass)
					);
				throw;
			}
		}

		/// <summary>
		/// Запустить сервис.
		/// </summary>
		/// <param name="service">Сервис для запуска</param>
		private static ServiceHost StartService(Type service) {
			ServiceHost host	= new ServiceHost(service);
			host.Open();
			return host;
		}

		/// <summary>
		/// Закрыть все сервисы.
		/// </summary>
		public void Dispose() => _Hosts.ForEach(host => host.Close());
	}
}