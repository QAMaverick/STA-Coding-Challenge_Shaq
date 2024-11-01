STA Coding Challenge - Shaq Reid-Robinson

How I made the scripts:

1. ValidJourneyAndResults.cs - A1_enterJourneyLocations()
	I originally was going for a standard SelectByValue method to ensure the correct values in the
	dropdown were picked everytime but the dropdown doesn't store its values on the page but uses ajax to pull from a server 
	containing locations so instead went for a Keys input method:
		>Type most of the location in field ensuring the right value will always appear first
		>Sendkeys arrow down once and enter
	I also had to use a couple Thread.Sleeps to give ajax time to load the dropdown values otherwise the test would fail.
	Even after implementing implicit waits for the elements didn't prevent this as the click method would interrupt the ajax stopping
	the value from loading.
	Even after fixing some semantics of the script and unwrapping the click method from the findelement locator reference to allow the implicit wait
	to work before clicking anything, I found this made the script inconsistent as it would occassionally break on the first field and throw a
	NoSuchElement exception handler.


1a. ValidJourneyAndResults.cs - A2_confirmJourneyResults()
	Retriving the route time in itself wasn't difficult but trying to present them neatly in the debug message was tricky as different text
	was in both <p> tags and <strong> tags within different hierachy of the divs. 


2. EditPreference.cs - EditPref_CheckDetails()
	This was a simple e2e script but I had a little trouble with retriving the access point info for the debug towards the end of the script.
	The first access point would print out in the debug but the other two would come back blank despite pointing to the correct elements. I
	tried different selectors but still had the same issue.


3. InvalidResults.cs - invalidResults_OneInvLocal_OneValidLocal()
	Simple script to code, mostly focused on checking for correct validation messages for invalid journey with some assertions.

3a. InvalidResults.cs - invalidResults_TwoInvLocals()
	Same as above.

4. NoLocationSearch.cs - blankJourneyFields()
	Main focus in the script was checking for appropriate field validations upon clicking the plan button with some assertions. 



What additional tests/scenarios I would add:

	A. Enter the same location in both To and From field
	B. Select different preference combinations in Preference tab in Journey results
	C. Select the 'Save these preferences for future visits' checkbox in Preference tab and clear browser cookies to check preferences remains
	D. Use the 'Use My Location' option in the location field to test its geological accuracy and route results
	E. Check the Recents tab to see it is storing journey info correctly
	