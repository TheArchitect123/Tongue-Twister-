using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

using System.Linq;
using UIKit;
using CoreFoundation;
using Foundation;

namespace FrenchPhraseBook
{
	public partial class WorkController : UITableViewController
	{
		public WorkController(IntPtr handle) : base(handle)
		{
		}

		public List<string> firstTimeDict = new List<string>() {
			{"How was work?"},{"I love this job!"},{"My boss can be a pain to work with sometimes"},{"My boss is fun to work with at times"},
			{"My colleagues really know how to work together"},{"Teamwork is needed to finish this task"},{"We are approaching a deadline here people"},
			{"I work at an office"},{"I work at a golf course"},{"I work at a...(Place)"},
			{"Desk job"},{"I hate this job"},{"Our server is down!"},{"Your new assignment is..."},{"Management"},{"Programmer"},{"Your job is to..."},{"The machine's main function is to..."},
			{"I've hired some new workers"},{"I've fired somebody today"},
			{"You're fired!"},{"You're hired!"},{"When can you come in for an interview?"},{"I work from morning to evening, everyday"},{"Is your job easy?"},{"Is your job hard?"},
			{"My job is easy!"},{"My job is hard!"},{"Her job is easy!"},{"Her job is hard!"},
			{"We're renovating this building today"},{"Our construction project has been halted"},{"Our stocks are up today"},{"Our stocks are down today"},
			{"This project was allot of work"},{"This project was easy to do"},{"We have a new team member to join our group"},{"I'm the boss here!"},{"Where is...(Name)...today?"},{"I thought of this myself"},
			{"We're getting some amazing work done here!"},{"My job pays well"},{"My job does not pay well"},{"I want a raise!"},{"I want a promotion!"},{"Stock broker"},{"Cocoa Engineer"},{"Carpenter"},{"Driver"},{"Pilot"},
			{"He is a air traffic controller"},{"She is a pilot"},{"When does our shift end?"},{"How much more work is there left to do?"},{"Where did you get your degree?"},{"Where did you get your masters?"},{"I take 30 minutes to drive to work, everyday"},
			{"Car manufacturer"},{"Chief Executive Officer"},{"Is your first day?"},
			{"I've worked here for over 10 years"},{"I'm new here"},{"This is her first day"},{"We work together here"},{"My project is nearly finished"},
			{"My financial advisor told me..."},{"I've talked to my accountant..."},{"They fire people left and right"},{"There is so much job opportunity here"},{"I use the train to get to work"},
			{"I walk to the office"}
		};

		//place the translated dictionary here
		Dictionary<int, string> firstTimeTranslated = new Dictionary<int, string>() {
			{0,"Comment était le travail?"},{1,"J'adore ce travail"},{2,"Mon patron peut être une douleur à travailler avec parfois"},{3,"Mon patron est amusant de travailler avec parfois"},{4,"Mes collègues savent vraiment comment travailler ensemble"},
			{5,"Le travail d'équipe est nécessaire pour terminer cette tâche"},{6,"Nous approchons de la date limite ici, les gens"},{7,"Je travaille dans un bureau"},{8,"Je travaille à un terrain de golf"},{9,"Je travaille dans un ..."},
			{10,"Emploi de bureau"},{11,"Je déteste ce travail"},{12,"Notre serveur est en panne!"},{13,"Votre nouvelle affectation est ..."},{14,"La gestion"},{15,"Programmeur"},{16,"Votre travail consiste à ..."},{17,"La fonction principale de la machine est de..."},{18,"J'ai embauché quelques nouveaux travailleurs"},
			{19,"Je l'ai viré quelqu'un aujourd'hui"},{20,"Vous êtes viré!"},{21,"Nous vous offrons le poste!"},{22,"Quand pouvez-vous venir pour une interview?"},{23,"Je travaille du matin au soir , tous les jours"},{24,"Est-ce que ton travail est facile?"},{25,"Votre travail est dur?"},{26,"Mon travail est facile!"},
			{27,"Mon travail est dur!"},{28,"Son travail est facile!"},{29,"Son travail est dur!"},{30,"Nous rénovons ce bâtiment aujourd'hui"},{31,"Notre projet de construction a été interrompue"},{32,"Nos stocks sont aujourd'hui"},{33,"Nos stocks sont bas aujourd'hui"},{34,"Ce projet a été attribuer de travail"},
			{35,"Ce projet a été facile à faire"},{36,"Nous avons un nouveau membre de l'équipe à se joindre à notre groupe"},{37,"Je suis le patron ici!"},{38,"Où se trouve...aujourd'hui?"},{39,"Je pensais à moi-même"},
			{40,"Nous obtenons un travail extraordinaire fait ici!"},{41,"Mon travail paie bien"},{42,"Mon travail ne paie pas bien"},{43,"Je veux une augmentation!"},{44,"Je veux une promotion!"},{45,"Stock courtier"},{46,"Cocoa Ingénieur"},{47,"Charpentier"},{48,"Chauffeur"},{49,"Pilote"},
			{50,"Il est un contrôleur de la circulation aérienne"},{51,"Elle est un pilote"},{52,"Quand se termine notre équipe?"},{53,"Comment beaucoup plus de travail est il à faire?"},{54,"Où avez-vous obtenu votre diplôme?"},{55,"Où avez-vous vos maîtres?"},{56,"Je prends 30 minutes pour aller au travail, tous les jours"},
			{57,"constructeur automobile"},{58,"Directeur Général"},{59,"Est votre premier jour?"},
			{60,"I've worked here for over 10 years"},{61,"Je suis nouveau ici"},{62,"Ceci est son premier jour"},{63,"Nous travaillons ensemble ici"},{64,"Mon projet est presque terminé"},{65,"Mon conseiller financier m'a dit ..."},
			{66,"J'ai parlé à mon comptable ..."},{67,"Ils tirent les gens à gauche et à droite"},{68,"Il y a tellement de possibilités d'emploi ici"},{69,"J'utilise le train pour se rendre au travail"},
			{70,"Je marche au bureau"}
		};
		string workID = "firstTimeCellID";


		public WorkController()
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
				AI.frenchPhraseBookAI(this.firstTimeDict[index.Row]);
			}
			else if (this.application.tabBarID == 0)
			{
				if (this.application.localizedWork.Count == 1)
				{
					this.application.cellWork.AccessoryView = this.favouritesIndicator;
					this.application.cellWork.EditingAccessoryView = this.favouritesIndicator;
					this.TableView.ReloadData();
				}
			}

			if (this.application.localizedWork.Count == 1)
			{
				this.application.cellWork.AccessoryView = this.favouritesIndicator;
				this.application.cellWork.EditingAccessoryView = this.favouritesIndicator;
				this.TableView.ReloadData();
			}
		}

		public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
		{
			return 80.0f;
		}

		public override void ViewDidLoad()
		{
			this.application.workControl = this;
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




			UIBarButtonItem optionButton = new UIBarButtonItem("<\ud83d\udc54", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
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

			this.NavigationItem.Title = "Work";

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
			UITableViewCell BusinessCell = tableView.DequeueReusableCell(this.workID);

			if (BusinessCell == null)
			{
				BusinessCell = new UITableViewCell(UITableViewCellStyle.Subtitle, this.workID);
			}


			this.application.cellWork = BusinessCell;

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
				this.application.cellWork.AccessoryView = null;
				this.application.cellWork.EditingAccessoryView = null;
			}

			if (this.application.localizedWork.Count == 0)
			{
				this.application.cellWork.AccessoryView = null;
				this.application.cellWork.EditingAccessoryView = null;
			}

			else if (this.application.localizedWork.Count >= 1)
			{
				if (this.application.localizedWork.Count == 1)
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
				if (indexPath.Row == this.application.indexTableWork.Row)
				{
					Console.WriteLine("index chosen");
					BusinessCell.AccessoryView = this.favouritesIndicator;
					BusinessCell.EditingAccessoryView = this.favouritesIndicator;
				}

				else if (this.application.indexIntWork.Contains(indexPath.Row) == false)
				{
					Console.WriteLine("Index is not found");
					BusinessCell.AccessoryView = null;
					BusinessCell.EditingAccessoryView = null;
				}

				else if (this.application.indexIntWork.Contains(indexPath.Row) == true)
				{
					Console.WriteLine("Index table chosen _ 1");
					if (indexPath.Row != this.application.indexTableWork.Row)
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
				if (this.application.indexIntWork.Contains(indexPath.Row) == true)
				{
					if (indexPath.Row != this.application.indexTableWork.Row || indexPath.Row == this.application.indexTableWork.Row)
					{
						return UITableViewCellEditingStyle.Delete;
					}
				}
				this.application.cellWork.AccessoryView = null;
				this.application.cellWork.EditingAccessoryView = null;

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

				if (this.application.localizedWork.Count((string arg) => arg.ToString() == this.firstTimeDict[indexPath.Row]) >= 1)
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

				else if (this.application.localizedWork.Count((string arg) => arg.ToString() == this.firstTimeDict[indexPath.Row]) == 0)
				{
					this.application.localizedWork.Add(this.firstTimeDict[indexPath.Row]);
					this.application.frenchTextWork.Add(this.firstTimeTranslated[indexPath.Row]);
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

					this.application.indexIntWork.Add(indexPath.Row);

					this.application.indexTableWork = index;

					this.TableView.ReloadData();
				}
			}
			else if (editingStyle == UITableViewCellEditingStyle.Delete)
			{
				if (this.application.indexIntWork.Contains(indexPath.Row) == true)
				{
					this.application.localizedWork.RemoveAll((string obj) => obj == this.firstTimeDict[indexPath.Row]);
					this.application.indexIntWork.RemoveAll((int obj) => obj == indexPath.Row);

					this.application.favourites.RemoveAll((string obj) => obj == this.firstTimeDict[indexPath.Row]);
					this.application.favouritesFrench.RemoveAll((string obj) => obj == this.firstTimeTranslated[indexPath.Row]);

					if (this.application.indexTableWork.Row == indexPath.Row || this.application.indexTableWork.Row != indexPath.Row)
					{
						this.application.cellWork.AccessoryView = null;
						this.application.cellWork.EditingAccessoryView = null;
						this.TableView.ReloadData();
					}

					if (this.application.localizedWork.Count == 1)
					{
						this.TableView.ReloadData();
					}

					if (this.application.cellWork.EditingStyle == UITableViewCellEditingStyle.Insert)
					{
						this.application.cellWork.AccessoryView = null;
						this.application.cellWork.EditingAccessoryView = null;
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
					this.application.cellWork.AccessoryView = null;
					this.application.cellWork.EditingAccessoryView = null;
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