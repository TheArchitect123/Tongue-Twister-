using System;
using FrenchPhraseBook;
using AVFoundation;

using UIKit;
using AudioToolbox;
using Foundation;
using CoreFoundation;

namespace FrenchPhraseBook
{
	public class BatteryMonitor
	{
		MasterController master = new MasterController();

		public void frenchPhraseBookAI(string textToSpeak)
		{
			Console.WriteLine("Hello");

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

		//english AI that notifies events of the background executions 
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

		public bool batteryLevel(bool allowed) {
			if (allowed == true)
			{
				if (UIDevice.CurrentDevice.BatteryLevel < 0.5 && UIDevice.CurrentDevice.BatteryLevel >= 0.3)
				{
					UIDevice.CurrentDevice.BatteryMonitoringEnabled = true;
					UIAlertController levelController = UIAlertController.Create("⚡️ Low Battery", "Battery is measured at " + UIDevice.CurrentDevice.BatteryLevel * 100 + "%", UIAlertControllerStyle.Alert);

					UIAlertAction confirmed = UIAlertAction.Create("Ok", UIAlertActionStyle.Default, (Action) =>
					{
						levelController.Dispose();
					});

					levelController.AddAction(confirmed);

					if (master.PresentedViewController == null)
					{
						master.PresentViewController(levelController, true, null);
					}

					else if (master.PresentedViewController != null)
					{
						master.PresentedViewController.DismissViewController(true, () =>
						{
							master.PresentedViewController.Dispose();
							master.PresentViewController(levelController, true, null);
				});
			}
					return true;
				}
			}
			else {
				Console.WriteLine("Battery monitor is disabled");
				return false;
			}
			return false;
		}
	}
}
