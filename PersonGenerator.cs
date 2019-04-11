using UnityEngine;

namespace ListView {
	public static class PersonGenerator {
		private static readonly string[] Firstname = {
			"Alice", "Bob", "John", "Julie"
		};
		
		private static readonly string[] Lastname = {
			"Becker", "Smith", "Nash", "Anderson"
		};

		public static PersonItem Generate(int id){
			return new PersonItem(id){
				Firstname = Firstname[Random.Range(0, Firstname.Length)],
				Lastname = Lastname[Random.Range(0, Lastname.Length)],
				Age = Random.Range(21, 90),
				Pin = Random.Range(1000, 10000)
			};
		}
	}
}