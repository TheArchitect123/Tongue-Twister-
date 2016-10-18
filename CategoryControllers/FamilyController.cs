using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

using UIKit;
using CoreFoundation;
using Foundation;

namespace FrenchPhraseBook
{
	public partial class FamilyController : UITableViewController
	{
		public FamilyController(IntPtr handle) : base(handle)
		{
		}

		public FamilyController()
		{
		}

		//Dictionary of family phrases 
		public List<string> familyDict = new List<string>{
			{"Nephew"}, {"Brother"}, {"Sister"}, {"Father"},{"Mother"},{"Aunt"},{"Uncle"},{"Grandfather"},{"Grandmother"},
			{"Niece"},{"Son"},{"Daughter"},{"This is my daughter"},{"That is my uncle"},{"He is my grandfather"},{"Family trip"},{"Where is your niece?"},{"My son left the house"},{"My sister is out working"},{"I have not seen my grandfather in three years"},{"This is a family trip"},
			{"We should celebrate christmas early this year!"},{"Who is he?"},{"Who are they?"},{"Is this your grandmother?"},{"It's good to see you again sister"},{"He is my younger brother"},{"She is my younger sister"},{"This is my older niece"},{"This is my great grandmother"},
			{"Do you enjoy the family christmas dinners?"}, {"My grandmother has left shopping"},{"My family lives in America"},{"My family is in...(Country)"},{"I'm travelling this year to see my family"},{"I have not seen my family 10 years"},{"I have not seen my family in...(Amount)"},
			{"We're going to the movies together"},{"My...(Family member)...is going out tonight"},{"I called to my cousin yesterday"},{"I called my...(Family member)...yesterday"},{"What have you been doing this past year?"},{"How was work today?"},{"How was your day?"},{"You will not believe what happened to me today?"},
			{"How is your mother doing?"},{"If your father well?"},{"Is your...(Family member)...well?"},{"My father is an Salesman"},{"My grandfather is an Engineer"},{"My grandfather works as a...(Job)"},{"What does your...(family member)...do for a living?"},{"Can you drive your sister to work today?"},{"I'm going with friends tonight?"},
			{"I'm going to the...(Place)...with...(family member)"},{"I worked with my brother on this project"},{"I worked with my...(family member)...on this project"},
			{"My brother is sick with the flu"},{"My...(family member)...is sick with the flu"},{"I see my family once a week"},{"I see my family once a...(Time)"},{"Do you live with your parents?"},{"Do you live with your...(Family member)?"},
			{"I pay my own bills and rent"},{"My father is well read"},{"My brother always takes of himself"},{"My brother preaches education"},
			{"My niece has recently finished college"},{"Have you done your chores?"},{"Have you done your homework?"},{"You want to join the gym with me?"},
		};

		Dictionary<int, string> familyTranslatedDict = new Dictionary<int, string>{

			{0,"Neveu"},{1,"Frère"},{2,"Sœur"},{3,"Père"},{4,"Mère"},{5,"Tante"},{6,"Oncle"},{7,"Grand-père"},{8,"Grand-mère"},{9,"Nièce"},{10,"Fils"},{11,"Fille"},{12,"C'est ma fille"},{13,"Voilà mon oncle"},{14,"Il est mon grand-père"},{15,"voyage en famille"},{16,"Where is your niece?"},{17,"Mon fils a quitté la maison"},
			{18,"Ma sœur est hors travail"},{19,"Je ne l'ai pas vu mon grand-père en trois ans"},{20,"Ceci est un voyage en famille"},{21,"Nous devrions célébrer Noël au début de cette année!"},{22,"Qui est-il?"},{23,"Qui sont-ils?"},
			{24,"Est-ce votre grand-mère?"},{25,"Il est bon de vous revoir soeur"},{26,"Il est mon frère cadet"},{27,"Elle est ma sœur cadette"},{28,"Ceci est ma nièce plus âgée"},{29,"Ceci est mon arrière grand-mère"},{30,"Aimez-vous la famille des dîners de Noël?"},
			{31,"Ma grand-mère a quitté le shopping"},{32,"Ma famille vit en Amérique"},{33,"Ma famille est en ..."},{34,"I'm travelling this year to see my family"},{35,"Je ne l'ai pas vu ma famille 10 ans"},{36,"Je ne l'ai pas vu ma famille dans ..."},{37,"Nous allons au cinéma ensemble"},{38,"Mon ...... va sortir ce soir"},
			{39,"J'ai appelé à mon cousin hier"},{40,"J'ai appelé mon..."},{41,"Qu'est-ce que tu as fait cette dernière année?"},{42,"Comment était le travail aujourd'hui?"},{43,"Comment était ta journée?"},{44,"Vous ne serez pas croire ce qui est arrivé à moi aujourd'hui?"},{45,"Comment votre mère est en train de faire?"},{46,"Si bien votre père?"},
			{47,"Est ton..."},{48,"Mon père est un Salesman"},{49,"Mon grand-père est ingénieur"},{50,"Mon grand-père travaille comme..."},{51,"Qu'est-ce que votre ... faire pour vivre?"},{52,"Pouvez-vous conduire votre soeur au travail aujourd'hui?"},{53,"Je vais avec des amis ce soir?"},{54,"Je vais le...avec..."},{55,"Je travaillais avec mon frère sur ce projet"},
			{56,"Je travaillais avec mon ... sur ce projet"},{57,"Mon frère est malade avec la grippe"},{58,"Mon ... est malade avec la grippe"},{59,"Je vois ma famille une fois par semaine"},
			{60,"Je vois ma famille une fois..."},{61,"Vivez-vous avec vos parents?"},{62,"vous vivez avec votre ... Est-ce que"},{63,"I pay my own bills and rent"},{64,"Mon père est bien lu"},{65,"Mon frère a toujours de lui-même"},{66,"Mon frère prêche l'éducation"},{67,"Ma nièce a récemment terminé ses études"},{68,"Avez-vous fait vos tâches?"},{69,"Avez-vous fait vos devoirs?"},
			{70,"Vous souhaitez rejoindre la salle de gym avec moi?"}
		};

		public string familyString = "Family";

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
				AI.frenchPhraseBookAI(this.familyTranslatedDict[index.Row]);
			}
			else if (this.application.tabBarID == 0)
			{
				if (this.application.localizedTextFamily.Count == 1)
				{
					this.application.cellFamily.AccessoryView = this.favouritesIndicator;
					this.application.cellFamily.EditingAccessoryView = this.favouritesIndicator;
					this.TableView.ReloadData();
				}
			}

			if (this.application.localizedTextFamily.Count == 1)
			{
				this.application.cellFamily.AccessoryView = this.favouritesIndicator;
				this.application.cellFamily.EditingAccessoryView = this.favouritesIndicator;
				this.TableView.ReloadData();
			}
		}

		public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
		{
			return 80.0f;
		}

		public override void ViewDidLoad()
		{
			this.application.familyControl = this;
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




			UIBarButtonItem optionButton = new UIBarButtonItem("<\ud83c\udf7d", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
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

			this.NavigationItem.Title = "Family & friends";

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
			UITableViewCell BusinessCell = tableView.DequeueReusableCell(this.familyString);

			if (BusinessCell == null)
			{
				BusinessCell = new UITableViewCell(UITableViewCellStyle.Subtitle, this.familyString);
			}


			this.application.cellFamily = BusinessCell;

			this.favouritesIndicator = new UILabel();
			this.favouritesIndicator.Text = "\ud83d\udc9d";
			//this.favouritesIndicator.Text = "⭐";
			this.favouritesIndicator.MinimumFontSize = 24.0f;
			this.favouritesIndicator.AdjustsFontSizeToFitWidth = true;
			this.favouritesIndicator.Frame = new CoreGraphics.CGRect(0, 20, 40, 40);

			BusinessCell.TextLabel.Text = this.familyDict[indexPath.Row];
			BusinessCell.DetailTextLabel.Text = this.familyTranslatedDict[indexPath.Row];
			BusinessCell.DetailTextLabel.TextColor = UIColor.Gray;
			BusinessCell.DetailTextLabel.Font = UIFont.SystemFontOfSize(12.5f);

			if (BusinessCell.EditingStyle == UITableViewCellEditingStyle.Insert)
			{
				this.application.cellFamily.AccessoryView = null;
				this.application.cellFamily.EditingAccessoryView = null;
			}

			if (this.application.localizedTextFamily.Count == 0)
			{
				this.application.cellFamily.AccessoryView = null;
				this.application.cellFamily.EditingAccessoryView = null;
			}

			else if (this.application.localizedTextFamily.Count >= 1)
			{
				if (this.application.localizedTextFamily.Count == 1)
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
				if (indexPath.Row == this.application.indexTableFamily.Row)
				{
					Console.WriteLine("index chosen");
					BusinessCell.AccessoryView = this.favouritesIndicator;
					BusinessCell.EditingAccessoryView = this.favouritesIndicator;
				}

				else if (this.application.indexIntFamily.Contains(indexPath.Row) == false)
				{
					Console.WriteLine("Index is not found");
					BusinessCell.AccessoryView = null;
					BusinessCell.EditingAccessoryView = null;
				}

				else if (this.application.indexIntFamily.Contains(indexPath.Row) == true)
				{
					Console.WriteLine("Index table chosen _ 1");
					if (indexPath.Row != this.application.indexTableFamily.Row)
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
				if (this.application.indexIntFamily.Contains(indexPath.Row) == true)
				{
					if (indexPath.Row != this.application.indexTableFamily.Row || indexPath.Row == this.application.indexTableFamily.Row)
					{
						return UITableViewCellEditingStyle.Delete;
					}
				}
				this.application.cellFamily.AccessoryView = null;
				this.application.cellFamily.EditingAccessoryView = null;

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

				if (this.application.localizedTextFamily.Count((string arg) => arg.ToString() == this.familyDict[indexPath.Row]) >= 1)
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

				else if (this.application.localizedTextFamily.Count((string arg) => arg.ToString() == this.familyDict[indexPath.Row]) == 0)
				{
					this.application.localizedTextFamily.Add(this.familyDict[indexPath.Row]);
					this.application.frenchTextFamily.Add(this.familyTranslatedDict[indexPath.Row]);
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

					this.application.indexIntFamily.Add(indexPath.Row);

					this.application.indexTableFamily = index;

					this.TableView.ReloadData();
				}
			}
			else if (editingStyle == UITableViewCellEditingStyle.Delete)
			{
				if (this.application.indexIntFamily.Contains(indexPath.Row) == true)
				{
					this.application.localizedTextFamily.RemoveAll((string obj) => obj == this.familyDict[indexPath.Row]);
					this.application.indexIntFamily.RemoveAll((int obj) => obj == indexPath.Row);

					this.application.favourites.RemoveAll((string obj) => obj == this.familyDict[indexPath.Row]);
					this.application.favouritesFrench.RemoveAll((string obj) => obj == this.familyTranslatedDict[indexPath.Row]);

					if (this.application.indexTableFamily.Row == indexPath.Row || this.application.indexTableFamily.Row != indexPath.Row)
					{
						this.application.cellFamily.AccessoryView = null;
						this.application.cellFamily.EditingAccessoryView = null;
						this.TableView.ReloadData();
					}

					if (this.application.localizedTextFamily.Count == 1)
					{
						this.TableView.ReloadData();
					}

					if (this.application.cellFamily.EditingStyle == UITableViewCellEditingStyle.Insert)
					{
						this.application.cellFamily.AccessoryView = null;
						this.application.cellFamily.EditingAccessoryView = null;
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
					this.application.cellFamily.AccessoryView = null;
					this.application.cellFamily.EditingAccessoryView = null;
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
			return this.familyDict.Count;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{

			BatteryMonitor AI = new BatteryMonitor();

			switch (indexPath.Row)
			{
				case 0:
					AI.frenchPhraseBookAI(familyTranslatedDict[0]);
					break;
				case 1:
					AI.frenchPhraseBookAI(familyTranslatedDict[1]);
					break;
				case 2:
					AI.frenchPhraseBookAI(familyTranslatedDict[2]);
					break;
				case 3:
					AI.frenchPhraseBookAI(familyTranslatedDict[3]);
					break;
				case 4:
					AI.frenchPhraseBookAI(familyTranslatedDict[4]);
					break;
				case 5:
					AI.frenchPhraseBookAI(familyTranslatedDict[5]);
					break;
				case 6:
					AI.frenchPhraseBookAI(familyTranslatedDict[6]);
					break;
				case 7:
					AI.frenchPhraseBookAI(familyTranslatedDict[7]);
					break;
				case 8:
					AI.frenchPhraseBookAI(familyTranslatedDict[8]);
					break;
				case 9:
					AI.frenchPhraseBookAI(familyTranslatedDict[9]);
					break;
				case 10:
					AI.frenchPhraseBookAI(familyTranslatedDict[10]);
					break;
				case 11:
					AI.frenchPhraseBookAI(familyTranslatedDict[11]);
					break;
				case 12:
					AI.frenchPhraseBookAI(familyTranslatedDict[12]);
					break;
				case 13:
					AI.frenchPhraseBookAI(familyTranslatedDict[13]);
					break;
				case 14:
					AI.frenchPhraseBookAI(familyTranslatedDict[14]);
					break;
				case 15:
					AI.frenchPhraseBookAI(familyTranslatedDict[15]);
					break;
				case 16:
					AI.frenchPhraseBookAI(familyTranslatedDict[16]);
					break;
				case 17:
					AI.frenchPhraseBookAI(familyTranslatedDict[17]);
					break;
				case 18:
					AI.frenchPhraseBookAI(familyTranslatedDict[18]);
					break;
				case 19:
					AI.frenchPhraseBookAI(familyTranslatedDict[19]);
					break;
				case 20:
					AI.frenchPhraseBookAI(familyTranslatedDict[20]);
					break;
				case 21:
					AI.frenchPhraseBookAI(familyTranslatedDict[21]);
					break;
				case 22:
					AI.frenchPhraseBookAI(familyTranslatedDict[22]);
					break;
				case 23:
					AI.frenchPhraseBookAI(familyTranslatedDict[23]);
					break;
				case 24:
					AI.frenchPhraseBookAI(familyTranslatedDict[24]);
					break;
				case 25:
					AI.frenchPhraseBookAI(familyTranslatedDict[25]);
					break;
				case 26:
					AI.frenchPhraseBookAI(familyTranslatedDict[26]);
					break;
				case 27:
					AI.frenchPhraseBookAI(familyTranslatedDict[27]);
					break;
				case 28:
					AI.frenchPhraseBookAI(familyTranslatedDict[28]);
					break;
				case 29:
					AI.frenchPhraseBookAI(familyTranslatedDict[29]);
					break;
				case 30:
					AI.frenchPhraseBookAI(familyTranslatedDict[30]);
					break;
				case 31:
					AI.frenchPhraseBookAI(familyTranslatedDict[31]);
					break;
				case 32:
					AI.frenchPhraseBookAI(familyTranslatedDict[32]);
					break;
				case 33:
					AI.frenchPhraseBookAI(familyTranslatedDict[33]);
					break;
				case 34:
					AI.frenchPhraseBookAI(familyTranslatedDict[34]);
					break;
				case 35:
					AI.frenchPhraseBookAI(familyTranslatedDict[35]);
					break;
				case 36:
					AI.frenchPhraseBookAI(familyTranslatedDict[36]);
					break;
				case 37:
					AI.frenchPhraseBookAI(familyTranslatedDict[37]);
					break;
				case 38:
					AI.frenchPhraseBookAI(familyTranslatedDict[38]);
					break;
				case 39:
					AI.frenchPhraseBookAI(familyTranslatedDict[39]);
					break;
				case 40:
					AI.frenchPhraseBookAI(familyTranslatedDict[40]);
					break;
				case 41:
					AI.frenchPhraseBookAI(familyTranslatedDict[41]);
					break;
				case 42:
					AI.frenchPhraseBookAI(familyTranslatedDict[42]);
					break;
				case 43:
					AI.frenchPhraseBookAI(familyTranslatedDict[43]);
					break;
				case 44:
					AI.frenchPhraseBookAI(familyTranslatedDict[44]);
					break;
				case 45:
					AI.frenchPhraseBookAI(familyTranslatedDict[45]);
					break;
				case 46:
					AI.frenchPhraseBookAI(familyTranslatedDict[46]);
					break;
				case 47:
					AI.frenchPhraseBookAI(familyTranslatedDict[47]);
					break;
				case 48:
					AI.frenchPhraseBookAI(familyTranslatedDict[48]);
					break;
				case 49:
					AI.frenchPhraseBookAI(familyTranslatedDict[49]);
					break;
				case 50:
					AI.frenchPhraseBookAI(familyTranslatedDict[50]);
					break;
				case 51:
					AI.frenchPhraseBookAI(familyTranslatedDict[51]);
					break;
				case 52:
					AI.frenchPhraseBookAI(familyTranslatedDict[52]);
					break;
				case 53:
					AI.frenchPhraseBookAI(familyTranslatedDict[53]);
					break;
				case 54:
					AI.frenchPhraseBookAI(familyTranslatedDict[54]);
					break;
				case 55:
					AI.frenchPhraseBookAI(familyTranslatedDict[55]);
					break;
				case 56:
					AI.frenchPhraseBookAI(familyTranslatedDict[56]);
					break;
				case 57:
					AI.frenchPhraseBookAI(familyTranslatedDict[57]);
					break;
				case 58:
					AI.frenchPhraseBookAI(familyTranslatedDict[58]);
					break;
				case 59:
					AI.frenchPhraseBookAI(familyTranslatedDict[59]);
					break;
				case 60:
					AI.frenchPhraseBookAI(familyTranslatedDict[60]);
					break;
				case 61:
					AI.frenchPhraseBookAI(familyTranslatedDict[61]);
					break;
				case 62:
					AI.frenchPhraseBookAI(familyTranslatedDict[62]);
					break;
				case 63:
					AI.frenchPhraseBookAI(familyTranslatedDict[63]);
					break;
				case 64:
					AI.frenchPhraseBookAI(familyTranslatedDict[64]);
					break;
				case 65:
					AI.frenchPhraseBookAI(familyTranslatedDict[65]);
					break;
				case 66:
					AI.frenchPhraseBookAI(familyTranslatedDict[66]);
					break;
				case 67:
					AI.frenchPhraseBookAI(familyTranslatedDict[67]);
					break;
				case 68:
					AI.frenchPhraseBookAI(familyTranslatedDict[68]);
					break;
				case 69:
					AI.frenchPhraseBookAI(familyTranslatedDict[69]);
					break;
				case 70:
					AI.frenchPhraseBookAI(familyTranslatedDict[70]);
					break;

				default:
					Console.WriteLine("No key selected");
					break;
			}
			tableView.DeselectRow(indexPath, true);
		}
	}
}
