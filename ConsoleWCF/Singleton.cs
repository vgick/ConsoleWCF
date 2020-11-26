using System.Collections.Generic;

namespace ConsoleWCF {
	/// <summary>
	/// Синглтоны.
	/// </summary>
	public static class Singleton<T> where T : class, new() {
		/// <summary>
		/// Список синглтонов.
		/// </summary>
		private static readonly List<T> _Singleton = new List<T>();

		/// <summary>
		/// Вернуть объект класса.
		/// </summary>
		/// <returns>Объект</returns>
		public static T Values {
			get {
				foreach (T @object in _Singleton) {
					if (@object is T) {
						return @object;
					}
				}

				T newObject = new T();
				_Singleton.Add(newObject);

				return newObject;
			}
		}
	}
}