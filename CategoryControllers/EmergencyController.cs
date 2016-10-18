using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

using UIKit;
using CoreFoundation;
using Foundation;

namespace FrenchPhraseBook
{
	public partial class EmergencyController : UITableViewController
	{
		public List<string> emergencyDict = new List<string>(){
			{"Help!"}, {"My wife is missing"}, {"My husband is missing"},{"Stop!"},{"Thief!"},{"He stole my wallet!"},{"She stole my wallet!"},
			{"My car is stolen"},{"My house was robbed"},{"Where is the police station?"},{"Call the police"},{"Call an ambulance"},{"I broke my leg"},
			{"I was robbed!"},{"Where is the nearest hospital?"},{"Is there a doctor here!"},{"This person needs medical attention"},{"Emergency services"},
			{"I've lost my phone"},{"My phone is stolen"},{"I need my lawyer"},{"This is an emergency situation"},{"Where is the nearest"},{"My tire has blown out"},
			{"My car's engine is destroyed"},{"Do you work for the police?"},{"Can you help me?"},{"Fire!"},{"This floor is flooded"},{"My computer is broken!"},{"He has a bullet wound"},
			{"I can't find my passport!"},{"My child is missing!"},{"Have you seen my wallet?"},{"I didn't do it"},{"My computer's infected with a virus"},
			{"Report a missing person"},{"I need your help!"},{"What's your emergency..?"},{"I'm lost"},{"Can you tell me where I am?"},{"I had an emergency call"},
			{"This place is a warzone"},{"Flooding"},{"Escape Hatch"},{"He needs help!"},{"She needs help!"},{"Where is the escape area?"},{"This is not a drill"},{"There was an explosion near here"},
			{"How can I help you?"},
		};

		Dictionary<int, string> emergencyTransDict = new Dictionary<int, string> {
			{0,"Aidez-moi!"},{1,"Ma femme est absente"}, {2,"Mon mari est absent"}, {3,"Arrêtez!"}, {4,"Voleur!"}, {5,"Il a volé mon portefeuille!"},{6,"Elle a volé mon portefeuille"},{7,"Ma voiture est volée"},
			{8,"Ma maison a été volé"},{9,"Ou est la station de police?"},{10,"Appelle la police"},{11,"Appelle une ambulance"},{12,"Je me suis cassé la jambe"},{13,"J'ai été volé!"},{14,"Où se trouve l'hôpital le plus proche?"},
			{15,"Y at-il un médecin ici!"},{16,"Cette personne a besoin d'attention médicale"},{17,"Services d'urgence"},{18,"J'ai perdu mon téléphone"},{19,"Mon téléphone est volé"},{20,"Je dois mon avocat"},{21,"Ceci est une situation d'urgence"},
			{22,"Où est le plus proche"},{23,"Mon pneu a soufflé"},{24,"Le moteur de ma voiture est détruite"},{25,"Travaillez-vous pour la police?"},{26,"Pouvez-vous m'aider?"},{27,"Feu!"},{28,"Cet étage est inondé"},{29,"Mon ordinateur est cassé!"},{30,"Il a une blessure par balle"},
			{31,"Je ne peux pas trouver mon passeport!"},{32,"Mon enfant a disparu !"},{33,"Avez-vous vu mon portefeuille ?"},{34,"Je ne le fais pas"},{35,"Mon ordinateur est infecté par un virus"},
			{36,"Signaler une personne disparue"},{37,"J'ai besoin de ton aide!"},{38,"Quelle est votre urgence .. ?"},{39,"je suis perdu"},{40,"Pouvez- vous me dire où je suis?"},{41,"J'ai eu un appel d'urgence"},{42,"Cet endroit est une zone de guerre"},{43,"Inondation"},
			{44,"Évadez- Hatch"},{45,"Il a besoin d'aide!"},{46,"Elle a besoin d'aide !"},{47,"Où se trouve la zone d'évacuation?"},{48,"Ce n'est pas une perceuse"},{49,"Il y avait une explosion près d'ici"},
			{50,"Comment puis-je t'aider?"}
		};

		public string emergencyID = "emergencyID";

		public EmergencyController(IntPtr handle) : base(handle)
		{
		}

		public EmergencyController() { }

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
				AI.frenchPhraseBookAI(this.emergencyTransDict[index.Row]);
			}
			else if (this.application.tabBarID == 0)
			{
				if (this.application.localizedTextEmergency.Count == 1)
				{
					this.application.cellEmergency.AccessoryView = this.favouritesIndicator;
					this.application.cellEmergency.EditingAccessoryView = this.favouritesIndicator;
					this.TableView.ReloadData();
				}
			}

			if (this.application.localizedTextEmergency.Count == 1)
			{
				this.application.cellEmergency.AccessoryView = this.favouritesIndicator;
				this.application.cellEmergency.EditingAccessoryView = this.favouritesIndicator;
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




			UIBarButtonItem optionButton = new UIBarButtonItem("<\ud83d\ude91", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
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

			this.NavigationItem.Title = "Emergency";

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
			UITableViewCell BusinessCell = tableView.DequeueReusableCell(this.emergencyID);

			if (BusinessCell == null)
			{
				BusinessCell = new UITableViewCell(UITableViewCellStyle.Subtitle, this.emergencyID);
			}


			this.application.cellEmergency = BusinessCell;

			this.favouritesIndicator = new UILabel();
			this.favouritesIndicator.Text = "\ud83d\udc9d";
			//this.favouritesIndicator.Text = "⭐";
			this.favouritesIndicator.MinimumFontSize = 24.0f;
			this.favouritesIndicator.AdjustsFontSizeToFitWidth = true;
			this.favouritesIndicator.Frame = new CoreGraphics.CGRect(0, 20, 40, 40);

			BusinessCell.TextLabel.Text = this.emergencyDict[indexPath.Row];
			BusinessCell.DetailTextLabel.Text = this.emergencyTransDict[indexPath.Row];
			BusinessCell.DetailTextLabel.TextColor = UIColor.Gray;
			BusinessCell.DetailTextLabel.Font = UIFont.SystemFontOfSize(12.5f);

			if (BusinessCell.EditingStyle == UITableViewCellEditingStyle.Insert)
			{
				this.application.cellEmergency.AccessoryView = null;
				this.application.cellEmergency.EditingAccessoryView = null;
			}

			if (this.application.localizedTextEmergency.Count == 0)
			{
				this.application.cellEmergency.AccessoryView = null;
				this.application.cellEmergency.EditingAccessoryView = null;
			}

			else if (this.application.localizedTextEmergency.Count >= 1)
			{
				if (this.application.localizedTextEmergency.Count == 1)
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
				if (indexPath.Row == this.application.indexTableEmergency.Row)
				{
					Console.WriteLine("index chosen");
					BusinessCell.AccessoryView = this.favouritesIndicator;
					BusinessCell.EditingAccessoryView = this.favouritesIndicator;
				}

				else if (this.application.indexIntEmergency.Contains(indexPath.Row) == false)
				{
					Console.WriteLine("Index is not found");
					BusinessCell.AccessoryView = null;
					BusinessCell.EditingAccessoryView = null;
				}

				else if (this.application.indexIntEmergency.Contains(indexPath.Row) == true)
				{
					Console.WriteLine("Index table chosen _ 1");
					if (indexPath.Row != this.application.indexTableEmergency.Row)
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
				if (this.application.indexIntEmergency.Contains(indexPath.Row) == true)
				{
					if (indexPath.Row != this.application.indexTableEmergency.Row || indexPath.Row == this.application.indexTableEmergency.Row)
					{
						return UITableViewCellEditingStyle.Delete;
					}
				}
				this.application.cellEmergency.AccessoryView = null;
				this.application.cellEmergency.EditingAccessoryView = null;

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

				if (this.application.localizedTextEmergency.Count((string arg) => arg.ToString() == this.emergencyDict[indexPath.Row]) >= 1)
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

				else if (this.application.localizedTextEmergency.Count((string arg) => arg.ToString() == this.emergencyDict[indexPath.Row]) == 0)
				{
					this.application.localizedTextEmergency.Add(this.emergencyDict[indexPath.Row]);
					this.application.frenchTextEmergency.Add(this.emergencyTransDict[indexPath.Row]);
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

					this.application.indexIntEmergency.Add(indexPath.Row);

					this.application.indexTableEmergency = index;

					this.TableView.ReloadData();
				}
			}
			else if (editingStyle == UITableViewCellEditingStyle.Delete)
			{
				if (this.application.indexIntEmergency.Contains(indexPath.Row) == true)
				{
					this.application.localizedTextEmergency.RemoveAll((string obj) => obj == this.emergencyDict[indexPath.Row]);
					this.application.indexIntEmergency.RemoveAll((int obj) => obj == indexPath.Row);

					this.application.favourites.RemoveAll((string obj) => obj == this.emergencyDict[indexPath.Row]);
					this.application.favouritesFrench.RemoveAll((string obj) => obj == this.emergencyTransDict[indexPath.Row]);

					if (this.application.indexTableEmergency.Row == indexPath.Row || this.application.indexTableEmergency.Row != indexPath.Row)
					{
						this.application.cellEmergency.AccessoryView = null;
						this.application.cellEmergency.EditingAccessoryView = null;
						this.TableView.ReloadData();
					}

					if (this.application.localizedTextEmergency.Count == 1)
					{
						this.TableView.ReloadData();
					}

					if (this.application.cellEmergency.EditingStyle == UITableViewCellEditingStyle.Insert)
					{
						this.application.cellEmergency.AccessoryView = null;
						this.application.cellEmergency.EditingAccessoryView = null;
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
					this.application.cellEmergency.AccessoryView = null;
					this.application.cellEmergency.EditingAccessoryView = null;
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
			return this.emergencyDict.Count;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			BatteryMonitor AI = new BatteryMonitor();
			switch (indexPath.Row)
			{
				case 0:
					AI.frenchPhraseBookAI(emergencyTransDict[0]);
					break;
				case 1:
					AI.frenchPhraseBookAI(emergencyTransDict[1]);
					break;
				case 2:
					AI.frenchPhraseBookAI(emergencyTransDict[2]);
					break;
				case 3:
					AI.frenchPhraseBookAI(emergencyTransDict[3]);
					break;
				case 4:
					AI.frenchPhraseBookAI(emergencyTransDict[4]);
					break;
				case 5:
					AI.frenchPhraseBookAI(emergencyTransDict[5]);
					break;
				case 6:
					AI.frenchPhraseBookAI(emergencyTransDict[6]);
					break;
				case 7:
					AI.frenchPhraseBookAI(emergencyTransDict[7]);
					break;
				case 8:
					AI.frenchPhraseBookAI(emergencyTransDict[8]);
					break;
				case 9:
					AI.frenchPhraseBookAI(emergencyTransDict[9]);
					break;
				case 10:
					AI.frenchPhraseBookAI(emergencyTransDict[10]);
					break;
				case 11:
					AI.frenchPhraseBookAI(emergencyTransDict[11]);
					break;
				case 12:
					AI.frenchPhraseBookAI(emergencyTransDict[12]);
					break;
				case 13:
					AI.frenchPhraseBookAI(emergencyTransDict[13]);
					break;
				case 14:
					AI.frenchPhraseBookAI(emergencyTransDict[14]);
					break;
				case 15:
					AI.frenchPhraseBookAI(emergencyTransDict[15]);
					break;
				case 16:
					AI.frenchPhraseBookAI(emergencyTransDict[16]);
					break;
				case 17:
					AI.frenchPhraseBookAI(emergencyTransDict[17]);
					break;
				case 18:
					AI.frenchPhraseBookAI(emergencyTransDict[18]);
					break;
				case 19:
					AI.frenchPhraseBookAI(emergencyTransDict[19]);
					break;
				case 20:
					AI.frenchPhraseBookAI(emergencyTransDict[20]);
					break;
				case 21:
					AI.frenchPhraseBookAI(emergencyTransDict[21]);
					break;
				case 22:
					AI.frenchPhraseBookAI(emergencyTransDict[22]);
					break;
				case 23:
					AI.frenchPhraseBookAI(emergencyTransDict[23]);
					break;
				case 24:
					AI.frenchPhraseBookAI(emergencyTransDict[24]);
					break;
				case 25:
					AI.frenchPhraseBookAI(emergencyTransDict[25]);
					break;
				case 26:
					AI.frenchPhraseBookAI(emergencyTransDict[26]);
					break;
				case 27:
					AI.frenchPhraseBookAI(emergencyTransDict[27]);
					break;
				case 28:
					AI.frenchPhraseBookAI(emergencyTransDict[28]);
					break;
				case 29:
					AI.frenchPhraseBookAI(emergencyTransDict[29]);
					break;
				case 30:
					AI.frenchPhraseBookAI(emergencyTransDict[30]);
					break;
				case 31:
					AI.frenchPhraseBookAI(emergencyTransDict[31]);
					break;
				case 32:
					AI.frenchPhraseBookAI(emergencyTransDict[32]);
					break;
				case 33:
					AI.frenchPhraseBookAI(emergencyTransDict[33]);
					break;
				case 34:
					AI.frenchPhraseBookAI(emergencyTransDict[34]);
					break;
				case 35:
					AI.frenchPhraseBookAI(emergencyTransDict[35]);
					break;
				case 36:
					AI.frenchPhraseBookAI(emergencyTransDict[36]);
					break;
				case 37:
					AI.frenchPhraseBookAI(emergencyTransDict[37]);
					break;
				case 38:
					AI.frenchPhraseBookAI(emergencyTransDict[38]);
					break;
				case 39:
					AI.frenchPhraseBookAI(emergencyTransDict[39]);
					break;
				case 40:
					AI.frenchPhraseBookAI(emergencyTransDict[40]);
					break;
				case 41:
					AI.frenchPhraseBookAI(emergencyTransDict[41]);
					break;
				case 42:
					AI.frenchPhraseBookAI(emergencyTransDict[42]);
					break;
				case 43:
					AI.frenchPhraseBookAI(emergencyTransDict[43]);
					break;
				case 44:
					AI.frenchPhraseBookAI(emergencyTransDict[44]);
					break;
				case 45:
					AI.frenchPhraseBookAI(emergencyTransDict[45]);
					break;
				case 46:
					AI.frenchPhraseBookAI(emergencyTransDict[46]);
					break;
				case 47:
					AI.frenchPhraseBookAI(emergencyTransDict[47]);
					break;
				case 48:
					AI.frenchPhraseBookAI(emergencyTransDict[48]);
					break;
				case 49:
					AI.frenchPhraseBookAI(emergencyTransDict[49]);
					break;
				case 50:
					AI.frenchPhraseBookAI(emergencyTransDict[50]);
					break;
				default:
					Console.WriteLine("No key selected");
					break;
			}
			tableView.DeselectRow(indexPath, true);
		}
	}
}
