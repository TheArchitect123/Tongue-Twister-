using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

using UIKit;
using CoreFoundation;
using Foundation;


namespace FrenchPhraseBook
{
	public partial class FoodController : UITableViewController
	{
		public List<string> foodDict = new List<string>(){
			 {"I like this dish"},{"Are you enjoying your meal"},{"How's dinner?"},{"How's breakfast?"},{"How's lunch?"},{"Afternoon tea"},{"Steak"},
			{"Cereal"},{"Chicken"},{"Spinach"},{"Lemon"},{"I am a great chef"},{"Spoon"},{"Knife"},{"Cutlery"},{"Do you know how to cook..."},{"Pizza"},
			{"Vegetables and rice"},{"I'm starving"},{"I'm a vegetarian"},{"I want to order some take away"},{"This is tasty"},{"This is spicy"},{"Did you make this yourself?"},
			{"Salt"},{"Sugar"},{"Soy sauce is great for sushi"},{"Beer"},{"Milk and Honey"},{"Coffee"},{"This tea is hot"},{"Orange juice"},{"Watermelon"},
			{"Fruit smoothie"},{"Do you drink protein shakes?"},{"How much is this?"},{"Glass cup"},{"What do you call this?"},{"Can I have a menu please?"},{"Can I have the bill please?"},
			{"I don't eat meat"},{"Can I order some..?"},{"Apple"},{"Fruit salad"},{"Sultanas"},{"Dried fruit"},{"I like eating..."},{"Milk"},{"Hot"},{"Cold"},{"Mild"},{"Spicy"},{"Sweet"},{"Salty"},{"Sour"},
			{"This is too sweet"},{"This is too salty"},{"Too much salt is not good"},{"Can I have some ice with that?"},{"A toast to you"},{"Cake"},{"Bread"},{"Toast"},{"Yoghourt"},{"Butter"},{"Ice-cream"},{"This is my recipe"},
		};

		Dictionary<int, string> foodTranslatedDict = new Dictionary<int, string> {
			{0,"J'aime ce plat"},{1,"Êtes-vous profiter de votre repas"},{2,"Comment est le dîner?"},{3,"Comment est le petit déjeuner?"},{4,"Comment est le déjeuner?"},{5,"Le thé de l'après-midi"},{6,"Steak"},{7,"Céréale"},
			{8,"poulet"},{9,"épinard"},{10,"citron"},{11,"Je suis un grand chef"},{12,"Cuillère"},{13,"Couteau"},{14,"Coutellerie"},{15,"Savez-vous comment faire cuire..."},
			{16,"Pizza"},{17,"Les légumes et le riz"},{18,"je meurs de faim"},{19,"Je suis un végétarien"},{20,"Je veux commander quelque emporter"},{21,"Ceci est savoureux"},{22,"Ceci est épicé"},{23,"Avez-vous fait vous-même?"},{24,"Sel"},
			{25,"Sucre"},{26,"La sauce de soja est excellent pour sushi"},{27,"Bière"},{28,"Lait et miel"},{29,"café"},{30,"Ce thé est chaud"},{31,"du jus d'orange"},
			{32,"Pastèque"},{33,"Smoothie aux fruits"},{34,"Vous buvez shakes de protéines?"},{35,"Combien ça coûte?"},{36,"Coupe en verre"},{37,"Comment appelles-tu ceci?"},{38,"Puis-je avoir un menu s'il vous plaît?"},{39,"Puis-je avoir la note s'il vous plaît?"},
			{40,"Je ne mange pas de viande"},{41,"Puis-je commander un certain..?"},{42,"pomme"},{43,"Salade de fruit"},{44,"Sultanes"},{45,"Fruit sec"},{46,"J'aime manger..."},{47,"Lait"},
			{48,"Chaud"},{49,"Du froid"},{50,"Doux"},{51,"Épicé"},{52,"Doux"},{53,"Salé"},{54,"Acide"},{55,"Ceci est trop sucré"},{56,"Ceci est trop salée"},{57,"Trop de sel est pas bon"},{58,"Puis-je avoir un peu de glace avec ça?"},
			{59,"Un toast à vous"},{60,"gâteau"},{61,"Pain"},{62,"Pain grillé"},{63,"Yaourt"}, {64,"beurre"},{65,"Crème glacée"},{66,"Ceci est ma recette"}

		};
		public string foodID = "food";

		public FoodController(IntPtr handle) : base(handle)
		{
		}

		public FoodController() { }

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
				AI.frenchPhraseBookAI(this.foodTranslatedDict[index.Row]);
			}
			else if (this.application.tabBarID == 0)
			{
				if (this.application.localizedTextFood.Count == 1)
				{
					this.application.cellFood.AccessoryView = this.favouritesIndicator;
					this.application.cellFood.EditingAccessoryView = this.favouritesIndicator;
					this.TableView.ReloadData();
				}
			}

			if (this.application.localizedTextFood.Count == 1)
			{
				this.application.cellFood.AccessoryView = this.favouritesIndicator;
				this.application.cellFood.EditingAccessoryView = this.favouritesIndicator;
				this.TableView.ReloadData();
			}
		}

		public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
		{
			return 80.0f;
		}

		public override void ViewDidLoad()
		{
			this.application.foodControl = this;
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




			UIBarButtonItem optionButton = new UIBarButtonItem("<\ud83c\udf54", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
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

			this.NavigationItem.Title = "Food";

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
			UITableViewCell BusinessCell = tableView.DequeueReusableCell(this.foodID);

			if (BusinessCell == null)
			{
				BusinessCell = new UITableViewCell(UITableViewCellStyle.Subtitle, this.foodID);
			}


			this.application.cellFood = BusinessCell;

			this.favouritesIndicator = new UILabel();
			this.favouritesIndicator.Text = "\ud83d\udc9d";
			//this.favouritesIndicator.Text = "⭐";
			this.favouritesIndicator.MinimumFontSize = 24.0f;
			this.favouritesIndicator.AdjustsFontSizeToFitWidth = true;
			this.favouritesIndicator.Frame = new CoreGraphics.CGRect(0, 20, 40, 40);

			BusinessCell.TextLabel.Text = this.foodDict[indexPath.Row];
			BusinessCell.DetailTextLabel.Text = this.foodTranslatedDict[indexPath.Row];
			BusinessCell.DetailTextLabel.TextColor = UIColor.Gray;
			BusinessCell.DetailTextLabel.Font = UIFont.SystemFontOfSize(12.5f);

			if (BusinessCell.EditingStyle == UITableViewCellEditingStyle.Insert)
			{
				this.application.cellFood.AccessoryView = null;
				this.application.cellFood.EditingAccessoryView = null;
			}

			if (this.application.localizedTextFood.Count == 0)
			{
				this.application.cellFood.AccessoryView = null;
				this.application.cellFood.EditingAccessoryView = null;
			}

			else if (this.application.localizedTextFood.Count >= 1)
			{
				if (this.application.localizedTextFood.Count == 1)
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
				if (indexPath.Row == this.application.indexTableFood.Row)
				{
					Console.WriteLine("index chosen");
					BusinessCell.AccessoryView = this.favouritesIndicator;
					BusinessCell.EditingAccessoryView = this.favouritesIndicator;
				}

				else if (this.application.indexIntFood.Contains(indexPath.Row) == false)
				{
					Console.WriteLine("Index is not found");
					BusinessCell.AccessoryView = null;
					BusinessCell.EditingAccessoryView = null;
				}

				else if (this.application.indexIntFood.Contains(indexPath.Row) == true)
				{
					Console.WriteLine("Index table chosen _ 1");
					if (indexPath.Row != this.application.indexTableFood.Row)
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
				if (this.application.indexIntFood.Contains(indexPath.Row) == true)
				{
					if (indexPath.Row != this.application.indexTableFood.Row || indexPath.Row == this.application.indexTableFood.Row)
					{
						return UITableViewCellEditingStyle.Delete;
					}
				}
				this.application.cellFood.AccessoryView = null;
				this.application.cellFood.EditingAccessoryView = null;

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

				if (this.application.localizedTextFood.Count((string arg) => arg.ToString() == this.foodDict[indexPath.Row]) >= 1)
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

				else if (this.application.localizedTextFood.Count((string arg) => arg.ToString() == this.foodDict[indexPath.Row]) == 0)
				{
					this.application.localizedTextFood.Add(this.foodDict[indexPath.Row]);
					this.application.frenchTextFood.Add(this.foodTranslatedDict[indexPath.Row]);
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

					this.application.indexIntFood.Add(indexPath.Row);

					this.application.indexTableFood = index;

					this.TableView.ReloadData();
				}
			}
			else if (editingStyle == UITableViewCellEditingStyle.Delete)
			{
				if (this.application.indexIntFood.Contains(indexPath.Row) == true)
				{
					this.application.localizedTextFood.RemoveAll((string obj) => obj == this.foodDict[indexPath.Row]);
					this.application.indexIntFood.RemoveAll((int obj) => obj == indexPath.Row);

					this.application.favourites.RemoveAll((string obj) => obj == this.foodDict[indexPath.Row]);
					this.application.favouritesFrench.RemoveAll((string obj) => obj == this.foodTranslatedDict[indexPath.Row]);

					if (this.application.indexTableFood.Row == indexPath.Row || this.application.indexTableFood.Row != indexPath.Row)
					{
						this.application.cellFood.AccessoryView = null;
						this.application.cellFood.EditingAccessoryView = null;
						this.TableView.ReloadData();
					}

					if (this.application.localizedTextFood.Count == 1)
					{
						this.TableView.ReloadData();
					}

					if (this.application.cellFood.EditingStyle == UITableViewCellEditingStyle.Insert)
					{
						this.application.cellFood.AccessoryView = null;
						this.application.cellFood.EditingAccessoryView = null;
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
					this.application.cellFood.AccessoryView = null;
					this.application.cellFood.EditingAccessoryView = null;
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
			return this.foodDict.Count;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			BatteryMonitor AI = new BatteryMonitor();
			switch (indexPath.Row)
			{
				case 0:
					AI.frenchPhraseBookAI(foodTranslatedDict[0]);
					break;
				case 1:
					AI.frenchPhraseBookAI(foodTranslatedDict[1]);
					break;
				case 2:
					AI.frenchPhraseBookAI(foodTranslatedDict[2]);
					break;
				case 3:
					AI.frenchPhraseBookAI(foodTranslatedDict[3]);
					break;
				case 4:
					AI.frenchPhraseBookAI(foodTranslatedDict[4]);
					break;
				case 5:
					AI.frenchPhraseBookAI(foodTranslatedDict[5]);
					break;
				case 6:
					AI.frenchPhraseBookAI(foodTranslatedDict[6]);
					break;
				case 7:
					AI.frenchPhraseBookAI(foodTranslatedDict[7]);
					break;
				case 8:
					AI.frenchPhraseBookAI(foodTranslatedDict[8]);
					break;
				case 9:
					AI.frenchPhraseBookAI(foodTranslatedDict[9]);
					break;
				case 10:
					AI.frenchPhraseBookAI(foodTranslatedDict[10]);
					break;
				case 11:
					AI.frenchPhraseBookAI(foodTranslatedDict[11]);
					break;
				case 12:
					AI.frenchPhraseBookAI(foodTranslatedDict[12]);
					break;
				case 13:
					AI.frenchPhraseBookAI(foodTranslatedDict[13]);
					break;
				case 14:
					AI.frenchPhraseBookAI(foodTranslatedDict[14]);
					break;
				case 15:
					AI.frenchPhraseBookAI(foodTranslatedDict[15]);
					break;
				case 16:
					AI.frenchPhraseBookAI(foodTranslatedDict[16]);
					break;
				case 17:
					AI.frenchPhraseBookAI(foodTranslatedDict[17]);
					break;
				case 18:
					AI.frenchPhraseBookAI(foodTranslatedDict[18]);
					break;
				case 19:
					AI.frenchPhraseBookAI(foodTranslatedDict[19]);
					break;
				case 20:
					AI.frenchPhraseBookAI(foodTranslatedDict[20]);
					break;
				case 21:
					AI.frenchPhraseBookAI(foodTranslatedDict[21]);
					break;
				case 22:
					AI.frenchPhraseBookAI(foodTranslatedDict[22]);
					break;
				case 23:
					AI.frenchPhraseBookAI(foodTranslatedDict[23]);
					break;
				case 24:
					AI.frenchPhraseBookAI(foodTranslatedDict[24]);
					break;
				case 25:
					AI.frenchPhraseBookAI(foodTranslatedDict[25]);
					break;
				case 26:
					AI.frenchPhraseBookAI(foodTranslatedDict[26]);
					break;
				case 27:
					AI.frenchPhraseBookAI(foodTranslatedDict[27]);
					break;
				case 28:
					AI.frenchPhraseBookAI(foodTranslatedDict[28]);
					break;
				case 29:
					AI.frenchPhraseBookAI(foodTranslatedDict[29]);
					break;
				case 30:
					AI.frenchPhraseBookAI(foodTranslatedDict[30]);
					break;
				case 31:
					AI.frenchPhraseBookAI(foodTranslatedDict[31]);
					break;
				case 32:
					AI.frenchPhraseBookAI(foodTranslatedDict[32]);
					break;
				case 33:
					AI.frenchPhraseBookAI(foodTranslatedDict[33]);
					break;
				case 34:
					AI.frenchPhraseBookAI(foodTranslatedDict[34]);
					break;
				case 35:
					AI.frenchPhraseBookAI(foodTranslatedDict[35]);
					break;
				case 36:
					AI.frenchPhraseBookAI(foodTranslatedDict[36]);
					break;
				case 37:
					AI.frenchPhraseBookAI(foodTranslatedDict[37]);
					break;
				case 38:
					AI.frenchPhraseBookAI(foodTranslatedDict[38]);
					break;
				case 39:
					AI.frenchPhraseBookAI(foodTranslatedDict[39]);
					break;
				case 40:
					AI.frenchPhraseBookAI(foodTranslatedDict[40]);
					break;
				case 41:
					AI.frenchPhraseBookAI(foodTranslatedDict[41]);
					break;
				case 42:
					AI.frenchPhraseBookAI(foodTranslatedDict[42]);
					break;
				case 43:
					AI.frenchPhraseBookAI(foodTranslatedDict[43]);
					break;
				case 44:
					AI.frenchPhraseBookAI(foodTranslatedDict[44]);
					break;
				case 45:
					AI.frenchPhraseBookAI(foodTranslatedDict[45]);
					break;
				case 46:
					AI.frenchPhraseBookAI(foodTranslatedDict[46]);
					break;
				case 47:
					AI.frenchPhraseBookAI(foodTranslatedDict[47]);
					break;
				case 48:
					AI.frenchPhraseBookAI(foodTranslatedDict[48]);
					break;
				case 49:
					AI.frenchPhraseBookAI(foodTranslatedDict[49]);
					break;
				case 50:
					AI.frenchPhraseBookAI(foodTranslatedDict[50]);
					break;
				case 51:
					AI.frenchPhraseBookAI(foodTranslatedDict[51]);
					break;
				case 52:
					AI.frenchPhraseBookAI(foodTranslatedDict[52]);
					break;
				case 53:
					AI.frenchPhraseBookAI(foodTranslatedDict[53]);
					break;
				case 54:
					AI.frenchPhraseBookAI(foodTranslatedDict[54]);
					break;
				case 55:
					AI.frenchPhraseBookAI(foodTranslatedDict[55]);
					break;
				case 56:
					AI.frenchPhraseBookAI(foodTranslatedDict[56]);
					break;
				case 57:
					AI.frenchPhraseBookAI(foodTranslatedDict[57]);
					break;
				case 58:
					AI.frenchPhraseBookAI(foodTranslatedDict[58]);
					break;
				case 59:
					AI.frenchPhraseBookAI(foodTranslatedDict[59]);
					break;
				case 60:
					AI.frenchPhraseBookAI(foodTranslatedDict[60]);
					break;
				case 61:
					AI.frenchPhraseBookAI(foodTranslatedDict[61]);
					break;
				case 62:
					AI.frenchPhraseBookAI(foodTranslatedDict[62]);
					break;
				case 63:
					AI.frenchPhraseBookAI(foodTranslatedDict[63]);
					break;
				case 64:
					AI.frenchPhraseBookAI(foodTranslatedDict[64]);
					break;
				case 65:
					AI.frenchPhraseBookAI(foodTranslatedDict[65]);
					break;
				case 66:
					AI.frenchPhraseBookAI(foodTranslatedDict[66]);
					break;
				case 67:
					AI.frenchPhraseBookAI(foodTranslatedDict[67]);
					break;
				default:
					Console.WriteLine("No key selected");
					break;
			}
			tableView.DeselectRow(indexPath, true);
		}

	}
}

