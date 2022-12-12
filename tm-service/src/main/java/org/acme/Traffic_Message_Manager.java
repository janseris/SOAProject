package org.acme;

import java.util.ArrayList;
import java.util.List;

import javax.ws.rs.BadRequestException;

public class Traffic_Message_Manager {
	private Message_Manager_status _managerStatus = Message_Manager_status.Waiting;
	private Traffic_Sensor_Manager _sensorManager = new Traffic_Sensor_Manager();
	private List<Traffic_Message> _messageBuffer = new ArrayList<Traffic_Message>();

	public Message_Manager_status getStatus() {
		return _managerStatus;
	}

	private List<Traffic_Sensor_Measurment> processRequst(Traffic_Message message) {
		message.setMessageStatus(Traffic_Message_Status.Open);
		List<Traffic_Sensor_Measurment> search = _sensorManager.getMeasurments(message.getMessageText());
		if (search.isEmpty()) {
			message.setMessageStatus(Traffic_Message_Status.Closed);
			throw new BadRequestException("No result found for requst:\n" + message.getMessageText());
		}
		_managerStatus = Message_Manager_status.Waiting;
		message.setResultText(search);
		return search;
	}

	private List<Traffic_Sensor_Measurment> processAccept(Traffic_Message message) {
		_messageBuffer.forEach(msg -> 
			{if (msg.getMessageText() == message.getMessageText()) 
				msg.setMessageStatus(Traffic_Message_Status.Closed);});
		_managerStatus = Message_Manager_status.Waiting;
		return new ArrayList<Traffic_Sensor_Measurment>();
	}

	private List<Traffic_Sensor_Measurment> processRepeat(Traffic_Message message) {
		for (Traffic_Message msg : _messageBuffer) {
			if (msg.getMessageText() == message.getMessageText()) 
				return msg.getResultText();
		}	
		throw new BadRequestException("Request not found" + message.getMessageText());
	}

	public List<Traffic_Sensor_Measurment> newMessage(String type, String msg) {
		_managerStatus = Message_Manager_status.Busy;
		Traffic_Message message = new Traffic_Message(type, msg);
		_messageBuffer.add(message);
		switch (message.getMessageType()) {
			case Request:
				return processRequst(message);
			case Accept:
				return processAccept(message);
			case Repeat:
				return processRepeat(message);
			default:
				message.setMessageStatus(Traffic_Message_Status.Closed);
				throw new BadRequestException("Uknown message:\n" + message.getMessageText());
		}
	}

	public List<Traffic_Sensor> getSensors(String type , String conditions) {
		_managerStatus = Message_Manager_status.Busy;
		Traffic_Message message = new Traffic_Message(type, conditions);
		_messageBuffer.add(message);
		_managerStatus = Message_Manager_status.Waiting;
		List<Traffic_Sensor> search = _sensorManager.getSensors(conditions);
		message.setMessageStatus(Traffic_Message_Status.Closed);
		if (search.isEmpty()) {			
			throw new BadRequestException("No result found for requst:\n" + message.getMessageText());
		}
		return search;
	}
}
