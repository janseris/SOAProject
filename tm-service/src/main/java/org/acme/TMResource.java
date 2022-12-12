package org.acme;

import java.util.List;
import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.core.MediaType;

import org.eclipse.microprofile.openapi.annotations.Operation;
import org.eclipse.microprofile.openapi.annotations.media.Content;
import org.eclipse.microprofile.openapi.annotations.responses.APIResponse;
import org.eclipse.microprofile.openapi.annotations.enums.SchemaType;
import org.eclipse.microprofile.openapi.annotations.media.Schema;

import io.quarkus.security.ForbiddenException;

@Path("/TM")
public class TMResource {

    Traffic_Message_Manager manager = new Traffic_Message_Manager(); 

    @GET
	@Operation(
		operationId = "askStatus"
	)
	@APIResponse(
		responseCode = "200",
		description = "Current service status",
		content = @Content(
			mediaType = MediaType.TEXT_PLAIN
		)
	)
	@Path("/status")	
	public String askStatus() {
		return "Service is currently in state:" + manager.getStatus();
	}

    
	@GET
	@Operation(
		operationId = "help"
	)
	@APIResponse(
		responseCode = "200",
		description = "Help meassage display",
		content = @Content(
			mediaType = MediaType.TEXT_PLAIN
		)
	)
	@Path("/help")
	public String help() {
		return "Help message for Traffic Manager \n'/TM/status' get current status of the service \n'/TM/help' display help message \n'/TM/help-measurments' display message formating \n'T/TM/help-sensors' display sensor formating \n'/TM/measurments-request/args' Request traffic manager measurments data \n'/TM/measurments-accept/args' Confirm measurments data delivery \n'/TM/measurments-repeat/args' Repeat delivery of measurments data \n'/TM/sensors-request/args' Get traffic manager sensors data \n'/TM/sensors-accept/args' Confirm sensors data delivery \n'/TM/sensors-repeat/args' Repeat delivery of sensor data";}

    @GET
	@Operation(
		operationId = "helpMeasurments"
	)
	@APIResponse(
		responseCode = "200",
		description = "Formating of the measurments request arguments",
		content = @Content(
			mediaType = MediaType.TEXT_PLAIN
		)
	)
	@Path("/help-measurments")	
	public String helpMeasurments() {
		String help = "Formating of the measurments request arguments\n";
		help += "Arguments specify conditions for filtering measurments, possible filter arguments:\n";
		help += "'location:String' sensor location\n" 
		+ "'sensorType:(radar|grid|sensor)' sensor type\n"
		+ "'speedLimit:Int' sensor speed limit\n"
		+ "'vehicleType:(Motocycle|Car|Van|Truck|Bus|Unknown)' vehicle type \n"
		+ "'vehicleSpeed:Int-Int' vehicle speed range\n"
		+ "'timestamp:dd.mm.yyyy-dd.mm.yyyy' date range of measurments\n";
		help += "more arguments may be used, separated by '|' ,in any order\n\n";
		help += "FIlter arguments example:sensorType:grid|speedLimit:40|location:Praha|vehicleType:car|vehicleSpeed:40-46|timestamp:11.12.2022-14.12.2022"
		return help;
	}

	@GET
	@Operation(
		operationId = "helpSensors"
	)
	@APIResponse(
		responseCode = "200",
		description = "Formating of the sensor request arguments",
		content = @Content(
			mediaType = MediaType.TEXT_PLAIN
		)
	)
	@Path("/help-sensors")
	public String helpSensors() {
		String help = "Formating of the sensor request arguments\n"
		+ "Arguments specify conditions for filtering sensors, possible filter arguments:\n"
		+ "'all' return all sensors\n"
		+ "'location:String' sensor location\n" 
		+ "'sensorType:(radar|grid|sensor)' sensor type\n"
		+ "'speedLimit:Int' sensor speed limit\n";
		help += "more arguments may be used, separated by '|' ,in any order\n\n";
		help += "FIlter arguments example:sensorType:radar|speedLimit:50|location:Brno"
		return help;
	}

    @GET
	@Operation(
		operationId = "measurmentsRequest"
	)
	@APIResponse(
		responseCode = "200",
		description = "Measurments data sent confirmed",
		content = @Content(
			mediaType = MediaType.APPLICATION_JSON,
            schema = @Schema(type = SchemaType.ARRAY, implementation = Traffic_Sensor_Measurment.class)
		)
	)
	@APIResponse(
		responseCode = "400",
		description = "Invalid request arguments"
	)
	@APIResponse(
		responseCode = "403",
		description = "Function cannot be accessed now"
	)
	@Path("/measurments-request/{args}")	
    public List<Traffic_Sensor_Measurment> measurmentsRequest(String args) {
        while (manager.getStatus() != Message_Manager_status.Waiting) {
			throw new ForbiddenException("Function cannot be accessed now");
		}
        return manager.newMessage("request", args);
    }

	@GET
	@Operation(
		operationId = "measurmentsAccept"
	)
	@APIResponse(
		responseCode = "200",
		description = "Measurments data accept confirmed",
		content = @Content(
			mediaType = MediaType.APPLICATION_JSON,
            schema = @Schema(type = SchemaType.ARRAY, implementation = Traffic_Sensor_Measurment.class)
		)
	)
	@APIResponse(
		responseCode = "400",
		description = "Invalid request arguments"
	)
	@APIResponse(
		responseCode = "403",
		description = "Function cannot be accessed now"
	)
	@Path("/measurments-accept/{args}")	
    public List<Traffic_Sensor_Measurment> measurmentsAccept(String args) {
        while (manager.getStatus() != Message_Manager_status.Waiting) {
			throw new ForbiddenException("Function cannot be accessed now");
		}
    	return manager.newMessage("accept", args);
    }

	@GET
	@Operation(
		operationId = "measurmentsRepeat"
	)
	@APIResponse(
		responseCode = "200",
		description = "Measurments data repeat confirmed",
		content = @Content(
			mediaType = MediaType.APPLICATION_JSON,
            schema = @Schema(type = SchemaType.ARRAY, implementation = Traffic_Sensor_Measurment.class)
		)
	)
	@APIResponse(
		responseCode = "400",
		description = "Invalid request arguments"
	)
	@APIResponse(
		responseCode = "403",
		description = "Function cannot be accessed now"
	)
	@Path("/measurments-repeat/{args}")	
    public List<Traffic_Sensor_Measurment> measurmentsRepeat(String args) {
        while (manager.getStatus() != Message_Manager_status.Waiting) {
			throw new ForbiddenException("Function cannot be accessed now");
		}
        return manager.newMessage("repeat", args);
    }

    @GET
	@Operation(
		operationId = "sensorsRequest"
	)
	@APIResponse(
		responseCode = "200",
		description = "Sensor data sent confirmed",
		content = @Content(
			mediaType = MediaType.APPLICATION_JSON,
            schema = @Schema(type = SchemaType.ARRAY, implementation = Traffic_Sensor.class)
		)
	)
	@APIResponse(
		responseCode = "400",
		description = "Invalid request arguments"
	)
	@APIResponse(
		responseCode = "403",
		description = "Function cannot be accessed now"
	)
	@Path("/sensors-request/{conditions}")	
    public List<Traffic_Sensor> sensorsRequest(String conditions) {
		while (manager.getStatus() != Message_Manager_status.Waiting) {
			throw new ForbiddenException("Function cannot be accessed now");
		}
        return manager.getSensors("request", conditions);
    }

	@GET
	@Operation(
		operationId = "sensorsAccept"
	)
	@APIResponse(
		responseCode = "200",
		description = "Sensor data accept confirmed",
		content = @Content(
			mediaType = MediaType.APPLICATION_JSON,
            schema = @Schema(type = SchemaType.ARRAY, implementation = Traffic_Sensor.class)
		)
	)
	@APIResponse(
		responseCode = "400",
		description = "Invalid request arguments"
	)
	@APIResponse(
		responseCode = "403",
		description = "Function cannot be accessed now"
	)
	@Path("/sensors-accept/{conditions}")	
    public List<Traffic_Sensor> sensorsAccept(String conditions) {
		if (manager.getStatus() != Message_Manager_status.Waiting) {
			throw new ForbiddenException("Function cannot be accessed now");
		}
        return manager.getSensors("accept", conditions);
    }

	@GET
	@Operation(
		operationId = "sensorsRepeat"
	)
	@APIResponse(
		responseCode = "200",
		description = "Repeat sensor data confirmed",
		content = @Content(
			mediaType = MediaType.APPLICATION_JSON,
            schema = @Schema(type = SchemaType.ARRAY, implementation = Traffic_Sensor.class)
		)
	)
	@APIResponse(
		responseCode = "400",
		description = "Invalid request arguments"
	)
	@APIResponse(
		responseCode = "403",
		description = "Function cannot be accessed now"
	)
	@Path("/sensors-repeat/{conditions}")	
    public List<Traffic_Sensor> sensorsRRepeat(String conditions) {
		while (manager.getStatus() != Message_Manager_status.Waiting) {
			throw new ForbiddenException("Function cannot be accessed now");
		}
        return manager.getSensors("repeat", conditions);
    }
}
