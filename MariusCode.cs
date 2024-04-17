const int RED_LED_PIN = 9;
const int GREEN_LED_PIN = 8;
const int BLUE_LED_PIN = 7;
const int START_BUTTON_PIN = 2; // Assuming a start/stop button connected to GPIO 2
const int NUM_BUTTONS = 4;
const int BUTTON_PINS[NUM_BUTTONS] = {3, 4, 5, 6}; // Pins for the buttons
const int BUTTON_LED_PINS[NUM_BUTTONS] = {RED_LED_PIN, GREEN_LED_PIN, BLUE_LED_PIN, RED_LED_PIN}; // LED pins corresponding to the buttons
const int NUM_LEDS = 3; // Number of LEDs
int LEDS[NUM_LEDS] = {RED_LED_PIN, GREEN_LED_PIN, BLUE_LED_PIN};

// Variables
int currentStep = 0;
unsigned long buttonPressStartTime = 0;
int lastButtonPressed = -1;

// Function declarations
void createRandomSequenceOfLength(int length, int* sequence, int* values);
void lightUp(int pin);
void turnOffAllLEDs();
void logButtonPress(int button, unsigned long duration);

void setup() {
  pinMode(START_BUTTON_PIN, INPUT_PULLUP); // Set start/stop button pin as input with pull-up resistor
  for (int i = 0; i < NUM_LEDS; i++) {
    pinMode(LEDS[i], OUTPUT); // Set LED pins as output
  }
  for (int i = 0; i < NUM_BUTTONS; i++) {
    pinMode(BUTTON_PINS[i], INPUT_PULLUP); // Set button pins as input with pull-up resistors
  }
  randomSeed(analogRead(0)); // Seed random number generator
}
void loop() {
  if (digitalRead(START_BUTTON_PIN) == LOW) { // If start/stop button is pressed
    // Start or stop sequence
    if (currentStep == 0) {
      // Start sequence
      currentStep = 1;
      int sequence[4];
      createRandomSequenceOfLength(4, sequence, LEDS); // Create random sequence
      for (int i = 0; i < 4; i++) {
        lightUp(sequence[i]); // Light up LEDs in sequence
        delay(2000); // Adjust delay as needed
        turnOffAllLEDs(); // Turn off LEDs
        delay(500); // Adjust delay as needed
      }
    } else {
      // Stop sequence
      currentStep = 0;
      // Reset logging variables
      buttonPressStartTime = 0;
      lastButtonPressed = -1;
    }
  }
for (int i = 0; i < NUM_BUTTONS; i++) {
    if (digitalRead(BUTTON_PINS[i]) == LOW) { // If button is pressed
      if (lastButtonPressed != i) { // If a new button is pressed
        if (buttonPressStartTime != 0) {
          unsigned long pressDuration = millis() - buttonPressStartTime;
          logButtonPress(lastButtonPressed, pressDuration);
        }
        lastButtonPressed = i;
        buttonPressStartTime = millis();
      }
    }
  }
}

// Function to create a random sequence
void createRandomSequenceOfLength(int length, int* sequence, int* values) {
  for (int i = 0; i < length; i++) {
    int randomIndex = random(NUM_LEDS); // Pick random array index
    sequence[i] = values[randomIndex]; // Set the value of the sequence based on the random index
  }
}

// Function to light up an LED
void lightUp(int pin) {
  digitalWrite(pin, HIGH); // Turn on LED
}

// Function to turn off all LEDs
void turnOffAllLEDs() {
  for (int i = 0; i < NUM_LEDS; i++) {
    digitalWrite(LEDS[i], LOW); // Turn off LED
  }
}
void logButtonPress(int button, unsigned long duration) {
  // Log button press
  Serial.print("Button ");
  Serial.print(button);
  Serial.print(" pressed for ");
  Serial.print(duration);
  Serial.println(" milliseconds");
}
