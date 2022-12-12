package org.acme;

import java.time.LocalDateTime;
import java.util.List;

public class Traffic_Message {
	private Traffic_Message_Type _messageType;
	private String _messageText;
	private List<Traffic_Sensor_Measurment> _resultText;
	private int _senderID;
	private LocalDateTime _timeStamp;
	private Traffic_Message_Status _messageStatus;

	public Traffic_Message(String type, String msg) {
		_timeStamp = LocalDateTime.now();

		switch (type) {
			case "request":
				_messageType = Traffic_Message_Type.Request;
				break;
			case "accept":
				_messageType = Traffic_Message_Type.Accept;
				break;
			case "repeat":
				_messageType = Traffic_Message_Type.Repeat;
				break;
			default:
				_messageType = Traffic_Message_Type.Unknown;
		}
		_messageText = msg;
		_messageStatus = Traffic_Message_Status.New;

	}

	public void setSenderId(int id) {
		_senderID = id;
	}

	public int getSenderId() {
		return _senderID;
	}

	public Traffic_Message_Status getMessage_Status() {
		return _messageStatus;
	}

	public void setMessageStatus(Traffic_Message_Status status) {
		_messageStatus = status;
	}

	public String getMessageText() {
		return _messageText;
	}

	public void setResultText(List<Traffic_Sensor_Measurment> text) {
		_resultText = text;
	}

	public List<Traffic_Sensor_Measurment> getResultText() {
		return _resultText;
	}

	public void setMessageType(Traffic_Message_Type type) {
		_messageType = type;
	}

	public Traffic_Message_Type getMessageType() {
		return _messageType;
	}

	public LocalDateTime getTimeStamp() {
		return _timeStamp;
	}
}
