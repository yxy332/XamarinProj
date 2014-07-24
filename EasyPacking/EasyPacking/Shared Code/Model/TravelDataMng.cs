using System;
using System.IO;
using System.Collections;
using System.Xml;
using OCTVision.Frame.Debug;

namespace EasyPacking
{
	public class TravelDataMng
	{
		static private TravelDataMng m_instance = null;

		protected TravelDataMng() {
		}

		static public TravelDataMng instance
		{
			get {
				if (m_instance == null) {
					m_instance = new TravelDataMng ();
				}

				return m_instance;
			}
		}

		#region Fields
		private ArrayList m_travel_datas = new ArrayList();
		#endregion

		#region Properties
		public int count {
			get { return m_travel_datas.Count; }
		}
		#endregion

		#region Methods
		public void LoadFromXml () 
		{
			XmlTextReader xml_reader = null;
			m_travel_datas.Clear ();

			#if __IOS__
			TextReader text_reader = new StreamReader("./UserData/TravelData.xml");
			xml_reader = XmlReader.Create(text_reader) as XmlTextReader;
			xml_reader.WhitespaceHandling = WhitespaceHandling.All;

			try{
				while(xml_reader.Read())
				{
					if(xml_reader.NodeType == XmlNodeType.Element)
					{
						if(xml_reader.Name == "item")
						{
							int id = Int32.Parse(xml_reader.GetAttribute("id"));
							string destination = xml_reader.GetAttribute("destination");
							int period = Int32.Parse(xml_reader.GetAttribute("period"));
							DateTime date = DateTime.Parse(xml_reader.GetAttribute("beg_date"));
							TravelData data = new TravelData(id);
							data.Set(destination, period, date);
							m_travel_datas.Add(data);
						}
					}
				}
					
				Debugger.LogInfo("Read Count = " + m_travel_datas.Count);
			} catch(Exception e){
				Debugger.LogError(e.Message);
			} finally{
				if(xml_reader != null)
				{
					xml_reader.Close();
				}
			}
			#else

			#endif
		}

		public void Edit (TravelData p_data) 
		{
			int id = p_data.id;

			foreach (TravelData item in m_travel_datas) {
				if (item.id == id) {
					item.Set (p_data.destination, p_data.period, p_data.beg_date);
					break;
				}
			}
		}

		public TravelData Find (int p_id)
		{
			foreach (TravelData item in m_travel_datas) {
				if (item.id == p_id) {
					return item;
				}
			}

			return null;
		}

		public TravelData Get (int p_idx)
		{
			if (p_idx >= count) {
				return null;
			}

			return m_travel_datas [p_idx] as TravelData;
		}

		public bool Contain (int p_id) 
		{
			foreach (TravelData item in m_travel_datas) {
				if (item.id == p_id) {
					return true;
				}
			}

			return false;
		}
		#endregion
	}
}

