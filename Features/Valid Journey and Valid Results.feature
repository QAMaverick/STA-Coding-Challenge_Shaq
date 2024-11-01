Feature: Valid Journey and Valid Results

Checking the Plan a Journey widget to see if a user can input a 'To' and 'From' journey, plan a journey and 
be presented with valid results

Scenario: Check to see if a user can plan a journey using the Plan a Journey widget
	Given I'm on the Plan A Journey widget page
	And I enter a location in both the 'To' and 'From' fields
	When I click on the Plan Journey button
	Then I should be presented with valid journey results

Scenario: Validate the result for both walking and cycling time
	Given I'm on the Plan A Journey widget page
	And I have planned a journey with two valid locations
	When I check the journey results
	Then I should be able to see valid walking and cycling times of each route

