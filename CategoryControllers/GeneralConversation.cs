
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

using System.Linq;

using UIKit;
using Foundation;
using CoreFoundation;


namespace FrenchPhraseBook
{
	public partial class GeneralConversation : UITableViewController
	{
		public List<string> generalDict = new List<string>(){
			{"Who are you?"},{"What's your name?"},{"Are you feeling alright?"},{"You look sick"},{"I need to lie down for a while"},
			{"See you tommorrow"},{"Where where you yesterday?"},{"See you tonight"},{"How was your day?"},
			{"How was work?"},{"Are you staying with us?"},{"I heard today that..."},{"I saw..."},{"Tommorrow I'm going to see..."},{"Next week is my birthday"},
			{"Congratulations on your business"},{"Well done on your..."},{"Are you going to..."},{"Do you want to go out?"},{"I'm going out"},{"I'm going to..."},
			{"Can you speak English?"},{"Can you speak...(language)"},{"Wait a minute"},{"Wait for...(Time)"},{"When is your job interview?"},{"Do you like your job"},
			{"It's been a long time since we saw each other"},{"How have you been in the recent months?"},{"What's your favourite TV show?"},
			{"Are you studying?"},{"What are you studying?"},{"I am studying engineering"},{"I am studying business"},{"I am studying..."},{"Nice to meet you"},{"You did not call me"},
			{"It's been too long"},{"I love this music"},{"I love this..."},{"I feel good"},{"I'm healthy"},{"I'm from America"},{"I'm from Brazil"},{"I'm from...(country)"},
			{"I am 40 years old"},{"Can you speak louder?"},{"Can you whisper?"},{"It's very loud in here"},{"What?"},{"How much?"},{"Why?"},{"Who?"},{"Where?"},{"When?"},{"Which was it?"},
			{"Why did you do that?"},{"Who is she?"},{"Who is he?"},{"When will you get here?"},{"What time is it?"},{"Why are you here?"},
		};

		Dictionary<int, string> generalTranslatedDict = new Dictionary<int, string> {
			{0,"Qui es-tu?"}, {1,"Quel est ton nom?"}, {2,"Vous sentez-vous bien?"},{3,"Tu semble malade"},{4,"Je dois allonger pendant un certain temps"},{5,"À demain"},
			{6,"Où où vous hier?"},{7,"À ce soir"},{8,"Comment était ta journée ?"},{9,"Comment était le travail?"},{10,"Vous restez avec nous ?"},{11,"J'ai entendu aujourd'hui que ..."},
			{12,"Je vis..."},{13,"Tommorrow Je vais voir ..."},{14,"La semaine prochaine est mon anniversaire"},{15,"Félicitations pour votre entreprise"},{16,"Eh bien fait sur votre ..."},{17,"Allez-vous ..."},{18,"Voulez-vous sortir?"},{19,"je sors"},{20,"Je vais..."},
			{21,"Pouvez-vous parler anglais?"},{22,"Pouvez vous parler..."},{23,"Attends une minute"},{24,"Attendre..."},{25,"Quand votre entretien d'embauche ?"},{26,"Aimez-vous votre travail?"},{27,"Cela fait longtemps que nous nous sommes vus"},
			{28,"Comment avez-vous été dans les derniers mois?"},{29,"Quelle est votre émission préférée?"},{30,"Étudiez-vous?"},{31,"Qu'est-ce que vous étudiez?"},{32,"J'étudie l'ingénierie"},{33,"J'étudie entreprise"},{34,"J'étudie..."},{35,"Ravi de vous rencontrer"},{36,"Tu ne m'as pas appelé"},
			{37,"Ça fait trop longtemps"},{38,"J'aime cette musique"},{39,"J'aime cela..."},{40,"je me sens bien"},{41,"je suis en bonne santé"},{42,"Je viens d'Amérique"},{43,"Je viens du Brésil"},
			{44,"Je viens de..."},{45,"j'ai 40 ans"},{46,"Pouvez-vous parler plus fort?"},{47,"Pouvez-vous murmurer?"},{48,"Il est très fort ici"},{49,"Quelle?"},{50,"Combien?"},{51,"Pourquoi?"},{52,"Qui?"},{53,"Où?"},{54,"Quand?"},{55,"Qui était-il?"},{56,"Pourquoi fais-tu ça?"},
			{57,"Qui est-elle?"},{58,"Qui est-il?"},{59,"Quand allez-vous y arriver?"},{60,"Quelle heure est-il?"},
			{61,"Pourquoi es-tu ici?"}
		};

		public string generalID = "generalConversation";

		public GeneralConversation(IntPtr handle) : base(handle)
		{
		}

		public GeneralConversation() { }

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
				AI.frenchPhraseBookAI(this.generalDict[index.Row]);
			}
			else if (this.application.tabBarID == 0)
			{
				if (this.application.localizedTextGeneralConversation.Count == 1)
				{
					this.application.cellGeneralConversation.AccessoryView = this.favouritesIndicator;
					this.application.cellGeneralConversation.EditingAccessoryView = this.favouritesIndicator;
					this.TableView.ReloadData();
				}
			}

			if (this.application.localizedTextGeneralConversation.Count == 1)
			{
				this.application.cellGeneralConversation.AccessoryView = this.favouritesIndicator;
				this.application.cellGeneralConversation.EditingAccessoryView = this.favouritesIndicator;
				this.TableView.ReloadData();
			}
		}

		public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
		{
			return 80.0f;
		}

		public override void ViewDidLoad()
		{
			this.application.generalControl = this;
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




			UIBarButtonItem optionButton = new UIBarButtonItem("<\ud83d\udde3", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
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

			this.NavigationItem.Title = "General Conversation";

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
			UITableViewCell BusinessCell = tableView.DequeueReusableCell(this.generalID);

			if (BusinessCell == null)
			{
				BusinessCell = new UITableViewCell(UITableViewCellStyle.Subtitle, this.generalID);
			}


			this.application.cellGeneralConversation = BusinessCell;

			this.favouritesIndicator = new UILabel();
			this.favouritesIndicator.Text = "\ud83d\udc9d";
			//this.favouritesIndicator.Text = "⭐";
			this.favouritesIndicator.MinimumFontSize = 24.0f;
			this.favouritesIndicator.AdjustsFontSizeToFitWidth = true;
			this.favouritesIndicator.Frame = new CoreGraphics.CGRect(0, 20, 40, 40);

			BusinessCell.TextLabel.Text = this.generalDict[indexPath.Row];
			BusinessCell.DetailTextLabel.Text = this.generalTranslatedDict[indexPath.Row];
			BusinessCell.DetailTextLabel.TextColor = UIColor.Gray;
			BusinessCell.DetailTextLabel.Font = UIFont.SystemFontOfSize(12.5f);

			if (BusinessCell.EditingStyle == UITableViewCellEditingStyle.Insert)
			{
				this.application.cellGeneralConversation.AccessoryView = null;
				this.application.cellGeneralConversation.EditingAccessoryView = null;
			}

			if (this.application.localizedTextGeneralConversation.Count == 0)
			{
				this.application.cellGeneralConversation.AccessoryView = null;
				this.application.cellGeneralConversation.EditingAccessoryView = null;
			}

			else if (this.application.localizedTextGeneralConversation.Count >= 1)
			{
				if (this.application.localizedTextGeneralConversation.Count == 1)
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
				if (indexPath.Row == this.application.indexTableGeneralConversation.Row)
				{
					Console.WriteLine("index chosen");
					BusinessCell.AccessoryView = this.favouritesIndicator;
					BusinessCell.EditingAccessoryView = this.favouritesIndicator;
				}

				else if (this.application.indexIntGeneralConversation.Contains(indexPath.Row) == false)
				{
					Console.WriteLine("Index is not found");
					BusinessCell.AccessoryView = null;
					BusinessCell.EditingAccessoryView = null;
				}

				else if (this.application.indexIntGeneralConversation.Contains(indexPath.Row) == true)
				{
					Console.WriteLine("Index table chosen _ 1");
					if (indexPath.Row != this.application.indexTableGeneralConversation.Row)
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
				if (this.application.indexIntGeneralConversation.Contains(indexPath.Row) == true)
				{
					if (indexPath.Row != this.application.indexTableGeneralConversation.Row || indexPath.Row == this.application.indexTableGeneralConversation.Row)
					{
						return UITableViewCellEditingStyle.Delete;
					}
				}
				this.application.cellGeneralConversation.AccessoryView = null;
				this.application.cellGeneralConversation.EditingAccessoryView = null;

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

				if (this.application.localizedTextGeneralConversation.Count((string arg) => arg.ToString() == this.generalDict[indexPath.Row]) >= 1)
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

				else if (this.application.localizedTextGeneralConversation.Count((string arg) => arg.ToString() == this.generalDict[indexPath.Row]) == 0)
				{
					this.application.localizedTextGeneralConversation.Add(this.generalDict[indexPath.Row]);
					this.application.frenchTextGeneralConversation.Add(this.generalTranslatedDict[indexPath.Row]);
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

					this.application.indexIntGeneralConversation.Add(indexPath.Row);

					this.application.indexTableGeneralConversation = index;

					this.TableView.ReloadData();
				}
			}
			else if (editingStyle == UITableViewCellEditingStyle.Delete)
			{
				if (this.application.indexIntGeneralConversation.Contains(indexPath.Row) == true)
				{
					this.application.localizedTextGeneralConversation.RemoveAll((string obj) => obj == this.generalDict[indexPath.Row]);
					this.application.indexIntGeneralConversation.RemoveAll((int obj) => obj == indexPath.Row);

					this.application.favourites.RemoveAll((string obj) => obj == this.generalDict[indexPath.Row]);
					this.application.favouritesFrench.RemoveAll((string obj) => obj == this.generalTranslatedDict[indexPath.Row]);

					if (this.application.indexTableGeneralConversation.Row == indexPath.Row || this.application.indexTableGeneralConversation.Row != indexPath.Row)
					{
						this.application.cellGeneralConversation.AccessoryView = null;
						this.application.cellGeneralConversation.EditingAccessoryView = null;
						this.TableView.ReloadData();
					}

					if (this.application.localizedTextGeneralConversation.Count == 1)
					{
						this.TableView.ReloadData();
					}

					if (this.application.cellGeneralConversation.EditingStyle == UITableViewCellEditingStyle.Insert)
					{
						this.application.cellGeneralConversation.AccessoryView = null;
						this.application.cellGeneralConversation.EditingAccessoryView = null;
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
					this.application.cellGeneralConversation.AccessoryView = null;
					this.application.cellGeneralConversation.EditingAccessoryView = null;
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
			return this.generalDict.Count;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			BatteryMonitor AI = new BatteryMonitor();
			switch (indexPath.Row)
			{
				case 0:
					AI.frenchPhraseBookAI(generalTranslatedDict[0]);
					break;
				case 1:
					AI.frenchPhraseBookAI(generalTranslatedDict[1]);
					break;
				case 2:
					AI.frenchPhraseBookAI(generalTranslatedDict[2]);
					break;
				case 3:
					AI.frenchPhraseBookAI(generalTranslatedDict[3]);
					break;
				case 4:
					AI.frenchPhraseBookAI(generalTranslatedDict[4]);
					break;
				case 5:
					AI.frenchPhraseBookAI(generalTranslatedDict[5]);
					break;
				case 6:
					AI.frenchPhraseBookAI(generalTranslatedDict[6]);
					break;
				case 7:
					AI.frenchPhraseBookAI(generalTranslatedDict[7]);
					break;
				case 8:
					AI.frenchPhraseBookAI(generalTranslatedDict[8]);
					break;
				case 9:
					AI.frenchPhraseBookAI(generalTranslatedDict[9]);
					break;
				case 10:
					AI.frenchPhraseBookAI(generalTranslatedDict[10]);
					break;
				case 11:
					AI.frenchPhraseBookAI(generalTranslatedDict[11]);
					break;
				case 12:
					AI.frenchPhraseBookAI(generalTranslatedDict[12]);
					break;
				case 13:
					AI.frenchPhraseBookAI(generalTranslatedDict[13]);
					break;
				case 14:
					AI.frenchPhraseBookAI(generalTranslatedDict[14]);
					break;
				case 15:
					AI.frenchPhraseBookAI(generalTranslatedDict[15]);
					break;
				case 16:
					AI.frenchPhraseBookAI(generalTranslatedDict[16]);
					break;
				case 17:
					AI.frenchPhraseBookAI(generalTranslatedDict[17]);
					break;
				case 18:
					AI.frenchPhraseBookAI(generalTranslatedDict[18]);
					break;
				case 19:
					AI.frenchPhraseBookAI(generalTranslatedDict[19]);
					break;
				case 20:
					AI.frenchPhraseBookAI(generalTranslatedDict[20]);
					break;
				case 21:
					AI.frenchPhraseBookAI(generalTranslatedDict[21]);
					break;
				case 22:
					AI.frenchPhraseBookAI(generalTranslatedDict[22]);
					break;
				case 23:
					AI.frenchPhraseBookAI(generalTranslatedDict[23]);
					break;
				case 24:
					AI.frenchPhraseBookAI(generalTranslatedDict[24]);
					break;
				case 25:
					AI.frenchPhraseBookAI(generalTranslatedDict[25]);
					break;
				case 26:
					AI.frenchPhraseBookAI(generalTranslatedDict[26]);
					break;
				case 27:
					AI.frenchPhraseBookAI(generalTranslatedDict[27]);
					break;
				case 28:
					AI.frenchPhraseBookAI(generalTranslatedDict[28]);
					break;
				case 29:
					AI.frenchPhraseBookAI(generalTranslatedDict[29]);
					break;
				case 30:
					AI.frenchPhraseBookAI(generalTranslatedDict[30]);
					break;
				case 31:
					AI.frenchPhraseBookAI(generalTranslatedDict[31]);
					break;
				case 32:
					AI.frenchPhraseBookAI(generalTranslatedDict[32]);
					break;
				case 33:
					AI.frenchPhraseBookAI(generalTranslatedDict[33]);
					break;
				case 34:
					AI.frenchPhraseBookAI(generalTranslatedDict[34]);
					break;
				case 35:
					AI.frenchPhraseBookAI(generalTranslatedDict[35]);
					break;
				case 36:
					AI.frenchPhraseBookAI(generalTranslatedDict[36]);
					break;
				case 37:
					AI.frenchPhraseBookAI(generalTranslatedDict[37]);
					break;
				case 38:
					AI.frenchPhraseBookAI(generalTranslatedDict[38]);
					break;
				case 39:
					AI.frenchPhraseBookAI(generalTranslatedDict[39]);
					break;
				case 40:
					AI.frenchPhraseBookAI(generalTranslatedDict[40]);
					break;
				case 41:
					AI.frenchPhraseBookAI(generalTranslatedDict[41]);
					break;
				case 42:
					AI.frenchPhraseBookAI(generalTranslatedDict[42]);
					break;
				case 43:
					AI.frenchPhraseBookAI(generalTranslatedDict[43]);
					break;
				case 44:
					AI.frenchPhraseBookAI(generalTranslatedDict[44]);
					break;
				case 45:
					AI.frenchPhraseBookAI(generalTranslatedDict[45]);
					break;
				case 46:
					AI.frenchPhraseBookAI(generalTranslatedDict[46]);
					break;
				case 47:
					AI.frenchPhraseBookAI(generalTranslatedDict[47]);
					break;
				case 48:
					AI.frenchPhraseBookAI(generalTranslatedDict[48]);
					break;
				case 49:
					AI.frenchPhraseBookAI(generalTranslatedDict[49]);
					break;
				case 50:
					AI.frenchPhraseBookAI(generalTranslatedDict[50]);
					break;
				case 51:
					AI.frenchPhraseBookAI(generalTranslatedDict[51]);
					break;
				case 52:
					AI.frenchPhraseBookAI(generalTranslatedDict[52]);
					break;
				case 53:
					AI.frenchPhraseBookAI(generalTranslatedDict[53]);
					break;
				case 54:
					AI.frenchPhraseBookAI(generalTranslatedDict[54]);
					break;
				case 55:
					AI.frenchPhraseBookAI(generalTranslatedDict[55]);
					break;
				case 56:
					AI.frenchPhraseBookAI(generalTranslatedDict[56]);
					break;
				case 57:
					AI.frenchPhraseBookAI(generalTranslatedDict[57]);
					break;
				case 58:
					AI.frenchPhraseBookAI(generalTranslatedDict[58]);
					break;
				case 59:
					AI.frenchPhraseBookAI(generalTranslatedDict[59]);
					break;
				case 60:
					AI.frenchPhraseBookAI(generalTranslatedDict[60]);
					break;
				case 61:
					AI.frenchPhraseBookAI(generalTranslatedDict[61]);
					break;
				default:
					Console.WriteLine("No key selected");
					break;
			}
			tableView.DeselectRow(indexPath, true);
		}

	}
}
