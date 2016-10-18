using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

using UIKit;
using CoreFoundation;
using Foundation;


namespace FrenchPhraseBook
{
	public partial class TransportController : UITableViewController
	{


		public TransportController(IntPtr handle) : base(handle)
		{
		}

		public TransportController() { }

		public List<string> transportDict = new List<string>(){
			{"Taxi"}, {"Bus"},{"Car"},{"Train"},{"Tram"},{"Carriage"},{"Airport"},{"Plane"},{"Aircraft"},{"Jet"},
			{"Train station"},{"Transit lane"},{"Highway"},{"Road"},{"Ticket"},{"Where is the next station?"},{"Can you drive me to the airport?"},
			{"Boat"},{"Ship"},{"Bicycle"},{"Motorbike"},{"Drive me to the train station"},{"Subway"},{"When is the next bus stop?"},{"Rental car"},{"How much does it cost to rent..."},{"I'm lost"},
			{"Tram"},{"I would like to buy a ticket"},{"Monorail"},{"How much for a taxi?"},{"Is there a toilet on this bus?"},{"What time does the bus get here?"},{"When is the next train?"},{"The next flight is in 8 hours"},
			{"The next flight is in..."},{"Please stop here"},{"Please stop at...(Address)"},{"When will we get there?"},{"This is the 704 bus"},{"This is the ...(Number)...bus"},
		};

		Dictionary<int, string> transportTranslatedDict = new Dictionary<int, string> {
			{0,"Taxi"}, {1,"Autobus"},{2,"Voiture"}, {3,"Entrainer"},{4,"Tram"}, {5,"Le chariot"},{6,"Aéroport"}, {7,"Avion"},{8,"Avion"}, {9,"Jet"},{10,"Gare"}, {11,"voie de transit"},{12,"Autoroute"}, {13,"Route"},
			{14,"Billet"}, {15,"Où est la prochaine station?"},{16,"Pouvez-vous me conduire à l'aéroport?"}, {17,"Bateau"},{18,"Navire"}, {19,"Vélo"},{20,"Moto"}, {21,"Conduisez-moi à la gare"},{22,"Métro"}, {23,"Quand arrêter le prochain bus?"},{24,"Voiture de location"}, {25,"Combien coûte la location ..."},
			{26,"je suis perdu"}, {27,"Tram"},{28,"Je voudrais acheter un billet"}, {29,"Monorail"},
			{30,"Combien pour un taxi?"}, {31,"Y at-il une toilette sur ce bus?"},{32,"À quelle heure le bus arriver ici?"}, {33,"Quand part le prochain train?"},{34,"Le prochain vol est en 8 heures"}, {35,"Le prochain vol est en ..."},{36,"S'il vous plaît arrêter ici"}, {37,"S'il vous plaît arrêter à ..."},{38,"Quand allons-nous y arriver?"},
			{39,"Ceci est le bus 704"},{40,"C'est le ... Autobus"}
		};

		public string transportString = "Family";

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
				AI.frenchPhraseBookAI(this.transportDict[index.Row]);
			}
			else if (this.application.tabBarID == 0)
			{
				if (this.application.localizedTransport.Count == 1)
				{
					this.application.cellTransport.AccessoryView = this.favouritesIndicator;
					this.application.cellTransport.EditingAccessoryView = this.favouritesIndicator;
					this.TableView.ReloadData();
				}
			}

			if (this.application.localizedTransport.Count == 1)
			{
				this.application.cellTransport.AccessoryView = this.favouritesIndicator;
				this.application.cellTransport.EditingAccessoryView = this.favouritesIndicator;
				this.TableView.ReloadData();
			}
		}

		public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
		{
			return 80.0f;
		}

		public override void ViewDidLoad()
		{
			this.application.transportControl = this;
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




			UIBarButtonItem optionButton = new UIBarButtonItem("<\ud83d\ude8d", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
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

			this.NavigationItem.Title = "Public Transport";

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
			UITableViewCell BusinessCell = tableView.DequeueReusableCell(this.transportString);

			if (BusinessCell == null)
			{
				BusinessCell = new UITableViewCell(UITableViewCellStyle.Subtitle, this.transportString);
			}


			this.application.cellTransport = BusinessCell;

			this.favouritesIndicator = new UILabel();
			this.favouritesIndicator.Text = "\ud83d\udc9d";
			//this.favouritesIndicator.Text = "⭐";
			this.favouritesIndicator.MinimumFontSize = 24.0f;
			this.favouritesIndicator.AdjustsFontSizeToFitWidth = true;
			this.favouritesIndicator.Frame = new CoreGraphics.CGRect(0, 20, 40, 40);

			BusinessCell.TextLabel.Text = this.transportDict[indexPath.Row];
			BusinessCell.DetailTextLabel.Text = this.transportTranslatedDict[indexPath.Row];
			BusinessCell.DetailTextLabel.TextColor = UIColor.Gray;
			BusinessCell.DetailTextLabel.Font = UIFont.SystemFontOfSize(12.5f);

			if (BusinessCell.EditingStyle == UITableViewCellEditingStyle.Insert)
			{
				this.application.cellTransport.AccessoryView = null;
				this.application.cellTransport.EditingAccessoryView = null;
			}

			if (this.application.localizedTransport.Count == 0)
			{
				this.application.cellTransport.AccessoryView = null;
				this.application.cellTransport.EditingAccessoryView = null;
			}

			else if (this.application.localizedTransport.Count >= 1)
			{
				if (this.application.localizedTransport.Count == 1)
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
				if (indexPath.Row == this.application.indexTableTransport.Row)
				{
					Console.WriteLine("index chosen");
					BusinessCell.AccessoryView = this.favouritesIndicator;
					BusinessCell.EditingAccessoryView = this.favouritesIndicator;
				}

				else if (this.application.indexIntTransport.Contains(indexPath.Row) == false)
				{
					Console.WriteLine("Index is not found");
					BusinessCell.AccessoryView = null;
					BusinessCell.EditingAccessoryView = null;
				}

				else if (this.application.indexIntTransport.Contains(indexPath.Row) == true)
				{
					Console.WriteLine("Index table chosen _ 1");
					if (indexPath.Row != this.application.indexTableTransport.Row)
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
				if (this.application.indexIntTransport.Contains(indexPath.Row) == true)
				{
					if (indexPath.Row != this.application.indexTableTransport.Row || indexPath.Row == this.application.indexTableTransport.Row)
					{
						return UITableViewCellEditingStyle.Delete;
					}
				}
				this.application.cellTechnology.AccessoryView = null;
				this.application.cellTechnology.EditingAccessoryView = null;

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

				if (this.application.localizedTransport.Count((string arg) => arg.ToString() == this.transportDict[indexPath.Row]) >= 1)
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

				else if (this.application.localizedTransport.Count((string arg) => arg.ToString() == this.transportDict[indexPath.Row]) == 0)
				{
					this.application.localizedTransport.Add(this.transportDict[indexPath.Row]);
					this.application.frenchTextTransport.Add(this.transportTranslatedDict[indexPath.Row]);
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

					this.application.indexIntTransport.Add(indexPath.Row);

					this.application.indexTableTransport = index;

					this.TableView.ReloadData();
				}
			}
			else if (editingStyle == UITableViewCellEditingStyle.Delete)
			{
				if (this.application.indexIntTransport.Contains(indexPath.Row) == true)
				{
					this.application.localizedTransport.RemoveAll((string obj) => obj == this.transportDict[indexPath.Row]);
					this.application.indexIntTransport.RemoveAll((int obj) => obj == indexPath.Row);

					this.application.favourites.RemoveAll((string obj) => obj == this.transportDict[indexPath.Row]);
					this.application.favouritesFrench.RemoveAll((string obj) => obj == this.transportTranslatedDict[indexPath.Row]);

					if (this.application.indexTableTransport.Row == indexPath.Row || this.application.indexTableTransport.Row != indexPath.Row)
					{
						this.application.cellTransport.AccessoryView = null;
						this.application.cellTransport.EditingAccessoryView = null;
						this.TableView.ReloadData();
					}

					if (this.application.localizedTransport.Count == 1)
					{
						this.TableView.ReloadData();
					}

					if (this.application.cellTransport.EditingStyle == UITableViewCellEditingStyle.Insert)
					{
						this.application.cellTransport.AccessoryView = null;
						this.application.cellTransport.EditingAccessoryView = null;
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
					this.application.cellTransport.AccessoryView = null;
					this.application.cellTransport.EditingAccessoryView = null;
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
			return this.transportDict.Count;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			BatteryMonitor AI = new BatteryMonitor();
			switch (indexPath.Row)
			{
				case 0:
					AI.frenchPhraseBookAI(transportTranslatedDict[0]);
					break;
				case 1:
					AI.frenchPhraseBookAI(transportTranslatedDict[1]);
					break;
				case 2:
					AI.frenchPhraseBookAI(transportTranslatedDict[2]);
					break;
				case 3:
					AI.frenchPhraseBookAI(transportTranslatedDict[3]);
					break;
				case 4:
					AI.frenchPhraseBookAI(transportTranslatedDict[4]);
					break;
				case 5:
					AI.frenchPhraseBookAI(transportTranslatedDict[5]);
					break;
				case 6:
					AI.frenchPhraseBookAI(transportTranslatedDict[6]);
					break;
				case 7:
					AI.frenchPhraseBookAI(transportTranslatedDict[7]);
					break;
				case 8:
					AI.frenchPhraseBookAI(transportTranslatedDict[8]);
					break;
				case 9:
					AI.frenchPhraseBookAI(transportTranslatedDict[9]);
					break;
				case 10:
					AI.frenchPhraseBookAI(transportTranslatedDict[10]);
					break;
				case 11:
					AI.frenchPhraseBookAI(transportTranslatedDict[11]);
					break;
				case 12:
					AI.frenchPhraseBookAI(transportTranslatedDict[12]);
					break;
				case 13:
					AI.frenchPhraseBookAI(transportTranslatedDict[13]);
					break;
				case 14:
					AI.frenchPhraseBookAI(transportTranslatedDict[14]);
					break;
				case 15:
					AI.frenchPhraseBookAI(transportTranslatedDict[15]);
					break;
				case 16:
					AI.frenchPhraseBookAI(transportTranslatedDict[16]);
					break;
				case 17:
					AI.frenchPhraseBookAI(transportTranslatedDict[17]);
					break;
				case 18:
					AI.frenchPhraseBookAI(transportTranslatedDict[18]);
					break;
				case 19:
					AI.frenchPhraseBookAI(transportTranslatedDict[19]);
					break;
				case 20:
					AI.frenchPhraseBookAI(transportTranslatedDict[20]);
					break;
				case 21:
					AI.frenchPhraseBookAI(transportTranslatedDict[21]);
					break;
				case 22:
					AI.frenchPhraseBookAI(transportTranslatedDict[22]);
					break;
				case 23:
					AI.frenchPhraseBookAI(transportTranslatedDict[23]);
					break;
				case 24:
					AI.frenchPhraseBookAI(transportTranslatedDict[24]);
					break;
				case 25:
					AI.frenchPhraseBookAI(transportTranslatedDict[25]);
					break;
				case 26:
					AI.frenchPhraseBookAI(transportTranslatedDict[26]);
					break;
				case 27:
					AI.frenchPhraseBookAI(transportTranslatedDict[27]);
					break;
				case 28:
					AI.frenchPhraseBookAI(transportTranslatedDict[28]);
					break;
				case 29:
					AI.frenchPhraseBookAI(transportTranslatedDict[29]);
					break;
				case 30:
					AI.frenchPhraseBookAI(transportTranslatedDict[30]);
					break;
				case 31:
					AI.frenchPhraseBookAI(transportTranslatedDict[31]);
					break;
				case 32:
					AI.frenchPhraseBookAI(transportTranslatedDict[32]);
					break;
				case 33:
					AI.frenchPhraseBookAI(transportTranslatedDict[33]);
					break;
				case 34:
					AI.frenchPhraseBookAI(transportTranslatedDict[34]);
					break;
				case 35:
					AI.frenchPhraseBookAI(transportTranslatedDict[35]);
					break;
				case 36:
					AI.frenchPhraseBookAI(transportTranslatedDict[36]);
					break;
				case 37:
					AI.frenchPhraseBookAI(transportTranslatedDict[37]);
					break;
				case 38:
					AI.frenchPhraseBookAI(transportTranslatedDict[38]);
					break;
				case 39:
					AI.frenchPhraseBookAI(transportTranslatedDict[39]);
					break;
				case 40:
					AI.frenchPhraseBookAI(transportTranslatedDict[40]);
					break;
				default:
					Console.WriteLine("No key selected");
					break;
			}
			tableView.DeselectRow(indexPath, true);
		}
	}
}
