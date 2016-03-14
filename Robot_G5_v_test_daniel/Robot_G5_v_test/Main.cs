
using System;
using MonoBrickFirmware;
using MonoBrickFirmware.Display.Dialogs;
using MonoBrickFirmware.Display;
using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;
using System.Threading;

namespace MonoBrickHelloWorld
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Vehicle robot = new Vehicle (MotorPort.OutC, MotorPort.OutB);
			EV3IRSensor IRsensor = new EV3IRSensor (SensorPort.In4);
			EV3GyroSensor GyroSensor = new EV3GyroSensor (SensorPort.In3, GyroMode.Angle);
			//int x = 40;
			Font f = Font.SmallFont;
			Point offset = new Point(0,25);
			Point p = new Point(10, Lcd.Height-75);
			Point boxSize = new Point(100, 24);
			Rectangle box = new Rectangle(p, p+boxSize);

			InfoDialog dialog = new InfoDialog ("Start Robot");
			dialog.Show ();//Wait for enter to be pressed
			//Motor motor = new Motor (MotorPort.OutA);
			//motor.SetSpeed (50);
			//Thread.Sleep (3000);
			/*for (int i = 1; i <= x; i++) 
			{
				Lcd.Clear ();
				Lcd.WriteTextBox (f, box + offset * 0, "Counter: " + i, true);
				Lcd.WriteTextBox (f, box + offset * 1, "Distance: " + IRsensor.ReadAsString(), false);
				Lcd.WriteTextBox (f, box + offset * 2, "Angle: " + GyroSensor.ReadAsString(), true);
				Thread.Sleep (500);
				Lcd.Update ();
			}*/
			sbyte spinSpeed = 40;
			Lcd.Clear ();
			while (IRsensor.ReadDistance() > 5) 
			{
				while (GyroSensor.Read() > -60) {
					Lcd.Clear ();
					Lcd.WriteTextBox (f, box + offset * 0, "SpinLeft", true);
					Lcd.WriteTextBox (f, box + offset * 1, "Angle: " + GyroSensor.ReadAsString(), true);
					robot.SpinLeft (spinSpeed);
					if (IRsensor.ReadDistance() < 5) {
						break;
					}
					Lcd.Update ();
				}
				robot.Forward (80);
				Lcd.Clear ();
				while (GyroSensor.Read() < 60) {
					Lcd.Clear ();
					Lcd.WriteTextBox (f, box + offset * 0, "SpinRight", true);
					Lcd.WriteTextBox (f, box + offset * 1, "Angle: " + GyroSensor.ReadAsString(), true);
					robot.SpinRight (spinSpeed);
					if (IRsensor.ReadDistance() < 5) {
						break;
					}
					Lcd.Update ();
				}
				robot.Forward (80);
			}
			Lcd.WriteTextBox (f, box + offset * 0, "Robot Stop", true);
			robot.Brake ();
			robot.Off ();
			//motor.Off ();
			Lcd.Clear ();
			Lcd.Update ();
		}
	}
}

