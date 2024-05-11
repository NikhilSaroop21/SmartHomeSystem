using System;

public class SmartHomeSystem
{
	// Event argument classes
	public class MotionDetectedEventArgs : EventArgs
	{
		public int SensorId { get; set; }
		public DateTime Timestamp { get; set; }
	}

	public class DoorOpenedEventArgs : EventArgs
	{
		public int SensorId { get; set; }
		public DateTime Timestamp { get; set; }
	}

	public class TemperatureChangedEventArgs : EventArgs
	{
		public int SensorId { get; set; }
		public DateTime Timestamp { get; set; }
		public double Temperature { get; set; }
	}

	// Event delegates
	public delegate void MotionDetectedEventHandler(object sender, MotionDetectedEventArgs e);
	public delegate void DoorOpenedEventHandler(object sender, DoorOpenedEventArgs e);
	public delegate void TemperatureChangedEventHandler(object sender, TemperatureChangedEventArgs e);

	// Central Event Hub
	public class EventHub
	{
		public event MotionDetectedEventHandler MotionDetected;
		public event DoorOpenedEventHandler DoorOpened;
		public event TemperatureChangedEventHandler TemperatureChanged;

		public void SimulateMotionDetected(int sensorId)
		{
			MotionDetected?.Invoke(this, new MotionDetectedEventArgs { SensorId = sensorId, Timestamp = DateTime.Now });
		}

		public void SimulateDoorOpened(int sensorId)
		{
			DoorOpened?.Invoke(this, new DoorOpenedEventArgs { SensorId = sensorId, Timestamp = DateTime.Now });
		}

		public void SimulateTemperatureChanged(int sensorId, double temperature)
		{
			TemperatureChanged?.Invoke(this, new TemperatureChangedEventArgs { SensorId = sensorId, Timestamp = DateTime.Now, Temperature = temperature });
		}
	}

	// Smart Home Devices
	public class MotionSensor
	{
		public int SensorId { get; }

		public MotionSensor(int id)
		{
			SensorId = id;
		}

		public void DetectMotion(EventHub eventHub)
		{
			eventHub.SimulateMotionDetected(SensorId);
		}
	}

	public class DoorSensor
	{
		public int SensorId { get; }

		public DoorSensor(int id)
		{
			SensorId = id;
		}

		public void OpenDoor(EventHub eventHub)
		{
			eventHub.SimulateDoorOpened(SensorId);
		}
	}

	public class Thermostat
	{
		public int SensorId { get; }

		public Thermostat(int id)
		{
			SensorId = id;
		}

		public void ChangeTemperature(EventHub eventHub, double temperature)
		{
			eventHub.SimulateTemperatureChanged(SensorId, temperature);
		}
	}

	// Main method
	public static void Main(string[] args)
	{
		EventHub eventHub = new EventHub();

		// Subscription and event handling
		eventHub.MotionDetected += (sender, e) =>
		{
			Console.WriteLine($"Motion detected by sensor {e.SensorId} at {e.Timestamp}");
			// Add your action here, like turning on lights
		};

		eventHub.DoorOpened += (sender, e) =>
		{
			Console.WriteLine($"Door opened by sensor {e.SensorId} at {e.Timestamp}");
			// Add your action here, like sending notifications
		};

		eventHub.TemperatureChanged += (sender, e) =>
		{
			Console.WriteLine($"Temperature changed by sensor {e.SensorId} at {e.Timestamp}: {e.Temperature}°C");
			// Add your action here, like adjusting thermostat settings
		};

		// Simulating events
		MotionSensor motionSensor = new MotionSensor(1);
		DoorSensor doorSensor = new DoorSensor(2);
		Thermostat thermostat = new Thermostat(3);

		motionSensor.DetectMotion(eventHub);
		doorSensor.OpenDoor(eventHub);
		thermostat.ChangeTemperature(eventHub, 25.5);

		Console.ReadLine(); // Keep the console window open
	}
}
