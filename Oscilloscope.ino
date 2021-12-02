/*
 * Oscilloscope
 * Version 1.0.0
 * Copyright (c) 2021 stene.xyz.
 * 
 * +--------------+
 * | ABOUT        |
 * +--------------+
 * Firmware for a small DIY oscilloscope/voltmeter. Super simple, just 
 * reads the data through a voltage divider on the first analog pin and
 * spits it out to the serial console.
 * 
 * +--------------+
 * | SCHEMATIC    |
 * +--------------+
 * 
 * (Measurement input)
 *   |
 *   \/
 * 100k Resistor
 *   |
 * 	 \/
 * Arduino A0 pin
 *   |
 *   \/
 * 10k Resistor
 *   |
 *   \/
 * (GND connection)
 * +--------------+
 * | CHANGELOG    |
 * +--------------+
 * v1.0.0:
 * 		[+] Initial release.
 */

// Voltmeter settings.
// The voltmeter is a voltage divider between GND (negative) and the battery voltage (positive)
// 100K 
#define DIVIDER_POSITIVE_RESISTANCE 100000.0
// 10K
#define DIVIDER_NEGATIVE_RESISTANCE 10000.0
// The max voltage the voltmeter reads. Set to 5, then send 5V to the voltmeter.
// Set this value to whatever it thinks it is.
#define VOLTMETER_MAX_VOLTAGE 4.89
#define VOLTMETER_PIN A0

// Voltmeter readout from ChargeController
float get_voltmeter_value() {
	int val = analogRead(VOLTMETER_PIN);
	float vout = (val * VOLTMETER_MAX_VOLTAGE) / 1024.0;
	float vin = vout / (DIVIDER_NEGATIVE_RESISTANCE / (DIVIDER_POSITIVE_RESISTANCE + DIVIDER_NEGATIVE_RESISTANCE));
	return vin;
}

void setup() {
	Serial.begin(9600);
	pinMode(VOLTMETER_PIN, INPUT);
}

void loop() {
	Serial.println(get_voltmeter_value());
}
