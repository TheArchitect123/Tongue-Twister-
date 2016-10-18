using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

using UIKit;
using CoreFoundation;
using Foundation;

namespace FrenchPhraseBook
{
	public partial class TechnologyController : UITableViewController
	{
		public TechnologyController(IntPtr handle) : base(handle)
		{
		}

		public TechnologyController()
		{
		}

		public List<string> firstTimeDict = new List<string>() {
			{"Do you have an iPhone?"},{"What iOS version do you have?"},{"This phone has a built in camera"},{"I own an iPad"},{"I love this app!"},{"Do you read the science paper?"},
			{"I read allot of sci fi novels"},{"This app uses API found in cocoa touch layer"},{"This device has many features"},{"There are many apps that implement global positioning systems"},
			{"I keep up to date with the latest features of Xcode"},{"Prototyping board"},{"Breadboard"},
			{"Integrated circuits"},{"Power Electronics"},{"Jet engine"},{"Radiator"},{"Keyboard"},{"Mouse"},{"Computer monitor"},{"Programming skills"},{"Agile methodology"},
			{"Navigation stack"},{"Stack trace"},{"Phone technology"},{"Integrated Development Enviornment"},{"Panoramics"},{"Lightbulb"},{"Specifications"},
			{"Local notification"},{"Push notification service"},{"Science"},{"This is an interesting science"},{"Do you know anything about cars?"},{"I am an engineer of cocoa systems"},{"Internet Protocol"},{"Transmission Control Protocol"},
			{"Linux operating system"},{"Internet Security"},{"User Interface"},{"Classes"},{"Properties"},{"Access modifiers"},{"I've been to this tech show lately"},{"Android phone"},{"I own...(device)"},{"Music player"},{"Exception handling"},
			{"Are you familiar with polymorphism?"},{"Do you implement inheritance?"},{"Core operating system"},
			{"Windows phone"},{"Parallel switches"},{"Electro magnetic pulse"},{"Power core"},{"Sim card"},{"My phone is broken"},
			{"My computer has been hacked!"},{"Artifical Intelligence"},{"Distribution Certificate Identification"},{"Lamp"},{"Fabrication Technology"},
			{"Plasma cutter"},{"3rd party network"},{"Switching circuit"},{"Television station"},{"Radio station"},{"Radio Transmitter"},{"Power distrupter"},{"Battery cable"},{"Battery charger"}
		};


		//place the translated dictionary her 
		Dictionary<int, string> firstTimeTranslated = new Dictionary<int, string>() {
			{0,"Avez-vous un iPhone?"},{1,"Qu'est-ce que la version iOS avez-vous?"},{2,"Ce téléphone a un appareil photo intégré"},{3,"Je possède un iPad"},{4,"J'adore cette application"},{5,"Avez-vous lu le document de la science?"},{6,"Je lis attribuer des sci fi romans"},{7,"Cette application utilise l'API trouvé dans le cacao couche tactile"},
			{8,"Cet appareil possède de nombreuses fonctionnalités"},{9,"Il existe de nombreuses applications qui mettent en œuvre des systèmes de positionnement global"},{10,"Je garde à jour avec les dernières fonctionnalités de Xcode"},
			{11,"carte de prototypage"},{12,"Breadboard"},
			{13,"Circuits intégrés"},{14,"Electronique de puissance"},{15,"Moteur d'avion"},{16,"Radiateur"},{17,"Clavier"},{18,"Souris"},{19,"Moniteur d'ordinateur"},{20,"compétences de programmation"},{21,"méthodologie Agile"},{22,"pile Navigation"},{23,"Trace de la pile"},
			{24,"la technologie de téléphone"},{25,"Enviornment développement intégré"},{26,"Panoramiques"},{27,"Ampoule"},{28,"Caractéristiques"},
			{29,"notification locale"},{30,"service de notification Push"},{31,"Science"},{32,"Ceci est une science intéressante"},
			{33,"Savez-vous quoi ce soit sur les voitures?"},{34,"Je suis un ingénieur des systèmes de cacao"},{35,"protocole Internet"},{36,"Protocole de transmission"},
			{37,"système d'exploitation Linux"},{38,"la sécurité sur Internet"},{39,"Interface utilisateur"},{40,"Des cours"},{41,"Propriétés"},{42,"Les modificateurs d'accès"},{43,"Je suis allé à ce spectacle tech récents"},{44,"téléphone Android"},{45,"Je possède..."},{46,"Lecteur de musique"},{47,"Gestion des exceptions"},
			{48,"Êtes-vous familier avec le polymorphisme?"},{49,"Avez- vous mettre en œuvre l'héritage?"},{50,"système d'exploitation de base"},{51,"Téléphone Windows"},{52,"commutateurs parallèles"},{53,"impulsion magnétique Electro"},
			{54,"noyau de puissance"},{55,"Carte SIM"},{56,"Mon téléphone est cassé"},
			{57,"Mon ordinateur a été piraté!"},{58,"Intelligence artificielle"},{59,"Répartition certificat d'identification"},{60,"Lampe"},{61,"Technologie de fabrication"},{62,"cutter plasma"},
			{63,"3ème réseau de parti"},{64,"circuit de commutation"},{65,"Poste de télévision"},{66,"Station de radio"},{67,"Transmetteur Radio"},{68,"Puissance distrupter"},{69,"câble de batterie"},{70,"Chargeur de batterie"}
		};
		public string technologyString = "Family";

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
				if (this.application.localizedTechnology.Count == 1)
				{
					this.application.cellTechnology.AccessoryView = this.favouritesIndicator;
					this.application.cellTechnology.EditingAccessoryView = this.favouritesIndicator;
					this.TableView.ReloadData();
				}
			}

			if (this.application.localizedTechnology.Count == 1)
			{
				this.application.cellTechnology.AccessoryView = this.favouritesIndicator;
				this.application.cellTechnology.EditingAccessoryView = this.favouritesIndicator;
				this.TableView.ReloadData();
			}
		}

		public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
		{
			return 80.0f;
		}

		public override void ViewDidLoad()
		{
			this.application.technologyControl = this;
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




			UIBarButtonItem optionButton = new UIBarButtonItem("<\ud83d\udd0c", UIBarButtonItemStyle.Plain, (object sender, EventArgs e) =>
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

			this.NavigationItem.Title = "Technology";

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
			UITableViewCell BusinessCell = tableView.DequeueReusableCell(this.technologyString);

			if (BusinessCell == null)
			{
				BusinessCell = new UITableViewCell(UITableViewCellStyle.Subtitle, this.technologyString);
			}


			this.application.cellTechnology = BusinessCell;

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
				this.application.cellTechnology.AccessoryView = null;
				this.application.cellTechnology.EditingAccessoryView = null;
			}

			if (this.application.localizedTechnology.Count == 0)
			{
				this.application.cellTechnology.AccessoryView = null;
				this.application.cellTechnology.EditingAccessoryView = null;
			}

			else if (this.application.localizedTechnology.Count >= 1)
			{
				if (this.application.localizedTechnology.Count == 1)
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
				if (indexPath.Row == this.application.indexTableTechnology.Row)
				{
					Console.WriteLine("index chosen");
					BusinessCell.AccessoryView = this.favouritesIndicator;
					BusinessCell.EditingAccessoryView = this.favouritesIndicator;
				}

				else if (this.application.indexIntTechnology.Contains(indexPath.Row) == false)
				{
					Console.WriteLine("Index is not found");
					BusinessCell.AccessoryView = null;
					BusinessCell.EditingAccessoryView = null;
				}

				else if (this.application.indexIntTechnology.Contains(indexPath.Row) == true)
				{
					Console.WriteLine("Index table chosen _ 1");
					if (indexPath.Row != this.application.indexTableTechnology.Row)
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
				if (this.application.indexIntTechnology.Contains(indexPath.Row) == true)
				{
					if (indexPath.Row != this.application.indexTableTechnology.Row || indexPath.Row == this.application.indexTableTechnology.Row)
					{
						return UITableViewCellEditingStyle.Delete;
					}
				}
				this.application.cellTechnology.AccessoryView = null;
				this.application.cellTechnology.EditingAccessoryView = null;

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

				if (this.application.localizedTechnology.Count((string arg) => arg.ToString() == this.firstTimeDict[indexPath.Row]) >= 1)
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

				else if (this.application.localizedTechnology.Count((string arg) => arg.ToString() == this.firstTimeDict[indexPath.Row]) == 0)
				{
					this.application.localizedTechnology.Add(this.firstTimeDict[indexPath.Row]);
					this.application.frenchTextTechnology.Add(this.firstTimeTranslated[indexPath.Row]);
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

					this.application.indexIntTechnology.Add(indexPath.Row);

					this.application.indexTableTechnology = index;

					this.TableView.ReloadData();
				}
			}
			else if (editingStyle == UITableViewCellEditingStyle.Delete)
			{
				if (this.application.indexIntTechnology.Contains(indexPath.Row) == true)
				{
					this.application.localizedTechnology.RemoveAll((string obj) => obj == this.firstTimeDict[indexPath.Row]);
					this.application.indexIntTechnology.RemoveAll((int obj) => obj == indexPath.Row);

					this.application.favourites.RemoveAll((string obj) => obj == this.firstTimeDict[indexPath.Row]);
					this.application.favouritesFrench.RemoveAll((string obj) => obj == this.firstTimeTranslated[indexPath.Row]);

					if (this.application.indexTableTechnology.Row == indexPath.Row || this.application.indexTableTechnology.Row != indexPath.Row)
					{
						this.application.cellTechnology.AccessoryView = null;
						this.application.cellTechnology.EditingAccessoryView = null;
						this.TableView.ReloadData();
					}

					if (this.application.localizedTechnology.Count == 1)
					{
						this.TableView.ReloadData();
					}

					if (this.application.cellTechnology.EditingStyle == UITableViewCellEditingStyle.Insert)
					{
						this.application.cellTechnology.AccessoryView = null;
						this.application.cellTechnology.EditingAccessoryView = null;
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
					this.application.cellTechnology.AccessoryView = null;
					this.application.cellTechnology.EditingAccessoryView = null;
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
				case 68:
					AI.frenchPhraseBookAI(firstTimeTranslated[68]);
					break;
				case 69:
					AI.frenchPhraseBookAI(firstTimeTranslated[69]);
					break;
				case 70:
					AI.frenchPhraseBookAI(firstTimeTranslated[70]);
					break;

				default:
					Console.WriteLine("No key selected");
					break;
			}
			tableView.DeselectRow(indexPath, true);

		}
	}
}
