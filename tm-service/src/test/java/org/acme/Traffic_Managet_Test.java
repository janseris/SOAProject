package org.acme;

import io.quarkus.test.junit.QuarkusTest;

import org.junit.jupiter.api.Test;
import static io.restassured.RestAssured.given;
import static org.hamcrest.CoreMatchers.is;

@QuarkusTest
public class Traffic_Managet_Test {

    @Test
    public void testHelp() {
        given()
          .when().get("/TM/help")
          .then()
             .statusCode(200);
    }

    @Test
    public void testHelpMeasurments() {
        given()
          .when().get("/TM/help-measurments")
          .then()
             .statusCode(200);
    }

    @Test
    public void testTMStatus() {
        given()
          .when().get("/TM/status")
          .then()
             .statusCode(200)
             .body(is("Service is currently in state:Waiting"));
    }

    @Test
    public void testAllSensors() {
        given()
          .when().get("/TM/sensors-request/all")
          .then()
             .statusCode(200)
             .body("$.size()", is(12));
    }

    @Test
    public void testLocalSensors() {
        given()
          .when().get("/TM/sensors-request/location:Brno")
          .then()
             .statusCode(200)
             .body("$.size()", is(4));
    }

    @Test
    public void testSpeedSensors() {
        given()
          .when().get("/TM/sensors-request/speedLimit:50")
          .then()
             .statusCode(200)
             .body("$.size()", is(3));
    }

    @Test
    public void testTypeSensors() {
        given()
          .when().get("/TM/sensors-request/sensorType:radar")
          .then()
             .statusCode(200)
             .body("$.size()", is(6));
    }

    @Test
    public void testLocationSpeedSensors() {
        given()
          .when().get("/TM/sensors-request/speedLimit:50|location:Brno")
          .then()
             .statusCode(200)
             .body("$.size()", is(2));
    }

    @Test
    public void testTypeSpeedSensors() {
        given()
          .when().get("/TM/sensors-request/speedLimit:50|sensorType:camera")
          .then()
             .statusCode(200)
             .body("$.size()", is(1));
    }

    @Test
    public void testMeasurments() {
        String measurments = given()
          .when().get("/TM/measurments-request/sensorType:radar")
          .then()
             .statusCode(200)
             .extract()
             .body()
             .asString();
        assert !measurments.isEmpty();

    }

    @Test
    public void testVehicleTypeMeasurments() {
      String response = given()
        .when().get("TM/measurments-request/vehicleType:bus")
        .then()
            .statusCode(200)
            .extract()
            .asString();
      
      for (String meas : response.split("\n"))
        assert meas.contains("Bus");

    }

    @Test
    public void testVehicleSpeedMeasurments() {
      String response = given()
        .when().get("TM/measurments-request/vehicleSpeed:55-80")
        .then()
            .statusCode(200)
            .extract()
            .asString();
      
      for (String meas : response.split("\n")) {
        String[] meas_args = meas.split(",");
        assert Integer.parseInt(meas_args[1].split(":")[1]) >= 5500 & Integer.parseInt(meas_args[1].split(":")[1]) <= 8000;
      }
    }

    @Test
    public void testVehicleSpeedTypeMeasurments() {
      String response = given()
        .when().get("/TM/measurments-request/vehicleSpeed:55-80|vehicleType:Truck")
        .then()
            .statusCode(200)
            .extract()
            .asString();
      
      for (String meas : response.split("\n")) {
        String[] meas_args = meas.split(",");
        assert Integer.parseInt(meas_args[1].split(":")[1]) >= 5500 & Integer.parseInt(meas_args[1].split(":")[1]) <= 8000;
      }
    }
}