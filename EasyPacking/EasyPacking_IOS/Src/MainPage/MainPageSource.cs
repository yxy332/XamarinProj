using System;
using MonoTouch.UIKit;
using EasyPacking;

namespace EasyPacking_IOS
{
	public class MainPageSource : UITableViewSource 
	{
		#region Fields
		private string m_cell_identifier = "travelcell"; // set in the Storyboard
		#endregion

		public MainPageSource ()
		{
		}

		#region override Methods
		public override UIView GetViewForHeader (UITableView tableView, int section)
		{
			
			throw new NotImplementedException ();
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return TravelDataMng.instance.count;
		}

		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			// in a Storyboard, Dequeue will ALWAYS return a cell, 
			UITableViewCell cell = tableView.DequeueReusableCell (m_cell_identifier);

			if (cell == null) { 
				cell = new UITableViewCell (UITableViewCellStyle.Default, m_cell_identifier); 
			}

			//---- set the item text
			// now set the properties as normal
			cell.TextLabel.Text = TravelDataMng.instance.Get(indexPath.Row).destination;

			return cell;
		}
		#endregion
	}
}

