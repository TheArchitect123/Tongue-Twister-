using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using CoreFoundation;
using UIKit;


namespace FrenchPhraseBook
{
	public partial class TravelController : UITableViewController
	{
		public List<string> travelDict = new List<string>(){
			{"This is my first time in france"},{"This is my first time in...(Country)"},
			{"How many times have you visited this country?"},{"What is there to do here?"},{"Where can I find the nearest cinema?"},
			{"I love this place!"},{"Do you like to travel?"},{"Paris"},{"Italy"},{"Eiffel tower"},{"Pizza"},{"There is a circus here"},
			{"Travelling is best done with friends"},{"I have been to the city many times"},{"Have you visited the Melbourne Aquarium?"},{"The Aquarium is so expensive"},
			{"This place is cheap"},{"Thailand is cheap to go to"},{"(Country)...has cheap apartments"},{"Have you travelled to the forbidden city"},{"Have you been to..."},
			{"What's the funnest thing to do here?"},{"Is there a comedy show here?"},{"There is a pool in this hotel"},{"How much does a room cost here?"},{"I'm staying here for a week"},
			{"We're staying here for a month"},{"I'm staying in this hotel for a night"},{"Where can I find the nearest supermarket"},{"How much for these?"},{"Singapore is a clean place to visit"},
			{"I have some euros in my pocket"},{"I don't speak french"},{"I speak french very well"},{"How many languages can you speak?"},{"I need a new camera"},{"Take a photo of us, please"},
			{"How long does it take to get there?"},{"Do you have any movies on this flight?"},{"Can you bring us some champagne?"},{"Museum of Arts"},{"Historical Museum"},
		};

		Dictionary<int, string> travelTranslatedDict = new Dictionary<int, string> {
			{0,"Ceci est ma première fois en France"},  {1,"Ceci est ma première fois en ..."},{2,"Combien de fois avez-vous visité ce pays?"},{3,"Qu'est-ce qu'il ya à faire ici?"},{4,"Où puis-je trouver le cinéma le plus proche?"},{5,"J'adore cet endroit!"},
			{6,"Aimes-tu voyager?"},{7,"Paris"},{8,"Italie"},{9,"Tour Eiffel"},{10,"Pizza"},{11,"Il y a un cirque ici"},{12,"Voyager est le mieux fait avec des amis"},{13,"I have been to the city many times"},{14,"Avez-vous visité l'aquarium de Melbourne?"},{15,"L' Aquarium est si cher"},
			{16,"Cet endroit est pas cher"},{17,"La Thaïlande est pas cher pour aller à"},{18,"... A des appartements bon marché"},{19,"Avez-vous voyagé à la ville interdite"},{20,"Avez-vous été à..."},{21,"Quelle est la funnest chose à faire ici?"},
			{22,"Y at-il un spectacle de comédie ici?"},{23,"Il y a une piscine dans cet hôtel"},
			{24,"Combien une chambre coûte ici?"},{25,"Je reste ici pendant une semaine"},{26,"Nous restons ici pour un mois"},{27,"Je reste dans cet hôtel pour une nuit"},{28,"Où puis-je trouver le supermarché le plus proche"},{29,"Combien pour ceux-ci?"},{30,"Singapour est un endroit propre à visiter"},
			{31,"J'ai quelques euros dans ma poche"},{32,"Je ne parle pas français"},{33,"Je parle français très bien"},{34,"Combien de langues pouvez-vous parler?"},{35,"Je besoin d'un nouvel appareil photo"},{36,"Prenez une photo de nous, s'il vous plaît"},{37,"Combien de temps cela prend-il pour s'y rendre?"},{38,"Avez-vous des films sur ce vol?"},{39,"Pouvez-vous nous apporter un peu de champagne?"},
			{40,"Musée des Arts"},{41,"Musée historique"}
		};
		public string travelID = "travelID";

		public TravelController(IntPtr handle) : base(handle)
		{
		}


		public TravelController()
		{

		}

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
				AI.frenchPhraseBookAI(this.travelDict[index.Row]);
			}
			else if (this.application.tabBarID == 0)
			{
				if (this.application.localizedTravel.Count == 1)
				{
					this.application.cellTravel.AccessoryView = this.favouritesIndicator;
					this.application.cellTravel.EditingAccessoryView = this.favouritesIndicator;
					this.TableView.ReloadData();
				}
			}

			if (this.application.localizedTravel.Count == 1)
			{
				this.application.cellTravel.AccessoryView = this.favouritesIndicator;
				this.application.cellTravel.EditingAccessoryView = this.favouritesIndicator;
				this.TableView.ReloadData();
			}
		}

		public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
		{
			return 80.0f;
		}

		public override void ViewDidLoad()
		{
			this.application.travelControl = this;
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




			UIBarButtonItem optionButton = new UIBarButtonItem("<\ud83c\udfdb", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
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

			this.NavigationItem.Title = "Travel";

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
			UITableViewCell BusinessCell = tableView.DequeueReusableCell(this.travelID);

			if (BusinessCell == null)
			{
				BusinessCell = new UITableViewCell(UITableViewCellStyle.Subtitle, this.travelID);
			}


			this.application.cellTravel = BusinessCell;

			this.favouritesIndicator = new UILabel();
			this.favouritesIndicator.Text = "\ud83d\udc9d";
			//this.favouritesIndicator.Text = "⭐";
			this.favouritesIndicator.MinimumFontSize = 24.0f;
			this.favouritesIndicator.AdjustsFontSizeToFitWidth = true;
			this.favouritesIndicator.Frame = new CoreGraphics.CGRect(0, 20, 40, 40);

			BusinessCell.TextLabel.Text = this.travelDict[indexPath.Row];
			BusinessCell.DetailTextLabel.Text = this.travelTranslatedDict[indexPath.Row];
			BusinessCell.DetailTextLabel.TextColor = UIColor.Gray;
			BusinessCell.DetailTextLabel.Font = UIFont.SystemFontOfSize(12.5f);

			if (BusinessCell.EditingStyle == UITableViewCellEditingStyle.Insert)
			{
				this.application.cellTravel.AccessoryView = null;
				this.application.cellTravel.EditingAccessoryView = null;
			}

			if (this.application.localizedTravel.Count == 0)
			{
				this.application.cellTravel.AccessoryView = null;
				this.application.cellTravel.EditingAccessoryView = null;
			}

			else if (this.application.localizedTravel.Count >= 1)
			{
				if (this.application.localizedTravel.Count == 1)
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
				if (indexPath.Row == this.application.indexTableTravel.Row)
				{
					Console.WriteLine("index chosen");
					BusinessCell.AccessoryView = this.favouritesIndicator;
					BusinessCell.EditingAccessoryView = this.favouritesIndicator;
				}

				else if (this.application.indexIntTravel.Contains(indexPath.Row) == false)
				{
					Console.WriteLine("Index is not found");
					BusinessCell.AccessoryView = null;
					BusinessCell.EditingAccessoryView = null;
				}

				else if (this.application.indexIntTravel.Contains(indexPath.Row) == true)
				{
					Console.WriteLine("Index table chosen _ 1");
					if (indexPath.Row != this.application.indexTableTravel.Row)
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
				if (this.application.indexIntTravel.Contains(indexPath.Row) == true)
				{
					if (indexPath.Row != this.application.indexTableTravel.Row || indexPath.Row == this.application.indexTableTravel.Row)
					{
						return UITableViewCellEditingStyle.Delete;
					}
				}
				this.application.cellTravel.AccessoryView = null;
				this.application.cellTravel.EditingAccessoryView = null;

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

				if (this.application.localizedTravel.Count((string arg) => arg.ToString() == this.travelDict[indexPath.Row]) >= 1)
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

				else if (this.application.localizedTravel.Count((string arg) => arg.ToString() == this.travelDict[indexPath.Row]) == 0)
				{
					this.application.localizedTravel.Add(this.travelDict[indexPath.Row]);
					this.application.frenchTextTravel.Add(this.travelTranslatedDict[indexPath.Row]);
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

					this.application.indexIntTravel.Add(indexPath.Row);

					this.application.indexTableTravel = index;

					this.TableView.ReloadData();
				}
			}
			else if (editingStyle == UITableViewCellEditingStyle.Delete)
			{
				if (this.application.indexIntTravel.Contains(indexPath.Row) == true)
				{
					this.application.localizedTravel.RemoveAll((string obj) => obj == this.travelDict[indexPath.Row]);
					this.application.indexIntTravel.RemoveAll((int obj) => obj == indexPath.Row);

					this.application.favourites.RemoveAll((string obj) => obj == this.travelDict[indexPath.Row]);
					this.application.favouritesFrench.RemoveAll((string obj) => obj == this.travelTranslatedDict[indexPath.Row]);

					if (this.application.indexTableTravel.Row == indexPath.Row || this.application.indexTableTravel.Row != indexPath.Row)
					{
						this.application.cellTravel.AccessoryView = null;
						this.application.cellTravel.EditingAccessoryView = null;
						this.TableView.ReloadData();
					}

					if (this.application.localizedTravel.Count == 1)
					{
						this.TableView.ReloadData();
					}

					if (this.application.cellTravel.EditingStyle == UITableViewCellEditingStyle.Insert)
					{
						this.application.cellTravel.AccessoryView = null;
						this.application.cellTravel.EditingAccessoryView = null;
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
					this.application.cellTravel.AccessoryView = null;
					this.application.cellTravel.EditingAccessoryView = null;
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
			return this.travelDict.Count;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			BatteryMonitor AI = new BatteryMonitor();
			switch (indexPath.Row)
			{
				case 0:
					AI.frenchPhraseBookAI(travelTranslatedDict[0]);
					break;
				case 1:
					AI.frenchPhraseBookAI(travelTranslatedDict[1]);
					break;
				case 2:
					AI.frenchPhraseBookAI(travelTranslatedDict[2]);
					break;
				case 3:
					AI.frenchPhraseBookAI(travelTranslatedDict[3]);
					break;
				case 4:
					AI.frenchPhraseBookAI(travelTranslatedDict[4]);
					break;
				case 5:
					AI.frenchPhraseBookAI(travelTranslatedDict[5]);
					break;
				case 6:
					AI.frenchPhraseBookAI(travelTranslatedDict[6]);
					break;
				case 7:
					AI.frenchPhraseBookAI(travelTranslatedDict[7]);
					break;
				case 8:
					AI.frenchPhraseBookAI(travelTranslatedDict[8]);
					break;
				case 9:
					AI.frenchPhraseBookAI(travelTranslatedDict[9]);
					break;
				case 10:
					AI.frenchPhraseBookAI(travelTranslatedDict[10]);
					break;
				case 11:
					AI.frenchPhraseBookAI(travelTranslatedDict[11]);
					break;
				case 12:
					AI.frenchPhraseBookAI(travelTranslatedDict[12]);
					break;
				case 13:
					AI.frenchPhraseBookAI(travelTranslatedDict[13]);
					break;
				case 14:
					AI.frenchPhraseBookAI(travelTranslatedDict[14]);
					break;
				case 15:
					AI.frenchPhraseBookAI(travelTranslatedDict[15]);
					break;
				case 16:
					AI.frenchPhraseBookAI(travelTranslatedDict[16]);
					break;
				case 17:
					AI.frenchPhraseBookAI(travelTranslatedDict[17]);
					break;
				case 18:
					AI.frenchPhraseBookAI(travelTranslatedDict[18]);
					break;
				case 19:
					AI.frenchPhraseBookAI(travelTranslatedDict[19]);
					break;
				case 20:
					AI.frenchPhraseBookAI(travelTranslatedDict[20]);
					break;
				case 21:
					AI.frenchPhraseBookAI(travelTranslatedDict[21]);
					break;
				case 22:
					AI.frenchPhraseBookAI(travelTranslatedDict[22]);
					break;
				case 23:
					AI.frenchPhraseBookAI(travelTranslatedDict[23]);
					break;
				case 24:
					AI.frenchPhraseBookAI(travelTranslatedDict[24]);
					break;
				case 25:
					AI.frenchPhraseBookAI(travelTranslatedDict[25]);
					break;
				case 26:
					AI.frenchPhraseBookAI(travelTranslatedDict[26]);
					break;
				case 27:
					AI.frenchPhraseBookAI(travelTranslatedDict[27]);
					break;
				case 28:
					AI.frenchPhraseBookAI(travelTranslatedDict[28]);
					break;
				case 29:
					AI.frenchPhraseBookAI(travelTranslatedDict[29]);
					break;
				case 30:
					AI.frenchPhraseBookAI(travelTranslatedDict[30]);
					break;
				case 31:
					AI.frenchPhraseBookAI(travelTranslatedDict[31]);
					break;
				case 32:
					AI.frenchPhraseBookAI(travelTranslatedDict[32]);
					break;
				case 33:
					AI.frenchPhraseBookAI(travelTranslatedDict[33]);
					break;
				case 34:
					AI.frenchPhraseBookAI(travelTranslatedDict[34]);
					break;
				case 35:
					AI.frenchPhraseBookAI(travelTranslatedDict[35]);
					break;
				case 36:
					AI.frenchPhraseBookAI(travelTranslatedDict[36]);
					break;
				case 37:
					AI.frenchPhraseBookAI(travelTranslatedDict[37]);
					break;
				case 38:
					AI.frenchPhraseBookAI(travelTranslatedDict[38]);
					break;
				case 39:
					AI.frenchPhraseBookAI(travelTranslatedDict[39]);
					break;
				case 40:
					AI.frenchPhraseBookAI(travelTranslatedDict[40]);
					break;
				case 41:
					AI.frenchPhraseBookAI(travelTranslatedDict[41]);
					break;
			}
			tableView.DeselectRow(indexPath, true);
		}
	}
}