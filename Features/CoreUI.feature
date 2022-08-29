Feature: CoreUI

A short summary of the feature

@UI
Scenario: Core UI scenario 1
	Given Go to "http://google.com"
	#And Access "abc.com" using "%username%" and "%password%" start by "http"
	When Enter "luatsqa" to "Google.HomePage.SearchBox.Txt"
	And Click to "Google.HomePage.SearchButton.Btn"
	Then Verify current web title is "luatsqa - Google Search"
