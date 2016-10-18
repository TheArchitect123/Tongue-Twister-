using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

using UIKit;
using CoreFoundation;
using Foundation;

namespace FrenchPhraseBook
{
	public partial class GardeningController : UITableViewController
	{
		public GardeningController(IntPtr handle) : base(handle)
		{
		}

		public GardeningController()
		{
		}
		public List<string> gardeningDict = new List<string>() {
			{"This a beautiful garden"},{"Grass"},{"Flowers"},{"Roses"},{"Garden"},{"Pool"},{"Bushes"},{"Landscape"},{"Gardening"},
			{"I love the view of this place"},{"Root"},{"Gardening tools"},{"There's allot of weeds growing here"},{"This is compost"},{"I grow my own crop"},{"Seeds"},{"Sunflower plants"},{"These plants need sunlight"},
			{"I water my plants everyday"},{"I use rainwater"},{"Impressive landscaping"},{"Self pollinating plants"},{"This soil is over watered"},{"What kind of compost do you use?"},{"Bolting vegetables"},{"Cabbages"},{"Greenhouse"},
			{"This is my greenhouse"},{"This is his greenhouse"},{"This soil needs to be renewed"}, {"Shovel"},{"Wheelbarrow"},{"Trolley"},{"Fencing tools"},{"Floral snips"},
		};

		//place the translated dictionary here
		Dictionary<int, string> gardeningTranslated = new Dictionary<int, string>() {
			{0,"Cet un beau jardin"},{1,"Herbe"},{2,"Fleurs"},{3,"Des roses"},{4,"Jardin"},{5,"Piscine"},{6,"Des buissons"},{7,"Paysage"},{8,"Jardinage"},
			{9,"J'adore la vue de ce lieu"},{10,"Racine"},{11,"Outils de jardinage"},{12,"Il y a Allot des mauvaises herbes qui poussent ici"},{13,"Ceci est le compost"},{14,"Je cultive ma propre culture"},{15,"Graines"},{16,"Les plants de tournesol"},{17,"Ces plantes ont besoin de lumière"},
			{18,"J'arrose mes plantes tous les jours"},{19,"J'utilise l'eau de pluie"},{20,"aménagement paysager impressionnant"},{21,"plantes auto pollinisateurs"},{22,"Ce sol est plus arrosé"},{23,"Quel genre de compost utilisez-vous?"},{24,"boulonnage légumes"},{25,"Choux"},{26,"Serre"},
			{27,"Ceci est ma serre"},{28,"Ceci est sa serre"},{29,"Ce sol a besoin d'être renouvelé"},{30,"Pelle"},{31,"Brouette"},{32,"Chariot"},{33,"outils d'escrime"},{34,"cisailles Floral"}
		};
		string gardeningID = "firstTimeCellID";

		UIButton button = new UIButton(UIButtonType.Custom);
		UILabel favouritesIndicator = new UILabel();

		public AppDelegate application
		{
			get
			{
				return (AppDelegate)UIApplication.SharedApplication.Delegate;
			}
		}

		UIBarButtonItem searchBack = new UIBarButtonItem();

		UIBarButtonItem favouritesButton = new UIBarButtonItem();
		UIBarButtonItem completeFavourite = new UIBarButtonItem();

		UIImage imageFav = new UIImage("FavouriteSelected.png");



		public override void ViewDidAppear(bool animated)
		{
			NSIndexPath index = NSIndexPath.FromRowSection(this.application.index, 0);

			Console.WriteLine("Index: " + index.Row);
			if (this.application.tabBarID == 1)
			{
				Console.WriteLine("wtf?");
				this.TableView.ScrollToRow(index, UITableViewScrollPosition.Top, true);
				this.TableView.SelectRow(index, true, UITableViewScrollPosition.Top);

				BatteryMonitor AI = new BatteryMonitor();
				AI.frenchPhraseBookAI(this.gardeningDict[index.Row]);
			}
			else if (this.application.tabBarID == 0)
			{
				if (this.application.localizedTextGardening.Count == 1)
				{
					this.application.cellGardening.AccessoryView = this.favouritesIndicator;
					this.application.cellGardening.EditingAccessoryView = this.favouritesIndicator;
					this.TableView.ReloadData();
				}
			}

			if (this.application.localizedTextGardening.Count == 1)
			{
				this.application.cellGardening.AccessoryView = this.favouritesIndicator;
				this.application.cellGardening.EditingAccessoryView = this.favouritesIndicator;
				this.TableView.ReloadData();
			}
		}

		public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
		{
			return 80.0f;
		}

		public override void ViewDidLoad()
		{
			this.application.gardeningControl = this;
			this.TableView.ReloadData();
			UITextAttributes heartSize = new UITextAttributes();
			heartSize.Font = UIFont.SystemFontOfSize(30.0f);

			this.favouritesButton.SetTitleTextAttributes(heartSize, UIControlState.Normal);
			this.favouritesButton.SetTitleTextAttributes(heartSize, UIControlState.Highlighted);

			this.NavigationItem.SetRightBarButtonItem(this.favouritesButton = new UIBarButtonItem("⭐", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
			{

				this.completeFavourite = new UIBarButtonItem("Finished", UIBarButtonItemStyle.Plain, (object sender_2, EventArgs e_2) =>
				{
					this.TableView.SetEditing(false, true);
					this.NavigationItem.SetRightBarButtonItem(this.favouritesButton, true);
				});

				this.NavigationItem.SetRightBarButtonItem(this.completeFavourite, true);

				this.TableView.SetEditing(true, true);
			}), false);




			UIBarButtonItem optionButton = new UIBarButtonItem("<\ud83c\udf3b", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
			{
				this.NavigationController.PopViewController(true);
			});

			UIBarButtonItem searchBack = new UIBarButtonItem("<\ud83d\udd0d", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
			{
				this.NavigationController.PopViewController(true);
				this.application.search.BecomeFirstResponder();
			});

			searchBack.TintColor = UIColor.Blue;

			optionButton.TintColor = UIColor.Blue;

			this.NavigationItem.Title = "Gardening & Landscaping";

			if (this.application.tabBarID == 0)
			{
				this.NavigationItem.SetLeftBarButtonItem(optionButton, true);
			}
			else if (this.application.tabBarID == 1)
			{
				this.NavigationItem.SetLeftBarButtonItem(searchBack, true);
			}
			this.TableView.SeparatorColor = UIColor.Gray;
			this.TableView.RowHeight = 70.0f;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			//check if the table data has changed before redrawing the table cells
			UITableViewCell BusinessCell = tableView.DequeueReusableCell(this.gardeningID);

			if (BusinessCell == null)
			{
				BusinessCell = new UITableViewCell(UITableViewCellStyle.Subtitle, this.gardeningID);
			}


			this.application.cellGardening = BusinessCell;

			this.favouritesIndicator = new UILabel();
			this.favouritesIndicator.Text = "\ud83d\udc9d";
			//this.favouritesIndicator.Text = "⭐";
			this.favouritesIndicator.MinimumFontSize = 24.0f;
			this.favouritesIndicator.AdjustsFontSizeToFitWidth = true;
			this.favouritesIndicator.Frame = new CoreGraphics.CGRect(0, 20, 40, 40);

			BusinessCell.TextLabel.Text = this.gardeningDict[indexPath.Row];
			BusinessCell.DetailTextLabel.Text = this.gardeningTranslated[indexPath.Row];
			BusinessCell.DetailTextLabel.TextColor = UIColor.Gray;
			BusinessCell.DetailTextLabel.Font = UIFont.SystemFontOfSize(12.5f);

			if (BusinessCell.EditingStyle == UITableViewCellEditingStyle.Insert)
			{
				this.application.cellGardening.AccessoryView = null;
				this.application.cellGardening.EditingAccessoryView = null;
			}

			if (this.application.localizedTextGardening.Count == 0)
			{
				this.application.cellGardening.AccessoryView = null;
				this.application.cellGardening.EditingAccessoryView = null;
			}

			else if (this.application.localizedTextGardening.Count >= 1)
			{
				if (this.application.localizedTextGardening.Count == 1)
				{
					if (this.application.tabBarID == 1)
					{
						BusinessCell.AccessoryView = this.favouritesIndicator;
						BusinessCell.EditingAccessoryView = this.favouritesIndicator;
					}
					else {
						BusinessCell.AccessoryView = this.favouritesIndicator;
						BusinessCell.EditingAccessoryView = this.favouritesIndicator;
					}
				}

				//try this code first
				if (indexPath.Row == this.application.indexTableGardening.Row)
				{
					Console.WriteLine("index chosen");
					BusinessCell.AccessoryView = this.favouritesIndicator;
					BusinessCell.EditingAccessoryView = this.favouritesIndicator;
				}

				else if (this.application.indexIntGardening.Contains(indexPath.Row) == false)
				{
					Console.WriteLine("Index is not found");
					BusinessCell.AccessoryView = null;
					BusinessCell.EditingAccessoryView = null;
				}

				else if (this.application.indexIntGardening.Contains(indexPath.Row) == true)
				{
					Console.WriteLine("Index table chosen _ 1");
					if (indexPath.Row != this.application.indexTableGardening.Row)
					{
						Console.WriteLine("Index table chosen _ 2");
						BusinessCell.AccessoryView = this.favouritesIndicator;
						BusinessCell.EditingAccessoryView = this.favouritesIndicator;
					}
				}


				//the previously listed indices have accessory views labelled
				//this takes the final index path instead of a range 
				/*	if (indexPath.Row == this.application.indexInt.Find((int obj) => obj >= indexPath.Row)) {
						Console.WriteLine("wtf?");
						BusinessCell.AccessoryView = this.favouritesIndicator;
					}*/

				//try this code. Using logic operators




				/*if(this.application.indexTableFavourite.Row == 0 || this.application.indexTableFavourite.Row == 1) {
					BusinessCell.AccessoryView = this.favouritesIndicator;
					return BusinessCell; 
				}
				else {
					BusinessCell.AccessoryView = null;
					return BusinessCell;
				}*/
				return BusinessCell;
			}

			return BusinessCell;
		}

		public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath)
		{
			return "Unfavourite";
		}

		public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
		{
			if (tableView.Editing == true)
			{
				if (this.application.indexIntGardening.Contains(indexPath.Row) == true)
				{
					if (indexPath.Row != this.application.indexTableGardening.Row || indexPath.Row == this.application.indexTableGardening.Row)
					{
						return UITableViewCellEditingStyle.Delete;
					}
				}
				this.application.cellGardening.AccessoryView = null;
				this.application.cellGardening.EditingAccessoryView = null;

				return UITableViewCellEditingStyle.Insert;
			}
			else {
				return UITableViewCellEditingStyle.None;
			}
			return UITableViewCellEditingStyle.None;
		}

		public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			if (editingStyle == UITableViewCellEditingStyle.Insert)
			{
				//user clicks the bar button item whateevr he has already favourit as in whaterver exists withint the applications delegate list if the strin already exist the editing saccessory style becomes delete

				if (this.application.localizedTextGardening.Count((string arg) => arg.ToString() == this.gardeningDict[indexPath.Row]) >= 1)
				{
					UIAlertController alreadyFavourited = UIAlertController.Create("Already favourited!", "You have already favourited this phrase!", UIAlertControllerStyle.Alert);

					UIAlertAction confirmed = UIAlertAction.Create("Ok", UIAlertActionStyle.Default, (Action) =>
					{
						alreadyFavourited.Dispose();
					});

					alreadyFavourited.AddAction(confirmed);


					if (this.PresentedViewController == null)
					{
						this.PresentViewController(alreadyFavourited, true, () =>
						{
							AudioToolbox.SystemSound sound = new AudioToolbox.SystemSound(4095);
							sound.PlaySystemSound();
						});
					}
					else {
						this.PresentedViewController.DismissViewController(true, () =>
						{
							this.PresentedViewController.Dispose();
							this.PresentViewController(alreadyFavourited, true, () =>
							{
								AudioToolbox.SystemSound sound = new AudioToolbox.SystemSound(4095);
								sound.PlaySystemSound();
							});
						});
					}

					Console.WriteLine("The object already exists inside the list");
				}

				else if (this.application.localizedTextGardening.Count((string arg) => arg.ToString() == this.gardeningDict[indexPath.Row]) == 0)
				{
					this.application.localizedTextGardening.Add(this.gardeningDict[indexPath.Row]);
					this.application.frenchTextGardening.Add(this.gardeningTranslated[indexPath.Row]);
					this.application.indexPathsRegister.Add(indexPath);
					this.application.indexPathsInt.Add(indexPath.Row);
					this.favouritesIndicator = new UILabel();

					this.favouritesIndicator.Text = this.favouritesButton.Title;
					this.favouritesIndicator.MinimumFontSize = 20.0f;
					this.favouritesIndicator.AdjustsFontSizeToFitWidth = true;
					this.favouritesIndicator.Frame = new CoreGraphics.CGRect(0, 20, 40, 40);


					NSIndexPath index = indexPath;
					List<NSIndexPath> indexChosen = new List<NSIndexPath>() { };
					indexChosen.Add(index);

					List<int> indexInt = new List<int>() { };
					indexInt.Add(indexPath.Row);

					this.application.indexIntGardening.Add(indexPath.Row);

					this.application.indexTableGardening = index;

					this.TableView.ReloadData();
				}
			}
			else if (editingStyle == UITableViewCellEditingStyle.Delete)
			{
				if (this.application.indexIntGardening.Contains(indexPath.Row) == true)
				{
					this.application.localizedTextGardening.RemoveAll((string obj) => obj == this.gardeningDict[indexPath.Row]);
					this.application.indexIntGardening.RemoveAll((int obj) => obj == indexPath.Row);

					this.application.favourites.RemoveAll((string obj) => obj == this.gardeningDict[indexPath.Row]);
					this.application.favouritesFrench.RemoveAll((string obj) => obj == this.gardeningTranslated[indexPath.Row]);

					if (this.application.indexTableGardening.Row == indexPath.Row || this.application.indexTableGardening.Row != indexPath.Row)
					{
						this.application.cellGardening.AccessoryView = null;
						this.application.cellGardening.EditingAccessoryView = null;
						this.TableView.ReloadData();
					}

					if (this.application.localizedTextGardening.Count == 1)
					{
						this.TableView.ReloadData();
					}

					if (this.application.cellGardening.EditingStyle == UITableViewCellEditingStyle.Insert)
					{
						this.application.cellGardening.AccessoryView = null;
						this.application.cellGardening.EditingAccessoryView = null;
						this.TableView.ReloadData();
					}
					/*if (this.application.localizedText.Count == 0)
					{
						if (this.application.cellBusiness.AccessoryView != null)
						{
							this.application.cellBusiness.AccessoryView = null;
							this.application.cellBusiness.EditingAccessoryView = null;
							this.TableView.ReloadData();
						}
					}*/
				}
				else {
					this.application.cellGardening.AccessoryView = null;
					this.application.cellGardening.EditingAccessoryView = null;
					this.TableView.ReloadData();
				}
				//this.application.localizedText.RemoveAt(this.companyDict.IndexOf(this.companyDict[indexPath.Row]));
				//this.application.indexInt.RemoveAt(this.companyDict.IndexOf(this.companyDict[indexPath.Row]));
				//tableView.ReloadData();
				this.TableView.ReloadData();
			}
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return this.gardeningDict.Count;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			BatteryMonitor AI = new BatteryMonitor();
			switch (indexPath.Row)
			{
				case 0:
					AI.frenchPhraseBookAI(gardeningTranslated[0]);
					break;
				case 1:
					AI.frenchPhraseBookAI(gardeningTranslated[1]);
					break;
				case 2:
					AI.frenchPhraseBookAI(gardeningTranslated[2]);
					break;
				case 3:
					AI.frenchPhraseBookAI(gardeningTranslated[3]);
					break;
				case 4:
					AI.frenchPhraseBookAI(gardeningTranslated[4]);
					break;
				case 5:
					AI.frenchPhraseBookAI(gardeningTranslated[5]);
					break;
				case 6:
					AI.frenchPhraseBookAI(gardeningTranslated[6]);
					break;
				case 7:
					AI.frenchPhraseBookAI(gardeningTranslated[7]);
					break;
				case 8:
					AI.frenchPhraseBookAI(gardeningTranslated[8]);
					break;
				case 9:
					AI.frenchPhraseBookAI(gardeningTranslated[9]);
					break;
				case 10:
					AI.frenchPhraseBookAI(gardeningTranslated[10]);
					break;
				case 11:
					AI.frenchPhraseBookAI(gardeningTranslated[11]);
					break;
				case 12:
					AI.frenchPhraseBookAI(gardeningTranslated[12]);
					break;
				case 13:
					AI.frenchPhraseBookAI(gardeningTranslated[13]);
					break;
				case 14:
					AI.frenchPhraseBookAI(gardeningTranslated[14]);
					break;
				case 15:
					AI.frenchPhraseBookAI(gardeningTranslated[15]);
					break;
				case 16:
					AI.frenchPhraseBookAI(gardeningTranslated[16]);
					break;
				case 17:
					AI.frenchPhraseBookAI(gardeningTranslated[17]);
					break;
				case 18:
					AI.frenchPhraseBookAI(gardeningTranslated[18]);
					break;
				case 19:
					AI.frenchPhraseBookAI(gardeningTranslated[19]);
					break;
				case 20:
					AI.frenchPhraseBookAI(gardeningTranslated[20]);
					break;
				case 21:
					AI.frenchPhraseBookAI(gardeningTranslated[21]);
					break;
				case 22:
					AI.frenchPhraseBookAI(gardeningTranslated[22]);
					break;
				case 23:
					AI.frenchPhraseBookAI(gardeningTranslated[23]);
					break;
				case 24:
					AI.frenchPhraseBookAI(gardeningTranslated[24]);
					break;
				case 25:
					AI.frenchPhraseBookAI(gardeningTranslated[25]);
					break;
				case 26:
					AI.frenchPhraseBookAI(gardeningTranslated[26]);
					break;
				case 27:
					AI.frenchPhraseBookAI(gardeningTranslated[27]);
					break;
				case 28:
					AI.frenchPhraseBookAI(gardeningTranslated[28]);
					break;
				case 29:
					AI.frenchPhraseBookAI(gardeningTranslated[29]);
					break;
				case 30:
					AI.frenchPhraseBookAI(gardeningTranslated[30]);
					break;
				case 31:
					AI.frenchPhraseBookAI(gardeningTranslated[31]);
					break;
				case 32:
					AI.frenchPhraseBookAI(gardeningTranslated[32]);
					break;
				case 33:
					AI.frenchPhraseBookAI(gardeningTranslated[33]);
					break;
				case 34:
					AI.frenchPhraseBookAI(gardeningTranslated[34]);
					break;
				default:
					Console.WriteLine("No key selected");
					break;
			}
			tableView.DeselectRow(indexPath, true);

		}
	}
}