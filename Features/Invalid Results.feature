Feature: Invalid Results

Checking the Plan A Journey widget will present the user with invalid journey results if
1 or more invalid location were inputted into the fields

Example: An invalid location is a location that does not exist such as '1234' or '$^%*()&'

Scenario: Inputting two invalid locations into the Plan A Journey widget
	Given I'm on the Plan A Journey Widget page
	And I have entered two invalid locations into the widget fields
	When I click the Plan A Journey button
	Then I should be shown invalid results on the page via a message
Example: Message will read as "Sorry, we can't find a journey matching your criteria" 