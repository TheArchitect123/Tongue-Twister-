using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

using System.Linq;

using UIKit;
using Foundation;
using CoreFoundation;

namespace FrenchPhraseBook
{
	public partial class ShoppingController : UITableViewController
	{
		public ShoppingController(IntPtr handle) : base(handle)
		{
		}

		public ShoppingController() { }


		public List<string> firstTimeDict = new List<string>() {
			{"Cash register"},{"Shopping"},{"Shelf"},{"Fridge"},{"Grocery store"},{"Mall"},{"How much does this cost?"},{"Where can I find...(Item)"},{"Do you work here?"},
			{"I work near the furniture section"},{"I am looking for...(item)"},
			{"Battery"},{"Electronics"},{"Here's your change"},{"Where my change?"},{"This is too expensive"},{"This too cheap"},{"This product is faulty"},
			{"This product is on display only"},{"What is this?"},{"What is that?"},{"This item costs...(Amount)"},
			{"Which one is the plasma TV?"},{"I want that...(item)...there"},{"I cannot reach that item on the shelf"},{"All this costs...(Amount)"},{"Would you like to buy...(item)?"},
			{"Would you like to sample this chocolate?"},{"Free TV for every $300 you spend"},{"I hate shopping!"},{"I love shopping!"},{"I need to buy some new clothes"},
			{"I'm going to the mall with my friends"},{"I'm looking for...(Shop name)"},{"Buy 2 get one free"},{"How much can I get for all these?"},{"50% off sale for today only"},{"(Number)%...off sale for tonight only"},
			{"I'll take 2 of those"},{"Do you take credit card here?"},{"We only accept cash"},{"We accept cash and credit card"},{"Where is the food court?"},{"I need to withdraw cash"},
			{"Put it on my tab"},
		};

		//place the translated dictionary here
		Dictionary<int, string> firstTimeTranslated = new Dictionary<int, string>() {
			{0,"Caisse enregistreuse"},{1,"Achats"},{2,"Étagère"},{3,"Frigo"},{4,"Épicerie"},{5,"Centre commercial"},{6,"Combien cela coûte?"},
			{7,"Où puis-je trouver..."},{8,"Est-ce que tu travailles ici?"},{9,"Je travaille près de la section de meubles"},{10,"Je cherche..."},
			{11,"les accumulateurs"},{12,"Électronique"},{13,"Voilà votre monnaie"},{14,"Lorsque mon changement?"},{15,"Ceci est trop cher"},
			{16,"Ce trop pas cher"},{17,"Ce produit est défectueux"},{18,"Ce produit est en affichage seulement"},{19,"Qu'est-ce que c'est?"},{20,"Qu'est-ce que c'est?"},{21,"Cet article coûte..."},
			{22,"Lequel est le téléviseur à écran plasma?"},{23,"Je veux que ... il"},{24,"Je ne peux pas accéder à cet article sur le plateau"},{25,"Tout cela coûte..."},{26,"Voulez-vous acheter..."},{27,"Voulez-vous goûter ce chocolat?"},
			{28,"Free TV pour chaque 300 $ que vous dépensez"},{29,"Je déteste le shopping!"},{30,"J'adore le shopping!"},{31,"Je besoin d'acheter de nouveaux vêtements"},{32,"Je vais au centre commercial avec mes amis"},
			{33,"Je cherche..."},{34,"Acheter 2 obtenir un gratuitement"},{35,"Combien puis-je obtenir pour tous ces?"},{36,"50% off vente pour aujourd'hui seulement"},{37,"... Hors vente pour ce soir seulement"},{38,"Je vais prendre 2 de ceux"},
			{39,"Prenez-vous la carte de crédit ici?"},{40,"Nous acceptons uniquement les espèces"},{41,"Nous acceptons l'argent comptant et carte de crédit"},{42,"Où est la cour de nourriture?"},{43,"Je dois retirer de l'argent"},
			{44,"Mettez-le sur mon onglet"}
		};
		public string shoppingString = "Family";

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
				AI.frenchPhraseBookAI(this.firstTimeDict[index.Row]);
			}
			else if (this.application.tabBarID == 0)
			{
				if (this.application.localizedShopping.Count == 1)
				{
					this.application.cellShopping.AccessoryView = this.favouritesIndicator;
					this.application.cellShopping.EditingAccessoryView = this.favouritesIndicator;
					this.TableView.ReloadData();
				}
			}

			if (this.application.localizedShopping.Count == 1)
			{
				this.application.cellShopping.AccessoryView = this.favouritesIndicator;
				this.application.cellShopping.EditingAccessoryView = this.favouritesIndicator;
				this.TableView.ReloadData();
			}
		}

		public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
		{
			return 80.0f;
		}

		public override void ViewDidLoad()
		{
			this.application.shoppingControl = this;
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




			UIBarButtonItem optionButton = new UIBarButtonItem("<\ud83d\udc60", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
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

			this.NavigationItem.Title = "Shopping";

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
			UITableViewCell BusinessCell = tableView.DequeueReusableCell(this.shoppingString);

			if (BusinessCell == null)
			{
				BusinessCell = new UITableViewCell(UITableViewCellStyle.Subtitle, this.shoppingString);
			}


			this.application.cellShopping = BusinessCell;

			this.favouritesIndicator = new UILabel();
			this.favouritesIndicator.Text = "\ud83d\udc9d";
			//this.favouritesIndicator.Text = "⭐";
			this.favouritesIndicator.MinimumFontSize = 24.0f;
			this.favouritesIndicator.AdjustsFontSizeToFitWidth = true;
			this.favouritesIndicator.Frame = new CoreGraphics.CGRect(0, 20, 40, 40);

			BusinessCell.TextLabel.Text = this.firstTimeDict[indexPath.Row];
			BusinessCell.DetailTextLabel.Text = this.firstTimeTranslated[indexPath.Row];
			BusinessCell.DetailTextLabel.TextColor = UIColor.Gray;
			BusinessCell.DetailTextLabel.Font = UIFont.SystemFontOfSize(12.5f);

			if (BusinessCell.EditingStyle == UITableViewCellEditingStyle.Insert)
			{
				this.application.cellShopping.AccessoryView = null;
				this.application.cellShopping.EditingAccessoryView = null;
			}

			if (this.application.localizedShopping.Count == 0)
			{
				this.application.cellShopping.AccessoryView = null;
				this.application.cellShopping.EditingAccessoryView = null;
			}

			else if (this.application.localizedShopping.Count >= 1)
			{
				if (this.application.localizedShopping.Count == 1)
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
				if (indexPath.Row == this.application.indexTableShopping.Row)
				{
					Console.WriteLine("index chosen");
					BusinessCell.AccessoryView = this.favouritesIndicator;
					BusinessCell.EditingAccessoryView = this.favouritesIndicator;
				}

				else if (this.application.indexIntShopping.Contains(indexPath.Row) == false)
				{
					Console.WriteLine("Index is not found");
					BusinessCell.AccessoryView = null;
					BusinessCell.EditingAccessoryView = null;
				}

				else if (this.application.indexIntShopping.Contains(indexPath.Row) == true)
				{
					Console.WriteLine("Index table chosen _ 1");
					if (indexPath.Row != this.application.indexTableShopping.Row)
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
				if (this.application.indexIntShopping.Contains(indexPath.Row) == true)
				{
					if (indexPath.Row != this.application.indexTableShopping.Row || indexPath.Row == this.application.indexTableShopping.Row)
					{
						return UITableViewCellEditingStyle.Delete;
					}
				}
				this.application.cellShopping.AccessoryView = null;
				this.application.cellShopping.EditingAccessoryView = null;

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

				if (this.application.localizedShopping.Count((string arg) => arg.ToString() == this.firstTimeDict[indexPath.Row]) >= 1)
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

				else if (this.application.localizedShopping.Count((string arg) => arg.ToString() == this.firstTimeDict[indexPath.Row]) == 0)
				{
					this.application.localizedShopping.Add(this.firstTimeDict[indexPath.Row]);
					this.application.frenchTextShopping.Add(this.firstTimeTranslated[indexPath.Row]);
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

					this.application.indexIntShopping.Add(indexPath.Row);

					this.application.indexTableShopping = index;

					this.TableView.ReloadData();
				}
			}
			else if (editingStyle == UITableViewCellEditingStyle.Delete)
			{
				if (this.application.indexIntShopping.Contains(indexPath.Row) == true)
				{
					this.application.localizedShopping.RemoveAll((string obj) => obj == this.firstTimeDict[indexPath.Row]);
					this.application.indexIntShopping.RemoveAll((int obj) => obj == indexPath.Row);

					this.application.favourites.RemoveAll((string obj) => obj == this.firstTimeDict[indexPath.Row]);
					this.application.favouritesFrench.RemoveAll((string obj) => obj == this.firstTimeTranslated[indexPath.Row]);

					if (this.application.indexTableShopping.Row == indexPath.Row || this.application.indexTableShopping.Row != indexPath.Row)
					{
						this.application.cellShopping.AccessoryView = null;
						this.application.cellShopping.EditingAccessoryView = null;
						this.TableView.ReloadData();
					}

					if (this.application.localizedShopping.Count == 1)
					{
						this.TableView.ReloadData();
					}

					if (this.application.cellShopping.EditingStyle == UITableViewCellEditingStyle.Insert)
					{
						this.application.cellShopping.AccessoryView = null;
						this.application.cellShopping.EditingAccessoryView = null;
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
					this.application.cellShopping.AccessoryView = null;
					this.application.cellShopping.EditingAccessoryView = null;
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
			return this.firstTimeDict.Count;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			BatteryMonitor AI = new BatteryMonitor();
			switch (indexPath.Row)
			{
				case 0:
					AI.frenchPhraseBookAI(firstTimeTranslated[0]);
					break;
				case 1:
					AI.frenchPhraseBookAI(firstTimeTranslated[1]);
					break;
				case 2:
					AI.frenchPhraseBookAI(firstTimeTranslated[2]);
					break;
				case 3:
					AI.frenchPhraseBookAI(firstTimeTranslated[3]);
					break;
				case 4:
					AI.frenchPhraseBookAI(firstTimeTranslated[4]);
					break;
				case 5:
					AI.frenchPhraseBookAI(firstTimeTranslated[5]);
					break;
				case 6:
					AI.frenchPhraseBookAI(firstTimeTranslated[6]);
					break;
				case 7:
					AI.frenchPhraseBookAI(firstTimeTranslated[7]);
					break;
				case 8:
					AI.frenchPhraseBookAI(firstTimeTranslated[8]);
					break;
				case 9:
					AI.frenchPhraseBookAI(firstTimeTranslated[9]);
					break;
				case 10:
					AI.frenchPhraseBookAI(firstTimeTranslated[10]);
					break;
				case 11:
					AI.frenchPhraseBookAI(firstTimeTranslated[11]);
					break;
				case 12:
					AI.frenchPhraseBookAI(firstTimeTranslated[12]);
					break;
				case 13:
					AI.frenchPhraseBookAI(firstTimeTranslated[13]);
					break;
				case 14:
					AI.frenchPhraseBookAI(firstTimeTranslated[14]);
					break;
				case 15:
					AI.frenchPhraseBookAI(firstTimeTranslated[15]);
					break;
				case 16:
					AI.frenchPhraseBookAI(firstTimeTranslated[16]);
					break;
				case 17:
					AI.frenchPhraseBookAI(firstTimeTranslated[17]);
					break;
				case 18:
					AI.frenchPhraseBookAI(firstTimeTranslated[18]);
					break;
				case 19:
					AI.frenchPhraseBookAI(firstTimeTranslated[19]);
					break;
				case 20:
					AI.frenchPhraseBookAI(firstTimeTranslated[20]);
					break;
				case 21:
					AI.frenchPhraseBookAI(firstTimeTranslated[21]);
					break;
				case 22:
					AI.frenchPhraseBookAI(firstTimeTranslated[22]);
					break;
				case 23:
					AI.frenchPhraseBookAI(firstTimeTranslated[23]);
					break;
				case 24:
					AI.frenchPhraseBookAI(firstTimeTranslated[24]);
					break;
				case 25:
					AI.frenchPhraseBookAI(firstTimeTranslated[25]);
					break;
				case 26:
					AI.frenchPhraseBookAI(firstTimeTranslated[26]);
					break;
				case 27:
					AI.frenchPhraseBookAI(firstTimeTranslated[27]);
					break;
				case 28:
					AI.frenchPhraseBookAI(firstTimeTranslated[28]);
					break;
				case 29:
					AI.frenchPhraseBookAI(firstTimeTranslated[29]);
					break;
				case 30:
					AI.frenchPhraseBookAI(firstTimeTranslated[30]);
					break;
				case 31:
					AI.frenchPhraseBookAI(firstTimeTranslated[31]);
					break;
				case 32:
					AI.frenchPhraseBookAI(firstTimeTranslated[32]);
					break;
				case 33:
					AI.frenchPhraseBookAI(firstTimeTranslated[33]);
					break;
				case 34:
					AI.frenchPhraseBookAI(firstTimeTranslated[34]);
					break;
				case 35:
					AI.frenchPhraseBookAI(firstTimeTranslated[35]);
					break;
				case 36:
					AI.frenchPhraseBookAI(firstTimeTranslated[36]);
					break;
				case 37:
					AI.frenchPhraseBookAI(firstTimeTranslated[37]);
					break;
				case 38:
					AI.frenchPhraseBookAI(firstTimeTranslated[38]);
					break;
				case 39:
					AI.frenchPhraseBookAI(firstTimeTranslated[39]);
					break;
				case 40:
					AI.frenchPhraseBookAI(firstTimeTranslated[40]);
					break;
				case 41:
					AI.frenchPhraseBookAI(firstTimeTranslated[41]);
					break;
				case 42:
					AI.frenchPhraseBookAI(firstTimeTranslated[42]);
					break;
				case 43:
					AI.frenchPhraseBookAI(firstTimeTranslated[43]);
					break;
				case 44:
					AI.frenchPhraseBookAI(firstTimeTranslated[44]);
					break;

				default:
					Console.WriteLine("No key selected");
					break;
			}
			tableView.DeselectRow(indexPath, true);

		}
	}
}