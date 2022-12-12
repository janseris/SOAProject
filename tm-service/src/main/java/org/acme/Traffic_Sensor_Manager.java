package org.acme;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Random;
import java.util.stream.Stream;

public class Traffic_Sensor_Manager {
	private List<Traffic_Sensor> _trafficSensors = new ArrayList<Traffic_Sensor>();
	String _sensorfile = "/home/lacike/TM/src/main/java/org/acme/Sensors.txt";

	public Traffic_Sensor_Manager(){
		try (Stream<String> stream = Files.lines(Paths.get(_sensorfile))) {
			stream.forEach(line -> {
				String[] args = line.split(",");
				_trafficSensors.add(new Traffic_Sensor(args[0], getSensorType(args[1]), Integer.parseInt(args[2])));
			});
		}
		catch (IOException e) {	
		}
		Random _random = new Random();
		for (Traffic_Sensor trafficSensor : _trafficSensors) {
			for (int i = 0; i < _random.nextInt(100); ++i) {
				trafficSensor.makeMeasurment();
			}
		}
	}

	public void addSensor(Traffic_Sensor sensor) {
		_trafficSensors.add(sensor);
	}

	public void addSensor(String location) {
		_trafficSensors.add(new Traffic_Sensor(location));
	}

	public void addSensor(String location, Traffic_Sensor_Type type) {
		_trafficSensors.add(new Traffic_Sensor(location, type));
	}

	public void removeSensore(Traffic_Sensor sensor) {
		_trafficSensors.remove(sensor);
	}

	private Traffic_Sensor_Type getSensorType(String type) {
		switch (type.toLowerCase()) {
			case "camera":
				return Traffic_Sensor_Type.Camera;
			case "radar":
				return Traffic_Sensor_Type.Radar;
			default:
				return Traffic_Sensor_Type.Grid;
		}
	}

	public List<Traffic_Sensor_Measurment> getMeasurments(String conditions) {
		List<Traffic_Sensor_Measurment> measurments = new ArrayList<Traffic_Sensor_Measurment>();
		List<Traffic_Sensor> sensors = new ArrayList<Traffic_Sensor>(_trafficSensors);
		Map<String, String> condParams = new HashMap<String, String>();
		String[] condValues = {"location", "sensorType", "speedLimit", "vehicleType", "vehicleSpeed", "timestamp"};

		for (String string : condValues) {
			int start = conditions.indexOf(string);
			if (start != -1) {
				int end = conditions.indexOf("|", start);
				if (end == -1) {
					condParams.put(string, conditions.substring(start + string.length() + 1));
				}
				else {
					condParams.put(string, conditions.substring(start + string.length() + 1, end));
				}					
			}
		}
		if (condParams.get("location") != null) {
			sensors.removeIf(sensor -> (!(sensor.get_location().contains(condParams.get("location")))));
		}
		if (condParams.get("sensorType") != null) {
			sensors.removeIf(sensor -> (sensor.get_sensorType() != getSensorType(condParams.get("sensorType"))));
		}
		if (condParams.get("speedLimit") != null) {
			sensors.removeIf(sensor -> (sensor.get_speedLimit() != Integer.parseInt(condParams.get("speedLimit")) * 100));
		}
		for (Traffic_Sensor sensor : sensors) {
			measurments.addAll(sensor.filterMeasurments(condParams));
		}
		return measurments;
	}

	public List<Traffic_Sensor> getSensors(String conditions) {
		if (conditions == "all") {
			return _trafficSensors;
		}
		List<Traffic_Sensor> sensors = new ArrayList<Traffic_Sensor>(_trafficSensors);
		Map<String, String> condParams = new HashMap<String, String>();
		String[] condValues = {"location", "sensorType", "speedLimit", "vehicleType", "vehicleSpeed", "timestamp"};

		for (String string : condValues) {
			int start = conditions.indexOf(string);
			if (start != -1) {
				int end = conditions.indexOf("|", start);
				if (end == -1) {
					condParams.put(string, conditions.substring(start + string.length() + 1));
				}
				else {
					condParams.put(string, conditions.substring(start + string.length() + 1, end));
				}				
			}
		}
		if (condParams.get("location") != null) {
			sensors.removeIf(sensor -> (!(sensor.get_location().contains(condParams.get("location")))));
		}
		if (condParams.get("sensorType") != null) {
			sensors.removeIf(sensor -> (sensor.get_sensorType() != getSensorType(condParams.get("sensorType"))));
		}
		if (condParams.get("speedLimit") != null) {
			sensors.removeIf(sensor -> (sensor.get_speedLimit() != Integer.parseInt(condParams.get("speedLimit")) * 100));
		}
		return sensors;
	}
}