using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UIKit;
using CoreFoundation; 
using Foundation;

using System.IO;
using SQLite;
using System.Data;
using System.Data.Sql;
using Mono.Data.Sqlite;

using AudioToolbox; 

namespace FrenchPhraseBook
{
	[Table("Favourites")]
    public partial class FavController : UITableViewController
    {
		List<string> favouriteListLocalized = new List<string>() { };
		List<string> favouriteListFrench = new List<string>() { };

        public FavController (IntPtr handle) : base (handle){
        }

		public FavController(){}

		public AppDelegate favouritesRef
		{
			get
			{
				return (AppDelegate)UIApplication.SharedApplication.Delegate;
			}
		}

		public UIBarButtonItem removeItem = new UIBarButtonItem(UIBarButtonSystemItem.Edit, null);
		public UIBarButtonItem cancel = new UIBarButtonItem("Done", UIBarButtonItemStyle.Done, null); 

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell tableCell = new UITableViewCell(UITableViewCellStyle.Subtitle, "favouriteCells");

			if (tableCell == null)
			{
				tableCell = new UITableViewCell(UITableViewCellStyle.Subtitle, "favouriteCells");
			}

			UILabel favouritesIndicator = new UILabel();
			tableCell.TextLabel.Text = this.favouriteListLocalized[indexPath.Row];
			tableCell.TextLabel.TextColor = UIColor.Black;

			tableCell.DetailTextLabel.Text = this.favouriteListFrench[indexPath.Row];
			tableCell.DetailTextLabel.TextColor = UIColor.LightGray;

			favouritesIndicator = new UILabel();
			favouritesIndicator.Text = "\ud83d\udc9d";
			favouritesIndicator.MinimumFontSize = 20.0f;
			favouritesIndicator.AdjustsFontSizeToFitWidth = true;
			favouritesIndicator.Frame = new CoreGraphics.CGRect(0, 20, 40, 40);

			this.TableView.Layer.BorderWidth = 0.4f;
			this.TableView.TintColor = UIColor.Black;

			tableCell.AccessoryView = favouritesIndicator;

			return tableCell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			BatteryMonitor AI = new BatteryMonitor();
			AI.frenchPhraseBookAI(tableView.CellAt(indexPath).DetailTextLabel.Text);
			tableView.DeselectRow(indexPath, true);
	 	}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return this.favouriteListLocalized.Count;
		}

		public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
		{
			if (tableView.Editing == true) {
				return UITableViewCellEditingStyle.Delete;
			}
			else {
				return UITableViewCellEditingStyle.None;
			}
		}

		public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath)
		{
			return "Delete?";
		}

		public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			if (editingStyle == UITableViewCellEditingStyle.Delete)
			{
				try {
					//business
					if (Double.IsNaN(this.favouritesRef.localizedText.IndexOf(this.favouriteListLocalized[indexPath.Row])) == true) {
						throw new ArgumentOutOfRangeException();
					}
					else {
						int indexBusiness = this.favouritesRef.localizedText.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						int indexBusinessInt = this.favouritesRef.businessCarrier.companyDict.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.indexInt.RemoveAll((int obj) => obj == indexBusinessInt);


						this.favouritesRef.localizedText.RemoveAll((string obj) => obj == this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.frenchText.RemoveAll((string obj) => obj == this.favouriteListFrench[indexPath.Row]);
					}

					//dating
					if (Double.IsNaN(this.favouritesRef.localizedTextDating.IndexOf(this.favouriteListLocalized[indexPath.Row])) == true)
					{
						throw new ArgumentOutOfRangeException();
					}
					else {
						int indexDating = this.favouritesRef.localizedTextDating.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						int indexDatingInt = this.favouritesRef.datingControl.datingDict.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.indexIntDating.RemoveAll((int obj) => obj == indexDatingInt);


						this.favouritesRef.localizedTextDating.RemoveAll((string obj) => obj == this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.frenchTextDating.RemoveAll((string obj) => obj == this.favouriteListFrench[indexPath.Row]);
					}

					//family & friends
					if (Double.IsNaN(this.favouritesRef.localizedTextFamily.IndexOf(this.favouriteListLocalized[indexPath.Row])) == true)
					{
						throw new ArgumentOutOfRangeException();
					}
					else {
						int indexFamily = this.favouritesRef.localizedTextFamily.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						int indexFamilyInt = this.favouritesRef.familyControl.familyDict.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.indexIntFamily.RemoveAll((int obj) => obj == indexFamilyInt);


						this.favouritesRef.localizedTextFamily.RemoveAll((string obj) => obj == this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.frenchTextFamily.RemoveAll((string obj) => obj == this.favouriteListFrench[indexPath.Row]);
					}

					//emergency
					if (Double.IsNaN(this.favouritesRef.localizedTextEmergency.IndexOf(this.favouriteListLocalized[indexPath.Row])) == true)
					{
						throw new ArgumentOutOfRangeException();
					}
					else {
						int indexEmergency = this.favouritesRef.localizedTextEmergency.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						int indexEmergencyInt = this.favouritesRef.emergencyControl.emergencyDict.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.indexIntEmergency.RemoveAll((int obj) => obj == indexEmergencyInt);


						this.favouritesRef.localizedTextEmergency.RemoveAll((string obj) => obj == this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.frenchTextEmergency.RemoveAll((string obj) => obj == this.favouriteListFrench[indexPath.Row]);
					}

					//first time meeting
					if (Double.IsNaN(this.favouritesRef.localizedTextMeeting.IndexOf(this.favouriteListLocalized[indexPath.Row])) == true)
					{
						throw new ArgumentOutOfRangeException();
					}
					else {
						int indexFirst = this.favouritesRef.localizedTextMeeting.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						int indexFirstInt = this.favouritesRef.firstTimeMeetingControl.firstTimeDict.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.indexIntFirstTimeMeeting.RemoveAll((int obj) => obj == indexFirstInt);


						this.favouritesRef.localizedTextMeeting.RemoveAll((string obj) => obj == this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.frenchTextMeeting.RemoveAll((string obj) => obj == this.favouriteListFrench[indexPath.Row]);
					}

					//food 
					if (Double.IsNaN(this.favouritesRef.localizedTextFood.IndexOf(this.favouriteListLocalized[indexPath.Row])) == true)
					{
						throw new ArgumentOutOfRangeException();
					}
					else {
						int indexDating = this.favouritesRef.localizedTextFood.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						int indexDatingInt = this.favouritesRef.foodControl.foodDict.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.indexIntFood.RemoveAll((int obj) => obj == indexDatingInt);


						this.favouritesRef.localizedTextFood.RemoveAll((string obj) => obj == this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.frenchTextFood.RemoveAll((string obj) => obj == this.favouriteListFrench[indexPath.Row]);
					}

					//gardening
					if (Double.IsNaN(this.favouritesRef.localizedTextGardening.IndexOf(this.favouriteListLocalized[indexPath.Row])) == true)
					{
						throw new ArgumentOutOfRangeException();
					}
					else {
						int indexDating = this.favouritesRef.localizedTextGardening.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						int indexDatingInt = this.favouritesRef.gardeningControl.gardeningDict.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.indexIntGardening.RemoveAll((int obj) => obj == indexDatingInt);


						this.favouritesRef.localizedTextGardening.RemoveAll((string obj) => obj == this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.frenchTextGardening.RemoveAll((string obj) => obj == this.favouriteListFrench[indexPath.Row]);
					}


					//general conversation
					if (Double.IsNaN(this.favouritesRef.localizedTextGeneralConversation.IndexOf(this.favouriteListLocalized[indexPath.Row])) == true)
					{
						throw new ArgumentOutOfRangeException();
					}
					else {
						int indexDating = this.favouritesRef.localizedTextGeneralConversation.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						int indexDatingInt = this.favouritesRef.generalControl.generalDict.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.indexIntGeneralConversation.RemoveAll((int obj) => obj == indexDatingInt);


						this.favouritesRef.localizedTextGeneralConversation.RemoveAll((string obj) => obj == this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.frenchTextGeneralConversation.RemoveAll((string obj) => obj == this.favouriteListFrench[indexPath.Row]);
					}

					//math & numbers
					if (Double.IsNaN(this.favouritesRef.localizedTextMathNumbers.IndexOf(this.favouriteListLocalized[indexPath.Row])) == true)
					{
						throw new ArgumentOutOfRangeException();
					}
					else {
						int indexDating = this.favouritesRef.localizedTextMathNumbers.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						int indexDatingInt = this.favouritesRef.mathControl.firstTimeDict.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.indexIntMathNumbers.RemoveAll((int obj) => obj == indexDatingInt);


						this.favouritesRef.localizedTextMathNumbers.RemoveAll((string obj) => obj == this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.frenchTextMathNumbers.RemoveAll((string obj) => obj == this.favouriteListFrench[indexPath.Row]);
					}

					//school
					if (Double.IsNaN(this.favouritesRef.localizedTextSchool.IndexOf(this.favouriteListLocalized[indexPath.Row])) == true)
					{
						throw new ArgumentOutOfRangeException();
					}
					else {
						int indexDating = this.favouritesRef.localizedTextSchool.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						int indexDatingInt = this.favouritesRef.schoolControl.schoolDict.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.indexIntSchool.RemoveAll((int obj) => obj == indexDatingInt);


						this.favouritesRef.localizedTextSchool.RemoveAll((string obj) => obj == this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.frenchTextSchool.RemoveAll((string obj) => obj == this.favouriteListFrench[indexPath.Row]);
					}

					//Shopping
					if (Double.IsNaN(this.favouritesRef.localizedShopping.IndexOf(this.favouriteListLocalized[indexPath.Row])) == true)
					{
						throw new ArgumentOutOfRangeException();
					}
					else {
						int indexDating = this.favouritesRef.localizedShopping.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						int indexDatingInt = this.favouritesRef.shoppingControl.firstTimeDict.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.indexIntShopping.RemoveAll((int obj) => obj == indexDatingInt);


						this.favouritesRef.localizedShopping.RemoveAll((string obj) => obj == this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.frenchTextShopping.RemoveAll((string obj) => obj == this.favouriteListFrench[indexPath.Row]);
					}

					//technology
					if (Double.IsNaN(this.favouritesRef.localizedTechnology.IndexOf(this.favouriteListLocalized[indexPath.Row])) == true)
					{
						throw new ArgumentOutOfRangeException();
					}
					else {
						int indexDating = this.favouritesRef.localizedTechnology.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						int indexDatingInt = this.favouritesRef.technologyControl.firstTimeDict.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.indexIntTechnology.RemoveAll((int obj) => obj == indexDatingInt);


						this.favouritesRef.localizedTechnology.RemoveAll((string obj) => obj == this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.frenchTextTechnology.RemoveAll((string obj) => obj == this.favouriteListFrench[indexPath.Row]);
					}

					//transport
					if (Double.IsNaN(this.favouritesRef.localizedTransport.IndexOf(this.favouriteListLocalized[indexPath.Row])) == true)
					{
						throw new ArgumentOutOfRangeException();
					}
					else {
						int indexDating = this.favouritesRef.localizedTransport.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						int indexDatingInt = this.favouritesRef.transportControl.transportDict.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.indexIntTransport.RemoveAll((int obj) => obj == indexDatingInt);


						this.favouritesRef.localizedTransport.RemoveAll((string obj) => obj == this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.frenchTextTransport.RemoveAll((string obj) => obj == this.favouriteListFrench[indexPath.Row]);
					}

					//travel
					if (Double.IsNaN(this.favouritesRef.localizedTravel.IndexOf(this.favouriteListLocalized[indexPath.Row])) == true)
					{
						throw new ArgumentOutOfRangeException();
					}
					else {
						int indexDating = this.favouritesRef.localizedTravel.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						int indexDatingInt = this.favouritesRef.travelControl.travelDict.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.indexIntTravel.RemoveAll((int obj) => obj == indexDatingInt);


						this.favouritesRef.localizedTravel.RemoveAll((string obj) => obj == this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.frenchTextTravel.RemoveAll((string obj) => obj == this.favouriteListFrench[indexPath.Row]);
					}

					//Work
					if (Double.IsNaN(this.favouritesRef.localizedWork.IndexOf(this.favouriteListLocalized[indexPath.Row])) == true)
					{
						throw new ArgumentOutOfRangeException();
					}
					else {
						int indexDating = this.favouritesRef.localizedWork.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						int indexDatingInt = this.favouritesRef.workControl.firstTimeDict.IndexOf(this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.indexIntWork.RemoveAll((int obj) => obj == indexDatingInt);


						this.favouritesRef.localizedWork.RemoveAll((string obj) => obj == this.favouriteListLocalized[indexPath.Row]);
						this.favouritesRef.frenchTextWork.RemoveAll((string obj) => obj == this.favouriteListFrench[indexPath.Row]);
					}
 				}
				catch(ArgumentOutOfRangeException) {
					Console.WriteLine("the fuck?");
				}


				this.favouriteListLocalized.RemoveAt(indexPath.Row);
				this.favouriteListFrench.RemoveAt(indexPath.Row);

				Console.WriteLine("Index count: " + this.favouritesRef.indexInt.Count);

				tableView.DeleteRows(new NSIndexPath[]{indexPath}, UITableViewRowAnimation.Fade);
				this.TableView.ReloadData();

				//this.NavigationItem.SetLeftBarButtonItem(this.cancel, true);
			}
			else {
				//this.NavigationItem.SetLeftBarButtonItem(this.removeItem, true);
				Console.WriteLine("Other editing style detected");
			}
		}

		public override void ViewDidAppear(bool animated)
		{
			this.favouritesRef.favourites = this.favouriteListLocalized;
			this.favouriteListFrench = this.favouritesRef.favouritesFrench;
			this.favouriteListLocalized = this.favouritesRef.favourites;
			Console.WriteLine("Localized dictionary: " + this.favouritesRef.localizedText.Count);

			Console.WriteLine("Dating Count: " + this.favouritesRef.localizedTextDating.Count);
	
			//dating
			for (int i = 0; i <= this.favouritesRef.localizedTextDating.Count - 1; i++)
			{
				if (this.favouriteListLocalized.Contains(this.favouritesRef.localizedTextDating[i]) == true && this.favouriteListFrench.Contains(this.favouritesRef.frenchTextDating[i]) == true)
				{
					Console.WriteLine("Phrase already exists");
				}
				else {
					this.favouriteListLocalized.Add(this.favouritesRef.localizedTextDating[i]);
					this.favouriteListFrench.Add(this.favouritesRef.frenchTextDating[i]);


					this.TableView.ReloadData();
				}
			}
			//family & friends
			for (int i = 0; i <= this.favouritesRef.localizedTextFamily.Count - 1; i++)
			{
				if (this.favouriteListLocalized.Contains(this.favouritesRef.localizedTextFamily[i]) == true && this.favouriteListFrench.Contains(this.favouritesRef.frenchTextFamily[i]) == true)
				{
					Console.WriteLine("Phrase already exists");
				}
				else {
					this.favouriteListLocalized.Add(this.favouritesRef.localizedTextFamily[i]);
					this.favouriteListFrench.Add(this.favouritesRef.frenchTextFamily[i]);

					this.TableView.ReloadData();
				}
			}

			//business 
			for (int i = 0; i <= this.favouritesRef.localizedText.Count - 1; i++)
			{
				if (this.favouriteListLocalized.Contains(this.favouritesRef.localizedText[i]) == true && this.favouriteListFrench.Contains(this.favouritesRef.frenchText[i]) == true)
				{
					Console.WriteLine("Phrase already exists");
				}
				else {
					Console.WriteLine("wtf?");
					this.favouriteListLocalized.Add(this.favouritesRef.localizedText[i]);
					this.favouriteListFrench.Add(this.favouritesRef.frenchText[i]);

					this.TableView.ReloadData();
				}
			}

			//emergency 
			for (int i = 0; i <= this.favouritesRef.localizedTextEmergency.Count - 1; i++)
			{
				if (this.favouriteListLocalized.Contains(this.favouritesRef.localizedTextEmergency[i]) == true && this.favouriteListFrench.Contains(this.favouritesRef.frenchTextEmergency[i]) == true)
				{
					Console.WriteLine("Phrase already exists Emergency");
				}
				else {
					this.favouriteListLocalized.Add(this.favouritesRef.localizedTextEmergency[i]);
					this.favouriteListFrench.Add(this.favouritesRef.frenchTextEmergency[i]);

					this.TableView.ReloadData();
				}
			}

			//first time meeting 
			for (int i = 0; i <= this.favouritesRef.localizedTextMeeting.Count - 1; i++)
			{
				if (this.favouriteListLocalized.Contains(this.favouritesRef.localizedTextMeeting[i]) == true && this.favouriteListFrench.Contains(this.favouritesRef.frenchTextMeeting[i]) == true)
				{
					Console.WriteLine("Phrase already exists");
				}
				else {
					this.favouriteListLocalized.Add(this.favouritesRef.localizedTextMeeting[i]);
					this.favouriteListFrench.Add(this.favouritesRef.frenchTextMeeting[i]);

					this.TableView.ReloadData();
				}
			}
			//food
			for (int i = 0; i <= this.favouritesRef.localizedTextFood.Count - 1; i++)
			{
				if (this.favouriteListLocalized.Contains(this.favouritesRef.localizedTextFood[i]) == true && this.favouriteListFrench.Contains(this.favouritesRef.frenchTextFood[i]) == true)
				{
					Console.WriteLine("Phrase already exists");
				}
				else {
					this.favouriteListLocalized.Add(this.favouritesRef.localizedTextFood[i]);
					this.favouriteListFrench.Add(this.favouritesRef.frenchTextFood[i]);

					this.TableView.ReloadData();
				}
			}

			//gardening
			for (int i = 0; i <= this.favouritesRef.localizedTextGardening.Count - 1; i++)
			{
				if (this.favouriteListLocalized.Contains(this.favouritesRef.localizedTextGardening[i]) == true && this.favouriteListFrench.Contains(this.favouritesRef.frenchTextGardening[i]) == true)
				{
					Console.WriteLine("Phrase already exists");
				}
				else {
					this.favouriteListLocalized.Add(this.favouritesRef.localizedTextGardening[i]);
					this.favouriteListFrench.Add(this.favouritesRef.frenchTextGardening[i]);

					this.TableView.ReloadData();
				}
			}

			//general conversation
			for (int i = 0; i <= this.favouritesRef.localizedTextGeneralConversation.Count - 1; i++)
			{
				if (this.favouriteListLocalized.Contains(this.favouritesRef.localizedTextGeneralConversation[i]) == true && this.favouriteListFrench.Contains(this.favouritesRef.frenchTextGeneralConversation[i]) == true)
				{
					Console.WriteLine("Phrase already exists");
				}
				else {
					this.favouriteListLocalized.Add(this.favouritesRef.localizedTextGeneralConversation[i]);
					this.favouriteListFrench.Add(this.favouritesRef.frenchTextGeneralConversation[i]);

					this.TableView.ReloadData();
				}
			}

			//math & numbers
			for (int i = 0; i <= this.favouritesRef.localizedTextMathNumbers.Count - 1; i++)
			{
				if (this.favouriteListLocalized.Contains(this.favouritesRef.localizedTextMathNumbers[i]) == true && this.favouriteListFrench.Contains(this.favouritesRef.frenchTextMathNumbers[i]) == true)
				{
					Console.WriteLine("Phrase already exists");
				}
				else {
					this.favouriteListLocalized.Add(this.favouritesRef.localizedTextMathNumbers[i]);
					this.favouriteListFrench.Add(this.favouritesRef.frenchTextMathNumbers[i]);

					this.TableView.ReloadData();
				}
			}

			//school
			for (int i = 0; i <= this.favouritesRef.localizedTextSchool.Count - 1; i++)
			{
				if (this.favouriteListLocalized.Contains(this.favouritesRef.localizedTextSchool[i]) == true && this.favouriteListFrench.Contains(this.favouritesRef.frenchTextSchool[i]) == true)
				{
					Console.WriteLine("Phrase already exists");
				}
				else {
					this.favouriteListLocalized.Add(this.favouritesRef.localizedTextSchool[i]);
					this.favouriteListFrench.Add(this.favouritesRef.frenchTextSchool[i]);

					this.TableView.ReloadData();
				}
			}

			//shopping
			for (int i = 0; i <= this.favouritesRef.localizedShopping.Count - 1; i++)
			{
				if (this.favouriteListLocalized.Contains(this.favouritesRef.localizedShopping[i]) == true && this.favouriteListFrench.Contains(this.favouritesRef.frenchTextShopping[i]) == true)
				{
					Console.WriteLine("Phrase already exists");
				}
				else {
					this.favouriteListLocalized.Add(this.favouritesRef.localizedShopping[i]);
					this.favouriteListFrench.Add(this.favouritesRef.frenchTextShopping[i]);

					this.TableView.ReloadData();
				}
			}

			//technology
			for (int i = 0; i <= this.favouritesRef.localizedTechnology.Count - 1; i++)
			{
				if (this.favouriteListLocalized.Contains(this.favouritesRef.localizedTechnology[i]) == true && this.favouriteListFrench.Contains(this.favouritesRef.frenchTextTechnology[i]) == true)
				{
					Console.WriteLine("Phrase already exists");
				}
				else {
					this.favouriteListLocalized.Add(this.favouritesRef.localizedTechnology[i]);
					this.favouriteListFrench.Add(this.favouritesRef.frenchTextTechnology[i]);

					this.TableView.ReloadData();
				}
			}

			//transport
			for (int i = 0; i <= this.favouritesRef.localizedTransport.Count - 1; i++)
			{
				if (this.favouriteListLocalized.Contains(this.favouritesRef.localizedTransport[i]) == true && this.favouriteListFrench.Contains(this.favouritesRef.frenchTextTransport[i]) == true)
				{
					Console.WriteLine("Phrase already exists");
				}
				else {
					this.favouriteListLocalized.Add(this.favouritesRef.localizedTransport[i]);
					this.favouriteListFrench.Add(this.favouritesRef.frenchTextTransport[i]);

					this.TableView.ReloadData();
				}
			}

			//Travel 
			for (int i = 0; i <= this.favouritesRef.localizedTravel.Count - 1; i++)
			{
				if (this.favouriteListLocalized.Contains(this.favouritesRef.localizedTravel[i]) == true && this.favouriteListFrench.Contains(this.favouritesRef.frenchTextTravel[i]) == true)
				{
					Console.WriteLine("Phrase already exists");
				}
				else {
					this.favouriteListLocalized.Add(this.favouritesRef.localizedTravel[i]);
					this.favouriteListFrench.Add(this.favouritesRef.frenchTextTravel[i]);

					this.TableView.ReloadData();
				}
			}

			//work
			for (int i = 0; i <= this.favouritesRef.localizedWork.Count - 1; i++)
			{
				if (this.favouriteListLocalized.Contains(this.favouritesRef.localizedWork[i]) == true && this.favouriteListFrench.Contains(this.favouritesRef.frenchTextWork[i]) == true)
				{
					Console.WriteLine("Phrase already exists");
				}
				else {
					this.favouriteListLocalized.Add(this.favouritesRef.localizedWork[i]);
					this.favouriteListFrench.Add(this.favouritesRef.frenchTextWork[i]);

					this.TableView.ReloadData();
				}
			}

			if (this.TableView.Editing == true)
			{
				if (this.favouriteListLocalized.Count == 0)
				{
					if (this.TableView.Editing == true)
					{
						Console.WriteLine("Nav item is cancel button. :)");
						this.favouritesRef.tab.NavigationItem.SetLeftBarButtonItem(this.removeItem, true);
						this.TableView.SetEditing(false, true);
					}
				}
			}

	
			this.TableView.ReloadData();
		}

		public override void ViewDidLoad()
		{
			this.favouritesRef.favouritesFrench = this.favouriteListFrench;

			this.favouritesRef.favControl = this;

			this.TableView.RowHeight = 70.0f;
		}
    }
}