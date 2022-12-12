package org.acme;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class Traffic_Sensor_Measurment {

	private LocalDateTime _timestamp;
	private int _vehicleSpeed;
	private Vehicle_Type _vehicleType;

	public Traffic_Sensor_Measurment (LocalDateTime time, Vehicle_Type type, int speed) {
		_timestamp = time;
		_vehicleSpeed = speed;
		_vehicleType = type;
	}

	public Traffic_Sensor_Measurment(int speed, Vehicle_Type type) {
		_timestamp = LocalDateTime.now();
		_vehicleSpeed = speed;
		_vehicleType = type;
	}

	public Traffic_Sensor_Measurment(){}

	public LocalDateTime get_timestamp() {
		return _timestamp;
	}

	public int get_vehicleSpeed() {
		return _vehicleSpeed;
	}

	public Vehicle_Type get_vehicleType() {
		return _vehicleType;
	}

	@Override
	public String toString() {
		return "Time:" + _timestamp.format(DateTimeFormatter.ofPattern("dd.mm.yyyy")) + " Speed:" + (_vehicleSpeed / 100) + " Type:" + _vehicleType + "\n";
	}

}