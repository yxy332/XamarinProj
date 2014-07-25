
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using EasyPacking;
using OCTVision.Frame.Debug;

namespace EasyPacking_IOS
{
	public partial class TravelViewCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("TravelViewCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("TravelViewCell");

		#region Fields
		private TravelData m_travel_data;
		#endregion

		#region Properties
		public TravelData travel_data {
			get { return m_travel_data; }
			set { 
				m_travel_data = value; 
				UILabel label_destination = RetriveViewByID ("label_destination") as UILabel;
				label_destination.Text = m_travel_data.destination;
				UILabel label_period = RetriveViewByID ("label_period") as UILabel;
				label_period.Text = string.Format ("{0}天之旅", m_travel_data.period);
				float string_size = label_destination.StringSize (label_destination.Text, label_destination.Font).Width;
				Debugger.LogInfo (string_size.ToString ());
				label_period.Frame = new RectangleF (label_destination.Frame.Right + string_size + 10, label_period.Frame.Y, 
					label_period.Frame.Width, label_period.Frame.Height);
			}
		}
		#endregion

		#region Methods
		public TravelViewCell (IntPtr ptr) : base (ptr)
		{
			SelectionStyle = UITableViewCellSelectionStyle.None;
		}

		public static TravelViewCell Create ()
		{
			return (TravelViewCell)Nib.Instantiate (null, null) [0];
		}

		public UIView RetriveViewByID (string p_restoration_id) 
		{
			for(int i = 0; i < ContentView.Subviews.Length; ++i)
			{
				if (ContentView.Subviews [i].RestorationIdentifier == p_restoration_id) {
					return ContentView.Subviews [i];
				}
			}

			return null;
		}
		#endregion

	}
}

