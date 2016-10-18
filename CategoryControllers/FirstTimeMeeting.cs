using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

using UIKit;
using CoreFoundation;
using Foundation;
using AudioToolbox;

namespace FrenchPhraseBook
{
	public partial class FirstTimeMeeting : UITableViewController
	{

		public List<string> firstTimeDict = new List<string>() {
			{"Yes"},{"No"},{"Do you understand me?"}, {"I don't understand"},
			{"I understand perfectly"}, {"Thank you. I'm good"}, {"I need your help"}, {"Please"},
			{"I'm sorry"}, {"Please repeat yourself"}, {"Can you speak a little slower?"}, {"No, thank you"}, {"Congratulations"},
			{"I feel sorry for you"}, {"It's fine"}, {"I don't know about that"}, {"I don't like this"},
			{"I like this very much"}, {"You're welcome"}, {"I understand that..."}, {"I think that..."},
			{"I want some..."}, {"I like..."},{"Can I borrow your phone"}, {"Are you sure about this?"}, {"Can you speak German?"}, {"What does that mean?"}, {"How do you pronounce this sentence?"},
			{"No, thanks! I'm good"},{"No, that's wrong"},{"That's right"}, {"No problem"},{"Help!"},{"Who?"},{"What?"},{"How many?"},{"How much? (amount)"},
			{"Why?"},{"Why not?"},{"Which?"},{"Where?"},{"When?"},{"What month is your birthday?"}, {"What are doing later?"}, {"Where are you going?"},{"I'm going to..."},
			{"What's this?"},{"Are you feeling alright?"}, {"Don't forget..."},{"Excuse me"}, {"Mind yourself"},{"Where can I find the elevator?"}, {"It's been a long time"},{"You want to talk?"},
			{"I love your car"},{"Do you go here often"},{"What do you do for a living?"}, {"I have never seen you here before"},{"Nice to see you"},{"Which country are you from?"},
			{"My name is..."},{"I am 60 years old"},{"I am 20 years old"},{"I work as an engineer"},{"What is your name?"},{"Are you studying right now?"}, {"Who are you?"}, {"Hello"}, {"Nice to meet you"}

		};

		//place the translated dictionary here
		Dictionary<int, string> firstTimeTranslated = new Dictionary<int, string>() {
			{0,"Oui"}, {1,"Non"}, {2,"Me comprenez-vous?"}, {3,"Je ne comprends pas"}, {4,"Je comprends parfaitement"},{5,"Je vous remercie. CA va bien"},{6,"j'ai besoin de ton aide"},{7,"S'il vous plaît"},{8,"Je suis désolé"},{9,"S'il vous plaît vous répétez"},
			{10,"Pouvez-vous parler un peu plus lent ?"},{11,"Non, merci"},{12,"Félicitations à vous"},{13,"Je suis désolé pour toi"},{14,"C'est bien"},{15,"Je ne sais pas"},{16,"Je n'aime pas cette"},{17,"J'aime beaucoup cela"},{18,"De rien"},{19,"Je comprends que..."},
			{20,"Je pense que..."},{21,"J'en veux..."},{22,"J'aime..."},{23,"Puis-je emprunter votre téléphone"}, {24,"Es-tu sûr de ça?"},{25,"Parlez-vous allemand?"},{26,"Qu'est-ce que ça veut dire?"},{27,"Comment prononcez-vous cette phrase ?"},{28,"Non merci! CA va bien"},{29,"Non c'est faux"},
			{30,"C'est vrai"},{31,"Pas de problème"},{32,"Aidez-moi!"},{33,"Qui?"},{34,"Quelle?"},
			{35,"Combien?"},{36,"Combien?"},{37,"Pourquoi?"},{38,"Pourquoi pas?"},{39,"Lequel?"},{40,"Où?"},{41,"Quand?"},{42,"Quel mois est votre anniversaire ?"},{43,"Que font plus tard?"},{44,"Où allez-vous?"},{45,"Je vais..."},{46,"Qu'est-ce que c'est ça?"},{47,"Vous sentez-vous bien?"},
			{48,"Ne pas oublier ..."},{49,"Excusez-moi"}, {50,"Faites attention à vous"},{51,"Où puis -je trouver l'ascenseur?"},{52,"Cela fait longtemps"},{53,"Vous voulez parler?"},{54,"J'adore votre voiture"},{55,"Allez-vous souvent ici"},{56,"Comment gagnez-vous votre vie?"},{57,"Je ne vous ai jamais vu ici avant"},{58,"Ravi de vous voir"},
			{59,"De quel pays êtes-vous?"},{60,"Mon nom est..."},{61,"Je suis âgé de 60 ans"},{62,"j'ai 20 ans"},{63,"Je travaille comme un ingénieur"},{64,"Je travaille comme un ingénieur"},{65,"Est-ce que vous étudiez en ce moment?"},{66,"Qui es-tu?"},{67,"Bonjour"}, {68,"Ravi de vous rencontrer"}
		};


		public FirstTimeMeeting(IntPtr handle) : base(handle)
		{
		}

		public FirstTimeMeeting()
		{
		}
		string firstTimeID = "firstTimeCellID";

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
				AI.frenchPhraseBookAI(this.firstTimeTranslated[index.Row]);
			}
			else if (this.application.tabBarID == 0)
			{
				if (this.application.localizedTextMeeting.Count == 1)
				{
					this.application.cellFirstMeeting.AccessoryView = this.favouritesIndicator;
					this.application.cellFirstMeeting.EditingAccessoryView = this.favouritesIndicator;
					this.TableView.ReloadData();
				}
			}

			if (this.application.localizedTextMeeting.Count == 1)
			{
				this.application.cellFirstMeeting.AccessoryView = this.favouritesIndicator;
				this.application.cellFirstMeeting.EditingAccessoryView = this.favouritesIndicator;
				this.TableView.ReloadData();
			}
		}

		public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
		{
			return 80.0f;
		}

		public override void ViewDidLoad()
		{
			this.application.firstTimeMeetingControl = this;
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




			UIBarButtonItem optionButton = new UIBarButtonItem("<\ud83d\udc65", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
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

			this.NavigationItem.Title = "First Time Meeting";

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
			UITableViewCell BusinessCell = tableView.DequeueReusableCell(this.firstTimeID);

			if (BusinessCell == null)
			{
				BusinessCell = new UITableViewCell(UITableViewCellStyle.Subtitle, this.firstTimeID);
			}


			this.application.cellFirstMeeting = BusinessCell;

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
				this.application.cellFirstMeeting.AccessoryView = null;
				this.application.cellFirstMeeting.EditingAccessoryView = null;
			}

			if (this.application.localizedTextMeeting.Count == 0)
			{
				this.application.cellFirstMeeting.AccessoryView = null;
				this.application.cellFirstMeeting.EditingAccessoryView = null;
			}

			else if (this.application.localizedTextMeeting.Count >= 1)
			{
				if (this.application.localizedTextMeeting.Count == 1)
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
				if (indexPath.Row == this.application.indexTableFirstTimeMeeting.Row)
				{
					Console.WriteLine("index chosen");
					BusinessCell.AccessoryView = this.favouritesIndicator;
					BusinessCell.EditingAccessoryView = this.favouritesIndicator;
				}

				else if (this.application.indexIntFirstTimeMeeting.Contains(indexPath.Row) == false)
				{
					Console.WriteLine("Index is not found");
					BusinessCell.AccessoryView = null;
					BusinessCell.EditingAccessoryView = null;
				}

				else if (this.application.indexIntFirstTimeMeeting.Contains(indexPath.Row) == true)
				{
					Console.WriteLine("Index table chosen _ 1");
					if (indexPath.Row != this.application.indexTableFirstTimeMeeting.Row)
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
				if (this.application.indexIntFirstTimeMeeting.Contains(indexPath.Row) == true)
				{
					if (indexPath.Row != this.application.indexTableFirstTimeMeeting.Row || indexPath.Row == this.application.indexTableFirstTimeMeeting.Row)
					{
						return UITableViewCellEditingStyle.Delete;
					}
				}
				this.application.cellFirstMeeting.AccessoryView = null;
				this.application.cellFirstMeeting.EditingAccessoryView = null;

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

				if (this.application.localizedTextMeeting.Count((string arg) => arg.ToString() == this.firstTimeDict[indexPath.Row]) >= 1)
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

				else if (this.application.localizedTextMeeting.Count((string arg) => arg.ToString() == this.firstTimeDict[indexPath.Row]) == 0)
				{
					this.application.localizedTextMeeting.Add(this.firstTimeDict[indexPath.Row]);
					this.application.frenchTextMeeting.Add(this.firstTimeTranslated[indexPath.Row]);
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

					this.application.indexIntFirstTimeMeeting.Add(indexPath.Row);

					this.application.indexTableFirstTimeMeeting = index;

					this.TableView.ReloadData();
				}
			}
			else if (editingStyle == UITableViewCellEditingStyle.Delete)
			{
				if (this.application.indexIntFirstTimeMeeting.Contains(indexPath.Row) == true)
				{
					this.application.localizedTextMeeting.RemoveAll((string obj) => obj == this.firstTimeDict[indexPath.Row]);
					this.application.indexIntFirstTimeMeeting.RemoveAll((int obj) => obj == indexPath.Row);

					this.application.favourites.RemoveAll((string obj) => obj == this.firstTimeDict[indexPath.Row]);
					this.application.favouritesFrench.RemoveAll((string obj) => obj == this.firstTimeTranslated[indexPath.Row]);

					if (this.application.indexTableFirstTimeMeeting.Row == indexPath.Row || this.application.indexTableFirstTimeMeeting.Row != indexPath.Row)
					{
						this.application.cellFirstMeeting.AccessoryView = null;
						this.application.cellFirstMeeting.EditingAccessoryView = null;
						this.TableView.ReloadData();
					}

					if (this.application.localizedTextMeeting.Count == 1)
					{
						this.TableView.ReloadData();
					}

					if (this.application.cellFirstMeeting.EditingStyle == UITableViewCellEditingStyle.Insert)
					{
						this.application.cellFirstMeeting.AccessoryView = null;
						this.application.cellFirstMeeting.EditingAccessoryView = null;
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
					this.application.cellFirstMeeting.AccessoryView = null;
					this.application.cellFirstMeeting.EditingAccessoryView = null;
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
				case 45:
					AI.frenchPhraseBookAI(firstTimeTranslated[45]);
					break;
				case 46:
					AI.frenchPhraseBookAI(firstTimeTranslated[46]);
					break;
				case 47:
					AI.frenchPhraseBookAI(firstTimeTranslated[47]);
					break;
				case 48:
					AI.frenchPhraseBookAI(firstTimeTranslated[48]);
					break;
				case 49:
					AI.frenchPhraseBookAI(firstTimeTranslated[49]);
					break;
				case 50:
					AI.frenchPhraseBookAI(firstTimeTranslated[50]);
					break;
				case 51:
					AI.frenchPhraseBookAI(firstTimeTranslated[51]);
					break;
				case 52:
					AI.frenchPhraseBookAI(firstTimeTranslated[52]);
					break;
				case 53:
					AI.frenchPhraseBookAI(firstTimeTranslated[53]);
					break;
				case 54:
					AI.frenchPhraseBookAI(firstTimeTranslated[54]);
					break;
				case 55:
					AI.frenchPhraseBookAI(firstTimeTranslated[55]);
					break;
				case 56:
					AI.frenchPhraseBookAI(firstTimeTranslated[56]);
					break;
				case 57:
					AI.frenchPhraseBookAI(firstTimeTranslated[57]);
					break;
				case 58:
					AI.frenchPhraseBookAI(firstTimeTranslated[58]);
					break;
				case 59:
					AI.frenchPhraseBookAI(firstTimeTranslated[59]);
					break;
				case 60:
					AI.frenchPhraseBookAI(firstTimeTranslated[60]);
					break;
				case 61:
					AI.frenchPhraseBookAI(firstTimeTranslated[61]);
					break;
				case 62:
					AI.frenchPhraseBookAI(firstTimeTranslated[62]);
					break;
				case 63:
					AI.frenchPhraseBookAI(firstTimeTranslated[63]);
					break;
				case 64:
					AI.frenchPhraseBookAI(firstTimeTranslated[64]);
					break;
				case 65:
					AI.frenchPhraseBookAI(firstTimeTranslated[65]);
					break;
				case 66:
					AI.frenchPhraseBookAI(firstTimeTranslated[66]);
					break;
				case 67:
					AI.frenchPhraseBookAI(firstTimeTranslated[67]);
					break;
				default:
					Console.WriteLine("No key selected");
					break;
			}
			tableView.DeselectRow(indexPath, true);

		}
	}
}
