package org.acme;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.Random;

import io.quarkus.security.ForbiddenException;

public class Traffic_Sensor {
	private String _location;
	private Traffic_Sensor_Type _sensorType = Traffic_Sensor_Type.Radar;
	private List<Traffic_Sensor_Measurment> _sensorData = new ArrayList<Traffic_Sensor_Measurment>();
	private int _speedLimit = 5000;

	public Traffic_Sensor(String location) {
		_location = location;
	}

	public Traffic_Sensor(String location, Traffic_Sensor_Type sensorType) {
		_location = location;
		_sensorType = sensorType;
	}

	public Traffic_Sensor(String location, Traffic_Sensor_Type sensorType, int speedLimit) {
		_location = location;
		_sensorType = sensorType;
		_speedLimit = speedLimit * 100;
	}

	public void makeMeasurment() {
		Random random = new Random();
		int vehycle_type_rand = random.nextInt(100);
		Vehicle_Type type = Vehicle_Type.Car;
		if (vehycle_type_rand < 60)
			type = Vehicle_Type.Car;
		if (60 < vehycle_type_rand && vehycle_type_rand < 70)
			type= Vehicle_Type.Van;
		if (70 < vehycle_type_rand && vehycle_type_rand < 80)
			type= Vehicle_Type.Bus;
		if (80 < vehycle_type_rand && vehycle_type_rand < 90)
			type= Vehicle_Type.Truck;
		if (90 < vehycle_type_rand && vehycle_type_rand < 95)
			type= Vehicle_Type.Motocycle;
		if (95 < vehycle_type_rand)
			type= Vehicle_Type.Unknown;
		int speed = (random.nextInt(40) + 80) * _speedLimit / 100;
		_sensorData.add(new Traffic_Sensor_Measurment(speed, type));
	}

	public String get_location() {
		return _location;
	}

	public Traffic_Sensor_Type get_sensorType() {
		return _sensorType;
	}

	public int get_speedLimit() {
		return _speedLimit;
	}

	private Vehicle_Type createVehicleType(String type) {
		switch (type.toLowerCase()) {
			case "car":
				return Vehicle_Type.Car;
			case "bus":
				return Vehicle_Type.Bus;
			case "truck":
				return Vehicle_Type.Truck;
			case "motocycle":
				return Vehicle_Type.Motocycle;
			case "van":
				return Vehicle_Type.Van;
			default:
				return Vehicle_Type.Unknown;
		}
	}

	private LocalDateTime formateTime(String time) {
		try {
			return LocalDateTime.parse(time + " 00:00",
        DateTimeFormatter.ofPattern("dd.MM.yyyy HH:mm"));
		}
		catch (Exception e) {
			throw new ForbiddenException("Wrong date format");
		}
		
	}

	public List<Traffic_Sensor_Measurment> filterMeasurments(Map<String, String> condParams) {
		List<Traffic_Sensor_Measurment> measurments = new ArrayList<Traffic_Sensor_Measurment>(_sensorData);
		if (condParams.get("vehicleType") != null) {
			measurments.removeIf(measure -> (measure.get_vehicleType() != createVehicleType(condParams.get("vehicleType"))));
		}
		if (condParams.get("vehicleSpeed") != null) {
			String[] limits = (condParams.get("vehicleSpeed")).split("-");
			measurments.removeIf(measure -> (measure.get_vehicleSpeed() < Integer.parseInt(limits[0]) * 100
				|| measure.get_vehicleSpeed() > Integer.parseInt(limits[1]) * 100));
		}
		if (condParams.get("timestamp") != null) {
			String[] limits = (condParams.get("timestamp")).split("-");			
			measurments.removeIf(measure -> (formateTime(limits[0]).isAfter(measure.get_timestamp())
				|| measure.get_timestamp().isAfter(formateTime(limits[1]))));
		}
		return measurments;
	}

	@Override
	public String toString() {
		return "Location:" + _location + " SpeedLimit:" + (_speedLimit / 100) + " SenorType:" + _sensorType + "\n";
	}
}