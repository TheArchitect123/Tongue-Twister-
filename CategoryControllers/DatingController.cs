using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

using UIKit;
using CoreFoundation;
using Foundation;

namespace FrenchPhraseBook
{
	public partial class DatingController : UITableViewController
	{
		//dictionary of categories
		public List<string> datingDict = new List<string>(){
			{"I love you"}, {"Nice to finally meet you"}, {"Can you give us a menu please"}, {"Do you have any tables?"},
			{"Do you like your spaghetti?"},{"What do you do?"}, {"This is my first date"}, {"Do you visit this place often?"},
			{"We'll do this again tommorrow"}, {"I'll pick you up at 8:00"}, {"How have you been lately?"}, {"How did you start your career?"},
			{"What hobbies do you have?"}, {"Are you interested in Art and culture?"}, {"I am interested in science and technology"}, {"So how about you?"},
			{"Do you enjoy your work"}, {"I work 12 hours a day as a software engineer"}, {"Do you travel often?"}, {"I have traveled to paris, singapore, and madrid"},
			{"That sounds amazing!"},{"I had allot of fun tonight"},{"You want to come upstairs?"},{"I know this great sushi bar near here"},{"We definitely should do this again"},
			{"I'll call you"},{"What did you do in paris?"}, {"I travelled to the eiffel tower"}, {"Can I have the bill please, waiter?"}, {"I'll take the bill"}, {"Really, it's ok, I'll pay the bill"},
			{"The thing I am most proud of..."}, {"I am proud of my job and life"}, {"Will you marry me?"}, {"Do you plan to have a family?"}, {"Are you enjoying your meal?"}, {"Let's order some wine"},
			{"The music here is nice"}, {"I am 30 years old"}, {"It's my birthday today"}, {"Take care of yourself"}, {"See you next time"}, {"What do you plan to do in the future"}, {"Do you read books"},
			{"What's your favourite movie?"},{"What's the boldest thing you have ever done?"},{"Who was your worst date?"},{"Who was your best date?"},{"Do you like cats or dogs?"},{"Do you have any pets?"},{"I have two pet turtles"},
			{"My favourite novel is..."},{"My favourite TV show is..."},
		};

		Dictionary<int, string> datingTranslatedDict = new Dictionary<int, string>{
			{0,"je t'aime"}, {1,"Ravi de vous rencontrer enfin"}, {2,"Pouvez-vous nous donner un menu s'il vous plaît"}, {3,"Avez-vous des tables?"},{4,"Aimez-vous vos spaghettis?"},{5,"Que faire?"},
			{6,"Ceci est mon premier rendez-vous"},{7,"Avez- vous visitez ce lieu souvent?"},{8,"Nous allons faire cela à nouveau tommorrow"},{9,"Je vais vous chercher à 8:00"},{10,"Comment vas-tu ces derniers temps?"},{11,"Comment avez-vous commencé votre carrière?"},
			{12,"Quels sont vos passe-temps?"},{13,"Êtes-vous intéressé à l'art et à la culture?"},{14,"Je suis intéressé par la science et de la technologie"},{15,"Alors que diriez- vous?"},{16,"Aimez-vous votre travail"},{17,"Je travaille 12 heures par jour comme un ingénieur logiciel"},
			{18,"Voyagez-vous souvent?"},{19,"J'ai voyagé à Paris, Singapour et madrid"},{20,"Cela semble incroyable!"},
			{21,"Je l'avais allouer de ce soir amusant"},{22,"Vous voulez venir à l'étage?"},{23,"Je sais que ce grand bar à sushis près d'ici"},
			{24,"Nous devrions certainement le faire à nouveau"},{25,"je t'appellerai"},{26,"Qu'avez-vous fait à paris?"},{27,"Je suis allé à la tour eiffel"},{28,"Puis-je avoir le projet de loi s'il vous plaît, serveur?"},
			{29,"Je vais prendre le projet de loi"},{30,"Vraiment, il est ok, je vais payer la facture"},{31,"La chose dont je suis le plus fier..."},{32,"Je suis fier de mon travail et de la vie"},{33,"Veux-tu m'épouser?"},{34,"Prévoyez- vous d'avoir une famille?"},
			{35,"Est-ce que vous appréciez votre repas?"},{36,"Nous allons commander du vin"},{37,"La musique ici est agréable"},{38,"j'ai 30 ans"},{39,"C'est mon anniversaire aujourd'hui"},{40,"Prenez soin de vous"},{41,"À la prochaine"},{42,"Que comptez-vous faire à l'avenir"},
			{43,"Lisez-vous des livres"},{44,"Quel est votre film préféré?"},{45,"Quelle est la chose la plus audacieuse que vous avez fait?"},{46,"Qui était votre pire jour?"},{47,"Qui était votre meilleur jour?"},{48,"Aimez-vous les chats ou les chiens?"},{49,"Avez-vous des animaux domestiques?"},
			{50,"J'ai deux tortues"},{51,"Mon roman préféré est..."},{52,"Mon émission préférée est..."}
		};

		public string datingID = "dating";

		public DatingController(IntPtr handle) : base(handle)
		{
		}

		public DatingController() { }

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
			this.TableView.ReloadData();
			NSIndexPath index = NSIndexPath.FromRowSection(this.application.index, 0);

			Console.WriteLine("Index: " + index.Row);
			if (this.application.tabBarID == 1)
			{
				Console.WriteLine("wtf?");
				this.TableView.ScrollToRow(index, UITableViewScrollPosition.Top, true);
				this.TableView.SelectRow(index, true, UITableViewScrollPosition.Top);

				BatteryMonitor AI = new BatteryMonitor();
				AI.frenchPhraseBookAI(this.datingTranslatedDict[index.Row]);
			}
			else if (this.application.tabBarID == 0)
			{
				if (this.application.localizedText.Count == 1)
				{
					this.application.cellDating.AccessoryView = this.favouritesIndicator;
					this.application.cellDating.EditingAccessoryView = this.favouritesIndicator;
					this.TableView.ReloadData();
				}
			}

			if (this.application.localizedText.Count == 1)
			{
				this.application.cellDating.AccessoryView = this.favouritesIndicator;
				this.application.cellDating.EditingAccessoryView = this.favouritesIndicator;
				this.TableView.ReloadData();
			}
		}

		public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
		{
			return 80.0f;
		}

		public override void ViewDidLoad()
		{
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




			UIBarButtonItem optionButton = new UIBarButtonItem("<\ud83d\udc8b", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
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

			this.NavigationItem.Title = "Dating";

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
			UITableViewCell BusinessCell = tableView.DequeueReusableCell(this.datingID);

			if (BusinessCell == null)
			{
				BusinessCell = new UITableViewCell(UITableViewCellStyle.Subtitle, this.datingID);
			}


			this.application.cellDating = BusinessCell;

			this.favouritesIndicator = new UILabel();
			this.favouritesIndicator.Text = "\ud83d\udc9d";
			//this.favouritesIndicator.Text = "⭐";
			this.favouritesIndicator.MinimumFontSize = 24.0f;
			this.favouritesIndicator.AdjustsFontSizeToFitWidth = true;
			this.favouritesIndicator.Frame = new CoreGraphics.CGRect(0, 20, 40, 40);

			BusinessCell.TextLabel.Text = this.datingDict[indexPath.Row];
			BusinessCell.DetailTextLabel.Text = this.datingTranslatedDict[indexPath.Row];
			BusinessCell.DetailTextLabel.TextColor = UIColor.Gray;
			BusinessCell.DetailTextLabel.Font = UIFont.SystemFontOfSize(12.5f);

			if (BusinessCell.EditingStyle == UITableViewCellEditingStyle.Insert)
			{
				this.application.cellDating.AccessoryView = null;
				this.application.cellDating.EditingAccessoryView = null;
			}

			if (this.application.localizedTextDating.Count == 0)
			{
				this.application.cellDating.AccessoryView = null;
				this.application.cellDating.EditingAccessoryView = null;
			}

			else if (this.application.localizedTextDating.Count >= 1)
			{
				if (this.application.localizedTextDating.Count == 1)
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
				if (indexPath.Row == this.application.indexTableDating.Row)
				{
					Console.WriteLine("index chosen");
					BusinessCell.AccessoryView = this.favouritesIndicator;
					BusinessCell.EditingAccessoryView = this.favouritesIndicator;
				}

				else if (this.application.indexIntDating.Contains(indexPath.Row) == false)
				{
					Console.WriteLine("Index is not found");
					BusinessCell.AccessoryView = null;
					BusinessCell.EditingAccessoryView = null;
				}

				else if (this.application.indexIntDating.Contains(indexPath.Row) == true)
				{
					Console.WriteLine("Index table chosen _ 1");
					if (indexPath.Row != this.application.indexTableDating.Row)
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
				if (this.application.indexIntDating.Contains(indexPath.Row) == true)
				{
					if (indexPath.Row != this.application.indexTableDating.Row || indexPath.Row == this.application.indexTableDating.Row)
					{
						return UITableViewCellEditingStyle.Delete;
					}
				}
				this.application.cellDating.AccessoryView = null;
				this.application.cellDating.EditingAccessoryView = null;

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

				if (this.application.localizedTextDating.Count((string arg) => arg.ToString() == this.datingDict[indexPath.Row]) >= 1)
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

				else if (this.application.localizedTextDating.Count((string arg) => arg.ToString() == this.datingDict[indexPath.Row]) == 0)
				{
					this.application.localizedTextDating.Add(this.datingDict[indexPath.Row]);
					this.application.frenchTextDating.Add(this.datingTranslatedDict[indexPath.Row]);
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

					this.application.indexIntDating.Add(indexPath.Row);

					this.application.indexTableDating = index;

					this.TableView.ReloadData();
				}
			}
			else if (editingStyle == UITableViewCellEditingStyle.Delete)
			{
				if (this.application.indexIntDating.Contains(indexPath.Row) == true)
				{
					this.application.localizedTextDating.RemoveAll((string obj) => obj == this.datingDict[indexPath.Row]);
					this.application.indexIntDating.RemoveAll((int obj) => obj == indexPath.Row);

					this.application.favourites.RemoveAll((string obj) => obj == this.datingDict[indexPath.Row]);
					this.application.favouritesFrench.RemoveAll((string obj) => obj == this.datingTranslatedDict[indexPath.Row]);

					if (this.application.indexTableDating.Row == indexPath.Row || this.application.indexTableDating.Row != indexPath.Row)
					{
						this.application.cellDating.AccessoryView = null;
						this.application.cellDating.EditingAccessoryView = null;
						this.TableView.ReloadData();
					}

					if (this.application.localizedTextDating.Count == 1)
					{
						this.TableView.ReloadData();
					}

					if (this.application.cellDating.EditingStyle == UITableViewCellEditingStyle.Insert)
					{
						this.application.cellDating.AccessoryView = null;
						this.application.cellDating.EditingAccessoryView = null;
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
					this.application.cellDating.AccessoryView = null;
					this.application.cellDating.EditingAccessoryView = null;
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
			return this.datingDict.Count;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			BatteryMonitor AI = new BatteryMonitor();
			switch (indexPath.Row)
			{
				case 0:
					AI.frenchPhraseBookAI(datingTranslatedDict[0]);
					break;
				case 1:
					AI.frenchPhraseBookAI(datingTranslatedDict[1]);
					break;
				case 2:
					AI.frenchPhraseBookAI(datingTranslatedDict[2]);
					break;
				case 3:
					AI.frenchPhraseBookAI(datingTranslatedDict[3]);
					break;
				case 4:
					AI.frenchPhraseBookAI(datingTranslatedDict[4]);
					break;
				case 5:
					AI.frenchPhraseBookAI(datingTranslatedDict[5]);
					break;
				case 6:
					AI.frenchPhraseBookAI(datingTranslatedDict[6]);
					break;
				case 7:
					AI.frenchPhraseBookAI(datingTranslatedDict[7]);
					break;
				case 8:
					AI.frenchPhraseBookAI(datingTranslatedDict[8]);
					break;
				case 9:
					AI.frenchPhraseBookAI(datingTranslatedDict[9]);
					break;
				case 10:
					AI.frenchPhraseBookAI(datingTranslatedDict[10]);
					break;
				case 11:
					AI.frenchPhraseBookAI(datingTranslatedDict[11]);
					break;
				case 12:
					AI.frenchPhraseBookAI(datingTranslatedDict[12]);
					break;
				case 13:
					AI.frenchPhraseBookAI(datingTranslatedDict[13]);
					break;
				case 14:
					AI.frenchPhraseBookAI(datingTranslatedDict[14]);
					break;
				case 15:
					AI.frenchPhraseBookAI(datingTranslatedDict[15]);
					break;
				case 16:
					AI.frenchPhraseBookAI(datingTranslatedDict[16]);
					break;
				case 17:
					AI.frenchPhraseBookAI(datingTranslatedDict[17]);
					break;
				case 18:
					AI.frenchPhraseBookAI(datingTranslatedDict[18]);
					break;
				case 19:
					AI.frenchPhraseBookAI(datingTranslatedDict[19]);
					break;
				case 20:
					AI.frenchPhraseBookAI(datingTranslatedDict[20]);
					break;
				case 21:
					AI.frenchPhraseBookAI(datingTranslatedDict[21]);
					break;
				case 22:
					AI.frenchPhraseBookAI(datingTranslatedDict[22]);
					break;
				case 23:
					AI.frenchPhraseBookAI(datingTranslatedDict[23]);
					break;
				case 24:
					AI.frenchPhraseBookAI(datingTranslatedDict[24]);
					break;
				case 25:
					AI.frenchPhraseBookAI(datingTranslatedDict[25]);
					break;
				case 26:
					AI.frenchPhraseBookAI(datingTranslatedDict[26]);
					break;
				case 27:
					AI.frenchPhraseBookAI(datingTranslatedDict[27]);
					break;
				case 28:
					AI.frenchPhraseBookAI(datingTranslatedDict[28]);
					break;
				case 29:
					AI.frenchPhraseBookAI(datingTranslatedDict[29]);
					break;
				case 30:
					AI.frenchPhraseBookAI(datingTranslatedDict[30]);
					break;
				case 31:
					AI.frenchPhraseBookAI(datingTranslatedDict[31]);
					break;
				case 32:
					AI.frenchPhraseBookAI(datingTranslatedDict[32]);
					break;
				case 33:
					AI.frenchPhraseBookAI(datingTranslatedDict[33]);
					break;
				case 34:
					AI.frenchPhraseBookAI(datingTranslatedDict[34]);
					break;
				case 35:
					AI.frenchPhraseBookAI(datingTranslatedDict[35]);
					break;
				case 36:
					AI.frenchPhraseBookAI(datingTranslatedDict[36]);
					break;
				case 37:
					AI.frenchPhraseBookAI(datingTranslatedDict[37]);
					break;
				case 38:
					AI.frenchPhraseBookAI(datingTranslatedDict[38]);
					break;
				case 39:
					AI.frenchPhraseBookAI(datingTranslatedDict[39]);
					break;
				case 40:
					AI.frenchPhraseBookAI(datingTranslatedDict[40]);
					break;
				case 41:
					AI.frenchPhraseBookAI(datingTranslatedDict[41]);
					break;
				case 42:
					AI.frenchPhraseBookAI(datingTranslatedDict[42]);
					break;
				case 43:
					AI.frenchPhraseBookAI(datingTranslatedDict[43]);
					break;
				case 44:
					AI.frenchPhraseBookAI(datingTranslatedDict[44]);
					break;
				case 45:
					AI.frenchPhraseBookAI(datingTranslatedDict[45]);
					break;
				case 46:
					AI.frenchPhraseBookAI(datingTranslatedDict[46]);
					break;
				case 47:
					AI.frenchPhraseBookAI(datingTranslatedDict[47]);
					break;
				case 48:
					AI.frenchPhraseBookAI(datingTranslatedDict[48]);
					break;
				case 49:
					AI.frenchPhraseBookAI(datingTranslatedDict[50]);
					break;
				case 51:
					AI.frenchPhraseBookAI(datingTranslatedDict[51]);
					break;
				case 52:
					AI.frenchPhraseBookAI(datingTranslatedDict[52]);
					break;
				default:
					Console.WriteLine("No key selected");
					break;
			}
			tableView.DeselectRow(indexPath, true);
		}

	}
}
