Feature: CoreUI

A short summary of the feature

@UI
Scenario: Core UI scenario 1
	Given Go to "http://google.com"
	#And Access "abc.com" using "%username%" and "%password%" start by "http"
	When Enter "luatsqa" to "Google.HomePage.SearchBox.Txt"
	And Click to "Google.HomePage.SearchButton.Btn"
	Then Verify current web title is "luatsqa - Google Search"



@UI
Scenario: Core UI scenario 2 using var
	Given Assign value "http://google.com" to local variables "url"
	And Go to "#url#"
	#And Access "abc.com" using "%username%" and "%password%" start by "http"
	When Enter "luatsqa" to "Google.HomePage.SearchBox.Txt"
	And Click to "Google.HomePage.SearchButton.Btn"
	Then Verify current web title is "luatsqa - Google Search"


@UI
Scenario: Core UI scenario 3 using var
	Given Assign value "http://google.com" to local variables "url"
	And Go to "#url#"
	#And Access "abc.com" using "%username%" and "%password%" start by "http"
	When Assign value "luatsqa" to global variables "keyword"
	When Enter "%keyword%" to "Google.HomePage.SearchBox.Txt"
	And Click to "Google.HomePage.SearchButton.Btn"
	Then Verify current web title is "%keyword% - Google Search"

@UI
Scenario: Core UI scenario 4 work with dropdown
	Given Go to "https://practies.luatsqa.com/"
	Then Verify dropdown "Luatsqa.practies.page1.select1.sel" contains below values
	| values   |
	| option 1 |
	| option 2 |
	| option 3 |
@UI
Scenario: Core UI scenario 4 work with dropdown 3
	Given Go to "https://practies.luatsqa.com/"
	Then Verify dropdown "Luatsqa.practies.page1.select1.sel" match and same order below values
	| values   |
	| option 1 |
	| option 2 |
	| option 3 |
	| option 4 |
	| option 5 |
	| option 6 |
@UI
Scenario: Core UI scenario 4 work with dropdown 2
	Given Go to "https://practies.luatsqa.com/"
	When Choose "option 1" from the drop down "Luatsqa.practies.page1.select1.sel"
	Then Verify "option 1" in dropdown "Luatsqa.practies.page1.select1.sel" is selected

@UI
Scenario: Core UI scenario 5 work with iframe
	Given Go to "https://practies.luatsqa.com/"
	When Switch to iframe "iframtest"
	And Click to "Luatsqa.practies.page1.iframe.submit.button"
	And Switch to default frame

@UI
Scenario: Core UI scenario 6 work with window
	Given Go to "https://practies.luatsqa.com/"
	When Click to "Luatsqa.practies.page1.navigationtopage2.lbl"
	And Switch to new Tab
	And Sleep in "2" seconds
	Then Verify current web title is "Luat SQA selenium pratices-counting"
	When Switch to first Tab
	And Sleep in "2" seconds
	Then Verify current web title contains "Luat SQA selenium pratices"

	@UI
Scenario: Core UI scenario 7 work with element contains
	Given Go to "https://practies.luatsqa.com/"
	Then Verify "Luatsqa.practies.page1.authorintroduce.lbl" have text contains "Luat"

	@UI
Scenario: Core UI scenario 7 work with element equal
	Given Go to "https://practies.luatsqa.com/"
	Then Verify "Luatsqa.practies.page1.authorintroduce.lbl" have text equal to "My name is Luat. I am SQA with 10 years experience in Automation"

	@UI
Scenario: Core UI scenario 7 work with element text and save to var
	Given Go to "https://practies.luatsqa.com/"
	When Save "Luatsqa.practies.page1.authorintroduce.lbl" Text to variable "authorintroduce"
	Then Verify local variable "authorintroduce" has value "My name is Luat. I am SQA with 10 years experience in Automation"
	
	@UI
Scenario: Core UI scenario 7 work with element text filter using regex
	Given Go to "https://practies.luatsqa.com/"
	When Save "Luatsqa.practies.page1.authorintroduce.lbl" Text to variable "yearsexperience" filter by regex "\d+"
	Then Verify local variable "yearsexperience" has value "10"
	@UI
Scenario: Core UI scenario 7 work with element state
	Given Go to "https://practies.luatsqa.com/"
	Then Verify current state of this element "Luatsqa.practies.page1.authorintroduce.lbl" is "Enable"
	And Verify current state of this element "Luatsqa.practies.page1.authorintroduce.lbl" is "Displayed"
	When Click to "Luatsqa.practies.page1.navigationtopage2.lbl"
	And Switch to new Tab
	Then Verify current state of this element "Luatsqa.practies.page2.disable.Btn" is "Disabled"
	And Verify element "Luatsqa.practies.page1.authorintroduce.lbl" is not displayed