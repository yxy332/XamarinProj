using System;

namespace EasyPacking
{
	public class TravelData
	{
		public TravelData (int p_id)
		{
			m_id = p_id;
		}

		#region Fields
		private int m_id = 0;
		private string m_destination = null;
		private int m_period = 0;
		private DateTime m_beg_date = new DateTime();
		#endregion

		#region Properties
		public int id {
			get { return m_id; }
		}

		public string destination {
			get { return m_destination; }
		}

		public int period {
			get { return m_period; }
		}

		public DateTime beg_date {
			get { return m_beg_date; }
		}
		#endregion

		#region Methods
		public void Set(string p_des, int p_period, DateTime p_beg_date) {
			if (string.IsNullOrEmpty(p_des) || p_period <= 0) {
				throw new ArgumentNullException ();
			}

			m_destination = p_des;
			m_period = p_period;
			m_beg_date = p_beg_date;
		}
		#endregion
	}
}

