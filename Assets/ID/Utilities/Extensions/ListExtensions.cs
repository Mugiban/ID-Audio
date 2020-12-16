using System.Collections.Generic;

namespace ID.Runtime.Utilities.Extensions
{
	public static class ListExtensions
	{
		public static T Random<T>(this IList<T> list)
		{
			int listCount = list.Count;

			if (listCount < 1)
				return default;

			var i = UnityEngine.Random.Range(0, listCount);
			return list[i];
		}

		public static T RemoveRandom<T>(this IList<T> list)
		{
			int listCount = list.Count;

			if (listCount < 1)
				return default;

			var i = UnityEngine.Random.Range(0, listCount);
			var item = list[i];
			list.RemoveAt(i);
			return item;
		}
		

		public static T First<T>(this IList<T> source)
		{
			if (source.Count == 0)
				return default(T);

			return source[0];
		}

		public static T Last<T>(this IList<T> source)
		{
			if (source.Count == 0)
				return default(T);

			return source[source.Count - 1];
		}

		public static List<T> Clone<T>(this List<T> source)
		{
			List<T> newList = new List<T>();

			for (int i = 0; i < source.Count; i++)
			{
				newList.Add(source[i]);
			}

			return newList;
		}

		public static void Shuffle<T>(this IList<T> list)
		{
			int index = list.Count;

			int newPos;
			T temp;

			while (index > 1)
			{
				index--;
				newPos = UnityEngine.Random.Range(0, index + 1);
				temp = list[newPos];
				list[newPos] = list[index];
				list[index] = temp;
			}
		}

		public static void ReplaceIndex<T>(this IList<T> list, int indexOne, int indexTwo)
		{
			if (indexOne < 0 || indexTwo < 0 || indexOne == indexTwo || indexOne >= list.Count || indexTwo >= list.Count)
				return;

			T temp = list[indexOne];
			list[indexOne] = list[indexTwo];
			list[indexTwo] = temp;
		}
	}
}