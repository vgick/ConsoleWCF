using System;
using ConsoleWCF.WCFService;
using Microsoft.Extensions.Logging;
using static ConsoleWCF.WCFService.WCFClass;

namespace ConsoleWCF {
	internal class Program {
		/// <summary>
		/// Логгер.
		/// </summary>
		internal static ILoggerFactory LoggerFactory { get; private set; }
		
		/// <summary>
		/// Метод main.
		/// </summary>
		/// <param name="args"></param>
		public static void Main(string[] args) {
			LoggerFactory	= new LoggerFactory().
				AddSeq().
				AddFile(AppDomain.CurrentDomain.BaseDirectory + "Logs/wcf-{Date}.txt");

			using (WCFClass wcf = new WCFClass()) {
				wcf.StartServices(LoggerFactory);
				Console.ReadLine();
			}
		}
	}
}