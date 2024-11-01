Feature: No Location in Fields

Checking field validations when no locations are entered but the Plan a Journey button is clicked

Scenario: Clicking on Plan Journey button when no locations are entered in field(s)
	Given I'm on the Plan a Journey widget page
	And Both 'To' and 'From' fields are left blank
	When I click the Plan Journey button
	Then Both fields should give me a warning that I must enter a location
