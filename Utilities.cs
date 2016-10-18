using System;
using FrenchPhraseBook;
using AVFoundation;

using UIKit;
using AudioToolbox;
using Foundation;
using CoreFoundation;

namespace French
{
	public class BatteryMonitor
	{
		//french voice that will be used to read the subtitle (DetailTextLabel) property of the french phrases, stored currently in the table view's memory cache
		public void frenchPhraseBookAI(string textToSpeak)
		{
			AVSpeechSynthesizer frenchSpeech = new AVSpeechSynthesizer();

			AVSpeechUtterance frenchVoice = new AVSpeechUtterance(textToSpeak)
			{
				Rate = AVSpeechUtterance.MaximumSpeechRate / 2.2f,
				Voice = AVSpeechSynthesisVoice.FromLanguage("fr"),
				Volume = 1.0f,
				PitchMultiplier = 1.0f
			};
			frenchSpeech.SpeakUtterance(frenchVoice);
		}

		//english voice that notifies the end user of background executions 
		public void englishAIBackground(string textToSpeak)
		{
			AVSpeechSynthesizer englishSpeech = new AVSpeechSynthesizer();

			AVSpeechUtterance englishVoice = new AVSpeechUtterance(textToSpeak)
			{
				Rate = AVSpeechUtterance.MaximumSpeechRate / 2.2f,
				Voice = AVSpeechSynthesisVoice.FromLanguage("en-US"),
				Volume = 1.0f,
				PitchMultiplier = 1.0f
			};
			englishSpeech.SpeakUtterance(englishVoice);
		}


		Categories mainControlRef = new Categories();
		public void batteryLow(string title, string message)
		{
			UIAlertController batteryAlertLow = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
			UIAlertAction LowPower = UIAlertAction.Create("Low Power Mode", UIAlertActionStyle.Default, (Action) =>
			{
				if (NSProcessInfo.ProcessInfo.LowPowerModeEnabled == true)
				{
					//switch on low power mode 

				}
				else {
					Console.WriteLine("Low power mode is disabled");
				}
			});

			UIAlertAction cancel = UIAlertAction.Create("Never Mind", UIAlertActionStyle.Destructive, null);

			batteryAlertLow.AddAction(LowPower);
			batteryAlertLow.AddAction(cancel);

			if (mainControlRef.PresentedViewController == null)
			{
				mainControlRef.PresentViewController(batteryAlertLow, true, null);
			}
			else {
				englishAIBackground("Your battery level is currently measured at: " + UIDevice.CurrentDevice.BatteryLevel * 100 + ". You should charge your device soon");
			}
		}

		public void updateOS(string title, string message)
		{
			UIAlertController update = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

			UIAlertAction updateOk = UIAlertAction.Create("Update", UIAlertActionStyle.Default, (Action) =>
			{
				NSUrl osUpdateUrl = new NSUrl("");
				if (UIApplication.SharedApplication.CanOpenUrl(osUpdateUrl))
				{
					UIApplication.SharedApplication.OpenUrl(osUpdateUrl);
				}

				else {
					englishAIBackground("No internet connection detected. Cannot connect to apple servers");
				}
			});

			UIAlertAction cancel = UIAlertAction.Create("Cancel", UIAlertActionStyle.Destructive, null);

			update.AddAction(updateOk);
			update.AddAction(cancel);

			if (mainControlRef.PresentedViewController == null)
			{
				mainControlRef.PresentViewController(update, true, null);
			}
			else {
				englishAIBackground("OS is out of date. You should update your OS as soon as possible");
			}
		}

		//battery methods 
		//Low level battery state handler 
		public void batteryLowNotifier()
		{
			UIDevice.CurrentDevice.BatteryMonitoringEnabled = true;
			if (UIDevice.CurrentDevice.BatteryLevel < 0.5 && UIDevice.CurrentDevice.BatteryLevel >= 0.3)
			{
				if (UIDevice.CurrentDevice.BatteryState != UIDeviceBatteryState.Charging &&
					UIDevice.CurrentDevice.BatteryState != UIDeviceBatteryState.Full)
				{
					batteryLow("Low power", "Would you like to switch to low power mode?");
					englishAIBackground("Your battery level is currently measured at: " + UIDevice.CurrentDevice.BatteryLevel * 100 + ". Would you like to switch to low power mode");
				}
			}
		}

		//battery state to notify of a fully charged battery 
		public void batteryStateNotifier()
		{
			UIDevice.CurrentDevice.BatteryMonitoringEnabled = true;

			switch (UIDevice.CurrentDevice.BatteryState)
			{
				case UIDeviceBatteryState.Full:
					if (UIDevice.CurrentDevice.BatteryLevel == 1.0)
					{
						SystemSound fullBattery = new SystemSound(1006);
						fullBattery.PlaySystemSound();
						NSDate.FromTimeIntervalSinceNow(1.5);
						englishAIBackground("Your device is fully charged");
					}
					break;
				default:
					break;
			}
		}

	}
}

