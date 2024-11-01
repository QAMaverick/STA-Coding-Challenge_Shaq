Feature: Edit Preferences and View Details

Editing the valid journey's travel preferences, updating the journey with the new preferences and 
viewing the accessibility info for the oncoming station

Scenario: Editing Travel Preferences to 'Select route with least walking' and updating the journey
	Given I'm on the Plan a Journey widget page
	And I've planned a journey with two valid locations
	When I click on the edit preferences tab
	And Clicked on 'Route with least walking' radio button
	And Clicked on 'Update Journey' button
	Then I should see revised results with new preferences added

Scenario: View journey details and checking the Station's points of access
	Given I'm on the Plan a Journey widget page
	And I have planned a journey with two valid locations
	When I click on the 'View Details' button on a journey
	Then I should sre the journey's step-by-step route and each station/stop's points of access
