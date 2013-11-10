using System;
using System.Text;

namespace Utilities {

	public class Csv {
	
		private StringBuilder CsvBuffer { get; set; }

		public Csv () {
			CsvBuffer = new StringBuilder ();
		}

		public Csv (string csvString) {
			CsvBuffer = new StringBuilder ();
			CsvBuffer.Append (csvString);
		}

		public void Append(bool item) {
			Append (item ? 1 : 0);
		}

		public void Append(int item) {
			if (CsvBuffer.Length == 0) {
				CsvBuffer.Append (item.ToString ());
			} else {
				CsvBuffer.AppendFormat (",{0}", item.ToString ());
			}
		}

		public override string ToString () {
			return CsvBuffer.ToString ();
		}

		public int[] ToIntArray() {
			string[] items = ToString ().Split (',');
			int[] ints = new int[items.Length];
			for (int i=0; i<items.Length; i++) {
				ints [i] = Convert.ToInt32 (items [i]);
			}
			return ints;
		}

	}
}

