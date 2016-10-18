using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

using System.Linq;

using UIKit;
using CoreFoundation;
using Foundation;


namespace FrenchPhraseBook
{
	public partial class SchoolController : UITableViewController
	{
		public SchoolController(IntPtr handle) : base(handle)
		{
		}

		public SchoolController() { }

		//Dictionary of family phrases 
		public List<string> schoolDict = new List<string>{
			{"Keep your focus"}, {"What are you studying?"},{"I am studying engineering, IT and business"},{"Are you studying at Highschool or at College?"},{"I am studying at...(Place)"},{"Highschool"},{"University"},{"College"},{"Business School"},{"Mathematics"},{"English"},{"Arts"},{"Media Studies"},
			{"Who is your teacher?"},{"Is your professor good at teaching?"},{"When do you get your degree?"},{"How long does your degree take to do?"},{"Professor"},{"Teacher"},{"Lecture theatre"},
			{"Lecture room"},{"Pencil"},{"Pen"},{"Calculator"},{"Textbook"},{"Exercise book"},{"Computer"},{"Drawing tools"},{"Higher education"},{"Tertiary"},{"Education"},
			{"Novel"},{"Programming"},{"Software"},{"Study room"},{"Learning"},{"Student Lobby"},{"Office Tools"},{"Engineering faculty"},{"Arts faculty"},{"(Field)...Faculty"},
			{"Lab room"},{"Tutorial"},{"Quantum mechanics"},{"(Field)...Mechanics"},{"Which is your best subject?"},{"Where is your next lecture?"},{"I have to ask you a question?"},{"Can you help me with...(Subject)?"},{"I can help you with...(Subject)"},{"We're having a group study today"},
			{"Do you want to study with us?"},{"Can we meet for a tutorial soon?"},{"Where do you want to meet?"},{"There are 50 students in my class"},{"My classroom is near...(Place)"},{"General assembly"},{"School assembly"},{"Parent teacher interviews"},
			{"I'm very skilled at maths"},{"I'm very good at...(Skill)"},{"Dictionary"},{"I will study hard today"},{"I'm learning French"},{"I'm studying Spanish"},{"I'm studying...(Subject"},{"My exam is in 3 hours"},
			{"My exam lasts for 3 hours"},{"That test was easy"},{"That test was hard"},{"Focus on your degree"},
		};

		Dictionary<int, string> schoolTranslatedDict = new Dictionary<int, string>{

			{0,"Gardez votre attention"},{1,"Qu'est-ce que vous étudiez?"},{2,"J'étudie l'ingénierie , l'informatique et les entreprises"},{3,"Vous étudiez à Highschool ou au collège?"},
			{4,"Je suis étudiant à ..?"},{5,"École secondaire"},{6,"Université"},{7,"Université"},{8,"École de commerce"},{9,"Mathématiques"},
			{10,"Anglais"},{11,"lettres"},{12,"Etudes des medias"},{13,"Qui est votre enseignant?"},{14,"Est votre professeur bien à l'enseignement?"},{15,"Quand vous obtenez votre diplôme?"},
			{16,"Combien de temps votre diplôme prendre à faire?"},{17,"Professeur"},{18,"Professeur"},{19,"Salle de conférence"},{20,"Salle de lecture"},
			{21,"Crayon"},{22,"Stylo"},{23,"Calculatrice"},{24,"Cahier de texte"},{25,"Cahier d'exercices"},{26,"Ordinateur"},{27,"Outils de dessin"},
			{28,"L'enseignement supérieur"},{29,"Tertiaire"},{30,"Éducation"},{31,"Roman"},{32,"La programmation"},{33,"Logiciel"},{34,"Salle d'étude"},{35,"Apprentissage"},{36,"Lobby étudiant"},{37,"Outils de bureau"},
			{38,"Faculté d'ingénierie"},{39,"Arts faculté"},{40,"...La faculté"},{41,"chambre Lab"},{42,"Tutorial"},{43,"Mécanique quantique"},{44,"...Mécanique"},{45,"Quel est votre meilleur sujet?"},
			{46,"Où est votre prochaine leçon?"},{47,"Je dois vous poser une question?"},{48,"Pouvez-vous me aider avec..."},{49,"Je peux vous aider à ..."},{50,"Nous allons avoir une étude de groupe aujourd'hui"},
			{51,"Voulez-vous étudier avec nous?"},{52,"Pouvons-nous rencontrer pour un tutoriel bientôt?"},{53,"Où voulez-vous que l'on se rencontre?"},{54,"Il y a 50 élèves dans ma classe"},{55,"Ma classe est proche..."},
			{56,"Assemblée générale"},{57,"School assembly"},{58,"entrevues avec les enseignants du parent"},{59,"Je suis très habile en maths"},{60,"Je suis très bien à..."},{61,"dictionnaire"},{62,"Je vais étudier dur aujourd'hui"},{63,"J'apprends le français"},
			{64,"Je suis étudiant en espagnol"},{65,"J'étudie..."},{66,"Mon examen est en 3 heures"},{67,"Mon examen dure 3 heures"},{68,"Ce test a été facile"},{69,"Ce test a été dur"},{70,"Focus sur votre diplôme"}
		};

		public string schoolString = "Family";

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
				AI.frenchPhraseBookAI(this.schoolDict[index.Row]);
			}
			else if (this.application.tabBarID == 0)
			{
				if (this.application.localizedTextSchool.Count == 1)
				{
					this.application.cellSchool.AccessoryView = this.favouritesIndicator;
					this.application.cellSchool.EditingAccessoryView = this.favouritesIndicator;
					this.TableView.ReloadData();
				}
			}

			if (this.application.localizedTextSchool.Count == 1)
			{
				this.application.cellSchool.AccessoryView = this.favouritesIndicator;
				this.application.cellSchool.EditingAccessoryView = this.favouritesIndicator;
				this.TableView.ReloadData();
			}
		}

		public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
		{
			return 80.0f;
		}

		public override void ViewDidLoad()
		{
			this.application.schoolControl = this;
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




			UIBarButtonItem optionButton = new UIBarButtonItem("<\ud83d\udcda", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
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

			this.NavigationItem.Title = "School & Education";

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
			UITableViewCell BusinessCell = tableView.DequeueReusableCell(this.schoolString);

			if (BusinessCell == null)
			{
				BusinessCell = new UITableViewCell(UITableViewCellStyle.Subtitle, this.schoolString);
			}


			this.application.cellSchool = BusinessCell;

			this.favouritesIndicator = new UILabel();
			this.favouritesIndicator.Text = "\ud83d\udc9d";
			//this.favouritesIndicator.Text = "⭐";
			this.favouritesIndicator.MinimumFontSize = 24.0f;
			this.favouritesIndicator.AdjustsFontSizeToFitWidth = true;
			this.favouritesIndicator.Frame = new CoreGraphics.CGRect(0, 20, 40, 40);

			BusinessCell.TextLabel.Text = this.schoolDict[indexPath.Row];
			BusinessCell.DetailTextLabel.Text = this.schoolTranslatedDict[indexPath.Row];
			BusinessCell.DetailTextLabel.TextColor = UIColor.Gray;
			BusinessCell.DetailTextLabel.Font = UIFont.SystemFontOfSize(12.5f);

			if (BusinessCell.EditingStyle == UITableViewCellEditingStyle.Insert)
			{
				this.application.cellSchool.AccessoryView = null;
				this.application.cellSchool.EditingAccessoryView = null;
			}

			if (this.application.localizedTextSchool.Count == 0)
			{
				this.application.cellSchool.AccessoryView = null;
				this.application.cellSchool.EditingAccessoryView = null;
			}

			else if (this.application.localizedTextSchool.Count >= 1)
			{
				if (this.application.localizedTextSchool.Count == 1)
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
				if (indexPath.Row == this.application.indexTableSchool.Row)
				{
					Console.WriteLine("index chosen");
					BusinessCell.AccessoryView = this.favouritesIndicator;
					BusinessCell.EditingAccessoryView = this.favouritesIndicator;
				}

				else if (this.application.indexIntSchool.Contains(indexPath.Row) == false)
				{
					Console.WriteLine("Index is not found");
					BusinessCell.AccessoryView = null;
					BusinessCell.EditingAccessoryView = null;
				}

				else if (this.application.indexIntSchool.Contains(indexPath.Row) == true)
				{
					Console.WriteLine("Index table chosen _ 1");
					if (indexPath.Row != this.application.indexTableSchool.Row)
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
				if (this.application.indexIntSchool.Contains(indexPath.Row) == true)
				{
					if (indexPath.Row != this.application.indexTableSchool.Row || indexPath.Row == this.application.indexTableSchool.Row)
					{
						return UITableViewCellEditingStyle.Delete;
					}
				}
				this.application.cellSchool.AccessoryView = null;
				this.application.cellSchool.EditingAccessoryView = null;

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

				if (this.application.localizedTextSchool.Count((string arg) => arg.ToString() == this.schoolDict[indexPath.Row]) >= 1)
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

				else if (this.application.localizedTextSchool.Count((string arg) => arg.ToString() == this.schoolDict[indexPath.Row]) == 0)
				{
					this.application.localizedTextSchool.Add(this.schoolDict[indexPath.Row]);
					this.application.frenchTextSchool.Add(this.schoolTranslatedDict[indexPath.Row]);
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

					this.application.indexIntSchool.Add(indexPath.Row);

					this.application.indexTableSchool = index;

					this.TableView.ReloadData();
				}
			}
			else if (editingStyle == UITableViewCellEditingStyle.Delete)
			{
				if (this.application.indexIntSchool.Contains(indexPath.Row) == true)
				{
					this.application.localizedTextSchool.RemoveAll((string obj) => obj == this.schoolDict[indexPath.Row]);
					this.application.indexIntSchool.RemoveAll((int obj) => obj == indexPath.Row);

					this.application.favourites.RemoveAll((string obj) => obj == this.schoolDict[indexPath.Row]);
					this.application.favouritesFrench.RemoveAll((string obj) => obj == this.schoolTranslatedDict[indexPath.Row]);

					if (this.application.indexTableSchool.Row == indexPath.Row || this.application.indexTableSchool.Row != indexPath.Row)
					{
						this.application.cellSchool.AccessoryView = null;
						this.application.cellSchool.EditingAccessoryView = null;
						this.TableView.ReloadData();
					}

					if (this.application.localizedTextSchool.Count == 1)
					{
						this.TableView.ReloadData();
					}

					if (this.application.cellSchool.EditingStyle == UITableViewCellEditingStyle.Insert)
					{
						this.application.cellSchool.AccessoryView = null;
						this.application.cellSchool.EditingAccessoryView = null;
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
					this.application.cellSchool.AccessoryView = null;
					this.application.cellSchool.EditingAccessoryView = null;
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
			return this.schoolDict.Count;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{

			BatteryMonitor AI = new BatteryMonitor();

			switch (indexPath.Row)
			{
				case 0:
					AI.frenchPhraseBookAI(schoolTranslatedDict[0]);
					break;
				case 1:
					AI.frenchPhraseBookAI(schoolTranslatedDict[1]);
					break;
				case 2:
					AI.frenchPhraseBookAI(schoolTranslatedDict[2]);
					break;
				case 3:
					AI.frenchPhraseBookAI(schoolTranslatedDict[3]);
					break;
				case 4:
					AI.frenchPhraseBookAI(schoolTranslatedDict[4]);
					break;
				case 5:
					AI.frenchPhraseBookAI(schoolTranslatedDict[5]);
					break;
				case 6:
					AI.frenchPhraseBookAI(schoolTranslatedDict[6]);
					break;
				case 7:
					AI.frenchPhraseBookAI(schoolTranslatedDict[7]);
					break;
				case 8:
					AI.frenchPhraseBookAI(schoolTranslatedDict[8]);
					break;
				case 9:
					AI.frenchPhraseBookAI(schoolTranslatedDict[9]);
					break;
				case 10:
					AI.frenchPhraseBookAI(schoolTranslatedDict[10]);
					break;
				case 11:
					AI.frenchPhraseBookAI(schoolTranslatedDict[11]);
					break;
				case 12:
					AI.frenchPhraseBookAI(schoolTranslatedDict[12]);
					break;
				case 13:
					AI.frenchPhraseBookAI(schoolTranslatedDict[13]);
					break;
				case 14:
					AI.frenchPhraseBookAI(schoolTranslatedDict[14]);
					break;
				case 15:
					AI.frenchPhraseBookAI(schoolTranslatedDict[15]);
					break;
				case 16:
					AI.frenchPhraseBookAI(schoolTranslatedDict[16]);
					break;
				case 17:
					AI.frenchPhraseBookAI(schoolTranslatedDict[17]);
					break;
				case 18:
					AI.frenchPhraseBookAI(schoolTranslatedDict[18]);
					break;
				case 19:
					AI.frenchPhraseBookAI(schoolTranslatedDict[19]);
					break;
				case 20:
					AI.frenchPhraseBookAI(schoolTranslatedDict[20]);
					break;
				case 21:
					AI.frenchPhraseBookAI(schoolTranslatedDict[21]);
					break;
				case 22:
					AI.frenchPhraseBookAI(schoolTranslatedDict[22]);
					break;
				case 23:
					AI.frenchPhraseBookAI(schoolTranslatedDict[23]);
					break;
				case 24:
					AI.frenchPhraseBookAI(schoolTranslatedDict[24]);
					break;
				case 25:
					AI.frenchPhraseBookAI(schoolTranslatedDict[25]);
					break;
				case 26:
					AI.frenchPhraseBookAI(schoolTranslatedDict[26]);
					break;
				case 27:
					AI.frenchPhraseBookAI(schoolTranslatedDict[27]);
					break;
				case 28:
					AI.frenchPhraseBookAI(schoolTranslatedDict[28]);
					break;
				case 29:
					AI.frenchPhraseBookAI(schoolTranslatedDict[29]);
					break;
				case 30:
					AI.frenchPhraseBookAI(schoolTranslatedDict[30]);
					break;
				case 31:
					AI.frenchPhraseBookAI(schoolTranslatedDict[31]);
					break;
				case 32:
					AI.frenchPhraseBookAI(schoolTranslatedDict[32]);
					break;
				case 33:
					AI.frenchPhraseBookAI(schoolTranslatedDict[33]);
					break;
				case 34:
					AI.frenchPhraseBookAI(schoolTranslatedDict[34]);
					break;
				case 35:
					AI.frenchPhraseBookAI(schoolTranslatedDict[35]);
					break;
				case 36:
					AI.frenchPhraseBookAI(schoolTranslatedDict[36]);
					break;
				case 37:
					AI.frenchPhraseBookAI(schoolTranslatedDict[37]);
					break;
				case 38:
					AI.frenchPhraseBookAI(schoolTranslatedDict[38]);
					break;
				case 39:
					AI.frenchPhraseBookAI(schoolTranslatedDict[39]);
					break;
				case 40:
					AI.frenchPhraseBookAI(schoolTranslatedDict[40]);
					break;
				case 41:
					AI.frenchPhraseBookAI(schoolTranslatedDict[41]);
					break;
				case 42:
					AI.frenchPhraseBookAI(schoolTranslatedDict[42]);
					break;
				case 43:
					AI.frenchPhraseBookAI(schoolTranslatedDict[43]);
					break;
				case 44:
					AI.frenchPhraseBookAI(schoolTranslatedDict[44]);
					break;
				case 45:
					AI.frenchPhraseBookAI(schoolTranslatedDict[45]);
					break;
				case 46:
					AI.frenchPhraseBookAI(schoolTranslatedDict[46]);
					break;
				case 47:
					AI.frenchPhraseBookAI(schoolTranslatedDict[47]);
					break;
				case 48:
					AI.frenchPhraseBookAI(schoolTranslatedDict[48]);
					break;
				case 49:
					AI.frenchPhraseBookAI(schoolTranslatedDict[49]);
					break;
				case 50:
					AI.frenchPhraseBookAI(schoolTranslatedDict[50]);
					break;
				case 51:
					AI.frenchPhraseBookAI(schoolTranslatedDict[51]);
					break;
				case 52:
					AI.frenchPhraseBookAI(schoolTranslatedDict[52]);
					break;
				case 53:
					AI.frenchPhraseBookAI(schoolTranslatedDict[53]);
					break;
				case 54:
					AI.frenchPhraseBookAI(schoolTranslatedDict[54]);
					break;
				case 55:
					AI.frenchPhraseBookAI(schoolTranslatedDict[55]);
					break;
				case 56:
					AI.frenchPhraseBookAI(schoolTranslatedDict[56]);
					break;
				case 57:
					AI.frenchPhraseBookAI(schoolTranslatedDict[57]);
					break;
				case 58:
					AI.frenchPhraseBookAI(schoolTranslatedDict[58]);
					break;
				case 59:
					AI.frenchPhraseBookAI(schoolTranslatedDict[59]);
					break;
				case 60:
					AI.frenchPhraseBookAI(schoolTranslatedDict[60]);
					break;
				case 61:
					AI.frenchPhraseBookAI(schoolTranslatedDict[61]);
					break;
				case 62:
					AI.frenchPhraseBookAI(schoolTranslatedDict[62]);
					break;
				case 63:
					AI.frenchPhraseBookAI(schoolTranslatedDict[63]);
					break;
				case 64:
					AI.frenchPhraseBookAI(schoolTranslatedDict[64]);
					break;
				case 65:
					AI.frenchPhraseBookAI(schoolTranslatedDict[65]);
					break;
				case 66:
					AI.frenchPhraseBookAI(schoolTranslatedDict[66]);
					break;
				case 67:
					AI.frenchPhraseBookAI(schoolTranslatedDict[67]);
					break;
				case 68:
					AI.frenchPhraseBookAI(schoolTranslatedDict[68]);
					break;
				case 69:
					AI.frenchPhraseBookAI(schoolTranslatedDict[69]);
					break;
				case 70:
					AI.frenchPhraseBookAI(schoolTranslatedDict[70]);
					break;

				default:
					Console.WriteLine("No key selected");
					break;
			}
			tableView.DeselectRow(indexPath, true);
		}
	}
}
